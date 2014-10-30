// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpCallerFactory.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication.WebAPI
{
    /// <summary>
    ///     TODO: A factory to make calls
    /// </summary>
    public class HttpCallerFactory : ICallerFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Create a caller
        /// </summary>
        /// <returns>a interface</returns>
        public ICaller CreateCaller()
        {
            return new HttpCaller();
        }

        #endregion
    }
}