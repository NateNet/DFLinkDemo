// -----------------------------------------------------------------------
// <copyright file="LoggingInstaller.cs" company="Sky123.Org">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Castle.Facilities.Logging;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LoggingInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<LoggingFacility>(f => f.UseLog4Net());
        }
    }
}
