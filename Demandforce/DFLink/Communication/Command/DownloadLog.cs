// -----------------------------------------------------------------------
// <copyright file="DownloadLog.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.Command
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using WebAPI;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DownloadLog : BaseCommand
    {
        /// <summary>
        /// Gets or sets the task id
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// To download the task log with a string format
        /// </summary>
        /// <param name="idleParam">not use parameters, set it to null</param>
        public override void Request(object idleParam)
        {
            string jsonStr = JsonPack<DownloadLog>.SerializeObject(this);
            string result = AgentSetting.CallerFactory.CreateCaller().PostCommand(AgentSetting.AddressUrl + AgentSetting.CommandLogDownload, jsonStr);
            this.SetTheCallResult(result);
        }
    }
}
