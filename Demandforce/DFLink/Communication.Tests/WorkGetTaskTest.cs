// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkGetTaskTest.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The work get task test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.Tests
{
    using Demandforce.DFLink.Common.Configuration;
    using Demandforce.DFLink.Communication.Model;
    using Demandforce.DFLink.Communication.WebAPI;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    /// <summary>
    /// The work get task test.
    /// </summary>
    [TestClass]
    public class WorkGetTaskTest
    {
        #region Fields

        /// <summary>
        /// The test context instance.
        /// </summary>
        private TestContext testContextInstance;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the test context.
        /// </summary>
        public TestContext TestContext { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Get Task Test
        /// </summary>
        [TestMethod]
        public void GetTaskTest()
        {
            var target = new WorkGetTask();
            target.Call = new HttpCall();
            string actual = target.GetTask();
            Assert.IsTrue(actual.Contains("<Tasks>"));
            Assert.IsTrue(actual.Contains("</Tasks>"));
        }

        /// <summary>
        ///     Get Task Test by using MOQ
        /// </summary>
        [TestMethod]
        public void GetTaskTest2()
        {
            IServerSettings serverSettings = new ServerSettings(new XmlSettings());

            var target = new WorkGetTask();
            var mockObject = new Mock<ICall>();
            mockObject.Setup(
                p => p.DoWork(serverSettings.AddressUrl + serverSettings.CommandTaskGet, It.IsAny<ISerializable>()))
                .Returns("Called");
            target.Call = mockObject.Object;

            string actual = target.GetTask();
            Assert.Equals("Called", actual);
        }

        #endregion
    }
}