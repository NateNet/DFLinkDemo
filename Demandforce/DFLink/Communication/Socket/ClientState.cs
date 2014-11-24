// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClientState.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The state object.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.Socket
{
    using System.Net.Sockets;

    /// <summary>
    /// The state object.
    /// </summary>
    internal class ClientState
    {
        #region Constants

        /// <summary>
        ///     The buffer size.
        /// </summary>
        public const int BufferSize = 1024;

        #endregion

        #region Fields

        /// <summary>
        /// The buffer.
        /// </summary>
        private readonly byte[] buffer = new byte[BufferSize];

        /// <summary>
        /// Gets the buffer.
        /// </summary>
        public byte[] Buffer
        {
            get
            {
                return this.buffer;
            }
        }

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        public TcpClient Client { get; set; }

        /// <summary>
        /// Gets or sets the total bytes read.
        /// </summary>
        public int TotalBytesRead { get; set; }

        #endregion
    }
}
