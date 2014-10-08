// -----------------------------------------------------------------------
// <copyright file="ITaskManager.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Controller
{
    /// <summary>
    /// The TaskManager interface for Mock.
    /// </summary>
    public interface ITaskManager
    {
        /// <summary>
        /// The request tasks.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/> task xml.
        /// </returns>
        string RequestTasks();

        /// <summary>
        /// The parse tasks.
        /// </summary>
        /// <param name="taskXml">
        /// The task xml.
        /// </param>
        void ParseTasks(string taskXml);
    }
}
