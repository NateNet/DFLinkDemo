// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExceptionPolicy.cs" company="Demandforce">
// Copyright (c) Demandforce. All rights reserved.   
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.ExceptionHandling.Logging.ExceptionHandleWrapper
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IExceptionPolicy
    {
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
        bool HandlerException(Exception ex, string policy);
    }
}
