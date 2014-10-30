// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AgentTaskTest.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   This is a test class for AgentTaskTest and is intended
//   to contain all AgentTaskTest Unit Tests
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.Tests
{
    using System.IO;
    using System.Reflection;

    using Demandforce.DFLink.Communication.WebAPI;
    using Demandforce.DFLink.Logger;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     This is a test class for AgentTaskTest and is intended
    ///     to contain all AgentTaskTest Unit Tests
    /// </summary>
    [TestClass]
    [DeploymentItem("log4net.Setting.xml")]
    [DeploymentItem("ServerSet.xml")]
    public class AgentTaskTest
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        #endregion

        // You can use the following additional attributes as you write your tests:
        // Use ClassInitialize to run code before running the first test in the class

        // Use ClassCleanup to run code after all tests in a class have run
        #region Public Methods and Operators

        /// <summary>
        /// The my class cleanup.
        /// </summary>
        [ClassCleanup]
        public static void MyClassCleanup()
        {
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
            AgentSetting.InitialSetting();
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
        ///     A test for GetTask
        /// </summary>
        [TestMethod]
        public void GetTaskTest()
        {
            ICallerFactory original = AgentSetting.CallerFactory;
            AgentSetting.CallerFactory = new TestCallerFactory();

            AgentTask target = AgentTask.GetStartedInstance(); // TODO: Initialize to an appropriate value
            target.GetTask();
            AgentSetting.CallerFactory = original;

            Assert.AreEqual(AgentSetting.AddressUrl + AgentSetting.CommandTaskGet, TestCallerFactory.UrlString);
            Assert.AreEqual(@"{""LicenseKey"":""xxxxx-xxxxxx""}", TestCallerFactory.JsonString);
        }

        /// <summary>
        ///     A test by using server
        /// </summary>
        [TestMethod]
        public void GetTaskTestFromServer()
        {
            AgentTask target = AgentTask.GetStartedInstance(); // TODO: Initialize to an appropriate value
            string xmlStr = target.GetTask();
            Assert.IsTrue(xmlStr != null);
        }

        #endregion
    }
}