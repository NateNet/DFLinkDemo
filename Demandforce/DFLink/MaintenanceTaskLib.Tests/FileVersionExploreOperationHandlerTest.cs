// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileVersionExploreOperationHandlerTest.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   This is a test class for FileVersionExploreOperationHandlerTest and is intended
//   to contain all FileVersionExploreOperationHandlerTest Unit Tests
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MaintenanceTaskLib.Tests
{
    using System;
    using System.Reflection;

    using Demandforce.DFLink.MaintenanceTaskLib.FileMaintenance;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     This is a test class for FileVersionExploreOperationHandlerTest and is intended
    ///     to contain all FileVersionExploreOperationHandlerTest Unit Tests
    /// </summary>
    [TestClass]
    public class FileVersionExploreOperationHandlerTest
    {
        /// <summary>
        /// The file operation without file name.
        /// </summary>
        private FileOperation fileOperationWithoutFileName;

        /// <summary>
        /// The file operation.
        /// </summary>
        private FileOperation fileOperation;

        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes

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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            this.fileOperationWithoutFileName = 
                new FileOperation()
                    {
                        Directory = 
                        AppDomain.CurrentDomain.BaseDirectory,
                    };
            this.fileOperation = 
                new FileOperation()
                    {
                        Directory = AppDomain.CurrentDomain.BaseDirectory,
                        FileName = 
                        Assembly.GetExecutingAssembly().GetName().Name + ".dll"
                    };
        }

        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup()
        // {
        // }
        #endregion

        /// <summary>
        ///     A test for Handle operation without File name
        /// </summary>
        [TestMethod]
        public void HandleWithoutFileName()
        {
            var target = new FileVersionExploreOperationHandler();
            FileOperation operation = this.fileOperationWithoutFileName;
            target.Handle(operation);
            Assert.IsNotNull(operation.Result);
        }

        /// <summary>
        ///     A test for Handle operation with directory and file name
        /// </summary>
        [TestMethod]
        public void HandleWithtDirectoryAndFileName()
        {
            var target = new FileVersionExploreOperationHandler();
            FileOperation operation = this.fileOperation;
            target.Handle(operation);
            Assert.IsNotNull(operation.Result);
        }
    }
}
