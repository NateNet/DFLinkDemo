// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICall.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The Work interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication.WebAPI
{
    using Demandforce.DFLink.Communication.Model;

    /// <summary>
    ///     The Work interface.
    /// </summary>
    public interface ICall
    {
        #region Public Methods and Operators

        /// <summary>
        /// The do work.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <typeparam name="T">
        /// a T
        /// </typeparam>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string DoWork(string url, ISerializable data);

        #endregion
    }
}