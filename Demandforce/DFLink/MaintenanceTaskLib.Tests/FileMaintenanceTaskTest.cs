// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileMaintenanceTaskTest.cs" company="Demandforce">
//  TO do:
// </copyright>
// <summary>
//   This is a test class for FileMaintenanceTaskTest and is intended
//   to contain all FileMaintenanceTaskTest Unit Tests
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MaintenanceTaskLib.Tests
{
    #region

    using Demandforce.DFLink.MaintenanceTaskLib.FileMaintenance;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    #endregion

    /// <summary>
    ///     This is a test class for FileMaintenanceTaskTest and is intended
    ///     to contain all FileMaintenanceTaskTest Unit Tests
    /// </summary>
    [TestClass]
    public class FileMaintenanceTaskTest
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
        ///     A test for Execute
        /// </summary>
        [TestMethod]
        public void ExecuteTest()
        {
            FileMaintenanceTask target = new FileMaintenanceTask();
            target.Execute();
        }

        #endregion

        /// <summary>
        ///A test for FileMaintenanceTask Constructor
        ///</summary>
        [TestMethod()]
        public void FileMaintenanceTaskConstructorTest()
        {
            FileMaintenanceTask target = new FileMaintenanceTask();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Execute
        ///</summary>
        [TestMethod()]
        public void ExecuteTest1()
        {
            FileMaintenanceTask target = new FileMaintenanceTask(); // TODO: Initialize to an appropriate value
            target.Execute();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for MakeTask
        ///</summary>
        [TestMethod()]
        public void MakeTaskTest()
        {
            //FileMaintenanceTask target = new FileMaintenanceTask(); // TODO: Initialize to an appropriate value
            //string arguments = string.Empty; // TODO: Initialize to an appropriate value
            //ITask expected = null; // TODO: Initialize to an appropriate value
            //ITask actual;
            //actual = target.MakeTask(arguments);
            //Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Description
        ///</summary>
        [TestMethod()]
        public void DescriptionTest()
        {
            FileMaintenanceTask target = new FileMaintenanceTask(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Description = expected;
            actual = target.Description;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FileOperation
        ///</summary>
        [TestMethod()]
        public void FileOperationTest()
        {
            FileMaintenanceTask target = new FileMaintenanceTask(); // TODO: Initialize to an appropriate value
            FileOperation expected = null; // TODO: Initialize to an appropriate value
            FileOperation actual;
            target.FileOperation = expected;
            actual = target.FileOperation;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FileOperationHandler
        ///</summary>
        [TestMethod()]
        public void FileOperationHandlerTest()
        {
            //FileMaintenanceTask target = new FileMaintenanceTask(); // TODO: Initialize to an appropriate value
            //IOperationHandler<FileOperation> expected = null; // TODO: Initialize to an appropriate value
            //IOperationHandler<FileOperation> actual;
            //target.FileOperationHandler = expected;
            //actual = target.FileOperationHandler;
            //Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Id
        ///</summary>
        [TestMethod()]
        public void IdTest()
        {
            FileMaintenanceTask target = new FileMaintenanceTask(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.Id = expected;
            actual = target.Id;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Name
        ///</summary>
        [TestMethod()]
        public void NameTest()
        {
            FileMaintenanceTask target = new FileMaintenanceTask(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.Name;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Schedule
        ///</summary>
        [TestMethod()]
        public void ScheduleTest()
        {
            //FileMaintenanceTask target = new FileMaintenanceTask(); // TODO: Initialize to an appropriate value
            //ISchedule expected = null; // TODO: Initialize to an appropriate value
            //ISchedule actual;
            //target.Schedule = expected;
            //actual = target.Schedule;
            //Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}