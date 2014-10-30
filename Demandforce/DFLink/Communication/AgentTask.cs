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
    using Demandforce.DFLink.Communication.Command;

    /// <summary>
    ///     TODO: It is a singleton class to operate all the task command
    /// </summary>
    public sealed class AgentTask
    {
        #region Static Fields

        /// <summary>
        ///     A instance
        /// </summary>
        private static readonly AgentTask Instance = new AgentTask();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Prevents a default instance of the <see cref="AgentTask" /> class from being created
        /// </summary>
        private AgentTask()
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Get a singleton
        /// </summary>
        /// <returns>a single object</returns>
        public static AgentTask GetStartedInstance()
        {
            return Instance;
        }

        /// <summary>
        ///     Get a task list as a string
        /// </summary>
        /// <returns>a string</returns>
        public string GetTask()
        {
            var downloadTask = new DownloadTask();
            downloadTask.BusinessCredentials = new BusinessInfo { LicenseKey = AgentSetting.LicenseId };
            downloadTask.Request(null);
            return downloadTask.GetTheCallResult();
        }

        #endregion
    }
}