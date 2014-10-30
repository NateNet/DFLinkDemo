// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOperationHandlerFactory.cs" company="Demandforce">
//  Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.MaintenanceTaskLib
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    /// <typeparam name="TOperation">
    /// The Operation information
    /// </typeparam>
    public interface IOperationHandlerFactory<TOperation>
        where TOperation : IOperation
    {
        /// <summary>
        /// The create handler.
        /// </summary>
        /// <param name="handlerName">
        /// The handler name.
        /// </param>
        /// <returns>
        /// The <see>
        ///         <cref>IOperationHandler</cref>
        ///     </see>
        ///     .
        /// </returns>
        IOperationHandler<TOperation> CreateHandler(string handlerName);
    }
}
