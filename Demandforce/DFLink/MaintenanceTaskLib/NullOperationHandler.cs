// -----------------------------------------------------------------------
// <copyright file="NullOperationHandler.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.MaintenanceTaskLib
{
    using System;

    /// <summary>
    /// The null operation handler.
    /// </summary>
    /// <typeparam name="TOperation">
    /// any operation can have null handler.
    /// </typeparam>
    public class NullOperationHandler<TOperation> : IOperationHandler<TOperation>
        where TOperation : IOperation
    {
        /// <summary>
        /// The handle for null operation.
        /// </summary>
        /// <param name="operation">
        /// The operation.
        /// </param>
        public void Handle(TOperation operation)
        {
            throw new Exception("Please Check the operation name!");
        }
    }
}
