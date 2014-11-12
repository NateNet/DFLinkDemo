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

        //private static ISocketListener listener;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Prevents a default instance of the <see cref="PushManager"/> class from being created.
        /// </summary>
        private PushManager()
        {
            ISocketListener listener = TcpclientListener.GetListener();
            listener.RegisterDataCallback(r => this.EventDataComming(r));
            listener.RegisterConnectedCallback(r => this.EventConnected(r));
            listener.RegisterDisconnectedCallBack(r => this.EventDisconnected(r));
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

        #endregion
    }
}