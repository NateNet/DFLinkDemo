// -----------------------------------------------------------------------
// <copyright file="TaskManager.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Controller
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Xml.Linq;
    using Demandforce.DFLink.Common.Extensions;
    using Demandforce.DFLink.Communication;
    using Demandforce.DFLink.Controller.Task;
    using Demandforce.DFLink.ExceptionHandling.Logging.ExceptionHandleWrapper;
    using Demandforce.DFLink.Logger;

    /// <summary>
    /// The class is to manage a task list
    /// </summary>
    public class TaskManager : ITaskManager
    {
        /// <summary>
        /// The task creator.
        /// </summary>
        private readonly ITaskCreator taskCreator;

        /// <summary>
        /// The exception policy.
        /// </summary>
        private readonly IExceptionPolicy exceptionPolicy;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskManager"/> class.
        /// </summary>
        /// <param name="taskCreator">
        /// The task Creator, which is create task with its schedule
        /// </param>
        /// <param name="exceptionPolicy">
        /// The exception Policy.
        /// </param>
        public TaskManager(ITaskCreator taskCreator, IExceptionPolicy exceptionPolicy)
        {
            this.Tasks = new ConcurrentDictionary<int, ITask>();
            this.taskCreator = taskCreator;
            this.exceptionPolicy = exceptionPolicy;
        }

        /// <summary>
        /// Gets or sets the task list
        /// </summary>
        public ConcurrentDictionary<int, ITask> Tasks { get; set; }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        public RequestTaskMode Mode { get; set; }

        /// <summary>
        /// The initialize task.
        /// </summary>
        public void InitializeTask()
        {
            // initialize the communication setting
             AgentSetting.InitialSetting();
            if (this.Mode == RequestTaskMode.Pull)
            {
                this.InitializeRequestTask(
                    TaskAction.Update.ToString(),
                    "SimpleIntervalSchedule");
            }
            else
            {
                PushManager.GetInstance().EventDataComming += this.NotifyHandler;
            }
        }

        /// <summary>
        /// The initialize task, that is to create request task with 
        /// simple interval schedule. the interval is 1 minute.
        /// or create real time schedule according to server message.
        /// </summary>
        /// <param name="action">
        /// The Action include create or update due to different request way.
        /// if request mode is pull, action is just create, if mix, action is 
        /// update.
        /// </param>
        /// <param name="scheduleTypeName">
        /// The schedule Type Name.
        /// </param>
        public void InitializeRequestTask(string action, string scheduleTypeName)
        {
            string initialTask = string.Format(
                "<Task><Action>{0}</Action><Id>0</Id><Name>Request</Name>"
                + "<Schedule><Type>{1}</Type></Schedule></Task>",
                action,
                scheduleTypeName);
            this.ParseTasks(initialTask);
            //var task = this.taskCreator.Creator(initialTask);
            //((RequestTask)task).TaskManager = this;
            //this.AddTask(task);
        }

        /// <summary>
        /// The notify handler, which handler server 
        /// notification what are some task need to be handler by client.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void NotifyHandler(string message)
        {
            LogHelper.GetLoggerHandle().Debug(
                "TaskManager",
                0,
                "The message from server:" + message);

            // Mix represents server notify there is task,
            // still need api to get task list, otherwise, Server
            // push task list here
            if (this.Mode == RequestTaskMode.Mix)
            {
                this.InitializeRequestTask(
                    TaskAction.Update.ToString(), 
                    "RealTimeSchedule");
            }
            else
            {
                this.ParseTasks(message);
            }
        }


        /// <summary>
        /// Request tasks xml from server
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string RequestTasks()
        {
             // it is for test
             // var taskxml = XDocument.Load(@"F:\DFLinkReload\TaskXML.xml");
             var taskxml = AgentTask.GetStartedInstance().GetTask();
             return taskxml;
        }

        /// <summary>
        /// It is to parse task list string to task instance list
        /// </summary>
        /// <param name="taskXml">
        /// The xml presents the task list</param>
        public void ParseTasks(string taskXml)
        {
            var doc = XDocument.Parse(taskXml);
            LogHelper.GetLoggerHandle().Debug(
                "TaskManager", 
                0, 
                "The task list:" + doc.Root.GetFormatXml());

            var tasks = from taskItem in doc.Descendants("Task") 
                        select new
                                   {
                                       Argument = taskItem.ToString(),
                                       Action = taskItem.Element("Action")
                                       .GetValueOrDefault("Update")
                                   };

            // Create the task instance and operate task in the current list
            foreach (var taskItem in tasks)
            {
                try
                {
                    ITask task = this.taskCreator.Creator(taskItem.Argument);
                    this.OperateTask(taskItem.Action, task);
                }
                catch (Exception ex)
                {
                    bool rethrow = this.exceptionPolicy.HandlerException(ex, "Default Policy");
                    if (rethrow)
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Operates task in the internal dictionary list
        /// </summary>
        /// <param name="action">The action:Add Update Delete </param>
        /// <param name="task">The task</param>
        private void OperateTask(string action, ITask task)
        {
            switch ((TaskAction)Enum.Parse(typeof(TaskAction),action))
            {
                case TaskAction.Create:
                    {
                        this.AddTask(task);
                        break;
                    }

                case TaskAction.Update:
                    {
                        this.UpdateTask(task);
                        break;
                    }

                case TaskAction.Delete:
                    {
                        this.DeleteTask(task.Id);
                        break;
                    }
            }
        }

        /// <summary>
        /// Add the task to the list
        /// </summary>
        /// <param name="newTask">The task need to be added</param>
        private void AddTask(ITask newTask)
        {
            if (!this.Tasks.ContainsKey(newTask.Id))
            {
                this.Tasks.TryAdd(newTask.Id, newTask);
            }
        }

        /// <summary>
        /// Update the same task in the list, 
        /// the way is to remove and add new
        /// </summary>
        /// <param name="newTask">The new task</param>
        private void UpdateTask(ITask newTask)
        {
            this.DeleteTask(newTask.Id);

            // This is default task, that is request task, it needs
            // TaskManager
            if (newTask.Id == 0)
            {
                ((RequestTask)newTask).TaskManager = this;
            }
            this.Tasks.TryAdd(newTask.Id, newTask);
        }

        /// <summary>
        /// Remove the task in the list
        /// </summary>
        /// <param name="taskId">The task id</param>
        private void DeleteTask(int taskId)
        {
            if (!this.Tasks.ContainsKey(taskId))
            {
                return;
            }

            ITask task;
            this.Tasks.TryRemove(taskId, out task);
        }
    }
}
