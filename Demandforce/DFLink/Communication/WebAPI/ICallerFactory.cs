// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICallerFactory.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication.WebAPI
{
    /// <summary>
    ///     TODO: factory's interface
    /// </summary>
    public interface ICallerFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Create a caller
        /// </summary>
        /// <returns>a interface</returns>
        ICaller CreateCaller();

        #endregion
    }
}