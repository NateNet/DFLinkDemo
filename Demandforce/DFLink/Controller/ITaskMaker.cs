// -----------------------------------------------------------------------
// <copyright file="ITaskMaker.cs" company="Sky123.Org">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Controller
{

    /// <summary>
    /// The Interface to make task 
    /// </summary>
    public interface ITaskMaker
    {
        /// <summary>
        /// Make task with its schedule or other properties
        /// </summary>
        /// <param name="arguments">The arguments for task instance
        /// </param>
        /// <param name="taskManager">The task manager</param>
        /// <returns>The task instance</returns>
        ITask MakeTask(string arguments, TaskManager taskManager);
    }
}
