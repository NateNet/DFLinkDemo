// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileVersionExploreOperationHandler.cs" company="Demandforce">
//  Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.MaintenanceTaskLib.FileMaintenance
{
    using System.Diagnostics;
    using System.IO;
    using System.Text;


    /// <summary>
    ///     TODO: Update summary.
    /// </summary>
    public class FileVersionExploreOperationHandler : IOperationHandler<FileOperation>
    {
        /// <summary>
        /// The handle.
        /// </summary>
        /// <param name="operation">
        /// The operation information.
        /// </param>
        public void Handle(FileOperation operation)
        {
            var sb = new StringBuilder();

            // if there is no filename, will return all files in the directory,
            // otherwise, the specific file name with its version
             if (string.IsNullOrEmpty(operation.FileName))
            {
                var dirInfo = new DirectoryInfo(operation.Directory);
                FileInfo[] fileNames = dirInfo.GetFiles("*.*");
                foreach (FileInfo fileName in fileNames)
                {
                    FileVersionInfo mfileVersionInfo = FileVersionInfo.GetVersionInfo(fileName.FullName);
                    sb.AppendFormat("{0}:{1}", fileName.Name, mfileVersionInfo.ProductVersion);
                }
            }
            else
            {
                FileVersionInfo mfileVersionInfo = FileVersionInfo.GetVersionInfo(operation.FileFullName);
                sb.AppendFormat("{0}:{1}", operation.FileName, mfileVersionInfo.ProductVersion);
            }

            // set the operation result with version information
            operation.Result = sb.ToString();
        }
    }
}