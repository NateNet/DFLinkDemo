// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGetTask.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The GetTask interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.WebAPI
{
    /// <summary>
    /// The GetTask interface.
    /// </summary>
    public interface IGetTask
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get task.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetTask();

        #endregion
    }
}