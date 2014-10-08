// -----------------------------------------------------------------------
// <copyright file="SimpleIntervalSchedule.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;
    using Demandforce.DFLink.Common.Extensions;

    /// <summary>
    /// Implement the ISchedule and provide the schedule by time interval
    /// </summary>
    public class SimpleIntervalSchedule : ISchedule
    {
        #region Constructors
        /// <summary>
        ///  Initializes a new instance of the <see cref="SimpleIntervalSchedule" /> class.
        /// </summary>
        public SimpleIntervalSchedule()
        {
        }

        #endregion

        #region properties
        /// <summary>
        ///  Gets or sets Time interval for schedule
        /// </summary>
        public TimeSpan Interval { get; set; }

        /// <summary>
        /// Gets or sets start time for schedule
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets end time for schedule
        /// </summary>
        public DateTime EndTime { get; set; }
        #endregion

        /// <summary>
        /// Get next run time according a time interval
        /// </summary>
        /// <param name="time">The start time of getting next time</param>
        /// <param name="includeCurrentTime">decide if consider the start
        /// time as return</param>
        /// <returns>The next run time</returns>
        public DateTime GetNextRunTime(DateTime time, bool includeCurrentTime)
        {
            TimeSpan span = time - this.StartTime;
            //// if the time happens to be the run time
            if (span.TotalMilliseconds % this.Interval.TotalMilliseconds == 0)
            {
                return includeCurrentTime ? time : time + this.Interval;
            }

            //// time is before the start time of schedule
            if (span < TimeSpan.Zero)
            {
                return this.StartTime + this.Interval;
            }

            ////as timer limit, use uint to calculate next time
            var gapToNextRunTime = 
                (uint)(this.Interval.TotalMilliseconds - 
                ((uint)span.TotalMilliseconds % 
                (uint)this.Interval.TotalMilliseconds));
            return time.AddMilliseconds(gapToNextRunTime);
        }

        /// <summary>
        /// Get run times between start time and end time according to a time interval
        /// </summary>
        /// <param name="startTime">The start time</param>
        /// <param name="endTime">The end time</param>
        /// <returns>All available times to execute task</returns>
        public List<DateTime> GetRunTimes(DateTime startTime, DateTime endTime)
        {
            var runTimeList = new List<DateTime>();
            if (startTime < endTime)
            {
                DateTime nextTime = this.GetNextRunTime(startTime, true);
                System.Diagnostics.Debug.WriteLine(nextTime + ":" + nextTime.TimeOfDay);

                while (nextTime < endTime)
                {
                    runTimeList.Add(nextTime);
                    nextTime = this.GetNextRunTime(nextTime, false);
                }
            }

            return runTimeList;
        }

        /// <summary>
        /// Set the properties according to schedule parameters from server.
        /// </summary>
        /// <param name="arguments">The xml format for arguments</param>
        public void SetSchedule(string arguments)
        {
            XDocument scheduleDoc = XDocument.Parse(arguments);

            // The schedule defuault value
            DateTime startTime = DateTime.Now, endTime = DateTime.MaxValue;
            int count = 0, interval = 5;

            var scheduleElement = scheduleDoc.Root;
            if (scheduleElement != null)
            {
                var propertyElement = scheduleElement.Element("Parameters");
                if (propertyElement != null)
                {
                    // Get the start time value
                    if (!DateTime.TryParse(
                        propertyElement.Element("StartTime")
                        .GetValueOrDefault(DateTime.Now.ToString()), 
                        out startTime))
                    {
                        startTime = DateTime.Now;
                    }

                    // Get the end time value
                    if (!DateTime.TryParse(
                        propertyElement.Element("EndTime")
                        .GetValueOrDefault(DateTime.MinValue.ToString()), 
                        out endTime))
                    {
                        endTime = DateTime.MinValue;
                    }

                    // Get the interval and Count value
                    int.TryParse(
                        propertyElement.Element("Interval")
                        .GetValueOrDefault("0"), 
                        out interval);
                    int.TryParse(
                        propertyElement.Element("Count")
                        .GetValueOrDefault("0"), 
                        out count);
                }
            }

            this.StartTime = startTime;
            this.Interval = new TimeSpan(0, interval, 0);
            if (endTime != DateTime.MinValue)
            {
                this.EndTime = endTime;
            }
            else
            {
                if (count > 0)
                {
                    // there is no set of endtime and set of count, calculate the endtime
                    this.EndTime = this.StartTime + TimeSpan.FromTicks(this.Interval.Ticks * count);
                }
                else
                {
                    this.EndTime = DateTime.MaxValue;
                }
            }
        }
    }
}
