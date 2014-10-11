// -----------------------------------------------------------------------
// <copyright file="RealTimeSchedule.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Controller.Schedule
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Implement the ISchedule and provider real time schedule
    /// </summary>
    public class RealTimeSchedule : ISchedule
    {
        /// <summary>
        /// The real execute time, just once
        /// </summary>
        private DateTime executeTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="RealTimeSchedule"/> class.
        /// </summary>
        public RealTimeSchedule()
        {
            this.executeTime = DateTime.MinValue;
        }

        /// <summary>
        /// Get next run time for task from a start time, that is start time
        /// </summary>
        /// <param name="time">The start time</param>
        /// <param name="includeCurrentTime">this parameter is not used
        /// </param>
        /// <returns>The execute time</returns>
        public DateTime GetNextRunTime(DateTime time, bool includeCurrentTime)
        {
            if (this.executeTime != DateTime.MinValue)
            {
                return this.executeTime;
            }

            this.executeTime = time;
            return time;
        }

        /// <summary>
        /// Get all run times, that is one execute time.
        /// </summary>
        /// <param name="startTime">The parameter is not used.</param>
        /// <param name="endTime">The parameter is not used.</param>
        /// <returns>The list with execute time</returns>
        public List<DateTime> GetRunTimes(DateTime startTime, DateTime endTime)
        {
            var runTimeList = new List<DateTime>();

            // Invoker trigger time is endTime, 
            // so use endTime as this schedule execution time
            DateTime nextTime = this.GetNextRunTime(endTime, true);
            System.Diagnostics.Debug.WriteLine("Next Run Time:" + nextTime.TimeOfDay);
            if (nextTime == endTime)
            {
                runTimeList.Add(nextTime);
            }

            return runTimeList;
        }

        /// <summary>
        /// Mock Interface method
        /// </summary>
        /// <param name="arguments">The parameter is not used.</param>
        public void SetSchedule(string arguments)
        {
        }
    }
}
