// -----------------------------------------------------------------------
// <copyright file="AgentTask.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Communication
{
    using Command;

    /// <summary>
    /// TODO: It is a singleton class to operate all the task command
    /// </summary>
    public sealed class AgentTask
    {
        /// <summary>
        /// A instance
        /// </summary>
        private static readonly AgentTask Instance = new AgentTask();

        /// <summary>
        /// Prevents a default instance of the <see cref="AgentTask"/> class from being created
        /// </summary>
        private AgentTask()
        {
        }

        /// <summary>
        /// Get a singleton
        /// </summary>
        /// <returns>a single object</returns>
        public static AgentTask GetStartedInstance()
        {
            return Instance;
        }

        /// <summary>
        /// Get a task list as a string
        /// </summary>
        /// <returns>a string</returns>
        public string GetTask()
        {
            DownloadTask downloadTask = new DownloadTask();
            downloadTask.BusinessCredentials = new BusinessInfo() { LicenseKey = AgentSetting.LicenseId };
            downloadTask.Request(null);
            return downloadTask.GetTheCallResult();
        }
    }
}
