// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatusModel.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The json status.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication.Model
{
    using Demandforce.DFLink.Common;

    /// <summary>
    ///     The json status.
    /// </summary>
    public class StatusModel : ISerializable
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the business credentials.
        /// </summary>
        public LicenseModel BusinessCredentials { get; set; }

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Gets or sets the status.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     Gets or sets the task id.
        /// </summary>
        public int TaskId { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Serialize itself
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