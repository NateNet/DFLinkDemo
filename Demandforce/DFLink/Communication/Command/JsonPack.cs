// -----------------------------------------------------------------------
// <copyright file="JsonPack.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.Command
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    /// <summary>
    /// TODO: it is a class to serialize a class to or from a JSON string
    /// </summary>
    /// <typeparam name="T">It is a class</typeparam>
    public static class JsonPack<T> where T : class
    {
        /// <summary>
        /// To serialize a object o a JSON string
        /// </summary>
        /// <param name="obj">a instance</param>
        /// <returns>a JSON string</returns>
        public static string SerializeObject(T obj)
        {
            JsonSerializer serializer = new JsonSerializer();
            StringWriter sw = new StringWriter();
            serializer.Serialize(new JsonTextWriter(sw), obj);
            return sw.GetStringBuilder().ToString();
        }

        /// <summary>
        /// To un-serialize a JSON string to a object
        /// </summary>
        /// <param name="jsonString">a JSON string</param>
        /// <returns>a object</returns>
        public static T UnSerializeObject(string jsonString)
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(jsonString);
            T p = (T)serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            return p;
        }
    }
}
