// -----------------------------------------------------------------------
// <copyright file="LogEventArgs.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Logger.Listener
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using log4net.Core;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LogEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogEventArgs" /> class.
        /// </summary>
        /// <param name="logEvent">it is the log event</param>
        public LogEventArgs(LoggingEvent logEvent)
        {
            this.LogEvent = logEvent;
        }

        /// <summary>
        /// Gets or sets the field
        /// It is the log event
        /// </summary>
        public LoggingEvent LogEvent { get; set; }
    }
}
