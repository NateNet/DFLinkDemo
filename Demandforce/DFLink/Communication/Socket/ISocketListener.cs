// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISocketListener.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The SocketListener interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.Socket
{
    using System;

    /// <summary>
    /// The SocketListener interface.
    /// </summary>
    public interface ISocketListener
    {
        #region Public Methods and Operators

        /// <summary>
        /// The register data callback.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        void RegisterDataCallback(Action<string> action);

        /// <summary>
        /// the register
        /// </summary>
        /// <param name="action">the action</param>
        void RegisterConnectedCallback(Action<string> action);

        /// <summary>
        /// the register
        /// </summary>
        /// <param name="action">the action</param>
        void RegisterDisconnectedCallBack(Action<string> action);

        #endregion
    }
}