// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TcpclientListener.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The tcpclient listener.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.Socket
{
    using System;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// The TCP client listener.
    /// </summary>
    public class TcpclientListener : ISocketListener
    {
        #region Static Fields

        /// <summary>
        /// The instance.
        /// </summary>
        private static readonly TcpclientListener Instance = new TcpclientListener();

        #endregion

        #region Fields

        /// <summary>
        /// The connect done.
        /// </summary>
        private readonly ManualResetEvent connectDone = new ManualResetEvent(false);

        /// <summary>
        /// The connect thread.
        /// </summary>
        private Thread connectThread;

        /// <summary>
        /// The connection beat
        /// </summary>
        private Thread connectBeat;

        /// <summary>
        /// The data call back.
        /// </summary>
        private Action<string> dataCallBack;

        /// <summary>
        /// The connected call back
        /// </summary>
        private Action<string> connectedCallBack;

        /// <summary>
        /// The disconnected call back
        /// </summary>
        private Action<string> disconnectedCallBack;

        /// <summary>
        /// The stop connect attempt.
        /// </summary>
        private bool stopConnectAttept;

        /// <summary>
        /// The TCP.
        /// </summary>
        private TcpClient tcp;

        /// <summary>
        /// The work stream.
        /// </summary>
        private NetworkStream workStream;

        /// <summary>
        /// is beat
        /// </summary>
        //private bool needBeat;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Prevents a default instance of the <see cref="TcpclientListener"/> class from being created.
        /// </summary>
        private TcpclientListener()
        {
            this.AsynConnectServer();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="TcpclientListener"/> class. 
        /// </summary>
        ~TcpclientListener()
        {
            this.stopConnectAttept = true;
            this.DisConnect();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get listener.
        /// </summary>
        /// <returns>
        /// The <see cref="ISocketListener"/>.
        /// </returns>
        public static ISocketListener GetListener()
        {
            return Instance;
        }

        #endregion

        #region Explicit Interface Methods

        /// <summary>
        /// The register data callback.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        void ISocketListener.RegisterDataCallback(Action<string> action)
        {
            this.dataCallBack = action;
        }

        /// <summary>
        /// the register
        /// </summary>
        /// <param name="action">the action</param>
        public void RegisterConnectedCallback(Action<string> action)
        {
            this.connectedCallBack = action;
        }

        /// <summary>
        /// the register
        /// </summary>
        /// <param name="action">the action</param>
        public void RegisterDisconnectedCallBack(Action<string> action)
        {
            this.disconnectedCallBack = action;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Connect the server asynchronously
        /// </summary>
        private void AsynConnectServer()
        {
            if (this.stopConnectAttept)
            {
                return;
            }

            Thread.Sleep(50);
            this.connectThread = new Thread(this.Connect);
            this.connectThread.IsBackground = true;
            this.connectThread.Start();

            if (AgentSetting.SocketBeat)
            {
                this.connectBeat = new Thread(this.Beat);
                this.connectBeat.IsBackground = true;
                this.connectBeat.Start();
            }
        }




        /// <summary>
        /// asynchronous reading the data from TCP client
        /// </summary>
        /// <param name="sock">
        /// the sock
        /// </param>
        private void AsyncRead(TcpClient sock)
        {
            var state = new StateObject { Client = sock };
            NetworkStream stream = sock.GetStream();

            if (stream.CanRead)
            {
                try
                {
                    var ar = stream.BeginRead(
                        state.Buffer, 
                        0, 
                        StateObject.BufferSize, 
                        this.TcpReadCallBack, 
                        state);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Network IO problem " + e);
                }
            }
        }

        /// <summary>
        /// connection beat
        /// </summary>
        private void Beat()
        {
            while (true)
            {
                Thread.Sleep(5000);
                try
                {
                    if (this.tcp.Connected && this.workStream != null)
                    {
                        byte[] data;
                        data = Encoding.ASCII.GetBytes("b");
                        this.workStream.Write(data, 0, data.Length);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Thread.Sleep(5000);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        /// <summary>
        /// connect the server
        /// </summary>       
        private void Connect()
        {
            if ((this.tcp == null) || (!this.tcp.Connected))
            {
                try
                {
                    this.tcp = new TcpClient { ReceiveTimeout = 10 };

                    this.connectDone.Reset();

                    this.tcp.BeginConnect(
                        AgentSetting.SocketIp, 
                        AgentSetting.SocketPort, 
                        this.ConnectCallback, 
                        this.tcp);

                    this.connectDone.WaitOne();

                    if ((this.tcp != null) && this.tcp.Connected)
                    {
                        this.workStream = this.tcp.GetStream();
                        this.SendLicense();
                        this.DoConnectedCallback(AgentSetting.SocketIp + ":" + AgentSetting.SocketPort);
                        this.AsyncRead(this.tcp);
                    }
                    else
                    {
                        this.AsynConnectServer();
                    }
                }
                catch (Exception se)
                {
                    Console.WriteLine(se.Message + " Conn......." + Environment.NewLine);
                    this.AsynConnectServer();
                }
            }
        }

        /// <summary>
        /// asynchronous callback
        /// </summary>
        /// <param name="ar">
        /// the result
        /// </param>
        private void ConnectCallback(IAsyncResult ar)
        {
            this.connectDone.Set();
            var t = (TcpClient)ar.AsyncState;
            try
            {
                if (t.Connected)
                {
                    Console.WriteLine("connect successfull");
                    t.EndConnect(ar);
                    Console.WriteLine("connect completed");
                }
                else
                {
                    Console.WriteLine("connect failed");
                    t.EndConnect(ar);
                }
            }
            catch (SocketException se)
            {
                Console.WriteLine("Error raising: ConnCallBack.......:" + se.Message);
            }
        }

        /// <summary>
        /// disconnect the connection
        /// </summary>
        private void DisConnect()
        {
            if ((this.tcp != null) && this.tcp.Connected)
            {
                this.workStream.Close();
                this.tcp.Close();
            }
        }

        /// <summary>
        /// Get the data and send it to the delegation
        /// </summary>
        /// <param name="data">
        /// the data
        /// </param>
        private void DoGetData(byte[] data)
        {
            string msg;
            msg = new string(Encoding.UTF8.GetChars(data));
            if (this.dataCallBack != null)
            {
                this.dataCallBack(msg);
            }
        }

        /// <summary>
        /// when connected, call this method
        /// </summary>
        /// <param name="port">a port</param>
        private void DoConnectedCallback(string port)
        {
            if (this.connectedCallBack != null)
            {
                this.connectedCallBack(port);
            }
        }

        /// <summary>
        /// When disconnected, call this method
        /// </summary>
        private void DoDisconnectedCallback()
        {
            if (this.disconnectedCallBack != null)
            {
                this.disconnectedCallBack(string.Empty);
            }
        }

        /// <summary>
        /// The send license.
        /// </summary>
        private void SendLicense()
        {
            if (this.workStream != null)
            {
                byte[] data = Encoding.ASCII.GetBytes(AgentSetting.LicenseId);
                this.workStream.Write(data, 0, data.Length);
            }
        }

        /// <summary>
        /// call back when before reading
        /// </summary>
        /// <param name="ar">
        /// the result
        /// </param>
        private void TcpReadCallBack(IAsyncResult ar)
        {
            var state = (StateObject)ar.AsyncState;

            // active disconnect
            if ((state.Client == null) || (!state.Client.Connected))
            {
                return;
            }

            int numberOfBytesRead;
            NetworkStream mas = state.Client.GetStream();

            try
            {
                numberOfBytesRead = mas.EndRead(ar);
                state.TotalBytesRead += numberOfBytesRead;

                Console.WriteLine("Bytes read ------ " + numberOfBytesRead);
                if (numberOfBytesRead > 0)
                {
                    var dd = new byte[numberOfBytesRead];
                    Array.Copy(state.Buffer, 0, dd, 0, numberOfBytesRead);

                    this.DoGetData(dd);

                    mas.BeginRead(state.Buffer, 0, StateObject.BufferSize, this.TcpReadCallBack, state);
                }
                else
                {
                    // passive disconnec 
                    mas.Close();
                    state.Client.Close();
                    Console.WriteLine("Bytes read ------ " + numberOfBytesRead);
                    Console.WriteLine("read stop");
                    this.DoDisconnectedCallback();
                    mas = null;
                    state = null;
                    this.AsynConnectServer();
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

                Console.WriteLine("connecting failed " + e.Message);
                this.DoDisconnectedCallback();
                this.AsynConnectServer();
            }
        }

        #endregion
    }
}