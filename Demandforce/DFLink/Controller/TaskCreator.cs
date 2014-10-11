// -----------------------------------------------------------------------
// <copyright file="TaskCreator.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Controller
{
    using System;
    using System.Xml.Linq;

    using Demandforce.DFLink.Common.Extensions;
    using Demandforce.DFLink.Controller.Schedule;
    using Demandforce.DFLink.Controller.Task;

    /// <summary>
    /// The default task creator.that is to create task from xml element.
    /// </summary>
    public class DefaultTaskCreator : ITaskCreator
    {
        /// <summary>
        /// The task factory.
        /// </summary>
        private readonly ITaskFactory taskFactory;

        /// <summary>
        /// The schedule factory.
        /// </summary>
        private readonly IScheduleFactory scheduleFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultTaskCreator"/> class.
        /// </summary>
        /// <param name="taskFactory">
        /// The task factory.
        /// </param>
        /// <param name="scheduleFactory">
        /// The schedule factory.
        /// </param>
        public DefaultTaskCreator(ITaskFactory taskFactory, IScheduleFactory scheduleFactory)
        {
            this.taskFactory = taskFactory;
            this.scheduleFactory = scheduleFactory;
        }

        /// <summary>
        /// The creator.
        /// </summary>
        /// <param name="taskItem">
        /// The task item.
        /// </param>
        /// <returns>
        /// The <see cref="ITask"/>.
        /// </returns>
        public ITask Creator(string taskItem)
        {
            var taskDoc = XDocument.Parse(taskItem);
            var taskAttribute = taskDoc.Element("Task");
            var scheduleElement = taskAttribute.Element("Schedule");

            // create task instance
            ITaskMaker taskMaker = this.taskFactory.CreateTaskMaker(
                taskAttribute.Element("Name")
                .GetValueOrDefault(string.Empty));
            ITask task = taskMaker.MakeTask(taskItem);
            task.Id = Convert.ToInt32(taskAttribute.Element("Id").Value);
            task.Description = taskAttribute.Element("Description")
                .GetValueOrDefault(string.Empty);

            // create schedule instance for task
            task.Schedule =
                this.scheduleFactory.CreateSchedule(
                scheduleElement.Element("Type")
                .GetValueOrDefault(string.Empty));
            task.Schedule.SetSchedule(scheduleElement.ToString());

            return task;
        }
    }
}
