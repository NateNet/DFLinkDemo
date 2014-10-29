// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileOperation.cs" company="Demandforce">
//  Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.MaintenanceTaskLib.FileMaintenance
{
    using System.IO;

    using Demandforce.DFLink.Controller.Task;

    /// <summary>
    /// This is for information that operates the file,
    /// which includes parameters and result that file handler needs 
    /// and may produce.
    /// </summary>
    public class FileOperation : IOperation
    {
        /// <summary>
        /// Gets or sets the directory.
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets the file full name.
        /// </summary>
        public string FileFullName 
        { 
            get
            {
                return Path.Combine(this.Directory, this.FileName);
            }
        }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// Gets or sets the task that operation belongs.
        /// </summary>
        public ITask Task { get; set; }

        /// <summary>
        /// The get result.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetResult()
        {
            return this.Result;
        }
    }
}
