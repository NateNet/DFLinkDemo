// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppenderListener.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Logger.Listener
{
    using System;

    using log4net.Appender;
    using log4net.Core;

    /// <summary>
    ///     TODO: a listener
    /// </summary>
    public class AppenderListener : AppenderSkeleton
    {
        #region Public Events

        /// <summary>
        ///     It is a Event for log
        /// </summary>
        public static event EventHandler<LogEventArgs> EventLogListen;

        #endregion

        #region Methods

        /// <summary>
        /// override the append function
        /// </summary>
        /// <param name="loggingEvent">
        /// it is a parameter
        /// </param>
        protected override void Append(LoggingEvent loggingEvent)
        {
            if (EventLogListen != null)
            {
                EventLogListen(this, new LogEventArgs(loggingEvent));
            }
        }

        #endregion
    }
}