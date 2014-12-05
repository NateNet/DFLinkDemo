// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUpdateStatus.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The UpdateStatus interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.WebAPI
{
    /// <summary>
    /// The UpdateStatus interface.
    /// </summary>
    public interface IUpdateStatus
    {
        #region Public Methods and Operators

        /// <summary>
        /// The update status.
        /// </summary>
        /// <param name="taskId">
        /// The task id.
        /// </param>
        /// <param name="status">
        /// The status.
        /// </param>
        /// <param name="details">
        /// The details.
        /// </param>
        void UpdateStatus(int taskId, int status, string details);

        #endregion
    }
}