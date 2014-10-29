using Demandforce.DFLink.MaintenanceTaskLib.FileMaintenance;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MaintenanceTaskLib.Tests
{
    
    
    /// <summary>
    ///This is a test class for FileUploadOperationHandlerTest and is intended
    ///to contain all FileUploadOperationHandlerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FileUploadOperationHandlerTest
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
        ///A test for FileUploadOperationHandler Constructor
        ///</summary>
        [TestMethod()]
        public void FileUploadOperationHandlerConstructorTest()
        {
            FileUploadOperationHandler target = new FileUploadOperationHandler();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Handle
        ///</summary>
        [TestMethod()]
        public void HandleTest()
        {
            FileUploadOperationHandler target = new FileUploadOperationHandler(); // TODO: Initialize to an appropriate value
            FileOperation operation = null; // TODO: Initialize to an appropriate value
            target.Handle(operation);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
