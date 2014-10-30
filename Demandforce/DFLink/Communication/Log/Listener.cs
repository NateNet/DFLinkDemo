// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Listener.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   TODO: It a sealed class for listen the log messages.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication.Log
{
    using System.Threading;

    using Demandforce.DFLink.Communication.Command;
    using Demandforce.DFLink.Logger.Listener;
    using log4net.Core;

    /// <summary>
    ///     TODO: It a sealed class for listen the log messages.
    /// </summary>
    public sealed class Listener
    {
        #region Static Fields

        /// <summary>
        ///     Static field which is be created at the beginning.
        /// </summary>
        private static readonly Listener Instance = new Listener();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Prevents a default instance of the <see cref="Listener" /> class from being created
        /// </summary>
        private Listener()
        {
            AppenderListener.EventLogListen += this.LogListener;
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="Listener" /> class.
        /// </summary>
        ~Listener()
        {
            AppenderListener.EventLogListen -= this.LogListener;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Get the listener.
        /// </summary>
        /// <returns>is a listener</returns>
        public static Listener GetInstance()
        {
            return Instance;
        }

        #endregion

        #region Methods

        /// <summary>
        /// It is a listener
        /// </summary>
        /// <param name="sender">
        /// a sender
        /// </param>
        /// <param name="e">
        /// a packed message
        /// </param>
        private void LogListener(object sender, LogEventArgs e)
        {
            LoggingEvent logEvent = e.LogEvent;
            var messagePack = (MessagePact)logEvent.MessageObject;

            switch (messagePack.MessageType)
            {
                case MsgType.MtLog:
                    var updateLog = new UpdateLog();
                    updateLog.BusinessCredentials = new BusinessInfo { LicenseKey = AgentSetting.LicenseId };
                    updateLog.Level = logEvent.Level.ToString();
                    updateLog.Message = messagePack.MessageDetails;
                    updateLog.TaskId = messagePack.TaskId;
                    updateLog.Time = logEvent.TimeStamp;
                    updateLog.ClassName = logEvent.LoggerName;
                    ThreadPool.QueueUserWorkItem(updateLog.Request);

                    break;
                case MsgType.MtStatus:
                    var updateStatus = new UpdateStatus();
                    updateStatus.BusinessCredentials = new BusinessInfo { LicenseKey = AgentSetting.LicenseId };
                    updateStatus.Status = messagePack.Status;
                    updateStatus.Message = messagePack.MessageDetails;
                    updateStatus.TaskId = messagePack.TaskId;
                    updateStatus.Time = logEvent.TimeStamp;
                    updateStatus.ClassName = logEvent.LoggerName;
                    ThreadPool.QueueUserWorkItem(updateStatus.Request);
                    break;
            }
        }

        #endregion
    }
}