// -----------------------------------------------------------------------
// <copyright file="AgentLog.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Communication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Command;
    using Log;

    /// <summary>
    /// TODO: It is a log class to operate all the log command
    /// </summary>
    public class AgentLog
    {
        /// <summary>
        /// a Singleton instance
        /// </summary>
        private static readonly AgentLog Instance = new AgentLog();

        /// <summary>
        /// Prevents a default instance of the <see cref="AgentLog"/> class from being created
        /// </summary>
        private AgentLog()
        {
        }

        /// <summary>
        /// To get a Singleton object
        /// </summary>
        /// <returns>a singleton object</returns>
        public static AgentLog GetStartedInstance()
        {
            return Instance;
        }

        /// <summary>
        /// To get logs with task id
        /// </summary>
        /// <param name="taskId">it is a task id</param>
        /// <returns>a string</returns>
        public string GetLog(int taskId)
        {
            DownloadLog downloadLog = new DownloadLog();
            downloadLog.BusinessCredentials = new BusinessInfo() { LicenseKey = AgentSetting.LicenseId };
            downloadLog.TaskId = taskId;
            downloadLog.Request(null);
            return downloadLog.GetTheCallResult();
        }
    }
}
