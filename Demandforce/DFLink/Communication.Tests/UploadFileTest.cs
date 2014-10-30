// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UploadFileTest.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   This is a test class for UploadFileTest and is intended
//   to contain all UploadFileTest Unit Tests
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.Tests
{
    using System.IO;
    using System.Reflection;

    using Demandforce.DFLink.Communication.Command;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     This is a test class for UploadFileTest and is intended
    ///     to contain all UploadFileTest Unit Tests
    /// </summary>
    [TestClass]
    public class UploadFileTest
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
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext)
        // {
        // }
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
        #region Public Methods and Operators

        /// <summary>
        ///     A test for ReadFile
        /// </summary>
        [TestMethod]
        public void ReadFileTest()
        {
            var target = new UploadFile(); // TODO: Initialize to an appropriate value
            string logSettingName = Assembly.GetExecutingAssembly().Location;
            logSettingName = Path.GetDirectoryName(logSettingName) + @"\ServerSet.xml";
            target.ReadFile(logSettingName);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        #endregion
    }
}