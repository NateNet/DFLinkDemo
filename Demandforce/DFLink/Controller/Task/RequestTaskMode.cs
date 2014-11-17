// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestTaskMode.cs" company="Demandforce">
// Copyright (c) Demandforce. All rights reserved.   
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Controller.Task
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public enum RequestTaskMode
    {
        /// <summary>
        /// The push is to server push instruction to client .
        /// </summary>
        Push = 0,

        /// <summary>
        /// The pull is to request server with specific interval.
        /// </summary>
        Pull = 1,

        /// <summary>
        /// The mix pull and push.
        /// </summary>
        Mix = 2
    }
}
