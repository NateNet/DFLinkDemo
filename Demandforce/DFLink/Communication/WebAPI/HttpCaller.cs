// -----------------------------------------------------------------------
// <copyright file="HttpCaller.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Communication.WebAPI
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class HttpCaller: ICaller
    {
        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="url">the url</param>
        /// <param name="jsonParam">parameters packed in a JSON format</param>
        /// <returns>a result</returns>
        public string PostCommand(string url, string jsonParam)
        {
            string result = string.Empty;

            result = HttpCaller.Post(url, jsonParam);

            return result;
        }


        /// <summary>
        /// Http Post method, use a CookieContainer:cookie to store the cookie and session information
        /// </summary>
        /// <param name="p_url">The Url that you want to post to.</param>
        /// <param name="p_params">The parameters that you want to post to server.</param>
        /// <returns>The string that Http server's return back.</returns>
        public static string Post(string p_url, string p_params)
        {
            HttpWebRequest request;

            byte[] bt = Encoding.UTF8.GetBytes(p_params);
            if (p_url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.CheckValidationResult);
                request = WebRequest.Create(p_url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(p_url) as HttpWebRequest;
            }

            request.ContentType = "text/json";
            request.Accept = "text/xml";
            request.Timeout = 300000;
            request.Method = "Post";
            request.KeepAlive = true;
            request.ContentLength = bt.Length;
            request.GetRequestStream().Write(bt, 0, bt.Length);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader sr_Reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return sr_Reader.ReadToEnd();
                }
            }
        }
    }
}
