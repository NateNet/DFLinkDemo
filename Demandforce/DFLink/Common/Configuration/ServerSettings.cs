// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServerSettings.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Common.Configuration
{
    #region

    using System;

    #endregion

    /// <summary>
    ///     TODO: Update summary.
    /// </summary>
    public class ServerSettings : IServerSettings
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerSettings"/> class.
        /// </summary>
        /// <param name="settings">
        /// The settings.
        /// </param>
        public ServerSettings(ISettings settings)
        {
            this.AddressUrl = settings.Get("serverSettings", "AddressUrl");
            this.SocketIp = settings.Get("serverSettings", "SocketIp");
            this.SocketPort = Convert.ToInt32(settings.Get("serverSettings", "SocketPort"));
            this.SocketBeat = Convert.ToBoolean(settings.Get("serverSettings", "SocketBeat"));
            this.CommandConfigUpload = settings.Get("serverSettings", "CommandConfigUpload");
            this.CommandLogDownload = settings.Get("serverSettings", "CommandLogDownload");
            this.CommandLogStatusUpdate = settings.Get("serverSettings", "CommandLogStatusUpdate");
            this.CommandLogUpload = settings.Get("serverSettings", "CommandLogUpload");
            this.CommandTaskGet = settings.Get("serverSettings", "CommandTaskGet");
            this.LicenseId = settings.Get("serverSettings", "LicenseId");
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the address url.
        /// </summary>
        public string AddressUrl { get; set; }

        /// <summary>
        ///     Gets or sets the command config upload.
        /// </summary>
        public string CommandConfigUpload { get; set; }

        /// <summary>
        ///     Gets or sets the command log download.
        /// </summary>
        public string CommandLogDownload { get; set; }

        /// <summary>
        ///     Gets or sets the command log status update.
        /// </summary>
        public string CommandLogStatusUpdate { get; set; }

        /// <summary>
        ///     Gets or sets the command log upload.
        /// </summary>
        public string CommandLogUpload { get; set; }

        /// <summary>
        ///     Gets or sets the command task get.
        /// </summary>
        public string CommandTaskGet { get; set; }

        /// <summary>
        ///     Gets or sets the license id.
        /// </summary>
        public string LicenseId { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether socket beat.
        /// </summary>
        public bool SocketBeat { get; set; }

        /// <summary>
        ///     Gets or sets the socket IP.
        /// </summary>
        public string SocketIp { get; set; }

        /// <summary>
        ///     Gets or sets the socket port.
        /// </summary>
        public int SocketPort { get; set; }

        #endregion
    }
}