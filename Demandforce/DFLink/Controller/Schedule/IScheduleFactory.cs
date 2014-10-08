// -----------------------------------------------------------------------
// <copyright file="IScheduleFactory.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Controller.Schedule
{
    /// <summary>
    /// The ScheduleFactory interface.
    /// </summary>
    public interface IScheduleFactory
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
        ISchedule CreateSchedule(string scheduleType);
    }
}
