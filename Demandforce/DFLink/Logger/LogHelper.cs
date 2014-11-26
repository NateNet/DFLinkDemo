// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogHelper.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Logger
{
    using System;
    using log4net;
    using Newtonsoft.Json;

    /// <summary>
    ///     TODO: Update summary.
    /// </summary>
    public class LogHelper : ILogger
    {
        #region Static Fields

        /// <summary>
        ///     a singleton instance
        /// </summary>
        private static readonly LogHelper Instance = new LogHelper();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Get the logger interface
        /// </summary>
        /// <returns>a interface</returns>
        public static ILogger GetLoggerHandle()
        {
            return Instance;
        }

        /// <summary>
        /// Write a debug log to the server
        /// </summary>
        /// <param name="className">
        /// the class name
        /// </param>
        /// <param name="taskId">
        /// It is the task id
        /// </param>
        /// <param name="message">
        /// log message
        /// </param>
        public void Debug(string className, int taskId, string message)
        {
            ILog logger = LogManager.GetLogger(className);
            MessagePact messagePack = this.GetMessagePack(taskId, message, -1);
            logger.Debug(messagePack);
        }

        /// <summary>
        /// Write a error log to the server
        /// </summary>
        /// <param name="className">
        /// the class name
        /// </param>
        /// <param name="taskId">
        /// It is the task id
        /// </param>
        /// <param name="message">
        /// log message
        /// </param>
        public void Error(string className, int taskId, string message)
        {
            ILog logger = LogManager.GetLogger(className);
            MessagePact messagePack = this.GetMessagePack(taskId, message, -1);
            logger.Error(messagePack);
        }

        /// <summary>
        /// Write a fatal log to the server
        /// </summary>
        /// <param name="className">
        /// the class name
        /// </param>
        /// <param name="taskId">
        /// It is the task id
        /// </param>
        /// <param name="message">
        /// log message
        /// </param>
        public void Fatal(string className, int taskId, string message)
        {
            ILog logger = LogManager.GetLogger(className);
            MessagePact messagePack = this.GetMessagePack(taskId, message, -1);
            logger.Fatal(messagePack);
        }

        /// <summary>
        /// Write a info log to the server
        /// </summary>
        /// <param name="className">
        /// the class name
        /// </param>
        /// <param name="taskId">
        /// It is the task id
        /// </param>
        /// <param name="message">
        /// log message
        /// </param>
        public void Info(string className, int taskId, string message)
        {
            ILog logger = LogManager.GetLogger(className);
            MessagePact messagePack = this.GetMessagePack(taskId, message, -1);
            logger.Info(messagePack);
        }

        /// <summary>
        /// Report the status to the server
        /// </summary>
        /// <param name="className">
        /// the class name
        /// </param>
        /// <param name="taskId">
        /// It is the task id
        /// </param>
        /// <param name="status">
        /// the status
        /// </param>
        /// <param name="details">
        /// the details of the status
        /// </param>
        public void ReportStatus(string className, int taskId, int status, string details)
        {
            ILog logger = LogManager.GetLogger(className);
            MessagePact messagePack = this.GetMessagePack(taskId, details, status);
            logger.Info(messagePack);
        }

        /// <summary>
        /// Report the status to the server
        /// </summary>
        /// <typeparam name="TP">
        /// type of the class
        /// </typeparam>
        /// <param name="className">
        /// component's name
        /// </param>
        /// <param name="taskId">
        /// task id
        /// </param>
        /// <param name="status">
        /// a status
        /// </param>
        /// <param name="details">
        /// a details
        /// </param>
        /// <param name="callFun">
        /// a function
        /// </param>
        public void ReportStatus<TP>(string className, int taskId, int status, TP details, Func<TP, string> callFun)
            where TP : class
        {
            ILog logger = LogManager.GetLogger(className);

            string s;
            if (callFun == null)
            {
                s = JsonConvert.SerializeObject(details);
            }
            else
            {
                s = callFun(details);
            }

            MessagePact messagePack = this.GetMessagePack(taskId, s, status);
            logger.Info(messagePack);
        }

        /// <summary>
        /// Write a warn log to the server
        /// </summary>
        /// <param name="className">
        /// the class name
        /// </param>
        /// <param name="taskId">
        /// It is the task id
        /// </param>
        /// <param name="message">
        /// log message
        /// </param>
        public void Warn(string className, int taskId, string message)
        {
            ILog logger = LogManager.GetLogger(className);
            MessagePact messagePack = this.GetMessagePack(taskId, message, -1);
            logger.Warn(messagePack);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Instance a MessagePack
        /// </summary>
        /// <param name="taskId">
        /// It is the task id
        /// </param>
        /// <param name="message">
        /// log message
        /// </param>
        /// <param name="status">
        /// The status, it is available when ReportStatus
        /// </param>
        /// <returns>
        /// It is a class
        /// </returns>
        private MessagePact GetMessagePack(int taskId, string message, int status)
        {
            var messagePack = new MessagePact();
            messagePack.TaskId = taskId;
            messagePack.MessageDetails = message;
            messagePack.Status = status;
            if (status == -1)
            {
                messagePack.MessageType = MsgType.MtLog;
            }
            else
            {
                messagePack.MessageType = MsgType.MtStatus;
            }

            return messagePack;
        }

        #endregion
    }
}