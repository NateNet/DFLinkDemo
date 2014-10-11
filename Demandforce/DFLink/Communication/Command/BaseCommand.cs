// -----------------------------------------------------------------------
// <copyright file="BaseCommand.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.Command
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class BaseCommand
    {
        /// <summary>
        /// to store the result form the web API
        /// </summary>
        private string callResult = string.Empty;

        /// <summary>
        /// Gets or sets the business credential information
        /// </summary>
        public BusinessInfo BusinessCredentials { get; set; }

        /// <summary>
        /// Get the callResult
        /// </summary>
        /// <returns>it is the call result</returns>
        public string GetTheCallResult()
        {
            return this.callResult;
        }

        /// <summary>
        /// Set the callResult
        /// </summary>
        /// <param name="value">the value</param>
        public void SetTheCallResult(string value)
        {
            this.callResult = value;
        }

        /// <summary>
        /// To call a web API
        /// </summary>
        /// <param name="idleParam">it is no use, set is to null</param>
        public abstract void Request(object idleParam);
    }
}
