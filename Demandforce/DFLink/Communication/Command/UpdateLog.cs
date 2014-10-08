// -----------------------------------------------------------------------
// <copyright file="UpdateLog.cs" company="Demandforce">
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
    /// TODO: A class to serialize function and get message from the server
    /// </summary>
    public class UpdateLog : BaseCommand
    {
        /// <summary>
        /// Gets or sets a class name
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// Gets or sets a task id
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// Gets or sets a message if
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a time
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Gets or sets the level
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// It is a function which is used for thread pool.
        /// </summary>
        /// <param name="idleParam">a command, no use, set it to null</param>
        public override void Request(object idleParam)
        {
            string jsonStr = JsonPack<UpdateLog>.SerializeObject(this);
            AgentSetting.CallerFactory.CreateCaller().PostCommand(AgentSetting.AddressUrl + AgentSetting.CommandLogUpload, jsonStr);
        }
    }
}
