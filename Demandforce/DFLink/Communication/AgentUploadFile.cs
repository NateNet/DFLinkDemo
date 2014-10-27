// -----------------------------------------------------------------------
// <copyright file="AgentUploadFiles.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Communication
{
    using Command;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AgentUploadFile
    {
                /// <summary>
        /// A instance
        /// </summary>
        private static readonly AgentUploadFile Instance = new AgentUploadFile();

        /// <summary>
        /// Prevents a default instance of the <see cref="AgentTask"/> class from being created
        /// </summary>
        private AgentUploadFile()
        {
        }

        /// <summary>
        /// Get a singleton
        /// </summary>
        /// <returns>a single object</returns>
        public static AgentUploadFile GetStartedInstance()
        {
            return Instance;
        }

        /// <summary>
        /// Get a task list as a string
        /// </summary>
        /// <returns>a string</returns>
        public void UploadFile(int taskId, string fileName)
        {
            UploadFile UploadFile = new UploadFile();
            UploadFile.BusinessCredentials = new BusinessInfo() { LicenseKey = AgentSetting.LicenseId };
            UploadFile.TaskId = taskId;
            UploadFile.ReadFile(fileName);

            UploadFile.Request(null);
        }
    }
}
