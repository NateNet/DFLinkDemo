using Demandforce.DFLink.Communication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Demandforce.DFLink.Logger;

namespace Demandforce.DFLink.Communication.Tests
{
    
    
    /// <summary>
    ///This is a test class for AgentTaskTest and is intended
    ///to contain all AgentTaskTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AgentTaskTest
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
            string logSettingName = @"C:\workspace\dflinkreload\DFLinkPrototype\Dll\log4net.xml";
            LogInit.InitLog(logSettingName);
            AgentSetting.AddressUrl = @"http://172.18.3.100";
            AgentSetting.LicenseId = @"xxxxx-xxxxxx";
            AgentSetting.HookLogger();
            AgentSetting.CallerFactory = new TestCallerFactory();
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
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
        ///A test for GetTask
        ///</summary>
        [TestMethod()]
        public void GetTaskTest()
        {

            AgentTask target = AgentTask.GetStartedInstance(); // TODO: Initialize to an appropriate value
            target.GetTask();
            Assert.AreEqual(AgentSetting.AddressUrl + AgentSetting.CommandTaskGet, TestCallerFactory.UrlString);
            Assert.AreEqual(@"{""LicenseKey"":""xxxxx-xxxxxx""}", TestCallerFactory.JsonString);
        }
    }
}
