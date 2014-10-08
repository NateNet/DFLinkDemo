
namespace Controller.Tests
{
    using Demandforce.DFLink.Controller;
    using Demandforce.DFLink.Controller.Task;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for TaskFactoryTest and is intended
    /// to contain all TaskFactoryTest Unit Tests
    /// </summary>
    [TestClass()]
    public class TaskFactoryTest
    {
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes

        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
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
        /// A test for CreateTask
        /// </summary>
       [DataSource(@"Provider=SQLOLEDB;
            Data Source=172.18.3.100;
            Initial Catalog=DFLinkTest;
            User ID=nate;Password=ondemand;",
            "CreateTaskTestData")]
        [TestMethod()]
        [DeploymentItem("App.config")]
        [DeploymentItem("Update.dll")]
        public void CreateTaskTest()
        {
            TaskFactory taskFactory = new TaskFactory();
            string taskName = TestContext.DataRow["TaskName"].ToString();
            string expected = TestContext.DataRow["Expected"].ToString();
            ITaskMaker actual = taskFactory.CreateTaskMaker(taskName);
            Assert.AreEqual(expected, ((ITask)actual).Name);
        }

        /// <summary>
        /// A test for GetTypeNameFromConfiguration
        /// </summary>
        [DataSource(@"Provider=SQLOLEDB;
            Data Source=172.18.3.100;
            Initial Catalog=DFLinkTest;
            User ID=nate;Password=ondemand;",
            "GetTypeNameFromConfigurationTestData")]
        [TestMethod()]
        [DeploymentItem("App.config")]
        public void GetTypeNameFromConfigurationTest()
        {
            TaskFactory taskFactory = new TaskFactory();
            string name = TestContext.DataRow["TaskName"].ToString();
            string expected = TestContext.DataRow["TaskType"].ToString();
            string actual = taskFactory.GetTypeNameFromConfiguration(name);
            Assert.AreEqual(expected, actual);
        }
    }
}
