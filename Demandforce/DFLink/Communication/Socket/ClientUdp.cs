// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClientUdp.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The client udp.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication.Socket
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Timers;

    using Demandforce.DFLink.Common.Configuration;

    using log4net;

    using Logger;

    /// <summary>
    ///     The client UDP.
    /// </summary>
    public class ClientUdp : INetworkClient
    {
        #region Fields

        /// <summary>
        /// The class name.
        /// </summary>
        private readonly string className = "Communication";

        /// <summary>
        ///     The client.
        /// </summary>
        private readonly UdpClient client;

        /// <summary>
        ///     The license.
        /// </summary>
        private readonly string license;

        /// <summary>
        ///     The remote IP end point.
        /// </summary>
        private readonly IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

        /// <summary>
        /// The log sender.
        /// </summary>
        private readonly ILogger logger = new LogHelper();

        /// <summary>
        ///     The need reset callback.
        /// </summary>
        private bool needResetCallback;

        /// <summary>
        ///     The remote IP.
        /// </summary>
        private string remoteIp;

        /// <summary>
        ///     The remote port.
        /// </summary>
        private int remotePort;

        /// <summary>
        ///     The timer send license.
        /// </summary>
        private Timer timerSendLicense;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientUdp"/> class.
        /// </summary>
        /// <param name="ip">
        /// The IP.
        /// </param>
        /// <param name="port">
        /// The port.
        /// </param>
        /// <param name="license">
        /// The license.
        /// </param>
        public ClientUdp(string ip, int port, string license)
        {
            this.client = new UdpClient(ip, port);

            // this.Client = new UdpClient();
            this.remoteIp = ip;
            this.remotePort = port;
            this.license = license;

            // StateTrans stat = new StateTrans(this.Client, remoteIpEndPoint);
            // this.Client.BeginReceive(AsyncReceiveCallback, stat);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientUdp"/> class.
        /// </summary>
        /// <param name="serverSettings">
        /// The server Settings.
        /// </param>
        public ClientUdp(IServerSettings serverSettings)
        {
            this.remoteIp = serverSettings.SocketIp;
            this.remotePort = serverSettings.SocketPort;
            this.license = serverSettings.LicenseId;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the callback receive data.
        /// </summary>
        public Action<string> OnGetData { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The connect.
        /// </summary>
        public void Connect()
        {
            this.SetReceiveCallback();
            this.SendLicense();

            this.timerSendLicense = new Timer(2000) { AutoReset = false };
            this.timerSendLicense.Elapsed += this.TimerElapsedEventHandler;
            this.timerSendLicense.Enabled = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The async receive callback.
        /// </summary>
        /// <param name="ar">
        /// The AR.
        /// </param>
        private void AsyncReceiveCallback(IAsyncResult ar)
        {
            try
            {
                var stat = ar.AsyncState as ClientUdpState;
                if (stat != null)
                {
                    UdpClient cl = stat.UdpClient;
                    IPEndPoint ip = stat.Ip;
                    byte[] receiveBytes = cl.EndReceive(ar, ref ip);
                    string msg = Encoding.UTF8.GetString(receiveBytes);

                    if (!string.IsNullOrEmpty(msg))
                    {
                        this.logger.Info(this.className, -1, "Receiving message:" + msg);

                        if (this.OnGetData != null)
                        {
                            this.OnGetData(msg);
                        }
                    }

                    cl.BeginReceive(this.AsyncReceiveCallback, stat);
                }
            }
            catch (Exception ex)
            {
                this.logger.Info(this.className, -1, ex.Message + " reconnecting");
                this.needResetCallback = true;
            }
        }

        /// <summary>
        ///     The send license.
        /// </summary>
        private void SendLicense()
        {
            byte[] sendBytes = Encoding.ASCII.GetBytes(this.license);
            this.client.Send(sendBytes, sendBytes.Length);
        }

        /// <summary>
        ///     The set receive callback.
        /// </summary>
        private void SetReceiveCallback()
        {
            var stat = new ClientUdpState(this.client, this.remoteIpEndPoint);
            this.client.BeginReceive(this.AsyncReceiveCallback, stat);
        }

        /// <summary>
        /// The timer elapsed event handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void TimerElapsedEventHandler(object sender, ElapsedEventArgs e)
        {
            if (this.needResetCallback)
            {
                this.needResetCallback = false;
                this.SetReceiveCallback();
            }

            this.SendLicense();

            this.timerSendLicense.Interval = 2000;
            this.timerSendLicense.Enabled = true;
        }

        #endregion
    }
}