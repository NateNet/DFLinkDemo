// -----------------------------------------------------------------------
// <copyright file="ILogger.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
namespace Demandforce.DFLink.Logger
{
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

        /// <summary>
        /// Report the status to the server
        /// </summary>
        /// <typeparam name="P">type of the class</typeparam>
        /// <param name="className">component's name</param>
        /// <param name="taskId">task id</param>
        /// <param name="status">status</param>
        /// <param name="details">details</param>
        /// <param name="callFun">a function</param>
        void ReportStatus<P>(string className, int taskId, int status, P details, Func<P, string> callFun) where P : class;
    }
}
