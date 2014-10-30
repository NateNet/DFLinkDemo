// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogHelperTest.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   This is a test class for LogHelperTest and is intended
//   to contain all LogHelperTest Unit Tests
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Logger.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using Demandforce.DFLink.Logger.Listener;
    using log4net.Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;

    using ILogger = Demandforce.DFLink.Logger.ILogger;

    /// <summary>
    ///     This is a test class for LogHelperTest and is intended
    ///     to contain all LogHelperTest Unit Tests
    /// </summary>
    [TestClass]
    [DeploymentItem("log4net.Setting.xml")]
    public class LogHelperTest
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the log level.
        /// </summary>
        private static Level LogLevel { get; set; }

        /// <summary>
        ///     Gets or sets the logger name.
        /// </summary>
        private static string LoggerName { get; set; }

        /// <summary>
        ///     Gets or sets the the return pact.
        /// </summary>
        private static MessagePact TheReturnPact { get; set; }

        #endregion

        // You can use the following additional attributes as you write your tests:
        // Use ClassInitialize to run code before running the first test in the class

        // Use ClassCleanup to run code after all tests in a class have run
        #region Public Methods and Operators

        /// <summary>
        ///     The my class cleanup.
        /// </summary>
        [ClassCleanup]
        public static void MyClassCleanup()
        {
            AppenderListener.EventLogListen -= LogListener;
        }

        /// <summary>
        /// The my class initialize.
        /// </summary>
        /// <param name="testContext">
        /// The test context.
        /// </param>
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            string logSettingName = Assembly.GetExecutingAssembly().Location;
            logSettingName = Path.GetDirectoryName(logSettingName) + @"\log4net.Setting.xml";
            LogInit.InitLog(logSettingName);
            AppenderListener.EventLogListen += LogListener;
        }

        // Use TestInitialize to run code before running each test
        // [TestInitialize()]
        // public void MyTestInitialize()
        // {
        // }
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup()
        // {
        // }

        /// <summary>
        /// The log write test.
        /// </summary>
        [DataSource(@"Provider=SQLOLEDB;
            Data Source=172.18.3.100;
            Initial Catalog=DFLinkTest;
            User ID=nate;Password=ondemand;", "LogWriteData")]
        [TestMethod]
        public void LogWriteTest()
        {
            int taskId = Convert.ToInt32(this.TestContext.DataRow["TaskId"].ToString());
            string logClass = this.TestContext.DataRow["ClassName"].ToString();
            string message = this.TestContext.DataRow["Message"].ToString();
            string logLevel = this.TestContext.DataRow["LogLevel"].ToString();
            int messageType = Convert.ToInt32(this.TestContext.DataRow["MessageType"].ToString());

            ILogger log;
            Level level = null;
            log = LogHelper.GetLoggerHandle();
            if (logLevel == "DEBUG")
            {
                log.Debug(logClass, taskId, message);
                level = Level.Debug;
            }

            if (logLevel == "INFO")
            {
                log.Info(logClass, taskId, message);
                level = Level.Info;
            }

            if (logLevel == "ERROR")
            {
                log.Error(logClass, taskId, message);
                level = Level.Error;
            }

            if (logLevel == "FATAL")
            {
                log.Fatal(logClass, taskId, message);
                level = Level.Fatal;
            }

            if (logLevel == "WARN")
            {
                log.Warn(logClass, taskId, message);
                level = Level.Warn;
            }

            Thread.Sleep(100);

            Assert.AreEqual(logClass, LoggerName);
            Assert.AreEqual(level.Name, logLevel);
            Assert.AreEqual(taskId, TheReturnPact.TaskId);
            Assert.AreEqual(message, TheReturnPact.MessageDetails);
            Assert.AreEqual((MsgType)messageType, TheReturnPact.MessageType);
        }

        /// <summary>
        ///     A test for ReportStatus
        /// </summary>
        [TestMethod]
        public void ReportStatusTest()
        {
            int taskId = 67; // TODO: Initialize to an appropriate value
            string logClass = this.GetType().AssemblyQualifiedName; // TODO: Initialize to an appropriate value
            string message = "This is a debug test message"; // TODO: Initialize to an appropriate value
            int status = 5;
            LogHelper.GetLoggerHandle().ReportStatus(logClass, taskId, status, message);
            Thread.Sleep(100);

            Assert.AreEqual(logClass, LoggerName);
            Assert.AreEqual(Level.Info, LogLevel);
            Assert.AreEqual(taskId, TheReturnPact.TaskId);
            Assert.AreEqual(message, TheReturnPact.MessageDetails);
            Assert.AreEqual(MsgType.MtStatus, TheReturnPact.MessageType);
            Assert.AreEqual(status, TheReturnPact.Status);
        }

        /// <summary>
        ///     A test for ReportStatus
        /// </summary>
        [TestMethod]
        public void ReportStatusTestT()
        {
            int taskId = 67; // TODO: Initialize to an appropriate value
            string logClass = this.GetType().AssemblyQualifiedName; // TODO: Initialize to an appropriate value
            var details = new ReportManager { Caption = "A report", Versions = new List<FileVersion>() };
            details.Versions.Add(new FileVersion { Name = "DFLink.exe", Version = "3.4.192" });
            details.Versions.Add(new FileVersion { Name = "d3123api.dll", Version = "3.0.12" });
            details.Versions.Add(new FileVersion { Name = "d3cotapi.dll", Version = "3.0.35" });

            int status = 5;
            LogHelper.GetLoggerHandle()
                .ReportStatus(logClass, taskId, status, details, obj => { return JsonConvert.SerializeObject(obj); });
            Thread.Sleep(100);

            Assert.AreEqual(logClass, LoggerName);
            Assert.AreEqual(Level.Info, LogLevel);
            Assert.AreEqual(taskId, TheReturnPact.TaskId);
            Assert.AreEqual(JsonConvert.SerializeObject(details), TheReturnPact.MessageDetails);
            Assert.AreEqual(MsgType.MtStatus, TheReturnPact.MessageType);
            Assert.AreEqual(status, TheReturnPact.Status);
        }

        /// <summary>
        ///     A test for Warn
        /// </summary>
        [TestMethod]
        public void WarnTest()
        {
            int taskId = 9; // TODO: Initialize to an appropriate value
            string logClass = this.GetType().AssemblyQualifiedName; // TODO: Initialize to an appropriate value
            string message = "This is a debug test message"; // TODO: Initialize to an appropriate value
            LogHelper.GetLoggerHandle().Warn(logClass, taskId, message);
            Thread.Sleep(100);

            Assert.AreEqual(logClass, LoggerName);
            Assert.AreEqual(Level.Warn, LogLevel);
            Assert.AreEqual(taskId, TheReturnPact.TaskId);
            Assert.AreEqual(message, TheReturnPact.MessageDetails);
            Assert.AreEqual(MsgType.MtLog, TheReturnPact.MessageType);
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
        private static void LogListener(object sender, LogEventArgs e)
        {
            LogLevel = e.LogEvent.Level;
            LoggerName = e.LogEvent.LoggerName;
            TheReturnPact = (MessagePact)e.LogEvent.MessageObject;
        }

        #endregion

        /// <summary>
        ///     The file version.
        /// </summary>
        private class FileVersion
        {
            #region Public Properties

            /// <summary>
            ///     Gets or sets the name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            ///     Gets or sets the version.
            /// </summary>
            public string Version { get; set; }

            #endregion
        }

        /// <summary>
        ///     The report manager.
        /// </summary>
        private class ReportManager
        {
            #region Public Properties

            /// <summary>
            ///     Gets or sets the caption.
            /// </summary>
            public string Caption { get; set; }

            /// <summary>
            ///     Gets or sets the versions.
            /// </summary>
            public List<FileVersion> Versions { get; set; }

            #endregion
        }
    }
}