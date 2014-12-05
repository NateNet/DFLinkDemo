// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITaskFrom.cs" company="Demandforce">
//  Copyright (c) Demandforce. All rights reserved. 
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Controller.EventStorage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Indicates the task source, likes local file, remote database, etc.
    /// </summary>
    public interface ITaskFrom
    {
        /// <summary>
        /// Load tasks from backup source
        /// </summary>
        /// <returns>
        /// the string for a task with xml format
        /// </returns>
        string GetTasks();

        /// <summary>
        /// Operates task in storage media. i.e. local storage file
        /// </summary>
        /// <param name="action">The action:Add Update Delete </param>
        /// <param name="taskXml">The task</param>
        void OperateTaskStorage(string action, string taskXml);
    }
}
