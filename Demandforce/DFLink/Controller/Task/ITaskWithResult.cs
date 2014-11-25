// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITaskWithResult.cs" company="Demandforce">
//  Copyright (c) Demandforce. All rights reserved.  
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Controller.Task
{
    /// <summary>
    /// The Task interface with return result for execution
    /// </summary>
    /// <typeparam name="T">
    /// The return type
    /// </typeparam>
    public interface ITaskWithResult<out T> : ITask
    {
        /// <summary>
        /// Gets the result.
        /// </summary>
        T Result { get; }
    }
}
