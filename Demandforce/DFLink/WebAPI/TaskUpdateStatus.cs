// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateStatus.cs" company="">
//   
// </copyright>
// <summary>
//   The update status.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.Web
{
    using Demandforce.DFLink.Common;
    using Demandforce.DFLink.Communication.Dao;

    /// <summary>
    /// The update status.
    /// </summary>
    public class TaskUpdateStatus : IWork
    {
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
        public void DoWork(int taskId, int status, string details)
        {
            var jsonLicense = new License { LicenseKey = AgentSetting.LicenseId };
            var jsonStatus = new Status
                                 {
                                     BusinessCredentials = jsonLicense, 
                                     TaskId = taskId, 
                                     Status = status, 
                                     Message = details
                                 };
            string jsonString = JsonUtils.SerializeObject(jsonStatus);
            HttpUtils.PostJsonForXml(AgentSetting.AddressUrl + AgentSetting.CommandLogStatusUpdate, jsonString);
        }

        #endregion
    }
}