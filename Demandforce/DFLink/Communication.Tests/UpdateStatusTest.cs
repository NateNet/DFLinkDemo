using Demandforce.DFLink.Communication.Command;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Demandforce.DFLink.Communication.Tests
{


    /// <summary>
    ///This is a test class for UpdateStatusTest and is intended
    ///to contain all UpdateStatusTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UpdateStatusTest
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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            AgentSetting.InitialSetting();
        }
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
        ///A test for Request
        ///</summary>
        [TestMethod()]
        public void RequestTest()
        {

            ReportManager details = new ReportManager() { Caption = "A report", Versons = new List<FileVersion>() };
            details.Versons.Add(new FileVersion() { Name = "DFLink.exe", Version = "3.4.192" });
            details.Versons.Add(new FileVersion() { Name = "d3123api.dll", Version = "3.0.12" });
            details.Versons.Add(new FileVersion() { Name = "d3cotapi.dll", Version = "3.0.35" });

            UpdateStatus target = new UpdateStatus(); // TODO: Initialize to an appropriate value
            target.ClassName = "Communication.Test";
            target.BusinessCredentials = new BusinessInfo() { LicenseKey = "61D13359-C3A6-301C-8B17-9283A2188A9A" };
            target.Message = JsonConvert.SerializeObject(details);
            target.Status = 9;
            target.TaskId = 3;
            target.Time = DateTime.Now;
            object idleParam = null; // TODO: Initialize to an appropriate value
            target.Request(idleParam);
        }
    }

    internal class ReportManager
    {
        public string Caption { get; set; }
        public List<FileVersion> Versons { get; set; }
    }

    internal class FileVersion
    {
        public string Name { get; set; }
        public string Version { get; set; }
    }
}
