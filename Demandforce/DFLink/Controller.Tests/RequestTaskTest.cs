// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestTaskTest.cs" company="Demandforce">
//  TO DO: Update copyright text. 
// </copyright>
// <summary>
//   This is a test class for RequestTaskTest and is intended
//   to contain all RequestTaskTest Unit Tests
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Controller.Tests
{
    using System;

    using Demandforce.DFLink.Controller;
    using Demandforce.DFLink.Controller.Schedule;
    using Demandforce.DFLink.Controller.Task;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    /// <summary>
    /// This is a test class for RequestTaskTest and is intended
    /// to contain all RequestTaskTest Unit Tests
    /// </summary>
    [TestClass()]
    public class RequestTaskTest
    {
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
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
        // [TestInitialize()]
        // public void MyTestInitialize()
        // {
        // }
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup()
        // {
        // }
        #endregion

        /// <summary>
        /// A test for Execute
        /// </summary>
        [TestMethod()]
        public void ExecuteTest()
        {
            // mock task manager
            var mockTaskManager = new Moq.Mock<ITaskManager>();
            mockTaskManager.Setup(manager => manager.RequestTasks()).Returns("xml");
            var target = new RequestTask();
            target.TaskManager = mockTaskManager.Object;
            target.Execute();
            mockTaskManager.Verify(manager => manager.ParseTasks("xml"), Times.Once);
        }
    }
}
