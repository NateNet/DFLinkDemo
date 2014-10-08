//------------------------------------------------------------------------------
// <copyright file="Downloader.cs" company="Demandforce">
// download a file from a url
// </copyright>
//------------------------------------------------------------------------------
namespace Demandforce.DFLink.Common
{
    using System.IO;
    using System.Net;

    /// <summary>
    /// It can download a file
    /// </summary>
    public class Downloader
    {
        /// <summary>
        /// Download a file from url + fileName
        /// with some arguments
        /// </summary>
        /// <param name="url">The url for the file</param>
        /// <param name="fileName">The file name without path</param>
        /// <returns>>Download result: True-success; False-failed</returns>
        public bool Download(string url, string fileName)
        {
            try
            {
                HttpWebRequest objRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();

                long totalBytes = objResponse.ContentLength;
                if (objResponse.ContentLength != 0)
                {
                    // File is exists
                    Stream sourceStream = objResponse.GetResponseStream();
                    FileStream destStream = new FileStream(fileName, FileMode.Create);

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
    }
}