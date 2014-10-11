// -----------------------------------------------------------------------
// <copyright file="ICaller.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.WebAPI
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ICaller
    {
        string PostCommand(string url, string jsonParam);  
    }
}
