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
    ///     The state object.
    /// </summary>
    internal class StateObject
    {
        #region Constants

        /// <summary>
        ///     The buffer size.
        /// </summary>
        public const int BufferSize = 1024;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StateObject"/> class.
        /// </summary>
        public StateObject()
        {
            this.Buffer = new byte[BufferSize];
            this.TotalBytesRead = 0;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets buffer
        /// </summary>
        public byte[] Buffer { get; private set; }

        /// <summary>
        /// Gets or sets Client
        /// </summary>
        public TcpClient Client { get; set; }

        /// <summary>
        /// Gets or sets the read type
        /// </summary>
        public string ReadType { get; set; }

        /// <summary>
        /// Gets or sets the total bytes read.
        /// </summary>
        public int TotalBytesRead { get; set; }

        #endregion
    }
}