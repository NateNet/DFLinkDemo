// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetTask.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The get task.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication.Web
{
    using Demandforce.DFLink.Common;
    using Demandforce.DFLink.Communication.Dao;

    /// <summary>
    ///     The get task.
    /// </summary>
    public class TaskGetTask : IWork
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The do work.
        /// </summary>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public string DoWork()
        {
            var jsonLicense = new License { LicenseKey = AgentSetting.LicenseId };
            string jsonString = JsonUtils.SerializeObject(jsonLicense);
            string result = HttpUtils.PostJsonForXml(AgentSetting.AddressUrl + AgentSetting.CommandTaskGet, jsonString);
            return result;
        }

        #endregion
    }
}