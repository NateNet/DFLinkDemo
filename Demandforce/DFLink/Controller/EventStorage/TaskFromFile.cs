// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskFromFile.cs" company="Demandforce">
//  Copyright (c) Demandforce. All rights reserved. 
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Controller.EventStorage
{
    using System;    
    using System.IO;
    using System.Linq; 
    using System.Text;
    using System.Xml.Linq;

    using Demandforce.DFLink.Controller.Task;

    /// <summary>
    /// Load tasks from local storage file
    /// </summary>
    public class TaskFromFile : ITaskFrom
    {
        /// <summary>
        /// Indicates the tasks storage file in local machine
        /// </summary>
        private const string LocalTasksFile = "Tasks.xml";
        
        /// <summary>
        /// Load tasks from local storage file
        /// </summary>
        /// <returns>
        /// the string for a task with xml format
        /// </returns>
        public string GetTasks()
        {
            if (File.Exists(LocalTasksFile))
            {
                XDocument fileDoc = XDocument.Load(LocalTasksFile);
                return fileDoc.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Operates task in local storage file
        /// </summary>
        /// <param name="action">The action:Add Update Delete </param>
        /// <param name="taskXml">The task</param>
        public void OperateTaskStorage(string action, string taskXml)
        {
            var doc = XDocument.Parse(taskXml);

            // get schedule Type
            string scheduleType = doc.Descendants("Schedule").Descendants("Type").Single().Value;

            if (!scheduleType.Equals("SimpleIntervalSchedule"))
            {
                return;
            }

            switch ((TaskAction)Enum.Parse(typeof(TaskAction), action))
            {
                case TaskAction.Create:
                    {
                        this.AddTaskStorage(taskXml);
                        break;
                    }

                case TaskAction.Update:
                    {
                        this.UpdateTaskStorage(taskXml);
                        break;
                    }

                case TaskAction.Delete:
                    {
                        this.DeleteTaskStorage(taskXml);
                        break;
                    }
            }
        }

        /// <summary>
        /// Add the task to the local storage file
        /// </summary>
        /// <param name="taskXml">a whole xml text for a task</param>
        private void AddTaskStorage(string taskXml)
        {
            // tasks.xml doesn't exist
            if (File.Exists(LocalTasksFile))
            {
                // get the task id from "taskXml" paramter
                XDocument stringDoc = XDocument.Parse(taskXml);
                string taskId = stringDoc.Element("Task").Element("Id").Value;

                XDocument fileDoc = XDocument.Load(LocalTasksFile);

                // get a collection with a filter
                var taskQuery = from task in fileDoc.Element("Tasks").Elements("Task")
                                where task.Element("Id").Value == taskId
                                select task;

                // if this task doesn't exist in storage file then add that
                if (taskQuery.Count() == 0)
                {
                    fileDoc.Element("Tasks").Add(stringDoc.Element("Task"));
                    fileDoc.Save(LocalTasksFile);
                }
            }
            else
            {
                // tasks.xml exist
                StringBuilder objStringBuilder = new StringBuilder();
                objStringBuilder.AppendLine("<Tasks>");
                objStringBuilder.AppendLine(taskXml);
                objStringBuilder.AppendLine("</Tasks>");

                XDocument xdoc = XDocument.Parse(objStringBuilder.ToString());
                xdoc.Save(LocalTasksFile);
            }
        }

        /// <summary>
        /// Update the task within the local storage file
        /// </summary>
        /// <param name="taskXml">a whole xml text for a task</param>
        private void UpdateTaskStorage(string taskXml)
        {
            // only do this if tasks.xml exists
            if (File.Exists(LocalTasksFile))
            {
                // get the task id from "taskXml" paramter
                XDocument stringDoc = XDocument.Parse(taskXml);
                string taskId = stringDoc.Element("Task").Element("Id").Value;

                // Exit if task is initialized Request Task (id is 0)
                if (taskId.Equals("0"))
                {
                    return;
                }

                // 1. delete this task firstly
                this.DeleteTaskStorage(taskXml);

                // 2. then add this task
                this.AddTaskStorage(taskXml);
            }
        }

        /// <summary>
        /// Delete the task within the local storage file
        /// </summary>
        /// <param name="taskXml">a whole xml text for a task</param>
        private void DeleteTaskStorage(string taskXml)
        {
            // only do this if tasks.xml exists
            if (File.Exists(LocalTasksFile))
            {
                // get the task id from "taskXml" paramter
                XDocument stringDoc = XDocument.Parse(taskXml);
                string taskId = stringDoc.Element("Task").Element("Id").Value;

                XDocument fileDoc = XDocument.Load(LocalTasksFile);

                // get a collection with a filter
                var taskQuery = from task in fileDoc.Element("Tasks").Elements("Task")
                                where task.Element("Id").Value == taskId
                                select task;

                // if this task exists in storage file then delete that
                if (taskQuery.Count() > 0)
                {
                    foreach (var task in taskQuery)
                    {
                        task.Remove();
                    }

                    fileDoc.Save(LocalTasksFile);

                    // delete file if no tasks in "tasks.xml"
                    if (!fileDoc.Element("Tasks").HasElements)
                    {
                        File.Delete(LocalTasksFile);
                    }
                }
            }
        }
    }
}
