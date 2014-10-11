using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using Demandforce.DFLink.Logger.Listener;

namespace Demandforce.DFLink.Logger.Tests
{
    /// <summary>
    ///This is a test class for LogInitTest and is intended
    ///to contain all LogInitTest Unit Tests
    ///</summary>
    [TestClass()]
    [DeploymentItem("log4net.Setting.xml")]
    public class LogInitTest
    {


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
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
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
        ///A test for InitLog
        ///</summary>
        [TestMethod()]
        public void InitLogTest()
        {
            string logSettingName = System.Reflection.Assembly.GetExecutingAssembly().Location;
            logSettingName = System.IO.Path.GetDirectoryName(logSettingName) + @"\log4net.Setting.xml";
            LogInit.InitLog(logSettingName);
            AppenderListener.EventLogListen += new EventHandler<LogEventArgs>(LogListener);
            
            string className = "abcde";
            int taskId = 12345;
            string message = "a message";
            _worked = false;
            LogHelper.GetLoggerHandle().Debug(className, taskId, message);
            Thread.Sleep(200);
            Assert.AreEqual(_worked, true);
            AppenderListener.EventLogListen -= new EventHandler<LogEventArgs>(LogListener);
        }

        /// <summary>
        /// To record if the logger works
        /// </summary>
        private static bool _worked = false;

        /// <summary>
        /// It is a listener
        /// </summary>
        /// <param name="sender">a sender</param>
        /// <param name="e">a packed message</param>
        private static void LogListener(object sender, LogEventArgs e)
        {
            _worked = true;
        }

    }
}
