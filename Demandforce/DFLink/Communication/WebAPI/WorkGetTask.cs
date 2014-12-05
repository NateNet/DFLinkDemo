// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkGetTask.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The get task.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication.WebAPI
{
    using Demandforce.DFLink.Common.Configuration;
    using Demandforce.DFLink.Communication.Model;

    /// <summary>
    ///     The get task.
    /// </summary>
    public class WorkGetTask : IGetTask
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
        ///     The do work.
        /// </summary>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public string GetTask()
        {
            var licenseModel = new LicenseModel { LicenseKey = this.serverSetting.LicenseId };
            return this.Call.DoWork(this.serverSetting.AddressUrl + this.serverSetting.CommandTaskGet, licenseModel);
        }

        #endregion
    }
}