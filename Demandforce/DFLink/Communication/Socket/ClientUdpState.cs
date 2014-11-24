// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClientUdpState.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The client udp state.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.Socket
{
    using System.Net;
    using System.Net.Sockets;

    /// <summary>
    /// The client udp state.
    /// </summary>
    public class ClientUdpState
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientUdpState"/> class.
        /// </summary>
        /// <param name="udpclient">
        /// The udpclient.
        /// </param>
        /// <param name="ip">
        /// The ip.
        /// </param>
        public ClientUdpState(UdpClient udpclient, IPEndPoint ip)
        {
            this.UdpClient = udpclient;
            this.Ip = ip;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientUdpState"/> class.
        /// </summary>
        /// <param name="udpclient">
        /// The udpclient.
        /// </param>
        /// <param name="ip">
        /// The ip.
        /// </param>
        /// <param name="license">
        /// The license.
        /// </param>
        public ClientUdpState(UdpClient udpclient, IPEndPoint ip, string license)
        {
            this.UdpClient = udpclient;
            this.Ip = ip;
            this.License = license;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the ip.
        /// </summary>
        public IPEndPoint Ip { get; private set; }

        /// <summary>
        /// Gets the license.
        /// </summary>
        public string License { get; private set; }

        /// <summary>
        /// Gets the udp client.
        /// </summary>
        public UdpClient UdpClient { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            return this.License;
        }

        #endregion
    }
}