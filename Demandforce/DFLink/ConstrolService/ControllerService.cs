// -----------------------------------------------------------------------
// <copyright file="ControllerService.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.ConstrolService
{
    using System.ServiceProcess;

    using Castle.Windsor;
    using Castle.Windsor.Installer;

    using Demandforce.DFLink.Controller;
    using Demandforce.DFLink.Logger;

    /// <summary>
    /// The controller service.
    /// </summary>
    public partial class ControllerService : ServiceBase
    {
        /// <summary>
        /// The container.
        /// </summary>
        private IWindsorContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerService"/> class.
        /// </summary>
        public ControllerService()
        {
            this.InitializeComponent();
            this.container = new WindsorContainer();
            this.container.Install(FromAssembly.This());
        }

        /// <summary>
        /// The on start.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        protected override void OnStart(string[] args)
        {
            string logSettingName = System.Reflection.Assembly.GetExecutingAssembly().Location;
            logSettingName = System.IO.Path.GetDirectoryName(logSettingName) + @"\log4net.Setting.xml";
            LogInit.InitLog(logSettingName);
            var taskManager = this.container.Resolve<ITaskManager>();
            ((TaskManager)taskManager).InitializeTask();
            var invoker = new Invoker((TaskManager)taskManager);
            invoker.Start();
        }

        /// <summary>
        /// The on stop.
        /// </summary>
        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
