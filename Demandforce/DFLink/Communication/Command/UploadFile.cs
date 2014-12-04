// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UploadFile.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication.Command
{
    using System;
    using System.IO;

    /// <summary>
    ///     TODO: It is a model for serialization
    /// </summary>
    public class UploadFile : BaseCommand
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the content of a file
        /// </summary>
        public string FileContent { get; set; }

        /// <summary>
        ///     Gets or sets the file name
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        ///     Gets or sets a task id
        /// </summary>
        public int TaskId { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Read a file to file content as base64 format
        /// </summary>
        /// <param name="fileName">
        /// file name
        /// </param>
        public void ReadFile(string fileName)
        {
            this.FileName = fileName;

            FileStream fileStream = new FileInfo(this.FileName).OpenRead();
            var reader = new BinaryReader(fileStream);
            byte[] data = reader.ReadBytes(Convert.ToInt32(fileStream.Length));
            this.FileContent = Convert.ToBase64String(data);
        }

        /// <summary>
        /// It is a function which is used for thread pool.
        /// </summary>
        /// <param name="idleParam">
        /// a command, no use, set it to null
        /// </param>
        public override void Request(object idleParam)
        {
            string jsonStr = JsonPack<UploadFile>.SerializeObject(this);
            this.Caller.PostCommand(this.ServerSettings.AddressUrl + this.ServerSettings.CommandConfigUpload, jsonStr);
        }

        #endregion
    }
}