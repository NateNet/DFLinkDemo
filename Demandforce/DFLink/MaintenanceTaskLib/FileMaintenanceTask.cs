// -----------------------------------------------------------------------
// <copyright file="FileMaintenanceTask.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace MaintenanceTaskLib
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Demandforce.DFLink.Controller.Schedule;
    using Demandforce.DFLink.Controller.Task;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class FileMaintenanceTask : ITask, ITaskMaker
    {

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get
            {
                return "FileMaintenanceTask";
            }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the schedule.
        /// </summary>
        public ISchedule Schedule { get; set; }

        /// <summary>
        /// Gets or sets the maintenance action.
        /// </summary>
        public int MaintenanceAction { get; set; }

        /// <summary>
        /// Gets or sets the directory.
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        /// The execute.
        /// </summary>
        public void Execute()
        {
        }

        /// <summary>
        /// The make task.
        /// </summary>
        /// <param name="arguments">
        /// The arguments.
        /// </param>
        /// <returns>
        /// The <see cref="ITask"/>.
        /// </returns>
        public ITask MakeTask(string arguments)
        {
            return this;
        }

        public void ExecuteUploadFile(string fileName)
        {

        }

    }
}
