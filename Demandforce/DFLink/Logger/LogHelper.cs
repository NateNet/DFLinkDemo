// -----------------------------------------------------------------------
// <copyright file="LogHelper.cs" company="Demandforce">
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
    using log4net;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LogHelper : ILogger
    {
        /// <summary>
        /// a singleton instance
        /// </summary>
        private static LogHelper instance = new LogHelper();

        /// <summary>
        /// Get the logger interface
        /// </summary>
        /// <returns>a interface</returns>
        public static ILogger GetLoggerHandle()
        {
            return instance;
        }

        /// <summary>
        /// Write a debug log to the server
        /// </summary>
        /// <param name="className">the class name</param>
        /// <param name="taskId">It is the task id</param>
        /// <param name="message">log message</param>
        public void Debug(string className, int taskId, string message)
        {
            ILog logger = LogManager.GetLogger(className);
            MessagePact messagePack = this.GetMessagePack(taskId, message, -1);
            logger.Debug(messagePack);
        }

        /// <summary>
        /// Write a info log to the server
        /// </summary>
        /// <param name="className">the class name</param>
        /// <param name="taskId">It is the task id</param>
        /// <param name="message">log message</param>
        public void Info(string className, int taskId, string message)
        {
            ILog logger = LogManager.GetLogger(className);
            MessagePact messagePack = this.GetMessagePack(taskId, message, -1);
            logger.Info(messagePack);
        }

        /// <summary>
        /// Write a warn log to the server
        /// </summary>
        /// <param name="className">the class name</param>
        /// <param name="taskId">It is the task id</param>
        /// <param name="message">log message</param>
        public void Warn(string className, int taskId, string message)
        {
            ILog logger = LogManager.GetLogger(className);
            MessagePact messagePack = this.GetMessagePack(taskId, message, -1);
            logger.Warn(messagePack);
        }

        /// <summary>
        /// Write a error log to the server
        /// </summary>
        /// <param name="className">the class name</param>
        /// <param name="taskId">It is the task id</param>
        /// <param name="message">log message</param>
        public void Error(string className, int taskId, string message)
        {
            ILog logger = LogManager.GetLogger(className);
            MessagePact messagePack = this.GetMessagePack(taskId, message, -1);
            logger.Error(messagePack);
        }

        /// <summary>
        /// Write a fatal log to the server
        /// </summary>
        /// <param name="className">the class name</param>
        /// <param name="taskId">It is the task id</param>
        /// <param name="message">log message</param>
        public void Fatal(string className, int taskId, string message)
        {
            ILog logger = LogManager.GetLogger(className);
            MessagePact messagePack = this.GetMessagePack(taskId, message, -1);
            logger.Fatal(messagePack);
        }

        /// <summary>
        /// Report the status to the server
        /// </summary>
        /// <param name="className">the class name</param>
        /// <param name="taskId">It is the task id</param>
        /// <param name="status">the status</param>
        /// <param name="details">the details of the status</param>
        public void ReportStatus(string className, int taskId, int status, string details)
        {
            ILog logger = LogManager.GetLogger(className);
            MessagePact messagePack = this.GetMessagePack(taskId, details, status);
            logger.Info(messagePack);
        }

        /// <summary>
        /// Instance a MessagePack
        /// </summary>
        /// <param name="taskId">It is the task id</param>
        /// <param name="message">log message</param>
        /// <param name="status">The status, it is available when ReportStatus</param>
        /// <returns>It is a class</returns>
        private MessagePact GetMessagePack(int taskId, string message, int status)
        {
            MessagePact messagePack = new MessagePact();
            messagePack.TaskId = taskId;
            messagePack.MessageDetails = message;
            messagePack.Status = status;
            if (status == -1)
            {
                messagePack.MessageType = MsgType.mtLog;
            }
            else
            {
                messagePack.MessageType = MsgType.mtStatus;
            }

            return messagePack;
        }
    }
}
