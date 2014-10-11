// -----------------------------------------------------------------------
// <copyright file="MessagePact.cs" company="Demanforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Logger.Listener
{
    /// <summary>
    /// It is a message type
    /// </summary>
    public enum MsgType
    {
        /// <summary>
        /// undefined value
        /// </summary>
        mtNull,

        /// <summary>
        /// It is a common log
        /// </summary>
        mtLog,

        /// <summary>
        /// It is a status log
        /// </summary>
        mtStatus
    }

    /// <summary>
    /// TODO: It is a pact for log transfer.
    /// </summary>
    public class MessagePact
    {
        /// <summary>
        /// Gets or sets Message's type
        /// </summary>
        public MsgType MessageType { get; set; }

        /// <summary>
        /// Gets or sets Id of task
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// Gets or sets log status, if message's type = mtStatus, this attribute is available
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets details
        /// </summary>
        public string MessageDetails { get; set; }

        /// <summary>
        /// override the base function
        /// </summary>
        /// <returns>The return</returns>
        public override string ToString()
        {
            return this.MessageDetails;
        }
    }
}
