// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Settings.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The configuration manage class
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Common
{
    #region

    using System;
    using System.Collections.Specialized;
    using System.Configuration;

    #endregion

    /// <summary>
    ///     TODO: Update summary.
    /// </summary>
    public class Settings
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
        public static string Get(string sectionName, string keyName)
        {
            var nameValueCollection = ConfigurationManager.GetSection(sectionName) as NameValueCollection;
            return nameValueCollection != null ? nameValueCollection[keyName] : string.Empty;
        }

        /// <summary>
        /// Update/Add a setting con configuration file.
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
        public static string Set(string sectionName, string keyName, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var section = config.GetSection(sectionName) as AppSettingsSection;

            if (section == null)
            {
                throw new Exception("Unknown section name.");
            }

            var key = section.Settings[keyName];

            if (key == null)
            {
                section.Settings.Add(keyName, value);
            }
            else
            {
                section.Settings[keyName].Value = value;
            }

            config.Save();
            ConfigurationManager.RefreshSection(sectionName);
            return Get(sectionName, keyName);
        }

        #endregion
    }
}