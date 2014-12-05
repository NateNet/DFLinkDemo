// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UploadFileModel.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The json upload file.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication.Model
{
    using Demandforce.DFLink.Common;

    /// <summary>
    ///     The json upload file.
    /// </summary>
    public class UploadFileModel : ISerializable
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the business credentials.
        /// </summary>
        public LicenseModel BusinessCredentials { get; set; }

        /// <summary>
        ///     Gets or sets the file content.
        /// </summary>
        public string FileContent { get; set; }

        /// <summary>
        ///     Gets or sets the file name.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        ///     Gets or sets the task id.
        /// </summary>
        public int TaskId { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///   Serialize itself
        /// </summary>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public string Serialize()
        {
            return JsonUtils.SerializeObject(this);
        }

        #endregion
    }
}