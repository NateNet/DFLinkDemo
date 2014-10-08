// -----------------------------------------------------------------------
// <copyright file="ITask.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Controller
{
    /// <summary>
    /// The Task interface for execution
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// Gets or sets the task Id
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Gets the task name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or sets the task description
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the task execution schedule
        /// </summary>
        ISchedule Schedule { get; set; }

        /// <summary>
        /// Execute the task
        /// </summary>
        void Execute();
    }
}
