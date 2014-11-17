// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClientTcp.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The client tcp.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.Socket
{
    using System;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// The client TCP.
    /// </summary>
    public class ClientTcp
    {
        #region Fields

        /// <summary>
        /// The work stream.
        /// </summary>
        private NetworkStream workStream = null;

        /// <summary>
        /// The connect done.
        /// </summary>
        private readonly ManualResetEvent connectDone = new ManualResetEvent(false);

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the license.
        /// </summary>
        public string License { get; set; }

        /// <summary>
        /// Gets or sets the on get data.
        /// </summary>
        public Action<string> OnGetData { get; set; }

        /// <summary>
        /// Gets or sets the on connected.
        /// </summary>
        public Action<string> OnConnected { get; set; }

        /// <summary>
        /// Gets or sets the on connecting.
        /// </summary>
        public Action<string> OnConnecting { get; set; }

        /// <summary>
        /// Gets or sets the on disconnected.
        /// </summary>
        public Action<string> OnDisconnected { get; set; }

        /// <summary>
        /// Gets or sets the on read error.
        /// </summary>
        public Action<string> OnReadError { get; set; }

        /// <summary>
        /// Gets or sets the remote IP.
        /// </summary>
        public string RemoteIp { get; set; }

        /// <summary>
        /// Gets or sets the remote port.
        /// </summary>
        public int RemotePort { get; set; }

        /// <summary>
        /// Gets or sets the TCP.
        /// </summary>
        public TcpClient TcpCl { get; set; }

        #endregion

        #region Public Methods and Operators


        /// <summary>
        /// asynchronous connection
        /// </summary>
        public void Connect()
        {
            if ((this.TcpCl == null) || (!this.TcpCl.Connected))
            {
                try
                {
                    this.TcpCl = new TcpClient { ReceiveTimeout = 10 };

                    this.connectDone.Reset();

                    this.DoEvent(this.OnConnecting, "Establishing Connection to " + this.RemoteIp + ":" + this.RemotePort);

                    this.TcpCl.BeginConnect(this.RemoteIp, this.RemotePort, this.ConnectCallback, this.TcpCl);

                    this.connectDone.WaitOne();

                    if ((this.TcpCl != null) && this.TcpCl.Connected)
                    {
                        this.workStream = this.TcpCl.GetStream();

                        this.DoEvent(this.OnConnected, "Connection established");
                        this.SendData(this.License);

                        this.Asyncread(this.TcpCl);
                    }
                }
                catch (Exception se)
                {
                    this.DoEvent(this.OnDisconnected, se.Message + " Conn......." + Environment.NewLine);
                }
            }
        }

        /// <summary>
        /// do disconnect
        /// </summary>
        public void DisConnect()
        {
            if ((this.TcpCl != null) && this.TcpCl.Connected)
            {
                this.workStream.Close();
                this.TcpCl.Close();
            }
        }

        /// <summary>
        /// Sending data
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void SendData(string message)
        {
            if (this.workStream != null)
            {
                byte[] data;

                data = Encoding.ASCII.GetBytes(message);

                this.workStream.Write(data, 0, data.Length);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// asynchronous callback
        /// </summary>
        /// <param name="ar">
        /// a result
        /// </param>
        private void ConnectCallback(IAsyncResult ar)
        {
            this.connectDone.Set();
            var t = (TcpClient)ar.AsyncState;
            try
            {
                if (t.Connected)
                {
                    this.DoEvent(this.OnConnected, "connect successfull");
                    t.EndConnect(ar);
                    this.DoEvent(this.OnConnected, "connect completed");
                }
                else
                {                  
                    t.EndConnect(ar);
                    this.DoEvent(this.OnDisconnected, "connect failed");
                }
            }
            catch (SocketException se)
            {
                this.DoEvent(this.OnDisconnected, "Error raising: ConnCallBack.......:" + se.Message);
            }
        }

        /// <summary>
        /// The do event.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        private void DoEvent(Action<string> action, string message)
        {
            if (action != null)
            {
                action(message);
            }
        }

        /// <summary>
        /// call back when before reading
        /// </summary>
        /// <param name="ar">
        /// a call back
        /// </param>
        private void TcpReadCallBack(IAsyncResult ar)
        {
            var state = (StateObject)ar.AsyncState;

            // active disconnect
            if ((state.Client == null) || (!state.Client.Connected))
            {
                return;
            }
          
            NetworkStream mas = state.Client.GetStream();
            try
            {
                var numberOfBytesRead = mas.EndRead(ar);
                state.TotalBytesRead += numberOfBytesRead;

                if (numberOfBytesRead > 0)
                {
                    var dd = new byte[numberOfBytesRead];
                    Array.Copy(state.Buffer, 0, dd, 0, numberOfBytesRead);
                    this.DoEvent(this.OnGetData, new string(Encoding.UTF8.GetChars(dd)));
                    mas.BeginRead(state.Buffer, 0, StateObject.BufferSize, this.TcpReadCallBack, state);
                }
                else
                {
                    // passive disconnec 
                    mas.Close();
                    state.Client.Close();                 
                    mas = null;
                    state = null;
                    this.DoEvent(this.OnReadError, "read stop");
                }
            }
            catch (Exception e)
            {
                if (mas != null)
                {
                    mas.Close();
                    state.Client.Close();
                }

                mas = null;
                state = null;

                this.DoEvent(this.OnDisconnected, "connecting failed: " + e.Message);
            }
        }

        /// <summary>
        /// asynchronous reading the data from TCP client
        /// </summary>
        /// <param name="sock">
        /// a TCP client
        /// </param>
        private void Asyncread(TcpClient sock)
        {
            var state = new StateObject();
            state.Client = sock;
            NetworkStream stream = sock.GetStream();

            if (stream.CanRead)
            {
                try
                {
                    var ar = stream.BeginRead(state.Buffer, 0, StateObject.BufferSize, this.TcpReadCallBack, state);
                }
                catch (Exception e)
                {
                    this.OnReadError("Network IO problem " + e);
                }
            }
        }

        #endregion
    }
}