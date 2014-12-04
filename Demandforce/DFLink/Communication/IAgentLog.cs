// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAgentLog.cs" company="Demanforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The AgentLog interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication
{
    /// <summary>
    /// The AgentLog interface.
    /// </summary>
    public interface IAgentLog
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get log.
        /// </summary>
        /// <param name="taskId">
        /// The task id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetLog(int taskId);

        #endregion
    }
}