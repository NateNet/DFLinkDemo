// --------------------------------------------------------------------------------------------------------------------
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

    /// <summary>
    /// This is a handler to upload file operation.
    /// </summary>
    public class FileUploadOperationHandler : IOperationHandler<FileOperation>
    {
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
                AgentUploadFile.GetStartedInstance()
                    .UploadFile(operation.Task.Id, operation.FileFullName);
            }

            // The operation result 
            operation.Result = operation.FileFullName + " uploaded.";
        }
    }
}
