// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IJsonSerialize.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The JsonSerialize interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.Model
{
    /// <summary>
    /// The Serialize interface.
    /// </summary>
    public interface ISerializable
    {
        #region Public Methods and Operators

        /// <summary>
        /// Serialize self.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string Serialize();

        #endregion
    }
}