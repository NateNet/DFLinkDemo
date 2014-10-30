// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateStatus.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   TODO: A class to serialize function and get message from the server
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication.Command
{
    using System;

    /// <summary>
    ///     TODO: A class to serialize function and get message from the server
    /// </summary>
    public class UpdateStatus : BaseCommand
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the class id
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        ///     Gets or sets the message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Gets or sets the status
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     Gets or sets the task id
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        ///     Gets or sets the time
        /// </summary>
        public DateTime Time { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// It is a function which is used for thread pool.
        /// </summary>
        /// <param name="idleParam">
        /// a command, no use, set it to null
        /// </param>
        public override void Request(object idleParam)
        {
            string jsonStr = JsonPack<UpdateStatus>.SerializeObject(this);
            string result =
                AgentSetting.CallerFactory.CreateCaller()
                    .PostCommand(AgentSetting.AddressUrl + AgentSetting.CommandLogStatusUpdate, jsonStr);
        }

        #endregion
    }
}