// -----------------------------------------------------------------------
// <copyright file="ScheduleFactory.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Controller.Schedule
{
    /// <summary>
    /// The class is to create the concrete schedule instance
    /// </summary>
    public class ScheduleFactory : IScheduleFactory
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
        public ISchedule CreateSchedule(string scheduleType)
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
