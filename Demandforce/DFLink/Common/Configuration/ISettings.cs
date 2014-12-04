// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISettings.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The configuration manage class
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Common.Configuration
{
    #region

    using System;

    #endregion

    /// <summary>
    ///     TODO: Update summary.
    /// </summary>
    public interface ISettings
    {
        #region Public Methods and Operators

        /// <summary>
        /// Get a setting value.
        /// </summary>
        /// <param name="sectionName">
        /// The section name.
        /// </param>
        /// <param name="keyName">
        /// The key name.
        /// </param>
        /// <returns>
        /// The value of the setting.
        /// </returns>
        string Get(string sectionName, string keyName);

        /// <summary>
        /// Update/Add a setting to configuration file.
        /// </summary>
        /// <param name="sectionName">
        /// The section name.
        /// </param>
        /// <param name="keyName">
        /// The key name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// unknown section exception
        /// </exception>
        string Set(string sectionName, string keyName, string value);

        #endregion
    }
}