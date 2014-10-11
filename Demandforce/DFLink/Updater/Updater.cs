// -----------------------------------------------------------------------
// <copyright file="Updater.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Updater
{
    using System;
    using System.IO;    
    using System.Text;
    using System.Xml;

    using Demandforce.DFLink.Common;
    using Demandforce.DFLink.Controller.Schedule;
    using Demandforce.DFLink.Controller.Task;
    using Demandforce.DFLink.Logger;

    /// <summary>
    /// It can upgrade/update file
    /// </summary>
    public class Updater : ITask, ITaskMaker
    {
        #region private variable
        /// <summary>
        /// DFUpdateInstruction.xml is from server and it stored the newest file information(md5, version)
        /// </summary>
        private const string InstructionFile = "DFUpdateInstruction.xml";

        /// <summary>
        /// The name of the root node of the DFUpdateInstruction.xml
        /// </summary>
        private const string RootInInstructionFile = "DFUPDATEINSTRUCTION";

        /// <summary>
        /// Stored some parameters for upload, 2 way.
        /// </summary>
        private const string BusinessFile = "DFLink.xml";

        /// <summary>
        /// The identification for Updater component and used into Logger.
        /// </summary>
        private const string LogTag = "Updater";

        /// <summary>
        /// A instance of class XmlDocument associated with DFUpdateInstruction.xml
        /// </summary>
        private XmlDocument xmlInstruction = new XmlDocument();        

        /// <summary>
        /// Stored the API name from server
        /// </summary>
        private string apiName;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Updater"/> class.
        /// </summary>
        public Updater()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Updater"/> class.
        /// </summary>
        /// <param name="mode">refer to property Mode</param>        
        /// <param name="businessLocation">refer to property BusinessConfigFileLocation</param>
        /// <param name="fileName">refer to property FileName</param>
        /// <param name="fileServerPath">refer to property FileServerPath</param>
        /// <param name="fileLocalPath">refer to property FileLocalPath</param>
        public Updater(int mode, int businessLocation, string fileName, string fileServerPath, string fileLocalPath)
        {
            this.Mode = mode;
            this.BusinessConfigFileLocation = businessLocation;
            this.FileName = fileName;
            this.FileServerPath = fileServerPath;
            this.FileLocalPath = fileLocalPath;
        }
        #endregion

        #region properties
        /// <summary>
        ///  Gets or sets the file want to update without the path
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets a value that must be 0 or 1. 
        /// 0: download the file indicated by "Filename"; 1: download all files for a business client 
        /// </summary>
        public int Mode { get; set; }

        /// <summary>
        /// Gets or sets a value that must be 0 or 1.
        /// 0: from Local; 1: from Server
        /// </summary>
        public int BusinessConfigFileLocation { get; set; }

        /// <summary>
        /// Gets or sets a url that indicates the server path        
        /// </summary>
        public string FileServerPath { get; set; }

        /// <summary>
        /// Gets or sets a url that indicates the local path
        /// </summary>
        public string FileLocalPath { get; set; }

        /// <summary>
        /// Gets or sets the task Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets the task name
        /// </summary>
        public string Name 
        {
            get
            {
                return "Update File";
            }
        }

        /// <summary>
        /// Gets or sets the task description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the schedule is for task to execute
        /// </summary>
        public ISchedule Schedule { get; set; }
        #endregion

        /// <summary>
        /// Execute the request task
        /// </summary>
        public void Execute()
        {
            this.Update();
            LogHelper.GetLoggerHandle().ReportStatus("Invoker", this.Id, 9, "Update done");
        }

        /// <summary>
        /// Initializes a new instance of the class DFUpdate
        /// with some arguments
        /// </summary>
        /// <param name="arguments">
        ///     The arguments for schedule
        /// </param>
        /// <returns>
        /// new instance of update task
        /// </returns>
        public ITask MakeTask(string arguments)
        {            
            int mode;
            int businessConfigFileLocation;
            string fileName;
            string fileServerPath;
            string fileLocalPath;
            string sapiName;
            int taskId;

            XmlDocument xmlParams = new XmlDocument();
            xmlParams.LoadXml(arguments);
            XmlNode rootNode = xmlParams.DocumentElement;
            XmlNode tempNode;
            XmlNode paramNode;

            tempNode = rootNode.SelectSingleNode("Id");
            taskId = int.Parse(tempNode.InnerText);
            
            tempNode = rootNode.SelectSingleNode("Parameters");

            paramNode = tempNode.SelectSingleNode("Mode");
            mode = int.Parse(paramNode.InnerText);

            paramNode = tempNode.SelectSingleNode("BusinessConfigFileLocation");
            businessConfigFileLocation = int.Parse(paramNode.InnerText);

            paramNode = tempNode.SelectSingleNode("FileName");
            fileName = paramNode.InnerText;

            paramNode = tempNode.SelectSingleNode("FileServerPath");
            fileServerPath = paramNode.InnerText;

            paramNode = tempNode.SelectSingleNode("ApiName");
            sapiName = paramNode.InnerText;

            fileLocalPath = AppDomain.CurrentDomain.BaseDirectory;

            LogHelper.GetLoggerHandle().Debug(LogTag, taskId, "Update arguments from server: " + arguments);
            // LogHelper.GetLoggerHandle().Debug(LogTag, this.Id, arguments); 

            var objDfUpdate = new Updater
            {
                Id = taskId,
                Mode = mode,
                BusinessConfigFileLocation = businessConfigFileLocation,
                FileName = fileName,
                FileServerPath = fileServerPath,
                FileLocalPath = fileLocalPath,
                apiName = sapiName
            };
            return objDfUpdate;
        }        
        
        /// <summary>
        /// Updater the core components
        /// </summary>
        /// <returns>Update result: True-success; False-failed</returns>
        public bool Update()
        {
            if (this.Mode == 0)
            {
                return this.SingleUpdate();
            }
            else
            {
                return this.FullUpdate();                
            }

        }

        /// <summary>
        /// Get a full name for a file         
        /// </summary>
        /// <param name="filePath">The file path</param>
        /// <param name="fileName">The file name</param>
        /// <returns>A full name for a file</returns>
        private string GetFullFileName(string filePath, string fileName)
        {
            StringBuilder objStringBuilder = new StringBuilder();
            objStringBuilder.Append(filePath);
            objStringBuilder.AppendFormat("/{0}", fileName);

            return objStringBuilder.ToString();
        }

        /// <summary>
        /// Create a instance of class XmlDocument associated with "business config xml"
        /// </summary>
        /// <returns>A instance of class XmlDocument associated with "business config xml"</returns>
        private XmlDocument CreateXmlForBusiness()
        {
            XmlDocument xmlBusiness = new XmlDocument();
            if (this.BusinessConfigFileLocation == 0)
            {
                xmlBusiness.Load(this.GetFullFileName(this.FileLocalPath, BusinessFile));
            }
            else
            {
                // TODO
            }

            return xmlBusiness;
        }

        /// <summary>
        /// Get the API name from "business config xml"
        /// </summary>
        /// <param name="xmlBusiness">a instance of class XmlDocument associated with "business config xml"</param>
        /// <returns>The API name</returns>
        private string GetApiNameFromBusiness(XmlDocument xmlBusiness)
        {
            string apiName;
            try
            {
                XmlNode rootNode = xmlBusiness.DocumentElement;
                rootNode = rootNode.SelectSingleNode("BusinessList");
                rootNode = rootNode.SelectSingleNode("Business");
                rootNode = rootNode.SelectSingleNode("APINAME");
                apiName = rootNode.InnerText;
            }
            catch (Exception)
            {
                apiName = string.Empty;
                LogHelper.GetLoggerHandle().Error(LogTag, this.Id, "Format error in DFLink.xml");
            }

            return apiName;
        }

        /// <summary>
        /// Updater the core components
        /// </summary>
        private void UpdateCoreComponents()
        {
            XmlNode rootNode = this.xmlInstruction.DocumentElement;
            XmlNode currentNode;
            string fileName;
            string md5;

            if (rootNode.Name != RootInInstructionFile)
            {
                LogHelper.GetLoggerHandle().Error(LogTag, this.Id, "Format error in DFUpdateInstruction.xml");
            }
            else
            {
                rootNode = rootNode.SelectSingleNode("CORECOMPONENTS");
                if (rootNode.ChildNodes.Count <= 0)
                {
                    return;
                }

                for (int i = 0; i < rootNode.ChildNodes.Count; i++)
                {
                    currentNode = rootNode.ChildNodes[i];
                    fileName = currentNode.Attributes["FILENAME"].Value;
                    if (currentNode.Name == "UPDATE")
                    {
                        md5 = currentNode.Attributes["MD5"].Value;
                        this.DoUpdate(fileName, md5);
                    }
                }
            }
        }

        /// <summary>
        /// Updater the API
        /// </summary>
        private void UpdateAPIComponents()
        {
            XmlNode rootNode = this.xmlInstruction.DocumentElement;
            XmlNode currentNode;
            string fileName;
            string md5;
            string apiName;
            XmlDocument xmlDFlink;

            if (rootNode.Name != RootInInstructionFile)
            {
                LogHelper.GetLoggerHandle().Error(LogTag, this.Id, "Format error in DFUpdateInstruction.xml");
            }
            else
            {
                if (this.BusinessConfigFileLocation == 0)
                {                                    
                    // Create a XmlDocument object to relate the DFLink.xml based on the property "BusinessConfigFileLocation"
                    xmlDFlink = this.CreateXmlForBusiness();
                    apiName = this.GetApiNameFromBusiness(xmlDFlink);
                }
                else
                {
                    apiName = this.apiName;
                }

                rootNode = rootNode.SelectSingleNode("APICOMPONENTS");
                if (rootNode.ChildNodes.Count <= 0 || apiName == string.Empty)
                {
                    return;
                }

                for (int i = 0; i < rootNode.ChildNodes.Count; i++)
                {
                    currentNode = rootNode.ChildNodes[i];
                    if (currentNode.Name == "UPDATE")
                    {
                        fileName = currentNode.Attributes["FILENAME"].Value;
                        if (fileName == apiName)
                        {
                            md5 = currentNode.Attributes["MD5"].Value;
                            this.DoUpdate(fileName, md5);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Updater a file via Downloader class
        /// </summary>
        /// <param name="updateFileName">The file want to update without the path</param>
        /// <param name="md5InServer">The md5 value from DFUpdateInstruction.xml</param>
        /// <returns>Updater result: True-success; False-failed</returns>
        private bool DoUpdate(string updateFileName, string md5InServer)
        {
            string fullFileName = this.GetFullFileName(this.FileLocalPath, updateFileName);
            string url = this.GetFullFileName(this.FileServerPath, updateFileName);
            bool isUpdated = false;
            string md5InLocal = string.Empty;
            var objDownload = new Downloader();

            if (File.Exists(fullFileName))
            {
                md5InLocal = MD5Maker.GetMD5ForFile(fullFileName);
                if (md5InLocal != md5InServer)
                {
                    isUpdated = objDownload.Download(url, fullFileName);
                }
            }
            else
            {
                isUpdated = objDownload.Download(url, fullFileName);
            }

            return isUpdated;
        }

        /// <summary>
        /// Updater a file
        /// </summary>
        /// <returns>Updater result: True-success; False-failed</returns>
        private bool SingleUpdate()
        {
            string url = this.GetFullFileName(this.FileServerPath, this.FileName);
            string fileName = this.GetFullFileName(this.FileLocalPath, this.FileName);
            var objDownload = new Downloader();

            return objDownload.Download(url, fileName);
        }

        /// <summary>
        /// Updater all files for a business client. Steps:
        /// 1. Download the DFUpdateInstruction.xml
        /// 2. Locate to "CORECOMPONENTS" node and upgrade every file if needs
        /// 3. Locate to "APICOMPONENTS" node and find out the API node based on DFLink.xml; Upgrade the API if needs
        /// </summary>  
        /// <returns>Updater result: True-success; False-failed</returns>
        private bool FullUpdate()
        {
            string url = this.GetFullFileName(this.FileServerPath, InstructionFile);
            string fileName = this.GetFullFileName(this.FileLocalPath, InstructionFile);
            bool resultDownload = false;
            var objDownload = new Downloader();

            resultDownload = objDownload.Download(url, fileName);
            if (resultDownload)
            {
                try
                {
                    this.xmlInstruction.Load(fileName);
                    this.UpdateCoreComponents();
                    this.UpdateAPIComponents();
                }
                catch (Exception)
                {
                    LogHelper.GetLoggerHandle().Error(LogTag, this.Id, "Format error in DFUpdateInstruction.xml");
                    resultDownload = false;
                }
            }

            return resultDownload;
        }
    }
}
