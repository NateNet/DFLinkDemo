// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessagePact.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   It is a message type
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Logger
{
    /// <summary>
    ///     It is a message type
    /// </summary>
    public enum MsgType
    {
        /// <summary>
        ///     undefined value
        /// </summary>
        MtNull, 

        /// <summary>
        ///     It is a common log
        /// </summary>
        MtLog, 

        /// <summary>
        ///     It is a status log
        /// </summary>
        MtStatus
    }

    /// <summary>
    ///     TODO: It is a pact for log transfer.
    /// </summary>
    public class MessagePact
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets details
        /// </summary>
        public string MessageDetails { get; set; }

        /// <summary>
        ///     Gets or sets Message's type
        /// </summary>
        public MsgType MessageType { get; set; }

        /// <summary>
        ///     Gets or sets log status, if message's type = mtStatus, this attribute is available
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     Gets or sets Id of task
        /// </summary>
        public int TaskId { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     override the base function
        /// </summary>
        /// <returns>The return</returns>
        public override string ToString()
        {
            return this.MessageDetails;
        }

        #endregion
    }
}