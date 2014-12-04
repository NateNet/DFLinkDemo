// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AgentLog.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   TODO: It is a log class to operate all the log command
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication
{
    using Demandforce.DFLink.Common.Configuration;
    using Demandforce.DFLink.Communication.Command;
    using Demandforce.DFLink.Communication.WebAPI;

    using Microsoft.Practices.Unity;

    /// <summary>
    ///     TODO: It is a log class to operate all the log command
    /// </summary>
    public class AgentLog : IAgentLog
    {
        /// <summary>
        /// Gets or sets the server settings.
        /// </summary>
        [Dependency]
        public IServerSettings ServerSettings { get; set; }

        #region Public Methods and Operators

        /// <summary>
        /// To get logs with task id
        /// </summary>
        /// <param name="taskId">
        /// it is a task id
        /// </param>
        /// <returns>
        /// a string
        /// </returns>
        public string GetLog(int taskId)
        {
            var downloadLog = new DownloadLog();
            downloadLog.ServerSettings = this.ServerSettings;
            downloadLog.Caller = new HttpCallerFactory().CreateCaller();
            downloadLog.BusinessCredentials = new BusinessInfo { LicenseKey = this.ServerSettings.LicenseId };
            downloadLog.TaskId = taskId;
            downloadLog.Request(null);
            return downloadLog.GetTheCallResult();
        }

        #endregion
    }
}