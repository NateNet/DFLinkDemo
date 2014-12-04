// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpUtil.cs" company="Demandforce">
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
    /// Some utils of http request
    /// </summary>
    public static class HttpUtil
    {
        #region Public Methods and Operators

        /// <summary>
        /// The download file.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="filePathName">
        /// The file path name.
        /// </param>
        /// <returns>
        /// True: successful, false: failed <see cref="bool"/>.
        /// </returns>
        public static bool DownloadFile(string url, string filePathName)
        {
            try
            {
                var objRequest = BuildHttpWebRequest(url);
                var objResponse = (HttpWebResponse)objRequest.GetResponse();

                long totalBytes = objResponse.ContentLength;
                if (objResponse.ContentLength != 0)
                {
                    // File is exists
                    Stream sourceStream = objResponse.GetResponseStream();
                    FileStream destStream = new FileStream(filePathName, FileMode.Create);

                    try
                    {
                        long totalDownloadedByte = 0;
                        byte[] bufferForReadWrite = new byte[1024];
                        int streamSize = sourceStream.Read(bufferForReadWrite, 0, (int)bufferForReadWrite.Length);

                        while (streamSize > 0)
                        {
                            totalDownloadedByte = streamSize + totalDownloadedByte;
                            destStream.Write(bufferForReadWrite, 0, streamSize);
                            streamSize = sourceStream.Read(bufferForReadWrite, 0, (int)bufferForReadWrite.Length);
                        }
                    }
                    finally
                    {
                        destStream.Close();
                        sourceStream.Close();
                    }

                    return true;
                }
                else
                {
                    // TODO: File is not exists
                    return false;
                }
            }
            catch
            {
                // TODO LOG (System.Exception e)
                return false;
            }
        }

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
        public static string PostFormData(
            string url, 
            int? timeOut, 
            string fileKeyName, 
            string filePathName, 
            NameValueCollection stringDict)
        {
            var webRequest = BuildHttpWebRequest(url);
            if (webRequest == null)
            {
                return null;
            }

            var defaultTimeOut = 30 * 1000;
            if (timeOut.HasValue)
            {
                defaultTimeOut = timeOut.Value;
            }

            string boundary = "--------" + DateTime.Now.Ticks.ToString("x");
            webRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            webRequest.Method = "POST";
            webRequest.Timeout = defaultTimeOut;


            var memStream = new MemoryStream();
            FillMemoryStream(memStream, boundary, fileKeyName, filePathName, stringDict);

            memStream.Position = 0;
            webRequest.ContentLength = memStream.Length;

            Stream requestStream = webRequest.GetRequestStream();
            memStream.CopyTo(requestStream);

            memStream.Close();
            requestStream.Close();

            string responseContent;

            using (var response = (HttpWebResponse)webRequest.GetResponse())
            {
                using (var streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return responseContent = streamReader.ReadToEnd();
                }
            }
        }

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
        public static string PostJsonForXml(string url, string paramsList)
        {
            var request = BuildHttpWebRequest(url);

            if (request == null)
            {
                return null;
            }

            byte[] bt = Encoding.UTF8.GetBytes(paramsList);

            request.ContentType = "text/json";
            request.Accept = "text/xml";
            request.Timeout = 30 * 10000;
            request.Method = "Post";
            request.KeepAlive = true;
            request.ContentLength = bt.Length;
            request.GetRequestStream().Write(bt, 0, bt.Length);

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The build http web request.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="HttpWebRequest"/>.
        /// </returns>
        private static HttpWebRequest BuildHttpWebRequest(string url)
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

            return webRequest;
        }

        /// <summary>
        /// The fill memory stream.
        /// </summary>
        /// <param name="memoryStream">
        /// The memory stream.
        /// </param>
        /// <param name="boundary">
        /// The boundary.
        /// </param>
        /// <param name="fileKeyName">
        /// The file key name.
        /// </param>
        /// <param name="filePathName">
        /// The file path name.
        /// </param>
        /// <param name="stringDict">
        /// It is a name-value collection.
        /// </param>
        private static void FillMemoryStream(
            MemoryStream memoryStream, 
            string boundary, 
            string fileKeyName, 
            string filePathName, 
            NameValueCollection stringDict)
        {
            byte[] beginBoundary = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");
            var fileStream = new FileStream(filePathName, FileMode.Open, FileAccess.Read);
            byte[] endBoundary = Encoding.ASCII.GetBytes("--" + boundary + "--\r\n");

            const string FilePartHeader =
                "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n"
                + "Content-Type: application/octet-stream\r\n\r\n";
            string header = string.Format(FilePartHeader, fileKeyName, filePathName);
            byte[] headerbytes = Encoding.UTF8.GetBytes(header);

            memoryStream.Write(beginBoundary, 0, beginBoundary.Length);
            memoryStream.Write(headerbytes, 0, headerbytes.Length);

            var buffer = new byte[1024];
            int bytesRead; // =0  

            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                memoryStream.Write(buffer, 0, bytesRead);
            }

            // write the Key  
            string stringKeyHeader = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\""
                                     + "\r\n\r\n{1}\r\n";

            foreach (var formitembytes in from string key in stringDict.Keys
                                          select string.Format(stringKeyHeader, key, stringDict[key])
                                          into formitem
                                          select Encoding.UTF8.GetBytes(formitem))
            {
                memoryStream.Write(formitembytes, 0, formitembytes.Length);
            }

            // write the last boundary
            memoryStream.Write(endBoundary, 0, endBoundary.Length);

            fileStream.Close();
        }

        #endregion
    }
}