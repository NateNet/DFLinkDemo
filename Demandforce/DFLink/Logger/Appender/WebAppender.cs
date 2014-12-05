// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebAppender.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   TODO: Appender
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Logger.Appender
{
    using System;
    using System.Threading;

    using Demandforce.DFLink.Common;
    using Demandforce.DFLink.Common.Configuration;

    using log4net.Appender;
    using log4net.Core;

    /// <summary>
    ///     TODO: a Append
    /// </summary>
    public class WebAppender : AppenderSkeleton
    {
        #region Static Fields

        /// <summary>
        ///     The post log. Default use a method in HttpUtils
        /// </summary>
        public static Func<string, string, string> PostLog = HttpUtils.PostJsonForXml;

        #endregion

        #region Fields

        /// <summary>
        ///     The setting.
        /// </summary>
        private readonly IServerSettings serverSetting = new ServerSettings(new XmlSettings());

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
            var messagePack = (MessagePact)loggingEvent.MessageObject;
            switch (messagePack.MessageType)
            {
                case MsgType.MtLog:
                    var updateLog =
                        new
                            {
                                BusinessCredentials = new { LicenseKey = this.serverSetting.LicenseId }, 
                                Level = loggingEvent.Level.ToString(), 
                                Message = messagePack.MessageDetails, 
                                TaskId = messagePack.Taskid, 
                                Time = loggingEvent.TimeStamp, 
                                ClassName = loggingEvent.LoggerName
                            };
                    string jsonLog = JsonUtils.SerializeObject(updateLog);

                    ThreadPool.QueueUserWorkItem(p => PostLog(string.Empty, jsonLog));

                    break;
                case MsgType.MtStatus:
                    var updateStatus =
                        new
                            {
                                BusinessCredentials = new { LicenseKey = this.serverSetting.LicenseId }, 
                                Status = messagePack.AStatus, 
                                Message = messagePack.MessageDetails,
                                TaskId = messagePack.Taskid, 
                                Time = loggingEvent.TimeStamp, 
                                ClassName = loggingEvent.LoggerName
                            };
                    string jsonUpdate = JsonUtils.SerializeObject(updateStatus);

                    ThreadPool.QueueUserWorkItem(p => PostLog(string.Empty, jsonUpdate));

                    break;
            }
        }

        #endregion
    }
}