// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebAPIClientFactory.cs" company="">
//   
// </copyright>
// <summary>
//   The agent factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication
{
    using Demandforce.DFLink.Communication.WebAPI;

    /// <summary>
    ///     The agent factory.
    /// </summary>
    public static class WebAPIClientFactory
    {
        #region Public Methods and Operators

        /// <summary>
        /// The instance get task.
        /// </summary>
        /// <returns>
        /// The <see cref="IGetTask"/>.
        /// </returns>
        public static IGetTask InstanceGetTask()
        {
            return new WorkGetTask { Call = new HttpCall() };
        }

        /// <summary>
        /// The instance update status.
        /// </summary>
        /// <returns>
        /// The <see cref="IUpdateStatus"/>.
        /// </returns>
        public static IUpdateStatus InstanceUpdateStatus()
        {
            return new WorkUpdateStatus { Call = new HttpCall() };
        }

        /// <summary>
        /// The instance upload file.
        /// </summary>
        /// <returns>
        /// The <see cref="IUploadFile"/>.
        /// </returns>
        public static IUploadFile InstanceUploadFile()
        {
            return new WorkUploadFile { Call = new HttpCall() };
        }

        #endregion
    }
}