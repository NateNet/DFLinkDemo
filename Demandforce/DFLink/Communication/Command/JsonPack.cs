// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonPack.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   TODO: it is a class to serialize a class to or from a JSON string
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication.Command
{
    using System.IO;
    using Newtonsoft.Json;

    /// <summary>
    /// TODO: it is a class to serialize a class to or from a JSON string
    /// </summary>
    /// <typeparam name="T">
    /// It is a class
    /// </typeparam>
    public static class JsonPack<T>
        where T : class
    {
        #region Public Methods and Operators

        /// <summary>
        /// To serialize a object o a JSON string
        /// </summary>
        /// <param name="obj">
        /// a instance
        /// </param>
        /// <returns>
        /// a JSON string
        /// </returns>
        public static string SerializeObject(T obj)
        {
            var serializer = new JsonSerializer();
            var sw = new StringWriter();
            serializer.Serialize(new JsonTextWriter(sw), obj);
            return sw.GetStringBuilder().ToString();
        }

        /// <summary>
        /// To un-serialize a JSON string to a object
        /// </summary>
        /// <param name="jsonString">
        /// a JSON string
        /// </param>
        /// <returns>
        /// a object
        /// </returns>
        public static T UnSerializeObject(string jsonString)
        {
            var serializer = new JsonSerializer();
            var sr = new StringReader(jsonString);
            var p = (T)serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            return p;
        }

        #endregion
    }
}