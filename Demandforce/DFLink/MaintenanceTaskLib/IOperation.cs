// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOperation.cs" company="Demandforce">
//  Copyright (c) Demandforce. All rights reserved. 
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.MaintenanceTaskLib
{
    /// <summary>
    /// The Operation interface.
    /// </summary>
    public interface IOperation
    {
        /// <summary>
        /// The get operation result.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/> that operation returns.
        /// </returns>
        string GetResult();
    }
}