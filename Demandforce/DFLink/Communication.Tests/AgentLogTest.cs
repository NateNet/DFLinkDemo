using Microsoft.VisualStudio.TestTools.UnitTesting;
using Demandforce.DFLink.Logger;

namespace Demandforce.DFLink.Communication.Tests
{
    /// <summary>
    ///This is a test class for AgentLogTest and is intended
    ///to contain all AgentLogTest Unit Tests
    ///</summary>
    [TestClass()]
    [DeploymentItem("log4net.Setting.xml")]
    [DeploymentItem("ServerSet.xml")]
    public class AgentLogTest
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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            string logSettingName = System.Reflection.Assembly.GetExecutingAssembly().Location;
            logSettingName = System.IO.Path.GetDirectoryName(logSettingName) + @"\log4net.Setting.xml";
            LogInit.InitLog(logSettingName);
            AgentSetting.InitialSetting();
            //AgentSetting.CallerFactory = new TestCallerFactory();
            
        }
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
        ///A test for GetLog
        ///</summary>
        [TestMethod()]
        public void GetLogTest()
        {
            AgentLog target = AgentLog.GetStartedInstance(); // TODO: Initialize to an appropriate value
            int taskId = 1; // TODO: Initialize to an appropriate value
            target.GetLog(taskId);
            Assert.AreEqual(AgentSetting.AddressUrl + AgentSetting.CommandLogDownload, TestCallerFactory.UrlString);
            Assert.AreEqual(@"{""TaskId"":0,""BusinessCredentials"":{""LicenseKey"":""xxxxx-xxxxxx""}}", TestCallerFactory.JsonString);
        }
    } 
}
