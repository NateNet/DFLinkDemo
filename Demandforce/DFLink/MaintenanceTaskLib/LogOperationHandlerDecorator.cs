// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogOperationHandlerDecorator.cs" company="Demandforce">
//  Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.MaintenanceTaskLib
{
    using System;

    using Demandforce.DFLink.Common.Constants;
    using Demandforce.DFLink.Controller.Task;
    using Demandforce.DFLink.Logger;

    /// <summary>
    /// The log operation handler decorator.
    /// </summary>
    /// <typeparam name="TOperation">
    /// It is the type of operation.
    /// </typeparam>
    public class LogOperationHandlerDecorator<TOperation> : IOperationHandler<TOperation>
        where TOperation : IOperation
    {
        /// <summary>
        /// The decorated operation.
        /// </summary>
        private IOperationHandler<TOperation> decoratedOperation;

        /// <summary>
        /// The task that decorated operation belongs to.
        /// </summary>
        private ITask decoratedOperationTask;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogOperationHandlerDecorator{TOperation}"/> class. 
        /// </summary>
        /// <param name="decorated">
        /// The decorated for log operation.
        /// </param>
        /// <param name="decoratedTask">
        /// The task that decorated belongs to.
        /// </param>
        public LogOperationHandlerDecorator(IOperationHandler<TOperation> decorated, ITask decoratedTask)
        {
            this.decoratedOperation = decorated;
            this.decoratedOperationTask = decoratedTask;
        }

        /// <summary>
        /// The handle for log operation as a decorator.
        /// </summary>
        /// <param name="operation">
        /// The operation.
        /// </param>
        public void Handle(TOperation operation)
        {
            try
            {
                LogHelper.GetLoggerHandle()
                    .Info(
                    "LogOperation",
                    this.decoratedOperationTask.Id, 
                    this.decoratedOperation + " started!");

                this.decoratedOperation.Handle(operation);

                LogHelper.GetLoggerHandle()
                    .Info(
                    "LogOperation",
                    this.decoratedOperationTask.Id,
                    this.decoratedOperation + " done!");

                LogHelper.GetLoggerHandle().ReportStatus(
                    this.decoratedOperationTask.ToString(),
                    this.decoratedOperationTask.Id, 
                    (int)Constants.TaskStatus.Done, 
                    operation.GetResult());
            }
            catch (Exception ex)
            {
                LogHelper.GetLoggerHandle()
                    .Info(
                    "LogOperation",
                    this.decoratedOperationTask.Id,
                    "There is exception: " + ex.Message);

                LogHelper.GetLoggerHandle()
                    .Info(
                    "LogOperation",
                    this.decoratedOperationTask.Id,
                    this.decoratedOperation + " failed!");

                LogHelper.GetLoggerHandle().ReportStatus(
                    this.decoratedOperationTask.ToString(),
                    this.decoratedOperationTask.Id,
                    (int)Constants.TaskStatus.Failed,
                    "failed");
            }
        }
    }
}
