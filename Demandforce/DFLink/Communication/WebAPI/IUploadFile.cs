// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUploadFile.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The UploadFile interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.WebAPI
{
    /// <summary>
    /// The UploadFile interface.
    /// </summary>
    public interface IUploadFile
    {
        #region Public Methods and Operators

        /// <summary>
        /// The upload file.
        /// </summary>
        /// <param name="taskId">
        /// The task id.
        /// </param>
        /// <param name="filePathName">
        /// The file path name.
        /// </param>
        void UploadFile(int taskId, string filePathName);

        #endregion
    }
}