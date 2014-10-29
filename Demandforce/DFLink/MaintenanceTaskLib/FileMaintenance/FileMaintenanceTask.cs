// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileMaintenanceTask.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.MaintenanceTaskLib.FileMaintenance
{
    #region

    using System;
    using System.IO;
    using System.Xml.Linq;

    using Demandforce.DFLink.Common.Extensions;
    using Demandforce.DFLink.Controller.Schedule;
    using Demandforce.DFLink.Controller.Task;

    #endregion

    /// <summary>
    /// This task can execute some file maintenance operation
    /// </summary>
    public class FileMaintenanceTask : ITask, ITaskMaker
    {
        /// <summary>
        /// The file operation handler factory.
        /// </summary>
        private readonly 
            IOperationHandlerFactory<FileOperation> fileOperationHandlerFactory;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FileMaintenanceTask"/> class.
        /// </summary>
        public FileMaintenanceTask()
        {
            this.fileOperationHandlerFactory = new FileOperationHandlerFactory();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get
            {
                return "FileMaintennaceTask";
            }
        }

        /// <summary>
        /// Gets or sets the schedule.
        /// </summary>
        public ISchedule Schedule { get; set; }

        /// <summary>
        /// Gets or sets the file operation.
        /// </summary>
        public FileOperation FileOperation { get; set; }

        /// <summary>
        /// Gets or sets the file operation handler.
        /// </summary>
        public IOperationHandler<FileOperation> FileOperationHandler { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The execute.
        /// </summary>
        public void Execute()
        {
            IOperationHandler<FileOperation> handler =
                new LogOperationHandlerDecorator<FileOperation>(this.FileOperationHandler, this);

            handler.Handle(this.FileOperation);
        }

        /// <summary>
        /// The make task.
        /// </summary>
        /// <param name="arguments">
        /// The arguments.
        /// </param>
        /// <returns>
        /// The <see cref="ITask"/>.
        /// </returns>
        public ITask MakeTask(string arguments)
        {
            // parse the arguments from server
            var taskDoc = XDocument.Parse(arguments);
            var taskElement = taskDoc.Root;
            var propertyElement = taskElement.Element("Parameters");
            if (propertyElement != null)
            {
                var directory = propertyElement.Element("Directory")
                    .GetValueOrDefault(Directory.GetCurrentDirectory());
                var fileName =
                    propertyElement.Element("FileName")
                    .GetValueOrDefault(AppDomain.CurrentDomain.FriendlyName);
                var operation = propertyElement.Element("Operation")
                    .GetValueOrDefault(string.Empty);

                this.FileOperation = new FileOperation() 
                { 
                    Directory = directory, 
                    FileName = fileName, 
                    Task = this 
                };

                this.FileOperationHandler = 
                    this.fileOperationHandlerFactory.CreateHandler(operation);
            }

            return this;
        }
        
        #endregion
    }
}