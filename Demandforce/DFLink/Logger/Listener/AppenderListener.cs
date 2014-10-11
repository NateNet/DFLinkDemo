// -----------------------------------------------------------------------
// <copyright file="AppenderListener.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Logger.Listener
{
    using System;
    using log4net.Appender;
    using log4net.Core;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AppenderListener : AppenderSkeleton
    {
        /// <summary>
        /// It is a Event for log 
        /// </summary>
        public static event EventHandler<LogEventArgs> EventLogListen;

        /// <summary>
        /// override the append function
        /// </summary>
        /// <param name="loggingEvent">it is a parameter</param>
        protected override void Append(LoggingEvent loggingEvent)
        {
            if (EventLogListen != null)
            {
                EventLogListen(this, new LogEventArgs(loggingEvent));
            }
        }
    }
}
