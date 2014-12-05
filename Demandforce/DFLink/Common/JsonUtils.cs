// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonUtils.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The json util.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Common
{
    using System.IO;

    using Newtonsoft.Json;

    /// <summary>
    /// The JSON utils.
    /// </summary>
    public static class JsonUtils
    {
        #region Public Methods and Operators

        /// <summary>
        /// The serialize object.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <returns>
        /// The JSON string<see cref="string"/>.
        /// </returns>
        public static string SerializeObject(object obj)
        {
            var serializer = new JsonSerializer();
            var sw = new StringWriter();
            serializer.Serialize(new JsonTextWriter(sw), obj);
            return sw.GetStringBuilder().ToString();
        }

        /// <summary>
        /// The UN serialize object.
        /// </summary>
        /// <param name="jsonString">
        /// The JSON string.
        /// </param>
        /// <typeparam name="T">
        /// a class type
        /// </typeparam>
        /// <returns>
        /// The object<see cref="T"/>.
        /// </returns>
        public static T UnSerializeObject<T>(string jsonString)
        {
            var serializer = new JsonSerializer();
            var sr = new StringReader(jsonString);
            var p = (T)serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            return p;
        }

        #endregion
    }
}