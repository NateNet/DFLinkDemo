// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LicenseModel.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The json license.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication.Model
{
    using Demandforce.DFLink.Common;

    /// <summary>
    ///     The JSON license.
    /// </summary>
    public class LicenseModel : ISerializable
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the license key.
        /// </summary>
        public string LicenseKey { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///  Serialize itself
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