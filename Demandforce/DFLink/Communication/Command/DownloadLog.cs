// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DownloadLog.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.  
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication.Command
{
    /// <summary>
    ///     TODO: It is a model for serialization
    /// </summary>
    public class DownloadLog : BaseCommand
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the task id
        /// </summary>
        public int TaskId { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// To download the task log with a string format
        /// </summary>
        /// <param name="idleParam">
        /// not use parameters, set it to null
        /// </param>
        public override void Request(object idleParam)
        {
            string jsonStr = JsonPack<DownloadLog>.SerializeObject(this);
            string result =
                AgentSetting.CallerFactory.CreateCaller()
                    .PostCommand(AgentSetting.AddressUrl + AgentSetting.CommandLogDownload, jsonStr);
            this.SetTheCallResult(result);
        }

        #endregion
    }
}