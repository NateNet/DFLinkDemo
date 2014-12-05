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

    using Demandforce.DFLink.Common.Configuration;
    using Demandforce.DFLink.Logger;

    /// <summary>
    ///     The client TCP.
    /// </summary>
    public class ClientTcp : INetworkClient
    {
        #region Fields

        /// <summary>
        /// The class name.
        /// </summary>
        private readonly string className = "Communication";

        /// <summary>
        ///     The connect done.
        /// </summary>
        private readonly ManualResetEvent connectDone = new ManualResetEvent(false);

        /// <summary>
        /// The license.
        /// </summary>
        private readonly string license;

        /// <summary>
        /// The log helper.
        /// </summary>
        private readonly ILogger logHelper = LogHelper.GetLoggerHandle();

        /// <summary>
        /// The remote IP.
        /// </summary>
        private readonly string remoteIp;

        /// <summary>
        /// The remote port.
        /// </summary>
        private readonly int remotePort;

        /// <summary>
        /// The need reconnect.
        /// </summary>
        private bool needReconnect = true;

        /// <summary>
        ///     Gets or sets the TCP.
        /// </summary>
        private TcpClient tcpClient;

        /// <summary>
        /// The work stream.
        /// </summary>
        private NetworkStream workStream;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientTcp"/> class.
        /// </summary>
        /// <param name="remoteIp">
        /// The remote IP.
        /// </param>
        /// <param name="remotePort">
        /// The remote port.
        /// </param>
        /// <param name="license">
        /// The license.
        /// </param>
        public ClientTcp(string remoteIp, int remotePort, string license)
        {
            this.remoteIp = remoteIp;
            this.remotePort = remotePort;
            this.license = license;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientTcp"/> class.
        /// </summary>
        /// <param name="serverSettings">
        /// The server Settings.
        /// </param>
        public ClientTcp(IServerSettings serverSettings)
        {
            this.remoteIp = serverSettings.SocketIp;
            this.remotePort = serverSettings.SocketPort;
            this.license = serverSettings.LicenseId;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the on get data.
        /// </summary>
        public Action<string> OnGetData { get; set; }

        /// <summary>
        /// Gets or sets the server settings.
        /// </summary>
        public IServerSettings ServerSettings { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     asynchronous connection
        /// </summary>
        public void Connect()
        {
            this.needReconnect = true;

            if ((this.tcpClient == null) || (!this.tcpClient.Connected))
            {
                try
                {
                    this.tcpClient = new TcpClient { ReceiveTimeout = 10 };

                    this.connectDone.Reset();

                    this.logHelper.Info(
                        this.className, 
                        -1, 
                        "Establishing Connection to " + this.remoteIp + ":" + this.remotePort);

                    this.tcpClient.BeginConnect(this.remoteIp, this.remotePort, this.ConnectCallback, this.tcpClient);

                    this.connectDone.WaitOne();

                    if ((this.tcpClient != null) && this.tcpClient.Connected)
                    {
                        this.workStream = this.tcpClient.GetStream();

                        this.logHelper.Info(this.className, -1, "Connection established");
                        this.SendData(this.license);

                        this.Asyncread(this.tcpClient);
                    }
                }
                catch (Exception se)
                {
                    this.logHelper.Info(this.className, -1, se.Message + " Conn......." + Environment.NewLine);
                    this.ReConnectServer();
                }
            }
        }

        /// <summary>
        ///     do disconnect
        /// </summary>
        public void DisConnect()
        {
            this.needReconnect = false;

            if ((this.tcpClient != null) && this.tcpClient.Connected)
            {
                this.workStream.Close();
                this.tcpClient.Close();
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
        /// asynchronous reading the data from TCP client
        /// </summary>
        /// <param name="sock">
        /// a TCP client
        /// </param>
        private void Asyncread(TcpClient sock)
        {
            var state = new ClientTcpState { Client = sock };
            NetworkStream stream = sock.GetStream();

            if (stream.CanRead)
            {
                try
                {
                    stream.BeginRead(
                        state.Buffer, 
                        0, 
                        ClientTcpState.BufferSize, 
                        this.TcpReadCallBack, 
                        state);
                }
                catch (Exception e)
                {
                    this.logHelper.Info(this.className, -1, "Network IO problem " + e);
                }
            }
        }

        /// <summary>
        /// asynchronous callback
        /// </summary>
        /// <param name="ar">
        /// a result
        /// </param>
        private void ConnectCallback(IAsyncResult ar)
        {
            var t = (TcpClient)ar.AsyncState;
            try
            {
                if (t.Connected)
                {
                    this.logHelper.Info(this.className, -1, "connect successfull");
                    t.EndConnect(ar);
                    this.logHelper.Info(this.className, -1, "connect completed");

                    this.connectDone.Set();
                }
                else
                {
                    t.EndConnect(ar);
                    this.logHelper.Info(this.className, -1, "connect failed");

                    this.connectDone.Set();
                    this.ReConnectServer();
                }
            }
            catch (SocketException se)
            {
                this.logHelper.Info(this.className, 1, "Error raising: ConnCallBack.......:" + se.Message);

                this.connectDone.Set();
                this.ReConnectServer();
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
        ///     reconnect the server
        /// </summary>
        private void ReConnectServer()
        {
            if (this.needReconnect)
            {
                Thread.Sleep(3000);
                this.Connect();
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
            var state = (ClientTcpState)ar.AsyncState;

            // active disconnect
            if ((state.Client == null) || (!state.Client.Connected))
            {
                return;
            }

            NetworkStream mas = state.Client.GetStream();
            try
            {
                int numberOfBytesRead = mas.EndRead(ar);
                state.TotalBytesRead += numberOfBytesRead;

                if (numberOfBytesRead > 0)
                {
                    var dd = new byte[numberOfBytesRead];
                    Array.Copy(state.Buffer, 0, dd, 0, numberOfBytesRead);
                    var message = new string(Encoding.UTF8.GetChars(dd));
                    this.DoEvent(this.OnGetData, message);
                    this.logHelper.Info(this.className, -1, "Received message:" + message);

                    mas.BeginRead(state.Buffer, 0, ClientTcpState.BufferSize, this.TcpReadCallBack, state);
                }
                else
                {
                    // passive disconnec 
                    mas.Close();
                    state.Client.Close();
                    mas = null;
                    state = null;
                    this.logHelper.Info(this.className, -1, "Read Error");
                }
            }
            catch (Exception e)
            {
                if (mas != null)
                {
                    mas.Close();
                    state.Client.Close();
                }

                this.logHelper.Info(this.className, -1, "connecting failed: " + e.Message);
                this.ReConnectServer();
            }
        }

        #endregion
    }
}