// -----------------------------------------------------------------------
// <copyright file="RequestTask.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Controller.Task
{
    using System;
    using System.Xml.Linq;

    using Demandforce.DFLink.Common.Extensions;
    using Demandforce.DFLink.Controller.Schedule;

    /// <summary>
    /// The task is to request the other tasks from server
    /// </summary>
    public class RequestTask : ITask, ITaskMaker
    {
        #region Constructor

        /// <summary>
        ///  Initializes a new instance of the <see cref="RequestTask"/> class.
        /// </summary>
        public RequestTask()
        {
        }
        
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the request task Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets the request task name
        /// </summary>
        public string Name
        {
            get { return "Request"; }
        }

        /// <summary>
        /// Gets or sets the request task description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the schedule is for task to execute
        /// </summary>
        public ISchedule Schedule { get; set; }

        /// <summary>
        /// Gets or sets the task manager. it is just for request task
        /// </summary>
        public ITaskManager TaskManager { get; set; }
        #endregion

        /// <summary>
        /// Execute the request task
        /// </summary>
        public void Execute()
        {
            if (this.TaskManager != null)
            {
                var taskXml = this.TaskManager.RequestTasks();
                if (taskXml != string.Empty)
                {
                    this.TaskManager.ParseTasks(taskXml);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestTask"/> class
        /// with some arguments
        /// </summary>
        /// <param name="arguments">
        ///     The arguments for schedule
        /// </param>
        /// <returns>
        /// new instance of request task
        /// </returns>
        public ITask MakeTask(string arguments)
        {
            return this;
        }
    }
}
