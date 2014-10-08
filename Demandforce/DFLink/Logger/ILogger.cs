// -----------------------------------------------------------------------
// <copyright file="ILogger.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Logger
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Write a debug log to the server
        /// </summary>
        /// <param name="className">the class name</param>
        /// <param name="taskId">It is the task id</param>
        /// <param name="message">log message</param>
        void Debug(string className, int taskId, string message);

        /// <summary>
        /// Write a info log to the server
        /// </summary>
        /// <param name="className">the class name</param>
        /// <param name="taskId">It is the task id</param>
        /// <param name="message">log message</param>
        void Info(string className, int taskId, string message);

        /// <summary>
        /// Write a warn log to the server
        /// </summary>
        /// <param name="className">the class name</param>
        /// <param name="taskId">It is the task id</param>
        /// <param name="message">log message</param>
        void Warn(string className, int taskId, string message);

        /// <summary>
        /// Write a error log to the server
        /// </summary>
        /// <param name="className">the class name</param>
        /// <param name="taskId">It is the task id</param>
        /// <param name="message">log message</param>
        void Error(string className, int taskId, string message);

        /// <summary>
        /// Write a fatal log to the server
        /// </summary>
        /// <param name="className">the class name</param>
        /// <param name="taskId">It is the task id</param>
        /// <param name="message">log message</param>
        void Fatal(string className, int taskId, string message);

        /// <summary>
        /// Report the status to the server
        /// </summary>
        /// <param name="className">the class name</param>
        /// <param name="taskId">It is the task id</param>
        /// <param name="status">the status</param>
        /// <param name="details">the details of the status</param>
        void ReportStatus(string className, int taskId, int status, string details);
    }
}
