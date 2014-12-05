// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AgentUploadFileTest.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   This is a test class for AgentUploadFileTest and is intended
//   to contain all AgentUploadFileTest Unit Tests
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.Tests
{
    using System.IO;
    using System.Reflection;

    using Demandforce.DFLink.Common.Configuration;
    using Demandforce.DFLink.Logger;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     This is a test class for AgentUploadFileTest and is intended
    ///     to contain all AgentUploadFileTest Unit Tests
    /// </summary>
    [TestClass]
    [DeploymentItem("log4net.Setting.xml")]
    [DeploymentItem("ServerSet.xml")]
    public class AgentUploadFileTest
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
        #region Public Methods and Operators

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
        }

        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup()
        // {
        // }
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
        ///     A test for UploadFile
        /// </summary>
        [TestMethod]
        public void UploadFileTest()
        {
            AgentUploadFile target = new AgentUploadFile();
            string settingFile = Assembly.GetExecutingAssembly().Location;
            settingFile = Path.GetDirectoryName(settingFile) + @"\app.xml";
            target.UploadFile(4, settingFile);
        }

        #endregion
    }
}