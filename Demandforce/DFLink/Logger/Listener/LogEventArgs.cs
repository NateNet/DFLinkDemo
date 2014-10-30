// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogEventArgs.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Logger.Listener
{
    using System;

    using log4net.Core;

    /// <summary>
    ///     TODO: Update summary.
    /// </summary>
    public class LogEventArgs : EventArgs
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEventArgs"/> class.
        /// </summary>
        /// <param name="logEvent">
        /// it is the log event
        /// </param>
        public LogEventArgs(LoggingEvent logEvent)
        {
            this.LogEvent = logEvent;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the field
        ///     It is the log event
        /// </summary>
        public LoggingEvent LogEvent { get; set; }

        #endregion
    }
}