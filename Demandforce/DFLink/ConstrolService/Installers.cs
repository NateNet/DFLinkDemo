// -----------------------------------------------------------------------
// <copyright file="Installers.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.ControlService
{
    using Castle.MicroKernel.Registration;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Installers : IWindsorInstaller
    {
        /// <summary>
        /// The install.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="store">
        /// The store.
        /// </param>
        public void Install(
            Castle.Windsor.IWindsorContainer container,
            Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
             container.Register(
                 Classes.FromAssemblyNamed("Controller")
                 .Pick()
                 .WithService.AllInterfaces());
        }
    }
}
