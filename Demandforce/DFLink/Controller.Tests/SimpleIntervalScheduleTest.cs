// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleIntervalScheduleTest.cs" company="Demandforce">
//  TODO: Update copyright text. 
// </copyright>
// <summary>
//   This is a test class for SimpleIntervalScheduleTest and is intended
//   to contain all SimpleIntervalScheduleTest Unit Tests
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Controller.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Globalization;
    using System.Linq;

    using Demandforce.DFLink.Controller;
    using Demandforce.DFLink.Controller.Schedule;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     This is a test class for SimpleIntervalScheduleTest and is intended
    ///     to contain all SimpleIntervalScheduleTest Unit Tests
    /// </summary>
    [TestClass]
    public class SimpleIntervalScheduleTest
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
        ///     A test for GetNextRunTime
        /// </summary>
        [DataSource(@"Provider=SQLOLEDB;
                    Data Source=172.18.3.100;
                    Initial Catalog=DFLinkTest;
                    User ID=nate;Password=ondemand;", 
                    "SimpleIntervalScheduleTestData")]
        [TestMethod]
        public void GetNextRunTimeTest()
        {
            var target = new SimpleIntervalSchedule()
                             {
                                 StartTime =
                                     Convert.ToDateTime(
                                         TestContext.DataRow["ScheduleStartTime"]),
                                 Interval =
                                     new TimeSpan(
                                     0,
                                     Convert.ToInt32(TestContext.DataRow["Interval"]),
                                     0),
                                 EndTime = DateTime.MaxValue
                             };
            var time = Convert.ToDateTime(TestContext.DataRow["StartTime"]);
            bool includeCurrentTime = 
                Convert.ToBoolean(TestContext.DataRow["IncludeStartTime"]);
            var expected = Convert.ToDateTime(TestContext.DataRow["ExpectedTime"]); 
            DateTime actual;
            actual = target.GetNextRunTime(time, includeCurrentTime);
            Assert.AreEqual(expected, actual);

           // Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///     A test for GetRunTimes
        /// </summary>
        [DataSource(@"Provider=SQLOLEDB;
                    Data Source=172.18.3.100;
                    Initial Catalog=DFLinkTest;
                    User ID=nate;Password=ondemand;",
                    "SimpleIntervalScheduleRunTimesTestData")]        
        [TestMethod]
        public void GetRunTimesTest()
        {
            var target = new SimpleIntervalSchedule()
                             {
                                 StartTime =
                                     Convert.ToDateTime(
                                         TestContext.DataRow["ScheduleStartTime"]),
                                 Interval =
                                     new TimeSpan(
                                     0,
                                     Convert.ToInt32(TestContext.DataRow["Interval"]),
                                     0),
                                 EndTime = DateTime.MaxValue
                             };
            var startTime = Convert.ToDateTime(TestContext.DataRow["StartTime"]);
            var endTime = Convert.ToDateTime(TestContext.DataRow["EndTime"]);
            string expectedString = TestContext.DataRow["ExpectedTimes"].ToString();
            var expectedList = expectedString.Split(',').ToList();
            var expected = expectedList.Select(Convert.ToDateTime).ToList();

            var actual = target.GetRunTimes(startTime, endTime);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        #endregion

        /// <summary>
        /// A test for SimpleIntervalSchedule Constructor
        /// </summary>
        [DataSource(@"Provider=SQLOLEDB;
            Data Source=172.18.3.100;
            Initial Catalog=DFLinkTest;
            User ID=nate;Password=ondemand;",
            "SimpleIntervalScheduleConstructorTestData")]
        [TestMethod()]
        public void SimpleIntervalScheduleConstructorTest()
        {
            DateTime startTime = 
                Convert.ToDateTime(TestContext.DataRow["StartTime"]);
            TimeSpan interval = 
                new TimeSpan(
                0, 
                Convert.ToInt32(TestContext.DataRow["Interval"]), 
                0); 
            int count = Convert.ToInt32(TestContext.DataRow["Count"]); 
            SimpleIntervalSchedule target = new SimpleIntervalSchedule();
            DateTime actual = target.EndTime;
            DateTime expected = Convert.ToDateTime(TestContext.DataRow["ExpectedEndTime"]);

            // as the DateTime.MaxValue precision, use string comparision
            Assert.AreEqual<string>(
                actual.ToString(CultureInfo.InvariantCulture),
                expected.ToString(CultureInfo.InvariantCulture));

            // Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}