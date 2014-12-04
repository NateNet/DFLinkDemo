﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileUploadOperationHandler.cs" company="Demandforce">
//  Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.MaintenanceTaskLib.FileMaintenance
{
    using System.IO;

    using Demandforce.DFLink.Communication;
    using Microsoft.Practices.Unity;

    /// <summary>
    /// This is a handler to upload file operation.
    /// </summary>
    public class FileUploadOperationHandler : IOperationHandler<FileOperation>
    {
        [Dependency]
        public IAgentUploadFile AgentUploadFile { get; set; }

        /// <summary>
        /// The handle.
        /// </summary>
        /// <param name="operation">
        /// The operation information.
        /// </param>
        public void Handle(FileOperation operation)
        {
            // if there is no exception for StreamReader,
            // so it can post file to server
            using (var sr = new StreamReader(operation.FileFullName))
            {
                AgentUploadFile.UploadFile(operation.Task.Id, operation.FileFullName);
            }

            // The operation result 
            operation.Result = operation.FileFullName + " uploaded.";
        }
    }
}
