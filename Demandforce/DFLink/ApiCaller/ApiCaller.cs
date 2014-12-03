// -----------------------------------------------------------------------
// <copyright file="ApiCaller.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.ApiCaller
{
    using System;
    using System.Configuration;
    using System.IO;        
    using System.Text;
    using System.Xml;

    using Demandforce.DFLink.Common;
    using Demandforce.DFLink.Controller.Schedule;
    using Demandforce.DFLink.Controller.Task;
    using Demandforce.DFLink.Logger;    

    /// <summary>
    /// Calls API that extracts out data from PMS(Created by Delphi)
    /// </summary>
    public class ApiCaller : ITask, ITaskMaker
    {
        #region private variable
        /// <summary>
        /// A interface of API named "Initialize" that extract out data from PMS to memory
        /// </summary>
        private const string APIFuncInitialize = "Initialize";

        /// <summary>
        /// A interface of API named "GetData" that create a xml from memory
        /// </summary>
        private const string APIFuncGetData = "GetData";

        /// <summary>
        /// A interface of API named "Initialize" that free memory
        /// </summary>
        private const string APIFuncCleanup = "Cleanup";

        /// <summary>
        /// A interface of API named "DebugAPIToLog" that output the log
        /// </summary>
        private const string APIFuncDebugAPIToLog = "DebugAPIToLog";

        /// <summary>
        /// The Buffer size for "GetData" interface
        /// </summary>
        private const int BufferSize = 4096000;

        /// <summary>
        /// The identification for APICaller component and used into Logger.
        /// </summary>
        private const string LogTag = "APICaller";

        /// <summary>
        /// A instance of class StringBuilder that do string concatenate job
        /// </summary>
        private StringBuilder objStringBuilder = new StringBuilder();

        /// <summary>
        /// Indicates the License for a customer
        /// </summary>
        private string businessLicense = string.Empty;

        /// <summary>
        /// declare API's interface function: Initialize
        /// </summary>
        /// <param name="dataLocation">database connection parameters</param>
        /// <param name="apiPath">API path</param>
        /// <param name="version">API version</param>
        /// <param name="parameters">API parameters(provider)</param>
        /// <param name="dateFilter">API date filter</param>
        /// <param name="optionList">API option list</param>
        /// <returns>the customer count that API extract out from PMS</returns>
        private delegate int Initialize(string dataLocation, string apiPath, string version, string parameters, string dateFilter, string optionList);

        /// <summary>
        /// declare API's interface function: GetData
        /// </summary>
        /// <param name="outBuffer">A string buffer that stored the data(customer, appointment, transaction, etc.)</param>
        /// <param name="startRec">current record number</param>
        /// <returns>the record number after executed GetData() once</returns>
        private delegate int GetData(byte[] outBuffer, int startRec);

        /// <summary>
        /// declare API's interface function: Cleanup
        /// </summary>        
        private delegate void Cleanup();

        /// <summary>
        /// declare API's interface function: DebugAPIToLog
        /// </summary>    
        /// <param name="level">A string buffer that stored the data(customer, appointment, transaction, etc.)</param>
        /// <param name="version">API version</param>
        /// <param name="options">API option list</param>        
        private delegate void DebugAPIToLog(DebugLevel level, string version, string options);
        #endregion

        #region properties
        /// <summary>
        ///  Gets or sets the file name of API that extracts out data from PMS(Created by Delphi)
        /// </summary>        
        public string ApiPath { get; set; }

        /// <summary>
        ///  Gets or sets the data location for API that extracts out data from PMS(Created by Delphi)
        /// </summary>        
        public string DataLocation { get; set; }

        /// <summary>
        ///  Gets or sets the version for API that extracts out data from PMS(Created by Delphi)
        /// </summary>        
        public string Version { get; set; }

        /// <summary>
        ///  Gets or sets the Parameters for API that extracts out data from PMS(Created by Delphi)
        /// </summary>        
        public string Parameters { get; set; }

        /// <summary>
        ///  Gets or sets the DateFilter for API that extracts out data from PMS(Created by Delphi)
        /// </summary>        
        public string DateFilter { get; set; }

        /// <summary>
        ///  Gets or sets the OptionList for API that extracts out data from PMS(Created by Delphi)
        /// </summary>        
        public string OptionList { get; set; }

        /// <summary>
        ///  Gets or sets the OptionList for API that extracts out data from PMS(Created by Delphi)
        /// </summary>        
        public string ApiDebugLevel { get; set; }

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
                return "Calls API";
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

        #region methods
        /// <summary>
        /// Execute the request task
        /// </summary>
        public void Execute()
        {
            this.Call();         
            LogHelper.GetLoggerHandle().ReportStatus(LogTag, this.Id, 9, "Calls API done");
        }

        /// <summary>
        /// Initializes a new instance of the this class 
        /// with some arguments
        /// </summary>
        /// <param name="arguments">
        ///     The arguments for schedule
        /// </param>
        /// <returns>
        /// new instance of this class
        /// </returns>
        public ITask MakeTask(string arguments)
        {
            string license;
            string apiPath;
            string dataLocation;
            string version;
            string parameters;
            string dateFilter;
            string optionList;
            string apiDebugLevel = string.Empty;
            int taskId = -1;
            XmlNode tempNode;
            XmlNode paramNode;

            this.objStringBuilder.Clear();
            this.objStringBuilder.Append("the arguments to calls API: ");
            this.objStringBuilder.Append(arguments);
            LogHelper.GetLoggerHandle().Debug(LogTag, taskId, this.objStringBuilder.ToString());

            try
            {
                XmlDocument xmlParams = new XmlDocument();
                xmlParams.LoadXml(arguments);
                XmlNode rootNode = xmlParams.DocumentElement;
              
                license = Settings.Get("serverSettings", "LicenseId");

                tempNode = rootNode.SelectSingleNode("Id");
                taskId = int.Parse(tempNode.InnerText);

                tempNode = rootNode.SelectSingleNode("Parameters");

                paramNode = tempNode.SelectSingleNode("ApiPath");
                apiPath = paramNode.InnerText;

                paramNode = tempNode.SelectSingleNode("DataLocation");
                dataLocation = paramNode.InnerText;

                paramNode = tempNode.SelectSingleNode("Version");
                version = paramNode.InnerText;

                paramNode = tempNode.SelectSingleNode("APIParameters");
                parameters = paramNode.InnerText;

                paramNode = tempNode.SelectSingleNode("DateFilter");
                dateFilter = paramNode.InnerText;

                paramNode = tempNode.SelectSingleNode("OptionList");
                optionList = paramNode.InnerText;
            }
            catch (Exception e)
            {
                this.objStringBuilder.Clear();
                this.objStringBuilder.Append("The arguments is incorrect: ");
                this.objStringBuilder.Append(e.Message);
                LogHelper.GetLoggerHandle().Error(LogTag, taskId, this.objStringBuilder.ToString());
                return null;
            }

            try
            {
                paramNode = tempNode.SelectSingleNode("DebugLevel");
                apiDebugLevel = paramNode.InnerText;
            }
            catch
            {
                apiDebugLevel = string.Empty;
            }            

            var caller = new ApiCaller()
            {
                businessLicense = license,
                Id = taskId,
                ApiPath = apiPath,
                DataLocation = dataLocation,
                Version = version,
                Parameters = parameters,
                DateFilter = dateFilter,
                OptionList = optionList,
                ApiDebugLevel = apiDebugLevel
            };
            return caller;
        }

        /// <summary>
        /// Load API via Invoker's construction function
        /// </summary>
        /// <returns>return result: null-load failed; not null-load successful</returns>
        private DllInvoker LoadApiDll()
        {
            var invoker = new DllInvoker(this.ApiPath);
            return invoker;            
        }                

        /// <summary>
        /// Calls the API's Initialize() function 
        /// </summary>
        /// <param name="invoker">
        ///     the handle of dynamic link library 
        /// </param>
        /// <returns>return the customer count extracted out</returns>
        private int CallApiInitialize(DllInvoker invoker)
        {
            var funcInitialize = (Initialize)invoker.Invoke(APIFuncInitialize, typeof(Initialize));
            if (funcInitialize == null)
            {
                this.objStringBuilder.Clear();
                this.objStringBuilder.Append("Invokes ");
                this.objStringBuilder.Append(this.ApiPath);
                this.objStringBuilder.Append("failed!(GetProcAddress('Initialize') return null)");
                LogHelper.GetLoggerHandle().Error(LogTag, this.Id, this.objStringBuilder.ToString());

                return 0;
            }

            int custCount = funcInitialize(this.DataLocation, this.ApiPath, this.Version, this.Parameters, this.DateFilter, this.OptionList);
            return custCount;
        }

        /// <summary>
        /// Calls the API's GetData() function 
        /// </summary>
        /// <param name="invoker">
        ///     the handle of dynamic link library 
        /// </param>        
        private void CallApiGetData(DllInvoker invoker)
        {
            var funcGetData = (GetData)invoker.Invoke(APIFuncGetData, typeof(GetData));
            if (funcGetData == null)
            {
                this.objStringBuilder.Clear();
                this.objStringBuilder.Append("Invokes ");
                this.objStringBuilder.Append(this.ApiPath);
                this.objStringBuilder.Append("failed!(GetProcAddress('GetData') return null)");
                LogHelper.GetLoggerHandle().Error(LogTag, this.Id, this.objStringBuilder.ToString());

                return;
            }

            // Define a byte array with BufferSize length 
            byte[] outBuffer = new byte[BufferSize];

            PmsDataHandler dataHandler = new PmsDataHandler();
            int lastRec = funcGetData(outBuffer, 0);
            if (lastRec > 0)
            {
                // Generate a complete xml file name
                string xmlPath = dataHandler.GenXmlFilePath();                                
                dataHandler.MakePathExist(xmlPath);
                string extractFileName = dataHandler.GenXmlFileName(xmlPath);
                string xmlContent = Encoding.ASCII.GetString(outBuffer).TrimEnd('\0');

                // 1. add header
                this.AddHeaderToFile(extractFileName);
                    
                // 2. add body: put data into xml file via PmsDataHandler.AppendToFile method
                dataHandler.AppendToFile(extractFileName, xmlContent); 
                while (lastRec > 0)
                {
                    lastRec = funcGetData(outBuffer, lastRec++);
                    xmlContent = Encoding.ASCII.GetString(outBuffer).TrimEnd('\0');
                    dataHandler.AppendToFile(extractFileName, xmlContent); 
                }

                // 3. add footer
                this.AddFooterToFile(extractFileName);
            }
            else
            {
                this.objStringBuilder.Clear();
                this.objStringBuilder.Append("Invokes ");
                this.objStringBuilder.Append(this.ApiPath);
                this.objStringBuilder.Append("failed!('GetData--record No returned is not > 0')");
                LogHelper.GetLoggerHandle().Error(LogTag, this.Id, this.objStringBuilder.ToString());                
            }
        }

        /// <summary>
        /// Calls the API's Cleanup() function 
        /// </summary>     
        /// <param name="invoker">
        ///     the handle of dynamic link library 
        /// </param>    
        private void CallApiCleanup(DllInvoker invoker)
        {
            var funcCleanup = (Cleanup)invoker.Invoke(APIFuncCleanup, typeof(Cleanup));
            if (funcCleanup == null)
            {
                this.objStringBuilder.Clear();
                this.objStringBuilder.Append("Invokes ");
                this.objStringBuilder.Append(this.ApiPath);
                this.objStringBuilder.Append("failed!(GetProcAddress('Cleanup') return null)");
                LogHelper.GetLoggerHandle().Error(LogTag, this.Id, this.objStringBuilder.ToString());

                return;
            }

            funcCleanup();
        }

        /// <summary>
        /// Calls the API's DebugAPIToLog() function 
        /// </summary>     
        /// <param name="invoker">
        ///     the handle of dynamic link library 
        /// </param>    
        private void CallApiDebugAPIToLog(DllInvoker invoker)
        {
            var funcDebugApiToLog = (DebugAPIToLog)invoker.Invoke(APIFuncDebugAPIToLog, typeof(DebugAPIToLog));
            if (funcDebugApiToLog == null)
            {
                this.objStringBuilder.Clear();
                this.objStringBuilder.Append("Invokes ");
                this.objStringBuilder.Append(this.ApiPath);
                this.objStringBuilder.Append("failed!(GetProcAddress('DebugAPIToLog') return null)");
                LogHelper.GetLoggerHandle().Error(LogTag, this.Id, this.objStringBuilder.ToString());  
              
                return;
            }

            funcDebugApiToLog(ApiAssistor.StrToDebugLevel(this.ApiDebugLevel), this.Version, this.OptionList);            
        }

        /// <summary>
        /// Calls the API that extracts out data from PMS(Created by Delphi) 
        /// </summary>
        /// <returns>call result: True-success; False-failed</returns>
        private bool Call()
        {
            // 1. call private method "LoadApiDll()" to load API dll
            DllInvoker invoker = this.LoadApiDll();
            if (invoker == null)
            {
                this.objStringBuilder.Clear();
                this.objStringBuilder.Append("Invokes ");
                this.objStringBuilder.Append(this.ApiPath);
                this.objStringBuilder.Append("failed!(LoadLibrary return null)");
                LogHelper.GetLoggerHandle().Error(LogTag, this.Id, this.objStringBuilder.ToString());
                
                return false;
            }

            // Notity api to run under debug mode if api implement debug mode
            if (ApiAssistor.IsDebugMode(ApiAssistor.StrToDebugLevel(this.ApiDebugLevel)))
            {
                this.CallApiDebugAPIToLog(invoker);
            }

            try
            {
                // 2. call private method "CallApiInitialize()" to Invoke dll's Initialize() function            
                int custCount = this.CallApiInitialize(invoker);
                if (custCount > 0)
                {
                    this.objStringBuilder.Clear();
                    this.objStringBuilder.Append("Invokes 'Initialize()' successful! The customers count is: ");
                    this.objStringBuilder.Append(custCount);                    
                    LogHelper.GetLoggerHandle().Info(LogTag, this.Id,  this.objStringBuilder.ToString());

                    // 3. call private method "CallApiGetData()" to Invoke dll's "GetData()" function     
                    this.CallApiGetData(invoker);
                }
                else
                {
                    // if no records returned then output to log
                    LogHelper.GetLoggerHandle().Info(LogTag, this.Id, "No records returned!");
                }
            }
            finally
            {
                // 4. call private method "CallApiCleanup()" to Invoke dll's "Cleanup()" function
                this.CallApiCleanup(invoker);
            }

            return true;
        }

        /// <summary>
        /// Add additional header information to the xml file
        /// </summary>
        /// <param name="fileName">
        ///     the xml file 
        /// </param>  
        /// <returns>call result: True-success; False-failed</returns>
        private bool AddHeaderToFile(string fileName)
        {
            this.objStringBuilder.Clear();

            // "xml" elemet
            this.objStringBuilder.AppendLine(@"<?xml version=""1.0""?>");

            // "DemandForce" element and its attributes
            this.objStringBuilder.Append("<DemandForce");
            this.objStringBuilder.Append(@" licenseKey=""");
            this.objStringBuilder.Append(this.businessLicense);
            this.objStringBuilder.Append(@"""");
            this.objStringBuilder.Append(@" dFAPI=""");
            this.objStringBuilder.Append(Path.GetFileName(this.ApiPath));
                this.objStringBuilder.Append(@"""");
            this.objStringBuilder.Append(@" dFAPIVersion=""");
            this.objStringBuilder.Append(this.Version);
            this.objStringBuilder.Append(@"""");
            this.objStringBuilder.Append(@" dataLocation=""");
            this.objStringBuilder.Append(this.DataLocation);
            this.objStringBuilder.AppendLine(@""">");

            // "Business" elemet
            this.objStringBuilder.AppendLine("<Business>");

            // "Extract" elemet and its attributes
            this.objStringBuilder.Append("<Extract");
            this.objStringBuilder.Append(@" managementSystemName=""");
            this.objStringBuilder.Append(Path.GetFileName(this.ApiPath));
            this.objStringBuilder.Append(@"""");
            this.objStringBuilder.Append(@" managementSystemVersion=""");
            this.objStringBuilder.Append(this.Version);
            this.objStringBuilder.Append(@"""");
            this.objStringBuilder.Append(@" manual=""1""");
            this.objStringBuilder.AppendLine(">");
            this.objStringBuilder.AppendLine("</Extract>");

            PmsDataHandler dataHandler = new PmsDataHandler();
            dataHandler.AppendToFile(fileName, this.objStringBuilder.ToString());

            return true;
        }

        /// <summary>
        /// Add additional footer information to the xml file
        /// </summary>
        /// <param name="fileName">
        ///     the xml file 
        /// </param>          
        private void AddFooterToFile(string fileName)
        {
            this.objStringBuilder.Clear();
            this.objStringBuilder.AppendLine("</Business>");
            this.objStringBuilder.AppendLine("</DemandForce>");

            PmsDataHandler dataHandler = new PmsDataHandler();
            dataHandler.AppendToFile(fileName, this.objStringBuilder.ToString());            
        }
        #endregion 
    }
}
