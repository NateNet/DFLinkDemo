// -----------------------------------------------------------------------
// <copyright file="EventStorage.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Controller.EventStorage
{
    using System;

    /// <summary>
    /// Local event storage keeps the last time in memory so that skipped events are not recovered.
    /// </summary>
    public class LocalEventStorage : IEventStorage
    {
        /// <summary>
        /// The last time field
        /// </summary>
        private DateTime lastTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalEventStorage"/> class.
        /// </summary>
        public LocalEventStorage()
        {
            this.lastTime = DateTime.MaxValue;
        }

        /// <summary>
        /// Store the last time that event fired
        /// </summary>
        /// <param name="time">The last time</param>
        public void RecordLastTime(DateTime time)
        {
            this.lastTime = time;
        }

        /// <summary>
        /// Get the last time that event fired
        /// </summary>
        /// <returns>The last time</returns>
        public DateTime ReadLastTime()
        {
            if (this.lastTime == DateTime.MaxValue)
            {
                this.lastTime = DateTime.Now;
            }

            return this.lastTime;
        }

    }
}
