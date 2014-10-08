// -----------------------------------------------------------------------
// <copyright file="IEventStorage.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Controller.EventStorage
{
    using System;

    /// <summary>
    /// IEventStorage is used to provide persistence of schedule during service shutdowns.
    /// </summary>
    public interface IEventStorage
    {
        /// <summary>
        /// Record last time that event fired
        /// </summary>
        /// <param name="time">The last time</param>
        void RecordLastTime(DateTime time);
        
        /// <summary>
        /// Get the last time that event fired
        /// </summary>
        /// <returns>The last time</returns>
        DateTime ReadLastTime();
    }
}
