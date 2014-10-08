// -----------------------------------------------------------------------
// <copyright file="HttpCallerFactory.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class HttpCallerFactory : ICallerFactory
    {
        /// <summary>
        /// Create a caller
        /// </summary>
        /// <returns>a interface</returns>
        public ICaller CreateCaller()
        {
            return new HttpCaller();
        }
    }
}
