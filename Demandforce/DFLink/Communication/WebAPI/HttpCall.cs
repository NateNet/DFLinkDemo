// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpCall.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The http work.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.WebAPI
{
    using Demandforce.DFLink.Common;
    using Demandforce.DFLink.Communication.Model;

    /// <summary>
    /// The http work.
    /// </summary>
    public class HttpCall : ICall
    {
        #region Public Methods and Operators

        /// <summary>
        /// The do work.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string DoWork(string url, ISerializable data)
        {
            return HttpUtils.PostJsonForXml(url, data.Serialize());
        }

        #endregion
    }
}