// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkUploadFile.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The upload file.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication.WebAPI
{
    using System;
    using System.IO;

    using Demandforce.DFLink.Common.Configuration;
    using Demandforce.DFLink.Communication.Model;

    /// <summary>
    ///     The upload file.
    /// </summary>
    public class WorkUploadFile : IUploadFile
    {
        #region Fields

        /// <summary>
        ///     The setting.
        /// </summary>
        private readonly IServerSettings serverSetting = new ServerSettings(new XmlSettings());

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the call.
        /// </summary>
        public ICall Call { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The do work.
        /// </summary>
        /// <param name="taskId">
        /// The task id.
        /// </param>
        /// <param name="filePathName">
        /// The file path name.
        /// </param>
        public void UploadFile(int taskId, string filePathName)
        {
            var licenseModel = new LicenseModel { LicenseKey = this.serverSetting.LicenseId };
            var uploadModel = new UploadFileModel
                                  {
                                      BusinessCredentials = licenseModel, 
                                      TaskId = taskId, 
                                      FileName = filePathName, 
                                      FileContent = ReadFile(filePathName)
                                  };
            this.Call.DoWork(this.serverSetting.AddressUrl + this.serverSetting.CommandLogUpload, uploadModel);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The read file.
        /// </summary>
        /// <param name="filePathName">
        /// The file path name.
        /// </param>
        /// <returns>
        /// The BASE64 format string<see cref="string"/>.
        /// </returns>
        private static string ReadFile(string filePathName)
        {
            FileStream fileStream = new FileInfo(filePathName).OpenRead();
            var reader = new BinaryReader(fileStream);
            byte[] data = reader.ReadBytes(Convert.ToInt32(fileStream.Length));
            return Convert.ToBase64String(data);
        }

        #endregion
    }
}