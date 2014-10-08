// -----------------------------------------------------------------------
// <copyright file="Listener.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.Log
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using Command;
    using log4net.Core;
    using Logger.Listener;

    /// <summary>
    /// TODO: It a sealed class for listen the log messages.
    /// </summary>
    public sealed class Listener
    {
        /// <summary>
        /// Static field which is be created at the beginning.
        /// </summary>
        private static readonly Listener Instance = new Listener();

        /// <summary>
        /// Prevents a default instance of the <see cref="Listener"/> class from being created
        /// </summary>
        private Listener()
        {
            AppenderListener.EventLogListen += new EventHandler<LogEventArgs>(this.LogListener);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="Listener"/> class.
        /// </summary>
        ~Listener()
        {
            AppenderListener.EventLogListen -= new EventHandler<LogEventArgs>(this.LogListener);
        }

        /// <summary>
        /// Get the listener.
        /// </summary>
        /// <returns>is a listener</returns>
        public static Listener GetInstance()
        {
            return Instance;
        }

        /// <summary>
        /// It is a listener
        /// </summary>
        /// <param name="sender">a sender</param>
        /// <param name="e">a packed message</param>
        private void LogListener(object sender, LogEventArgs e)
        {
            LoggingEvent logEvent = e.LogEvent;
            MessagePact messagePack = (MessagePact)logEvent.MessageObject;

            switch (messagePack.MessageType)
            {
                case MsgType.mtLog:
                    UpdateLog updateLog = new UpdateLog();
                    updateLog.BusinessCredentials = new BusinessInfo() { LicenseKey = AgentSetting.LicenseId };
                    updateLog.Level = logEvent.Level.ToString();
                    updateLog.Message = messagePack.MessageDetails;
                    updateLog.TaskId = messagePack.TaskId;
                    updateLog.Time = logEvent.TimeStamp;
                    updateLog.ClassName = logEvent.LoggerName;
                    ThreadPool.QueueUserWorkItem(updateLog.Request);

                    break;
                case MsgType.mtStatus:
                    UpdateStatus updateStatus = new UpdateStatus();
                    updateStatus.BusinessCredentials = new BusinessInfo() { LicenseKey = AgentSetting.LicenseId };
                    updateStatus.Status = messagePack.Status;
                    updateStatus.Message = messagePack.MessageDetails;
                    updateStatus.TaskId = messagePack.TaskId;
                    updateStatus.Time = logEvent.TimeStamp;
                    updateStatus.ClassName = logEvent.LoggerName;
                    ThreadPool.QueueUserWorkItem(updateStatus.Request);
                    break;
            }
        }
    }
}
