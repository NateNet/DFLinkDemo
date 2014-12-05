// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UploadFile.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The upload file.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.Web
{
    using System;
    using System.IO;

    using Demandforce.DFLink.Common;
    using Demandforce.DFLink.Communication.Dao;

    /// <summary>
    /// The upload file.
    /// </summary>
    public class TaskUploadFile : IWork
    {
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
        public void DoWork(int taskId, string filePathName)
        {
            var jsonLicense = new License { LicenseKey = AgentSetting.LicenseId };
            var upload = new UploadFile
                             {
                                 BusinessCredentials = jsonLicense, 
                                 TaskId = taskId, 
                                 FileName = filePathName, 
                                 FileContent = ReadFile(filePathName)
                             };
            string jsonString = JsonUtils.SerializeObject(upload);
            HttpUtils.PostJsonForXml(AgentSetting.AddressUrl + AgentSetting.CommandLogUpload, jsonString);
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