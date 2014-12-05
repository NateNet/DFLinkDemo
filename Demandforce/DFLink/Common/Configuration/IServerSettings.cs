// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IServerSettings.cs" company="">
//   
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Common.Configuration
{
    /// <summary>
    ///     TODO: Update summary.
    /// </summary>
    public interface IServerSettings
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the address url.
        /// </summary>
        string AddressUrl { get; set; }

        /// <summary>
        /// Gets or sets the command config upload.
        /// </summary>
        string CommandConfigUpload { get; set; }

        /// <summary>
        /// Gets or sets the command log download.
        /// </summary>
        string CommandLogDownload { get; set; }

        /// <summary>
        /// Gets or sets the command log status update.
        /// </summary>
        string CommandLogStatusUpdate { get; set; }

        /// <summary>
        /// Gets or sets the command log upload.
        /// </summary>
        string CommandLogUpload { get; set; }

        /// <summary>
        /// Gets or sets the command task get.
        /// </summary>
        string CommandTaskGet { get; set; }

        /// <summary>
        /// Gets or sets the license id.
        /// </summary>
        string LicenseId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether socket beat.
        /// </summary>
        bool SocketBeat { get; set; }

        /// <summary>
        /// Gets or sets the socket IP.
        /// </summary>
        string SocketIp { get; set; }

        /// <summary>
        /// Gets or sets the socket port.
        /// </summary>
        int SocketPort { get; set; }

        #endregion
    }
}