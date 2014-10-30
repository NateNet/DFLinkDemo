// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpCaller.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The week.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Communication.WebAPI
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

    /// <summary>
    /// The week.
    /// </summary>
    public enum Week
    {
        /// <summary>
        /// The monday.
        /// </summary>
        WkMonday, 

        /// <summary>
        /// The tuesday.
        /// </summary>
        WkTuesday, 

        /// <summary>
        /// The wednesday.
        /// </summary>
        WkWednesday, 

        /// <summary>
        /// The thursday.
        /// </summary>
        WkThursday, 

        /// <summary>
        /// The friday.
        /// </summary>
        WkFriday, 

        /// <summary>
        /// The saturday.
        /// </summary>
        WkSaturday, 

        /// <summary>
        /// The sunday.
        /// </summary>
        WkSunday
    }

    /// <summary>
    ///     TODO: Update summary.
    /// </summary>
    public class HttpCaller : ICaller
    {
        #region Public Methods and Operators

        /// <summary>
        /// Http Post method, use a CookieContainer:cookie to store the cookie and session information
        /// </summary>
        /// <param name="url">
        /// The Url that you want to post to.
        /// </param>
        /// <param name="paramsList">
        /// The parameters that you want to post to server.
        /// </param>
        /// <returns>
        /// The string that Http server's return back.
        /// </returns>
        public static string Post(string url, string paramsList)
        {
            HttpWebRequest request;

            byte[] bt = Encoding.UTF8.GetBytes(paramsList);
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                // ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }

            request.ContentType = "text/json";
            request.Accept = "text/xml";
            request.Timeout = 300000;
            request.Method = "Post";
            request.KeepAlive = true;
            request.ContentLength = bt.Length;
            request.GetRequestStream().Write(bt, 0, bt.Length);

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var sr_Reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return sr_Reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="url">
        /// the url
        /// </param>
        /// <param name="jsonParam">
        /// parameters packed in a JSON format
        /// </param>
        /// <returns>
        /// a result
        /// </returns>
        public string PostCommand(string url, string jsonParam)
        {
            string result = string.Empty;

            result = Post(url, jsonParam);

            return result;
        }

        #endregion
    }
}