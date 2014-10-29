// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileOperationHandlerFactory.cs" company="Demandforce">
//  Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.MaintenanceTaskLib.FileMaintenance
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class FileOperationHandlerFactory : IOperationHandlerFactory<FileOperation>
    {
        #region FileMaintenanceOperation

        /// <summary>
        /// The upload config file.
        /// </summary>
        public const string Uploadconfigfile = "UploadConfigFile";

        /// <summary>
        /// The check file version.
        /// </summary>
        public const string Checkfileversion = "CheckFileVersion";

        #endregion

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
        public IOperationHandler<FileOperation> CreateHandler(string handlerName)
        {
            if (handlerName == Uploadconfigfile)
            {
                return new FileUploadOperationHandler();
            }

            if (handlerName == Checkfileversion)
            {
                return new FileVersionExploreOperationHandler();
            }

            return new NullOperationHandler<FileOperation>();
        }
    }
}
