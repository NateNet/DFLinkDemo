// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestCall.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The test call.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.Tests
{
    using System;

    using Demandforce.DFLink.Communication.Model;
    using Demandforce.DFLink.Communication.WebAPI;

    /// <summary>
    ///     The test call.
    /// </summary>
    internal class TestCall : ICall
    {
        #region Fields

        /// <summary>
        ///     The fun.
        /// </summary>
        private readonly Func<string, string, string> fun;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCall"/> class.
        /// </summary>
        /// <param name="f">
        /// The f.
        /// </param>
        public TestCall(Func<string, string, string> f)
        {
            this.fun = f;
        }

        #endregion

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
            return this.fun(url, data.Serialize());
        }

        #endregion
    }
}