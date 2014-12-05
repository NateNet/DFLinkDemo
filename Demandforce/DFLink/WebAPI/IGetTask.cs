// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGetTask.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The GetTask interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.Web
{
    /// <summary>
    /// The GetTask interface.
    /// </summary>
    public interface IGetTask
    {
        #region Public Methods and Operators

        /// <summary>
        /// The do work.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string DoWork();

        #endregion
    }
}