// -----------------------------------------------------------------------
// <copyright file="AgentSetting.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Communication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Log;
    using WebAPI;

    /// <summary>
    /// TODO: It is a initialization
    /// Before call this module's others function, please initialize this work 
    /// </summary>
    public static class AgentSetting
    {
        /// <summary>
        /// The ticket for getting task
        /// </summary>
        public static readonly string CommandTaskGet = "/task/get";

        /// <summary>
        /// The ticket for uploading log
        /// </summary>
        public static readonly string CommandLogUpload = "/log/upload";

        /// <summary>
        /// The ticket for setting status
        /// </summary>
        public static readonly string CommandLogStatusUpdate = "/log/status/update";

        /// <summary>
        /// The ticket for downloading log
        /// </summary>
        public static readonly string CommandLogDownload = "/log/download";

        /// <summary>
        /// A factory
        /// </summary>
        private static ICallerFactory callerFactory = new HttpCallerFactory();

        /// <summary>
        /// Gets or sets the caller factory
        /// </summary>
        public static ICallerFactory CallerFactory
        {
            get { return callerFactory; }
            set { callerFactory = value; }
        }

        /// <summary>
        /// Gets or sets the server's AddressUrl
        /// </summary>
        public static string AddressUrl { get; set; }

        /// <summary>
        /// Gets or sets the LicenseID
        /// </summary>
        public static string LicenseId { get; set; }

        /// <summary>
        /// To add a listener to logger
        /// </summary>
        public static void HookLogger()
        {
            Listener.GetInstance();
        }
    }
}
