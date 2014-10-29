using Demandforce.DFLink.MaintenanceTaskLib.FileMaintenance;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Demandforce.DFLink.MaintenanceTaskLib;

namespace MaintenanceTaskLib.Tests
{
    
    
    /// <summary>
    ///This is a test class for FileOperationHandlerFactoryTest and is intended
    ///to contain all FileOperationHandlerFactoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FileOperationHandlerFactoryTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

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
        ///A test for FileOperationHandlerFactory Constructor
        ///</summary>
        [TestMethod()]
        public void FileOperationHandlerFactoryConstructorTest()
        {
            FileOperationHandlerFactory target = new FileOperationHandlerFactory();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for CreateHandler
        ///</summary>
        [TestMethod()]
        public void CreateHandlerTest()
        {
            FileOperationHandlerFactory target = new FileOperationHandlerFactory(); // TODO: Initialize to an appropriate value
            string handlerName = string.Empty; // TODO: Initialize to an appropriate value
            IOperationHandler<FileOperation> expected = null; // TODO: Initialize to an appropriate value
            IOperationHandler<FileOperation> actual;
            actual = target.CreateHandler(handlerName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
