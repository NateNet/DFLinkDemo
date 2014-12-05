// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkUpdateStatus.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The update status.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication.WebAPI
{
    using Demandforce.DFLink.Common.Configuration;
    using Demandforce.DFLink.Communication.Model;

    /// <summary>
    ///     The update status.
    /// </summary>
    public class WorkUpdateStatus : IUpdateStatus
    {
        #region Fields

        /// <summary>
        ///     The setting.
        /// </summary>
        private readonly IServerSettings serverSetting = new ServerSettings(new XmlSettings());

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the call.
        /// </summary>
        public ICall Call { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The do work.
        /// </summary>
        /// <param name="taskId">
        /// The task id.
        /// </param>
        /// <param name="status">
        /// The status.
        /// </param>
        /// <param name="details">
        /// The details.
        /// </param>
        public void UpdateStatus(int taskId, int status, string details)
        {
            var licenseModel = new LicenseModel { LicenseKey = this.serverSetting.LicenseId };
            var statusModel = new StatusModel
                                  {
                                      BusinessCredentials = licenseModel, 
                                      TaskId = taskId, 
                                      Status = status, 
                                      Message = details
                                  };
            this.Call.DoWork(this.serverSetting.AddressUrl + this.serverSetting.CommandLogStatusUpdate, statusModel);
        }

        #endregion
    }
}