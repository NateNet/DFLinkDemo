// -----------------------------------------------------------------------
// <copyright file="LogInit.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Logger
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Listener;

    /// <summary>
    /// TODO: initialize the log tool
    /// </summary>
    public static class LogInit
    {
        /// <summary>
        /// True: has been initialized, else hasn't
        /// </summary>
        private static bool haveCallInit = false;

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        public static bool HaveCallInit
        {
            get
            {
                return haveCallInit;
            }

            set
            {
                haveCallInit = value;
            }
        }

        /// <summary>
        /// initialize the log tool
        /// </summary>
        /// <param name="logSettingName">log setting name, it is a xml file</param>
        public static void InitLog(string logSettingName)
        {
            if (!HaveCallInit)
            {
                HaveCallInit = true;
                log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(logSettingName));
            }
        }
    }
}
