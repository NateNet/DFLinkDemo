// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogInit.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   TODO: initialize the log tool
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Logger
{
    using System.IO;

    using log4net.Config;

    /// <summary>
    ///     TODO: initialize the log tool
    /// </summary>
    public static class LogInit
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="LogInit"/> class.
        /// </summary>
        static LogInit()
        {
            HaveCallInit = false;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets a value indicating whether.
        /// </summary>
        public static bool HaveCallInit { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// initialize the log tool
        /// </summary>
        /// <param name="logSettingName">
        /// log setting name, it is a xml file
        /// </param>
        public static void InitLog(string logSettingName)
        {
            if (!HaveCallInit)
            {
                HaveCallInit = true;
                // XmlConfigurator.ConfigureAndWatch();
            }
        }

        #endregion
    }
}