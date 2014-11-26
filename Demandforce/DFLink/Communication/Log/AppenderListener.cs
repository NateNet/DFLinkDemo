// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppenderListener.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication.Log
{
    using System.Threading;
    using Demandforce.DFLink.Communication.Command;
    using Demandforce.DFLink.Logger;
    using log4net.Appender;
    using log4net.Core;
    
    /// <summary>
    ///     TODO: a listener
    /// </summary>
    public class AppenderListener : AppenderSkeleton
    {
        #region Methods

        /// <summary>
        /// override the append function
        /// </summary>
        /// <param name="loggingEvent">
        /// it is a parameter
        /// </param>
        protected override void Append(LoggingEvent loggingEvent)
        {
            if (loggingEvent != null)
            {
                LoggingEvent logEvent = loggingEvent;
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
        }

        #endregion
    }
}