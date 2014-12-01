// -----------------------------------------------------------------------
// <copyright file="PmsDataHandler.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.ApiCaller
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// some operation for the data extract out from PMS
    /// </summary>
    public class PmsDataHandler
    {                
        /// <summary>
        /// Append content to a file
        /// </summary>
        /// <param name="path">a file path. if not exists then create it else do nothing</param>
        public void MakePathExist(string path)
        {
            Directory.CreateDirectory(path);
        }
        
        /// <summary>
        /// Append content to a file
        /// </summary>
        /// <param name="fileName">a file name</param>
        /// <param name="content">text content</param> 
        public void AppendToFile(string fileName, string content)
        {
            StreamWriter objStreamWriter = File.AppendText(fileName);
            objStreamWriter.Write(content);
            objStreamWriter.Flush();
            objStreamWriter.Close();
        }

        /// <summary>
        /// Generate the xml file path without filename
        /// </summary>
        /// <returns>return the xml file path</returns>
        public string GenXmlFilePath()
        {                        
            StringBuilder objStringBuilder = new StringBuilder();
            objStringBuilder.Append(System.Environment.CurrentDirectory);
            objStringBuilder.Append("\\Office 1\\");
            return objStringBuilder.ToString();
        }
        
        /// <summary>
        /// Generate the xml file name, it's a complete path
        /// </summary>
        /// <param name="path">the file path</param>
        /// <returns>return a complete xml file name</returns>
        public string GenXmlFileName(string path)
        {
            StringBuilder objStringBuilder = new StringBuilder();
            objStringBuilder.Append(path);
            objStringBuilder.Append(DateTime.Now.ToString("yyyyMMdd_hhmmssffftt"));
            objStringBuilder.Append(".xml");
            return objStringBuilder.ToString();
        }                
    }
}
