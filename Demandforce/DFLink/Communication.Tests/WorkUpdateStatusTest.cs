// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkUpdateStatusTest.cs" company="">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The work update status test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.Tests
{
    using System;

    using Demandforce.DFLink.Common;
    using Demandforce.DFLink.Common.Configuration;
    using Demandforce.DFLink.Communication.Model;
    using Demandforce.DFLink.Communication.WebAPI;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The work update status test.
    /// </summary>
    [TestClass]
    public class WorkUpdateStatusTest
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
        /// The update status test.
        /// </summary>
        [TestMethod]
        public void UpdateStatusTest()
        {
            IServerSettings serverSettings = new ServerSettings(new XmlSettings());

            var target = new WorkUpdateStatus();
            string url = string.Empty;
            string param = string.Empty;

            target.Call = new TestCall(
                (p1, p2) =>
                    {
                        url = p1;
                        param = p2;
                        return string.Empty;
                    });

            int taskId = 1234;
            int status = 7;
            string details = "detailssss";
            target.UpdateStatus(taskId, status, details);

            Assert.AreEqual(url, serverSettings.AddressUrl + serverSettings.CommandLogStatusUpdate);

            var model = JsonUtils.UnSerializeObject<StatusModel>(param);
            Assert.AreEqual(model.Status, status);
            Assert.AreEqual(model.TaskId, taskId);
            Assert.AreEqual(model.Message, details);
        }

        #endregion
    }
}