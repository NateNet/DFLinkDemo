// -----------------------------------------------------------------------
// <copyright file="ScheduleFactory.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The class is to create the concrete schedule instance
    /// </summary>
    public class ScheduleFactory
    {
        /// <summary>
        /// The create schedule.
        /// </summary>
        /// <param name="scheduleType">
        /// The schedule type.
        /// </param>
        /// <returns>
        /// The <see cref="ISchedule"/>.
        /// </returns>
        public static ISchedule CreateSchedule(string scheduleType)
        {
            if (scheduleType == "SimpleIntervalSchedule")
            {
                return new SimpleIntervalSchedule();
            }
            else
            {
                return new RealTimeSchedule();
            }
        }
    }
}
