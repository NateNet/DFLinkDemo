// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWork.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The Work interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.WebAPI
{
    /// <summary>
    /// The Work interface.
    /// </summary>
    public interface IWork
    {
        #region Public Methods and Operators

        /// <summary>
        /// The do work.
        /// </summary>
        /// <param name="status">
        /// The status.
        /// </param>
        /// <typeparam name="T">
        /// a T
        /// </typeparam>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string DoWork<T>(T status);

        #endregion
    }
}