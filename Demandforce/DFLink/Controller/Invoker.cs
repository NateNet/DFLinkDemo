// -----------------------------------------------------------------------
// <copyright file="Invoker.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Timers;


    using Demandforce.DFLink.Controller.EventStorage;
    using Demandforce.DFLink.Controller.Task;



    /// <summary>
    /// This class is to execute different tasks using timer 
    /// </summary>
    public class Invoker : IDisposable
    {

        /// <summary>
        ///  The timer is to execute task according to schedule
        /// </summary>
        private Timer timer;

        /// <summary>
        /// The flag if executing the tasks
        /// </summary>
        private volatile bool stopFlag;

        /// <summary>
        /// The last time task executed
        /// </summary>
        private DateTime lastTime;

        /// <summary>
        /// The instance of taskManager
        /// </summary>
        private TaskManager taskManager;

        /// <summary>
        /// if nothing is scheduled the timer sleeps for a maximum of 1 minute.
        /// </summary>
        private TimeSpan maxInterval = new TimeSpan(0, 1, 0);

        /// <summary>
        /// It determines the method used to store the last event fire time.  It defaults to keeping it in memory.
        /// </summary>
        private IEventStorage eventStorage = new LocalEventStorage();

        /// <summary>
        /// The delegate instance 
        /// </summary>
        private ExecuteHandler executeHandler;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Invoker"/> class.
        /// </summary>
        /// <param name="taskManager">
        /// The task Manager.
        /// </param>
        public Invoker(TaskManager taskManager)
        {
            this.timer = new Timer();
            this.timer.AutoReset = false;
            this.timer.Elapsed += new ElapsedEventHandler(this.TimerElapsed);
            this.taskManager = taskManager;
            this.executeHandler += new ExecuteHandler(this.ExecuteTask);
        }

        #endregion

        /// <summary>
        /// The delegate to execute asynchronous task
        /// </summary>
        /// <param name="task">The task</param>
        private delegate void ExecuteHandler(ITask task);

        /// <summary>
        /// Begins executing all assigned tasks
        /// </summary>
        public void Start()
        {
            this.stopFlag = false;
            this.QueueNextTime(this.eventStorage.ReadLastTime());
        }

        /// <summary>
        /// Stop execution all tasks
        /// </summary>
        public void Stop()
        {
            this.stopFlag = true;
            this.timer.Stop();
        }

        /// <summary>
        /// Implement the IDisposable interface
        /// </summary>
        public void Dispose()
        {
            if (this.timer != null)
            {
                this.timer.Dispose();
            }
        }

        /// <summary>
        /// Timer to execute the task in the task list 
        /// </summary>
        /// <param name="sender">The object to pass parameter</param>
        /// <param name="e"> The data for the timer elapsed event</param>
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (this.taskManager.Tasks == null)
            {
                return;
            }

            this.timer.Stop();

            foreach (ITask task in this.taskManager.Tasks.Values)
            {
                System.Diagnostics.Debug.WriteLine("The Task is: " + task.Name);
                System.Diagnostics.Debug.WriteLine("Last Trigger Time: " + this.lastTime.TimeOfDay);
                System.Diagnostics.Debug.WriteLine("This Trigger Time: " + e.SignalTime.TimeOfDay);
                List<DateTime> runTimes = task.Schedule.GetRunTimes(this.lastTime, e.SignalTime);

                foreach (var runTime in runTimes)
                {
                    System.Diagnostics.Debug.WriteLine("Start to Execute task:" + runTime.TimeOfDay);

                    // Execute task asynchronously
                    this.executeHandler.BeginInvoke(task, null, null);
                }
            }

            if (!this.stopFlag)
            {
                this.QueueNextTime(e.SignalTime);
            }
        }

        /// <summary>
        /// Set the timer that runs on the next time
        /// </summary>
        /// <param name="thisTime">This time</param>
        private void QueueNextTime(DateTime thisTime)
        {
            this.timer.Interval = this.NextInterval(thisTime);
            this.lastTime = thisTime;
            this.eventStorage.RecordLastTime(thisTime);
            this.timer.Start();
        }

        /// <summary>
        /// Get the next timer interval
        /// </summary>
        /// <param name="thisTime">This time</param>
        /// <returns>The interval between this time and next run time</returns>
        private double NextInterval(DateTime thisTime)
        {
            TimeSpan interval = this.NextRunTime(thisTime) - thisTime;

            // for real time schedule, next time may be less than this time
            if ((interval > this.maxInterval) |
                (interval.TotalMilliseconds < 0))  
            {
                interval = this.maxInterval;
            }

            // Handles the case of 0 wait time, the interval property requires a duration > 0.
            return (interval.TotalMilliseconds == 0) ? 1 : interval.TotalMilliseconds;
        }

        /// <summary>
        /// Gets the next time any of the tasks in the list will run
        /// </summary>
        /// <param name="time">The starting time for the interval being queried. This time is included in the interval</param>
        /// <returns>The first absolute date one of the tasks will execute on</returns>
        private DateTime NextRunTime(DateTime time)
        {
            DateTime next = DateTime.MaxValue;

            // Get minimum datetime from the list.
            foreach (ITask task in this.taskManager.Tasks.Values)
            {
                DateTime proposed = task.Schedule.GetNextRunTime(time, true);
                next = (proposed < next) ? proposed : next;
            }

            return next;
        }

        /// <summary>
        /// Execute a task 
        /// </summary>
        /// <param name="task">The task</param>
        private void ExecuteTask(ITask task)
        {
            task.Execute();
        }
    }
}
