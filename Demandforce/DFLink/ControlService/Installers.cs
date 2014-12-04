// -----------------------------------------------------------------------
// <copyright file="Installers.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.ControlService
{
    using Demandforce.DFLink.Common.Configuration;
    using Demandforce.DFLink.Communication;
    using Demandforce.DFLink.Communication.Command;
    using Demandforce.DFLink.Communication.Socket;
    using Demandforce.DFLink.Communication.WebAPI;
    using Demandforce.DFLink.Controller;
    using Demandforce.DFLink.Controller.Schedule;
    using Demandforce.DFLink.Controller.Task;
    using Demandforce.DFLink.ExceptionHandling.Logging.ExceptionHandleWrapper;

    using Microsoft.Practices.Unity;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Installers 
    {
        /// <summary>
        /// The container.
        /// </summary>
        private IUnityContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="Installers"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public Installers(IUnityContainer container)
        {
            this.container = container;

            // initialize the communication setting
            // AgentSetting.InitialSetting();
        }

        /// <summary>
        /// The install to register type.
        /// </summary>
        public void Install()
        {
            this.container.RegisterType<ITaskManager, TaskManager>();
            this.container.RegisterType<ITaskCreator, DefaultTaskCreator>();
            this.container
                .RegisterType<IExceptionPolicy, ExceptionPolicyWrapper>();
            this.container.RegisterType<ITaskFactory, TaskFactory>();
            this.container.RegisterType<IScheduleFactory, ScheduleFactory>();

            this.container.RegisterType<ISettings, XmlSettings>();
            this.container.RegisterType<IServerSettings, ServerSettings>();
            this.container.RegisterType<IAgentTask, AgentTask>();
            

            this.container.RegisterType<INetworkClient, ClientTcp>(
                new InjectionConstructor(this.container.Resolve<IServerSettings>()));
        }
    }
}
