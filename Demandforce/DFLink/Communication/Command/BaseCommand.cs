// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseCommand.cs" company="Demandforce">
//  Copyright (c) Demandforce. All rights reserved.    
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication.Command
{
    using Demandforce.DFLink.Common.Configuration;
    using Demandforce.DFLink.Communication.WebAPI;

    /// <summary>
    ///     TODO: It is a base class for extended classes
    /// </summary>
    public abstract class BaseCommand
    {
        #region Fields

        /// <summary>
        ///     to store the result form the web API
        /// </summary>
        private string callResult = string.Empty;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the business credential information
        /// </summary>
        public BusinessInfo BusinessCredentials { get; set; }

        /// <summary>
        /// Gets or sets the web caller.
        /// </summary>
        public ICaller Caller { get; set; }

        /// <summary>
        /// Gets or sets the server settings.
        /// </summary>
        public IServerSettings ServerSettings { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Get the callResult
        /// </summary>
        /// <returns>it is the call result</returns>
        public string GetTheCallResult()
        {
            return this.callResult;
        }

        /// <summary>
        /// To call a web API
        /// </summary>
        /// <param name="idleParam">
        /// it is no use, set is to null
        /// </param>
        public abstract void Request(object idleParam);

        /// <summary>
        /// Set the callResult
        /// </summary>
        /// <param name="value">
        /// the value
        /// </param>
        public void SetTheCallResult(string value)
        {
            this.callResult = value;
        }

        #endregion
    }
}