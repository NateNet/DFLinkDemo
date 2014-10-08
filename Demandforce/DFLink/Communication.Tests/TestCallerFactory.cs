// -----------------------------------------------------------------------
// <copyright file="TestCallerFactory.cs" company="Demanforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Moq;
    using Demandforce.DFLink.Communication.WebAPI;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TestCallerFactory : ICallerFactory
    {
        public static string UrlString = string.Empty;
        public static string JsonString = string.Empty;

        /// <summary>
        /// Get caller  
        /// </summary>
        /// <returns>a interface</returns>
        public ICaller CreateCaller()
        {
            var trans = new Mock<ICaller>();
            trans.Setup(p => p.PostCommand(It.Is<string>(a => true), It.Is<string>(a => true)))
                .Callback((string a, string b) => { UrlString = a; JsonString = b; });
            return trans.Object;
        }
    }
}
