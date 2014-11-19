// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StateObject.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The state object.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.Socket
{
    using System.Net.Sockets;
    using System.Text;

    /// <summary>
    /// The state object.
    /// </summary>
    internal class StateObject
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
        /// The message buffer.
        /// </summary>
        private readonly StringBuilder messageBuffer = new StringBuilder();

        /// <summary>
        /// Gets the message buffer.
        /// </summary>
        public StringBuilder MessageBuffer
        {
            get
            {
                return this.messageBuffer;
            }
        }

        /// <summary>
        /// Gets or sets the total bytes read.
        /// </summary>
        public int TotalBytesRead { get; set; }

        #endregion
    }
}
