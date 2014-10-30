// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestCallerFactory.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication.Tests
{
    using Demandforce.DFLink.Communication.WebAPI;

    using Moq;

    /// <summary>
    ///     TODO: Update summary.
    /// </summary>
    public class TestCallerFactory : ICallerFactory
    {
        #region Static Fields

        /// <summary>
        /// Gets or sets JSON String
        /// </summary>
        public static string JsonString { get; set; }

        /// <summary>
        /// Gets or sets UrlString
        /// </summary>
        public static string UrlString { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Get caller
        /// </summary>
        /// <returns>a interface</returns>
        public ICaller CreateCaller()
        {
            var trans = new Mock<ICaller>();
            trans.Setup(p => p.PostCommand(It.Is<string>(a => true), It.Is<string>(a => true)))
                .Callback(
                    (string a, string b) =>
                        {
                            UrlString = a;
                            JsonString = b;
                        });
            return trans.Object;
        }

        #endregion
    }
}