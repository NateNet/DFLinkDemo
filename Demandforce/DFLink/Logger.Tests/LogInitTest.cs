// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogInitTest.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   This is a test class for LogInitTest and is intended
//   to contain all LogInitTest Unit Tests
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Logger.Tests
{
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     This is a test class is intended
    ///     to contain all the Unit Tests
    /// </summary>
    [TestClass]
    [DeploymentItem("log4net.Setting.xml")]
    public class LogInitTest
    {
        #region Static Fields

        /// <summary>
        ///     To record if the logger works
        /// </summary>
        private static bool worked;

        #endregion

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

    }
}