// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionPolicyWrapper.cs" company="Demandforce">
//  Copyright (c) Demandforce. All rights reserved.  
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.ExceptionHandling.Logging.ExceptionHandleWrapper
{
    #region

    using System;

    using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

    #endregion

    /// <summary>
    ///     TODO: Update summary.
    /// </summary>
    public class ExceptionPolicyWrapper : IExceptionPolicy
    {
        #region Public Methods and Operators

        /// <summary>
        /// The handler exception.
        /// </summary>
        /// <param name="ex">
        /// The ex.
        /// </param>
        /// <param name="policy">
        /// The policy.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool HandlerException(Exception ex, string policy)
        {
            return ExceptionPolicy.HandleException(ex, policy);
        }

        #endregion
    }
}