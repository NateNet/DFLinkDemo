// -----------------------------------------------------------------------
// <copyright file="ITaskFactory.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Controller.Task
{
    /// <summary>
    /// The TaskFactory interface.
    /// </summary>
    public interface ITaskFactory
    {
        /// <summary>
        /// The create task.
        /// </summary>
        /// <param name="taskName">
        /// The task name.
        /// </param>
        /// <returns>
        /// The <see cref="ITaskMaker"/>.
        /// </returns>
        ITaskMaker CreateTaskMaker(string taskName);

        /// <summary>
        /// The get type name from configuration.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetTypeNameFromConfiguration(string name);
    }
}
