// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AgentUploadFile.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication
{
    using Demandforce.DFLink.Common.Configuration;
    using Demandforce.DFLink.Communication.Command;
    using Demandforce.DFLink.Communication.WebAPI;

    using Microsoft.Practices.Unity;

    /// <summary>
    ///     TODO: Update summary.
    /// </summary>
    public class AgentUploadFile : IAgentUploadFile
    {
        /// <summary>
        /// Gets or sets the server settings.
        /// </summary>
        [Dependency]
        public IServerSettings ServerSettings { get; set; }

        #region Public Methods and Operators

        /// <summary>
        /// Get a task list as a string
        /// </summary>
        /// <param name="taskId">
        /// The task Id.
        /// </param>
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        public void UploadFile(int taskId, string fileName)
        {
            var uploadFile = new UploadFile
                                 {
                                     ServerSettings = this.ServerSettings,
                                     Caller = new HttpCallerFactory().CreateCaller(),
                                     BusinessCredentials =
                                         new BusinessInfo { LicenseKey = this.ServerSettings.LicenseId },
                                     TaskId = taskId
                                 };
            uploadFile.ReadFile(fileName);

            uploadFile.Request(null);
        }

        #endregion
    }
}