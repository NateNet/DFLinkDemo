// -----------------------------------------------------------------------
// <copyright file="ITaskCreator.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Controller
{

    using Demandforce.DFLink.Controller.Task;

    /// <summary>
    /// The TaskCreator interface.It create task with its schedule together.
    /// </summary>
    public interface ITaskCreator
    {
        /// <summary>
        /// The creator.
        /// </summary>
        /// <param name="taskItem">
        /// The task item element
        /// </param>
        /// <returns>
        /// The <see cref="ITask"/>.
        /// </returns>
        ITask Creator(string taskItem);
    }
}
