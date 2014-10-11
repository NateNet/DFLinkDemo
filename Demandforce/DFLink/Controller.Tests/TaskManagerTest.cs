// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskManagerTest.cs" company="Demandforce">
//  TO DO: Update copyright text. 
// </copyright>
// <summary>
//   This is a test class for RequestTaskTest and is intended
//   to contain all RequestTaskTest Unit Tests
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Controller.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using Demandforce.DFLink.Controller;
    using Demandforce.DFLink.Controller.Task;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    /// <summary>
    /// This is a test class for TaskManagerTest and is intended
    /// to contain all TaskManagerTest Unit Tests
    /// </summary>
    [TestClass()]
    public class TaskManagerTest
    {
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// A test for ParseTasks
        /// </summary>
        [TestMethod()]
        public void ParseTasksCreateNewTask()
        {
            // mock the dependency
            var mockTaskCreator = new Mock<ITaskCreator>();
            var mockUpdateTask = new Moq.Mock<ITask>();
            mockTaskCreator.Setup(ct => ct.Creator(It.IsAny<string>()))
                .Returns(mockUpdateTask.Object);
            var target = new TaskManager(mockTaskCreator.Object);

            // this set is not complete, it ignores the content for TaskCreator.
            const string Taskxml =
                "<Tasks><Task><Action>Create</Action></Task></Tasks>";

            target.ParseTasks(Taskxml);
            mockTaskCreator.Verify(
                m => m.Creator(It.IsAny<string>()), Times.Exactly(1));
            Assert.AreEqual(target.Tasks.Count, 1);
        }

        /// <summary>
        /// A test for ParseTasks
        /// </summary>
        [TestMethod()]
        public void ParseTasksDeleteTask()
        {
            var mockTaskCreator = new Mock<ITaskCreator>();

            // mock a task which Id is 2
            var mockUpdateTask = new Moq.Mock<ITask>();
            mockUpdateTask.SetupGet(x => x.Id).Returns(2);
            mockTaskCreator.Setup(ct => ct.Creator(It.IsAny<string>()))
                .Returns(mockUpdateTask.Object);
            var target = new TaskManager(mockTaskCreator.Object);

            // this set is not complete, it ignores the content for TaskCreator. execute 
            // create first
            var taskxml = 
                "<Tasks><Task><Id>2></Id>" 
                + "<Action>Create</Action></Task></Tasks>";
            target.ParseTasks(taskxml);
            Assert.AreEqual(target.Tasks.Count, 1);

            // this is for test delete action
            taskxml = "<Tasks><Task><Id>2</Id>" 
                + "<Action>Delete</Action></Task></Tasks>";
            target.ParseTasks(taskxml);
            mockTaskCreator.Verify(
                m => m.Creator(It.IsAny<string>()), Times.Exactly(2));
            Assert.AreEqual(target.Tasks.Count, 0); 
        }

        /// <summary>
        /// A test for ParseTasks
        /// </summary>
        [TestMethod()]
        public void ParseTasksCreateAndUpdateTask()
        {
            var mockTaskCreator = new Mock<ITaskCreator>();

            // Mock request task
            var mockRequestTask = new Moq.Mock<ITask>();
            mockRequestTask.SetupGet(x => x.Id).Returns(1);
            mockRequestTask.SetupGet(x => x.Description)
                .Returns("Create Request");

            // Mock Update task
            var mockUpdateTask = new Moq.Mock<ITask>();
            mockUpdateTask.SetupGet(x => x.Id).Returns(2);

            var tasks = new Queue<ITask>(new[]
                                             {
                                                 mockRequestTask.Object, 
                                                 mockUpdateTask.Object
                                             });
            mockTaskCreator.Setup(
                ct => ct.Creator(It.IsAny<string>())).Returns(tasks.Dequeue);
            var target = new TaskManager(mockTaskCreator.Object);

            // create tasks first
            var taskxml = "<Tasks>" 
                          + "<Task><Id>1</Id>"
                          + "<Action>Create</Action></Task>"
                          + "<Task><Id>2</Id>"
                          + "<Action>Create</Action></Task></Tasks>";
            target.ParseTasks(taskxml);
            Assert.AreEqual(target.Tasks.Count, 2);

            //// update one task  
            var mockUpdate = new Moq.Mock<ITask>();
            mockUpdate.SetupGet(x => x.Id).Returns(1);
            mockUpdate.SetupGet(
                x => x.Description).Returns("Update Request");
            mockTaskCreator.Setup(
                ct => ct.Creator(It.IsAny<string>())).Returns(mockUpdate.Object);
            taskxml = "<Tasks>"
                          + "<Task><Id>1</Id>"
                          + "<Action>Update</Action></Task></Tasks>";
            target.ParseTasks(taskxml);
            mockTaskCreator.Verify(
                m => m.Creator(It.IsAny<string>()), Times.Exactly(3));
            Assert.AreEqual(2, target.Tasks.Count);

            // check description was changed of task Id is 1 
            var actual = (
                from dicItem
                    in target.Tasks
                where dicItem.Key == 1
                select dicItem.Value).First().Description;    
            const string Expected = "Update Request";
            Assert.AreEqual(Expected, actual);
        }
    }
}
