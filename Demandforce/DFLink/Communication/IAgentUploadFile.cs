// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAgentUploadFile.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The AgentUploadFile interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication
{
    /// <summary>
    /// The AgentUploadFile interface.
    /// </summary>
    public interface IAgentUploadFile
    {
        #region Public Methods and Operators

        /// <summary>
        /// The upload file.
        /// </summary>
        /// <param name="taskId">
        /// The task id.
        /// </param>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        void UploadFile(int taskId, string fileName);

        #endregion
    }
}