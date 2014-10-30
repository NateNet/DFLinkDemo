// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICaller.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication.WebAPI
{
    /// <summary>
    ///     TODO: call's interface
    /// </summary>
    public interface ICaller
    {
        #region Public Methods and Operators

        /// <summary>
        /// Post the command
        /// </summary>
        /// <param name="url">a url</param>
        /// <param name="jsonParam">a parameter with JSON format</param>
        /// <returns>a return</returns>
        string PostCommand(string url, string jsonParam);

        #endregion
    }
}