// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XElementExtensions.cs" company="Demandforce">
//   TODO: Update copyright text.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Common.Extensions
{
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;

    /// <summary>
    /// This class include some Utilities relate to xml operations
    /// </summary>
    public static class XElementExtensions
    {
        /// <summary>
        /// Get value or default.
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

        /// <summary>
        /// Get format xml.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetFormatXml(this XElement element)
        {
            var stringBuilder = new StringBuilder();
            var settings = new XmlWriterSettings
                               {
                                   OmitXmlDeclaration = true,
                                   Indent = true,
                                   NewLineOnAttributes = true
                               };

            using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
            {
                element.Save(xmlWriter);
            }

            return stringBuilder.ToString();
        }
    }
}
