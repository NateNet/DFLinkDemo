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
    using Demandforce.DFLink.Communication.Command;

    /// <summary>
    ///     TODO: Update summary.
    /// </summary>
    public class AgentUploadFile
    {
        #region Static Fields

        /// <summary>
        ///     A instance
        /// </summary>
        private static readonly AgentUploadFile Instance = new AgentUploadFile();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Prevents a default instance of the <see cref="AgentUploadFile"/> class from being created. 
        ///     Prevents a default instance of the <see cref="AgentTask"/> class from being created
        /// </summary>
        private AgentUploadFile()
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Get a singleton
        /// </summary>
        /// <returns>a single object</returns>
        public static AgentUploadFile GetStartedInstance()
        {
            return Instance;
        }

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
                                     BusinessCredentials =
                                         new BusinessInfo { LicenseKey = AgentSetting.LicenseId },
                                     TaskId = taskId
                                 };
            uploadFile.ReadFile(fileName);

            uploadFile.Request(null);
        }

        #endregion
    }
}