// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUploadFile.cs" company="">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The UploadFile interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.Web
{
    /// <summary>
    /// The UploadFile interface.
    /// </summary>
    public interface IUploadFile
    {
        #region Public Methods and Operators

        /// <summary>
        /// The do work.
        /// </summary>
        /// <param name="taskId">
        /// The task id.
        /// </param>
        /// <param name="filePathName">
        /// The file path name.
        /// </param>
        void DoWork(int taskId, string filePathName);

        #endregion
    }
}