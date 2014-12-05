// -----------------------------------------------------------------------
// <copyright file="ControllerService.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.ConstrolService
{
    using System;
    using System.Configuration;
    using System.ServiceProcess;


    using Demandforce.DFLink.Controller;
    using Demandforce.DFLink.Controller.Task;
    using Demandforce.DFLink.ControlService;
    using Demandforce.DFLink.Logger;

    using Microsoft.Practices.Unity;

    /// <summary>
    /// The controller service.
    /// </summary>
    public partial class ControllerService : ServiceBase
    {
        /// <summary>
        /// The container.
        /// </summary>
        private IUnityContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerService"/> class.
        /// </summary>
        public ControllerService()
        {
            this.InitializeComponent();
            this.container = new UnityContainer();
            Installers installers = new Installers(this.container);
            installers.Install();
        }

        /// <summary>
        /// The on start.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        protected override void OnStart(string[] args)
        {
            // string logSettingName = System.Reflection.Assembly.GetExecutingAssembly().Location;
            // logSettingName = System.IO.Path.GetDirectoryName(logSettingName) + @"\log4net.Setting.xml";

            LogInit.InitLog(string.Empty); // use default configuration file: appname.exe.config
            var taskManager = this.container.Resolve<ITaskManager>();
            ((TaskManager)taskManager).Mode =
                (RequestTaskMode)
                Enum.Parse(typeof(RequestTaskMode), ConfigurationManager.AppSettings["RequestTaskMode"]);

            ((TaskManager)taskManager).InitializeTask();
            var invoker = new Invoker((TaskManager)taskManager);
            invoker.MaxInterval = new TimeSpan(
                int.Parse(ConfigurationManager.AppSettings["MaxInvokerInterval"])
                                  * TimeSpan.TicksPerSecond);
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
