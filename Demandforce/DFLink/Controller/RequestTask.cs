// -----------------------------------------------------------------------
// <copyright file="RequestTask.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Controller
{
    using System;
    using System.Xml.Linq;

    using Demandforce.DFLink.Common.Extensions;

    /// <summary>
    /// The task is to request the other tasks from server
    /// </summary>
    public class RequestTask : ITask, ITaskMaker
    {
        /// <summary>
        /// The task manager is the receiver for request task
        /// </summary>
        private readonly TaskManager taskManager;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestTask"/> class.
        /// </summary>
        /// <param name="schedule">
        /// The schedule.
        /// </param>
        /// <param name="taskManager">
        /// The receiver of request task
        /// </param>
        public RequestTask(ISchedule schedule, TaskManager taskManager)
        {
            this.Schedule = schedule;
            this.taskManager = taskManager;
        }

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
        #endregion

        /// <summary>
        /// Execute the request task
        /// </summary>
        public void Execute()
        {
            if (this.taskManager != null)
            {
                var taskXml = this.taskManager.RequestTasks();
                if (taskXml != string.Empty)
                {
                    this.taskManager.ParseTasks(taskXml);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestTask" /> class
        /// with some arguments
        /// </summary>
        /// <param name="arguments">The arguments for schedule</param>
        /// <param name="taskManager">The task manager</param>
        /// <returns>new instance of request task</returns>
        public ITask MakeTask(string arguments, TaskManager taskManager)
        {
            var doc = XDocument.Parse(arguments);
            var taskAttribute = doc.Element("TaskItem");
            int taskId = Convert.ToInt32(taskAttribute.Element("Id").Value);
            var descripton = taskAttribute.Element("Description")
                .GetValueOrDefault(string.Empty); 

            // there is default schedule for request task
            ISchedule schedule = new SimpleIntervalSchedule
                                     {
                                         StartTime = DateTime.Now,
                                         Interval = new TimeSpan(0, 5, 0),
                                         EndTime = DateTime.MaxValue
                                     };
            return new RequestTask(schedule, taskManager)
                       {
                           Id = taskId,
                           Description = descripton
                       };
        }
    }
}
