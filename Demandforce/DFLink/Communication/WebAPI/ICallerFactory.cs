// -----------------------------------------------------------------------
// <copyright file="ICallerFactory.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.WebAPI
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ICallerFactory
    {
        /// <summary>
        /// Create a caller
        /// </summary>
        /// <returns>a interface</returns>
        ICaller CreateCaller();
    }
}
