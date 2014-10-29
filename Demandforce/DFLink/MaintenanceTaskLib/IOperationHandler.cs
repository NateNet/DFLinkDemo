// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOperationHandler.cs" company="Demandforce">
//  Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
// This is interface for operation handler.  
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.MaintenanceTaskLib
{
    /// <summary>
    /// The OperationHandler interface.
    /// </summary>
    /// <typeparam name="TOperation">
    /// The operation belongs to maintenance task.
    /// </typeparam>
    public interface IOperationHandler<TOperation>
        where TOperation : IOperation
    {
        /// <summary>
        /// The handle.
        /// </summary>
        /// <param name="operation">The operation</param>
        void Handle(TOperation operation);
    }
}
