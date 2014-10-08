// -----------------------------------------------------------------------
// <copyright file="ISchedule.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Controller.Schedule
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// ISchedule represents times the task will executed and the next time.
    /// </summary>
    public interface ISchedule
    {
        /// <summary>
        /// Get next run time for task from a start time
        /// </summary>
        /// <param name="time">the start time</param>
        /// <param name="includeCurrentTime">if true, will consider the startTime
        /// as return date, otherwise ignore to calculate start time</param>
        /// <returns>The next run time</returns>
        DateTime GetNextRunTime(DateTime time, bool includeCurrentTime);
        
        /// <summary>
        /// Get all run time of task during a range of time
        /// </summary>
        /// <param name="startTime">The start time of time range </param>
        /// <param name="endTime">The end time of time range</param>
        /// <returns>All available times at which task need to be run</returns>
        List<DateTime> GetRunTimes(DateTime startTime, DateTime endTime);

        /// <summary>
        /// Set the schedule properties
        /// </summary>
        /// <param name="arguments">The arguments to set schedule,
        /// The structure is xml.
        /// </param>
        void SetSchedule(string arguments);
    }
}
