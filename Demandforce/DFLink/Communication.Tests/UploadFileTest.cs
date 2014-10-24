using Demandforce.DFLink.Communication.Command;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Demandforce.DFLink.Communication.Tests
{


    /// <summary>
    ///This is a test class for UploadFileTest and is intended
    ///to contain all UploadFileTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UploadFileTest
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
        ///A test for ReadFile
        ///</summary>
        [TestMethod()]
        public void ReadFileTest()
        {
            UploadFile target = new UploadFile(); // TODO: Initialize to an appropriate value
            string logSettingName = System.Reflection.Assembly.GetExecutingAssembly().Location;
            logSettingName = System.IO.Path.GetDirectoryName(logSettingName) + @"\ServerSet.xml";
            target.ReadFile(logSettingName);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
