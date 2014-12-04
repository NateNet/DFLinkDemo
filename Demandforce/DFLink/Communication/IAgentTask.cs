// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAgentTask.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The AgentTask interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication
{
    /// <summary>
    /// The AgentTask interface.
    /// </summary>
    public interface IAgentTask
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get task.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetTask();

        #endregion
    }
}