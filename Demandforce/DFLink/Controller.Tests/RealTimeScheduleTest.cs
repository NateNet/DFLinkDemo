// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RealTimeScheduleTest.cs" company="">
//  TODO: Update copyright text. 
// </copyright>
// <summary>
//   This is a test class for RealTimeScheduleTest and is intended
//   to contain all RealTimeScheduleTest Unit Tests
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Controller.Tests
{

    using System;
    using System.Collections.Generic;

    using Demandforce.DFLink.Controller.Schedule;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for RealTimeScheduleTest and is intended
    /// to contain all RealTimeScheduleTest Unit Tests
    /// </summary>
    [TestClass()]
    public class RealTimeScheduleTest
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
        /// A test for GetRunTimes
        /// </summary>
        [TestMethod()]
        public void GetRunTimesFirstReturnTimeifMultiCall()
        {
            var target = new RealTimeSchedule(); 
            var startTimes = new List<DateTime>
                                            {
                                new DateTime(2014, 10, 10, 10, 55, 49),
                                new DateTime(2014, 10, 10, 10, 56, 49),
                                new DateTime(2014, 10, 10, 10, 59, 50),
                                new DateTime(2014, 10, 10, 11, 01, 50)
                            };
            var endTimes = new List<DateTime>
                                            {
                                new DateTime(2014, 10, 10, 10, 56, 49),
                                new DateTime(2014, 10, 10, 10, 57, 49),
                                new DateTime(2014, 10, 10, 11, 00, 50),
                                new DateTime(2014, 10, 10, 11, 02, 50)
                            };
            for (int i = 0; i < 4; i++)
            {
                List<DateTime> expected;
                var actual = target.GetRunTimes(startTimes[i], endTimes[i]);
                if (i == 0)
                {
                    expected = new List<DateTime>
                                    {
                                        new DateTime(2014, 10, 10, 10, 56, 49)
                                    };

                    Assert.AreEqual(expected[0], actual[0]);
                }
                else
                {
                    expected = new List<DateTime>();
                    Assert.AreEqual(expected.Count, actual.Count);
                }
            }
        }

        /// <summary>
        /// A test for GetNextRunTime
        /// </summary>
        [TestMethod()]
        public void GetNextRunTimeSameTimeifMultiCall()
        {
            var target = new RealTimeSchedule();
            var times = new List<DateTime>
                            {
                                new DateTime(2014, 10, 10, 10, 56, 49),
                                new DateTime(2014, 10, 10, 10, 57, 49),
                                new DateTime(2014, 10, 10, 11, 00, 50),
                                new DateTime(2014, 10, 10, 11, 02, 50)
                            };
            foreach (var time in times)
            {
                var expected = new DateTime(2014, 10, 10, 10, 56, 49);
                DateTime actual = target.GetNextRunTime(time, true);
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
