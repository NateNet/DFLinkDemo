// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AgentTask.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   TODO: It is a singleton class to operate all the task command
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication
{
    using Demandforce.DFLink.Common.Configuration;
    using Demandforce.DFLink.Communication.Command;

    using Microsoft.Practices.Unity;
    using Demandforce.DFLink.Communication.WebAPI;

    /// <summary>
    ///     TODO: It is a singleton class to operate all the task command
    /// </summary>
    public sealed class AgentTask : IAgentTask
    {
        /// <summary>
        /// Gets or sets the server settings.
        /// </summary>
        [Dependency]
        public IServerSettings ServerSettings { get; set; }


        #region Public Methods and Operators

        /// <summary>
        ///     Get a task list as a string
        /// </summary>
        /// <returns>a string</returns>
        public string GetTask()
        {
            var downloadTask = new DownloadTask();
            downloadTask.ServerSettings = this.ServerSettings;
            downloadTask.Caller = new HttpCallerFactory().CreateCaller();
            downloadTask.BusinessCredentials = new BusinessInfo { LicenseKey = this.ServerSettings.LicenseId };
            downloadTask.Request(null);
            return downloadTask.GetTheCallResult();
        }

        #endregion
    }
}