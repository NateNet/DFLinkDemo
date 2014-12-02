// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UploadFile.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The upload file.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.Common
{
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;

    /// <summary>
    ///     The upload file.
    /// </summary>
    public class UploadFile
    {
        #region Public Methods and Operators

        /// <summary>
        /// The http post data.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="timeOut">
        /// The time out.
        /// </param>
        /// <param name="fileKeyName">
        /// The file key name.
        /// </param>
        /// <param name="filePathName">
        /// The file path name.
        /// </param>
        /// <param name="stringDict">
        /// The string dictionary.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string HttpPostData(
            string url, 
            int? timeOut, 
            string fileKeyName, 
            string filePathName, 
            NameValueCollection stringDict)
        {
            HttpWebRequest webRequest;
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                webRequest = WebRequest.Create(url) as HttpWebRequest;
                if (webRequest != null)
                {
                    webRequest.ProtocolVersion = HttpVersion.Version10;
                }
            }
            else
            {
                webRequest = WebRequest.Create(url) as HttpWebRequest;
            }

            if (webRequest == null)
            {
                return null;
            }

            string responseContent;
            var memStream = new MemoryStream();

            var boundary = "--------" + DateTime.Now.Ticks.ToString("x");
            var beginBoundary = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");
            var fileStream = new FileStream(filePathName, FileMode.Open, FileAccess.Read);
            var endBoundary = Encoding.ASCII.GetBytes("--" + boundary + "--\r\n");

            webRequest.Method = "POST";

            if (timeOut.HasValue)
            {
                webRequest.Timeout = timeOut.Value;
            }

            webRequest.ContentType = "multipart/form-data; boundary=" + boundary;

            const string FilePartHeader =
                "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n"
                + "Content-Type: application/octet-stream\r\n\r\n";
            var header = string.Format(FilePartHeader, fileKeyName, filePathName);
            var headerbytes = Encoding.UTF8.GetBytes(header);

            memStream.Write(beginBoundary, 0, beginBoundary.Length);
            memStream.Write(headerbytes, 0, headerbytes.Length);

            var buffer = new byte[1024];
            int bytesRead; // =0  

            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                memStream.Write(buffer, 0, bytesRead);
            }

            // write the Key  
            string stringKeyHeader = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\""
                                     + "\r\n\r\n{1}\r\n";

            foreach (byte[] formitembytes in from string key in stringDict.Keys
                                             select string.Format(stringKeyHeader, key, stringDict[key])
                                             into formitem
                                             select Encoding.UTF8.GetBytes(formitem))
            {
                memStream.Write(formitembytes, 0, formitembytes.Length);
            }

            // write the last boundary
            memStream.Write(endBoundary, 0, endBoundary.Length);

            webRequest.ContentLength = memStream.Length;

            Stream requestStream = webRequest.GetRequestStream();

            memStream.Position = 0;
            var tempBuffer = new byte[memStream.Length];
            memStream.Read(tempBuffer, 0, tempBuffer.Length);
            memStream.Close();

            requestStream.Write(tempBuffer, 0, tempBuffer.Length);
            requestStream.Close();

            var httpWebResponse = (HttpWebResponse)webRequest.GetResponse();

            using (
                var httpStreamReader = new StreamReader(
                    httpWebResponse.GetResponseStream(), 
                    Encoding.GetEncoding("utf-8")))
            {
                responseContent = httpStreamReader.ReadToEnd();
            }

            fileStream.Close();
            httpWebResponse.Close();
            webRequest.Abort();

            return responseContent;
        }

        #endregion
    }
}