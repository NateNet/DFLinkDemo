// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DownloadTask.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.  
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication.Command
{
    using Demandforce.DFLink.Common.Configuration;

    /// <summary>
    ///     TODO: It is a model for serialization
    /// </summary>
    public class DownloadTask : BaseCommand
    {
        #region Public Methods and Operators

        /// <summary>
        /// To lookup the task list with a xml string
        /// </summary>
        /// <param name="idleParam">
        /// No use parameter, set it to null
        /// </param>
        public override void Request(object idleParam)
        {
            string jsonStr = JsonPack<BusinessInfo>.SerializeObject(this.BusinessCredentials);
            string result = this.Caller.PostCommand(this.ServerSettings.AddressUrl + this.ServerSettings.CommandTaskGet, jsonStr);
            this.SetTheCallResult(result);
        }

        #endregion
    }
}