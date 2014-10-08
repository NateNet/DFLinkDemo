// -----------------------------------------------------------------------
// <copyright file="XMLUtils.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Common.Extensions
{
    using System.Xml.Linq;

    /// <summary>
    /// This class include some Utilities relate to xml operations
    /// </summary>
    public static class XElementExtensions
    {
        /// <summary>
        /// The get value or default.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <param name="defaultValue">
        /// The default value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetValueOrDefault(this XElement element, string defaultValue = null)
        {
            var value = element != null ? element.Value : defaultValue;
            return value;
        }
    }
}
