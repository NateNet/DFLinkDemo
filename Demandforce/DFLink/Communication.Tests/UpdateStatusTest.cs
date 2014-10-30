// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateStatusTest.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   This is a test class for UpdateStatusTest and is intended
//   to contain all UpdateStatusTest Unit Tests
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication.Tests
{
    using System;
    using System.Collections.Generic;

    using Demandforce.DFLink.Communication.Command;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Newtonsoft.Json;

    /// <summary>
    ///     This is a test class for UpdateStatusTest and is intended
    ///     to contain all UpdateStatusTest Unit Tests
    /// </summary>
    [TestClass]
    public class UpdateStatusTest
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
            AgentSetting.InitialSetting();
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
        ///     A test for Request
        /// </summary>
        [TestMethod]
        public void RequestTest()
        {
            var details = new ReportManager { Caption = "A report", Versions = new List<FileVersion>() };
            details.Versions.Add(new FileVersion { Name = "DFLink.exe", Version = "3.4.192" });
            details.Versions.Add(new FileVersion { Name = "d3123api.dll", Version = "3.0.12" });
            details.Versions.Add(new FileVersion { Name = "d3cotapi.dll", Version = "3.0.35" });

            var target = new UpdateStatus(); // TODO: Initialize to an appropriate value
            target.ClassName = "Communication.Test";
            target.BusinessCredentials = new BusinessInfo { LicenseKey = "61D13359-C3A6-301C-8B17-9283A2188A9A" };
            target.Message = JsonConvert.SerializeObject(details);
            target.Status = 9;
            target.TaskId = 3;
            target.Time = DateTime.Now;
            object idleParam = null; // TODO: Initialize to an appropriate value
            target.Request(idleParam);
        }

        #endregion

        /// <summary>
        ///     The file version.
        /// </summary>
        internal class FileVersion
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
        internal class ReportManager
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