// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AgentSetting.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   TODO: It is a initialization
//   Before call this module's others function, please initialize this work
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Xml;

    using Demandforce.DFLink.Common;
    using Demandforce.DFLink.Communication.Log;
    using Demandforce.DFLink.Communication.WebAPI;

    /// <summary>
    ///     TODO: It is a initialization
    ///     Before call this module's others function, please initialize this work
    /// </summary>
    public static class AgentSetting
    {
        #region Static Fields

        /// <summary>
        ///     A factory
        /// </summary>
        private static ICallerFactory callerFactory = new HttpCallerFactory();

        /// <summary>
        ///     root of a xml document
        /// </summary>
        private static XmlNode root;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the property
        /// </summary>
        public static string AddressUrl { get; set; }

        /// <summary>
        /// Gets or sets the URL for socket
        /// </summary>
        public static string SocketIp { get; set; }

        /// <summary>
        /// Gets or sets the port for socket
        /// </summary>
        public static int SocketPort { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to keep the connection alive
        /// </summary>
        public static bool SocketBeat { get; set; }

        /// <summary>
        ///     Gets or sets the caller factory
        /// </summary>
        public static ICallerFactory CallerFactory
        {
            get
            {
                return callerFactory;
            }

            set
            {
                callerFactory = value;
            }
        }

        /// <summary>
        ///     Gets or sets the property
        /// </summary>
        public static string CommandConfigUpload { get; set; }

        /// <summary>
        ///     Gets or sets the property
        /// </summary>
        public static string CommandLogDownload { get; set; }

        /// <summary>
        ///     Gets or sets the property
        /// </summary>
        public static string CommandLogStatusUpdate { get; set; }

        /// <summary>
        ///     Gets or sets the property
        /// </summary>
        public static string CommandLogUpload { get; set; }

        /// <summary>
        ///     Gets or sets the property
        /// </summary>
        public static string CommandTaskGet { get; set; }

        /// <summary>
        ///     Gets or sets the property
        /// </summary>
        public static string LicenseId { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Initialize the setting
        /// </summary>
        public static void InitialSetting()
        {
            Listener.GetInstance();
            AddressUrl = Settings.Get("serverSettings", "AddressUrl");
            SocketIp = Settings.Get("serverSettings", "SocketIp");
            SocketPort = Convert.ToInt32(Settings.Get("serverSettings", "SocketPort"));
            SocketBeat = Convert.ToBoolean(Settings.Get("serverSettings", "SocketBeat"));
            CommandConfigUpload = Settings.Get("serverSettings", "CommandConfigUpload");
            CommandLogDownload = Settings.Get("serverSettings", "CommandLogDownload");
            CommandLogStatusUpdate = Settings.Get("serverSettings", "CommandLogStatusUpdate");
            CommandLogUpload = Settings.Get("serverSettings", "CommandLogUpload");
            CommandTaskGet = Settings.Get("serverSettings", "CommandTaskGet");
            LicenseId = Settings.Get("serverSettings", "LicenseId");

            /*
            var doc = new XmlDocument();
            string settingFile = Assembly.GetExecutingAssembly().Location;
            settingFile = Path.GetDirectoryName(settingFile) + @"\ServerSet.xml";
            doc.Load(settingFile);
            root = doc.DocumentElement;
            foreach (XmlNode node in root.ChildNodes)
            {
                string nodeName = node.Name;
                string nodeValue = node.InnerText;
                PropertyInfo info = typeof(AgentSetting).GetProperty(nodeName);
                if (info != null)
                {
                    Type t = info.PropertyType;
                    if (t.Equals(typeof(string)))
                    {
                        info.SetValue(null, nodeValue, null);
                    }
                    else if (t.Equals(typeof(int)))
                    {
                        int defaultValue = 0;
                        if (int.TryParse(nodeValue, out defaultValue))
                        {
                            info.SetValue(null, defaultValue, null);
                        }
                    }
                    else if (t.Equals(typeof(bool)))
                    {
                        bool defaultValue = false;
                        if (bool.TryParse(nodeValue, out defaultValue))
                        {
                            info.SetValue(null, defaultValue, null);
                        }
                    }
                    else if (t.Equals(typeof(byte)))
                    {
                        byte defaultValue = 0;
                        if (byte.TryParse(nodeValue, out defaultValue))
                        {
                            info.SetValue(null, defaultValue, null);
                        }
                    }
                }
            }
            */
        }

        #endregion
    }
}