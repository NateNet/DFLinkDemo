// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlSettings.cs" company="">
//   
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Common.Configuration
{
    #region

    using System;
    using System.Collections.Specialized;
    using System.Configuration;

    #endregion

    /// <summary>
    ///     TODO: Update summary.
    /// </summary>
    public class XmlSettings : ISettings
    {
        #region Static Fields

        /// <summary>
        ///     The settings.
        /// </summary>
        private static XmlSettings settings;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Get an instance of XmlSettings.
        /// </summary>
        /// <returns>
        ///     The <see cref="XmlSettings" />.
        /// </returns>
        public static XmlSettings GetInstance()
        {
            return settings ?? (settings = new XmlSettings());
        }

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
        public string Get(string sectionName, string keyName)
        {
            try
            {
                var nameValueCollection = ConfigurationManager.GetSection(sectionName) as NameValueCollection;
                return nameValueCollection != null ? nameValueCollection[keyName] : string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

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
        public string Set(string sectionName, string keyName, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
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
            return this.Get(sectionName, keyName);
        }

        #endregion
    }
}