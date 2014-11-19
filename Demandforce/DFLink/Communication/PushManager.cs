// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PushManager.cs" company="Demanforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The push manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Communication
{
    using System;
    using System.Threading;
    using Demandforce.DFLink.Communication.Socket;

    /// <summary>
    /// The push manager.
    /// </summary>
    public class PushManager
    {
        #region Static Fields

        /// <summary>
        /// The instance.
        /// </summary>
        private static readonly PushManager Instance = new PushManager();

        /// <summary>
        /// The client.
        /// </summary>
        private static ClientTcp client;

        /// <summary>
        /// The UI context.
        /// </summary>
        private readonly SynchronizationContext uiContext = new SynchronizationContext();

        /// <summary>
        /// The timer.
        /// </summary>
        private Timer timer;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Prevents a default instance of the <see cref="PushManager"/> class from being created.
        /// </summary>
        private PushManager()
        {
            client = new ClientTcp
            {
                License = AgentSetting.LicenseId,
                RemoteIp = AgentSetting.SocketIp,
                RemotePort = AgentSetting.SocketPort,
                OnGetData = msg => this.SynchonizeRunEvent(this.EventDataComming, msg),
                OnConnecting = msg => this.SynchonizeRunEvent(this.EventConnecting, msg),
                OnConnected = msg => this.SynchonizeRunEvent(this.EventConnected, msg),
                OnDisconnected = msg => this.SynchonizeRunEvent(this.EventDisconnected, msg),
                OnReadError = msg => this.SynchonizeRunEvent(this.EventReadError, msg)
            };
        }

        #endregion

        #region Public Events

        /// <summary>
        /// when the data coming, this event will be called.
        /// </summary>
        public event Action<string> EventDataComming;

        /// <summary>
        /// When the connection connected, this event will be called
        /// </summary>
        public event Action<string> EventConnected;

        /// <summary>
        /// When the connection disconnected
        /// </summary>
        public event Action<string> EventDisconnected;

        /// <summary>
        /// When begin connecting call this event
        /// </summary>
        public event Action<string> EventConnecting;

        /// <summary>
        /// The event read error.
        /// </summary>
        public event Action<string> EventReadError;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get instance.
        /// </summary>
        /// <returns>
        /// The <see cref="PushManager"/>.
        /// </returns>
        public static PushManager GetInstance()
        {
            return Instance;
        }

        /// <summary>
        /// The start.
        /// </summary>
        public void Start()
        {
            client.Connect();
        }

        #endregion

        #region Private Methods and Operators

        /// <summary>
        /// The synchronized run event.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        private void SynchonizeRunEvent(Action<string> action, string message)
        {
            if (action != null)
            {
                if (action == this.EventDisconnected)
                {
                    this.uiContext.Post(
                        stat =>
                            {
                                string msg = stat.ToString();
                                action(msg);
                                this.ReconnectServer();
                            },
                        message);
                }
                else
                {
                    this.uiContext.Post(
                       stat =>
                       {
                           string msg = stat.ToString();
                           action(msg);
                       },
                        message);
                }
            }
        }

        /// <summary>
        /// The reconnect server.
        /// </summary>
        private void ReconnectServer()
        {
            this.timer = new Timer(this.TimeCall, this, 3000, 0);
        }

        /// <summary>
        /// The time call.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        private void TimeCall(object obj)
        {
            this.timer.Dispose();
            client.Connect();
        }

        #endregion
    }
}