using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using Demandforce.DFLink.Logger.Listener;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Demandforce.DFLink.Logger.Tests
{
    /// <summary>
    ///This is a test class for LogHelperTest and is intended
    ///to contain all LogHelperTest Unit Tests
    ///</summary>
    [TestClass()]
    [DeploymentItem("log4net.Setting.xml")]
    public class LogHelperTest
    {

        /// <summary>
        /// a instance
        /// </summary>
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            string logSettingName = System.Reflection.Assembly.GetExecutingAssembly().Location;
            logSettingName = System.IO.Path.GetDirectoryName(logSettingName) + @"\log4net.Setting.xml";
            LogInit.InitLog(logSettingName);
            AppenderListener.EventLogListen += new EventHandler<LogEventArgs>(LogListener);
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            AppenderListener.EventLogListen -= new EventHandler<LogEventArgs>(LogListener);
        }
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Debug
        ///</summary>
        [TestMethod()]
        public void DebugTest()
        {
            int taskId = 12; // TODO: Initialize to an appropriate value
            string logClass = this.GetType().AssemblyQualifiedName; // TODO: Initialize to an appropriate value
            string message = "This is a debug test message"; // TODO: Initialize to an appropriate value
            LogHelper.GetLoggerHandle().Debug(logClass, taskId, message);
            Thread.Sleep(100);

            Assert.AreEqual(logClass, LoggerName);
            Assert.AreEqual(log4net.Core.Level.Debug, LogLevel);
            Assert.AreEqual(taskId, TheReturnPact.TaskId);
            Assert.AreEqual(message, TheReturnPact.MessageDetails);
            Assert.AreEqual(MsgType.mtLog, TheReturnPact.MessageType);
        }

        /// <summary>
        ///A test for Error
        ///</summary>
        [TestMethod()]
        public void ErrorTest()
        {
            int taskId = 12; // TODO: Initialize to an appropriate value
            string logClass = this.GetType().AssemblyQualifiedName; // TODO: Initialize to an appropriate value
            string message = "This is a debug test message"; // TODO: Initialize to an appropriate value
            LogHelper.GetLoggerHandle().Error(logClass, taskId, message);
            Thread.Sleep(100);

            Assert.AreEqual(logClass, LoggerName);
            Assert.AreEqual(log4net.Core.Level.Error, LogLevel);
            Assert.AreEqual(taskId, TheReturnPact.TaskId);
            Assert.AreEqual(message, TheReturnPact.MessageDetails);
            Assert.AreEqual(MsgType.mtLog, TheReturnPact.MessageType);
        }

        /// <summary>
        ///A test for Fatal
        ///</summary>
        [TestMethod()]
        public void FatalTest()
        {
            int taskId = 15; // TODO: Initialize to an appropriate value
            string logClass = this.GetType().AssemblyQualifiedName; // TODO: Initialize to an appropriate value
            string message = "This is a debug test message"; // TODO: Initialize to an appropriate value
            LogHelper.GetLoggerHandle().Fatal(logClass, taskId, message);
            Thread.Sleep(100);

            Assert.AreEqual(logClass, LoggerName);
            Assert.AreEqual(log4net.Core.Level.Fatal, LogLevel);
            Assert.AreEqual(taskId, TheReturnPact.TaskId);
            Assert.AreEqual(message, TheReturnPact.MessageDetails);
            Assert.AreEqual(MsgType.mtLog, TheReturnPact.MessageType);
        }

        /// <summary>
        ///A test for Info
        ///</summary>
        [TestMethod()]
        public void InfoTest()
        {
            int taskId = 32; // TODO: Initialize to an appropriate value
            string logClass = this.GetType().AssemblyQualifiedName; // TODO: Initialize to an appropriate value
            string message = "This is a debug test message"; // TODO: Initialize to an appropriate value
            LogHelper.GetLoggerHandle().Info(logClass, taskId, message);
            Thread.Sleep(100);

            Assert.AreEqual(logClass, LoggerName);
            Assert.AreEqual(log4net.Core.Level.Info, LogLevel);
            Assert.AreEqual(taskId, TheReturnPact.TaskId);
            Assert.AreEqual(message, TheReturnPact.MessageDetails);
            Assert.AreEqual(MsgType.mtLog, TheReturnPact.MessageType);
        }

        /// <summary>
        ///A test for ReportStatus
        ///</summary>
        [TestMethod()]
        public void ReportStatusTest()
        {
            int taskId = 67; // TODO: Initialize to an appropriate value
            string logClass = this.GetType().AssemblyQualifiedName; // TODO: Initialize to an appropriate value
            string message = "This is a debug test message"; // TODO: Initialize to an appropriate value
            int status = 5;
            LogHelper.GetLoggerHandle().ReportStatus(logClass, taskId, status, message);
            Thread.Sleep(100);

            Assert.AreEqual(logClass, LoggerName);
            Assert.AreEqual(log4net.Core.Level.Info, LogLevel);
            Assert.AreEqual(taskId, TheReturnPact.TaskId);
            Assert.AreEqual(message, TheReturnPact.MessageDetails);
            Assert.AreEqual(MsgType.mtStatus, TheReturnPact.MessageType);
            Assert.AreEqual(status, TheReturnPact.Status);
        }

        /// <summary>
        ///A test for ReportStatus
        ///</summary>
        [TestMethod()]
        public void ReportStatusTestT()
        {
            int taskId = 67; // TODO: Initialize to an appropriate value
            string logClass = this.GetType().AssemblyQualifiedName; // TODO: Initialize to an appropriate value
            ReportManager details = new ReportManager() { Caption = "A report", Versons = new List<FileVersion>() };
            details.Versons.Add(new FileVersion() { Name = "DFLink.exe", Version = "3.4.192" });
            details.Versons.Add(new FileVersion() { Name = "d3123api.dll", Version = "3.0.12" });
            details.Versons.Add(new FileVersion() { Name = "d3cotapi.dll", Version = "3.0.35" });

            int status = 5;
            LogHelper.GetLoggerHandle().ReportStatus<ReportManager>(logClass, taskId, status, details, obj => { return JsonConvert.SerializeObject(obj); });
            Thread.Sleep(100);

            Assert.AreEqual(logClass, LoggerName);
            Assert.AreEqual(log4net.Core.Level.Info, LogLevel);
            Assert.AreEqual(taskId, TheReturnPact.TaskId);
            Assert.AreEqual(JsonConvert.SerializeObject(details), TheReturnPact.MessageDetails);
            Assert.AreEqual(MsgType.mtStatus, TheReturnPact.MessageType);
            Assert.AreEqual(status, TheReturnPact.Status);
        }

        /// <summary>
        ///A test for Warn
        ///</summary>
        [TestMethod()]
        public void WarnTest()
        {
            int taskId = 9; // TODO: Initialize to an appropriate value
            string logClass = this.GetType().AssemblyQualifiedName; // TODO: Initialize to an appropriate value
            string message = "This is a debug test message"; // TODO: Initialize to an appropriate value
            LogHelper.GetLoggerHandle().Warn(logClass, taskId, message);
            Thread.Sleep(100);

            Assert.AreEqual(logClass, LoggerName);
            Assert.AreEqual(log4net.Core.Level.Warn, LogLevel);
            Assert.AreEqual(taskId, TheReturnPact.TaskId);
            Assert.AreEqual(message, TheReturnPact.MessageDetails);
            Assert.AreEqual(MsgType.mtLog, TheReturnPact.MessageType);
        }


        private static MessagePact TheReturnPact { get; set; }
        private static log4net.Core.Level LogLevel { get; set; }
        private static string LoggerName { get; set; }

        /// <summary>
        /// It is a listener
        /// </summary>
        /// <param name="sender">a sender</param>
        /// <param name="e">a packed message</param>
        private static void LogListener(object sender, LogEventArgs e)
        {
            LogLevel = e.LogEvent.Level;
            LoggerName = e.LogEvent.LoggerName;
            TheReturnPact = (MessagePact)e.LogEvent.MessageObject;
        }
    }

    internal class ReportManager
    {
        public string Caption { get; set; }
        public List<FileVersion> Versons { get; set; }
    }

    internal class FileVersion
    {
        public string Name { get; set; }
        public string Version { get; set; }
    }
}
