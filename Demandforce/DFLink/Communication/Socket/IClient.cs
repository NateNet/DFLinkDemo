// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IClient.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The Client interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.Socket
{
    using System;

    /// <summary>
    /// The Client interface.
    /// </summary>
    public interface IClient
    {
        /// <summary>
        /// Gets or sets the on get data.
        /// </summary>
        Action<string> OnGetData { get; set; }

        /// <summary>
        /// asynchronous connection
        /// </summary>
        void Connect();
    }
}