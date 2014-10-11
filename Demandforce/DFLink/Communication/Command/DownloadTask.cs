// -----------------------------------------------------------------------
// <copyright file="DownloadTask.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.Command
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DownloadTask : BaseCommand
    {
        /// <summary>
        /// To lookup the task list with a xml string
        /// </summary>
        /// <param name="idleParam">No use parameter, set it to null</param>
        public override void Request(object idleParam)
        {
            string jsonStr = JsonPack<BusinessInfo>.SerializeObject(this.BusinessCredentials);
            string result = AgentSetting.CallerFactory.CreateCaller().PostCommand(AgentSetting.AddressUrl + AgentSetting.CommandTaskGet, jsonStr);
            this.SetTheCallResult(result);
        }
    }
}
