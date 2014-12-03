// -----------------------------------------------------------------------
// <copyright file="ApiCallerTest.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ApiCaller.Test
{
    using System;
    using System.Text;
    using Demandforce.DFLink.ApiCaller;
    using Demandforce.DFLink.Controller;
    using Demandforce.DFLink.Controller.Task;    
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for class APICaller
    /// </summary>
    [TestClass]
    public class ApiCallerTest
    {
        /// <summary>
        /// A test for Execute
        /// </summary>
        [TestMethod]
        public void ExecuteTest()
        {
            ApiCaller target = new ApiCaller(); // TODO: Initialize to an appropriate value
            target.Execute();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        /// A test for MakeTask
        /// </summary>
        [TestMethod]
        public void MakeTaskTest()
        {
            ApiCaller target = new ApiCaller(); // TODO: Initialize to an appropriate value
            string arguments = string.Empty; // TODO: Initialize to an appropriate value
            ITask actual;
            StringBuilder objStringBuilder = new StringBuilder();

            
            // case 1: d3ofmapi.dll, test for GetData() with old version
            objStringBuilder.Append("<Task>" + Environment.NewLine);
            objStringBuilder.Append("<Id>901</Id>" + Environment.NewLine);
            objStringBuilder.Append("<Parameters>" + Environment.NewLine);
            objStringBuilder.Append("<ApiPath>C:\\test\\d3ofmapi.dll</ApiPath>" + Environment.NewLine);
            objStringBuilder.Append("<DataLocation>C:\\test\\OMATE.MDB</DataLocation>" + Environment.NewLine);
            objStringBuilder.Append("<Version>7.4</Version>" + Environment.NewLine);
            objStringBuilder.Append("<APIParameters>1,*</APIParameters>" + Environment.NewLine);
            objStringBuilder.Append("<DateFilter>1/1/2000</DateFilter>" + Environment.NewLine);
            objStringBuilder.Append("<OptionList>OptionActiveOnly=True</OptionList>" + Environment.NewLine);
            objStringBuilder.Append("</Parameters>" + Environment.NewLine);
            objStringBuilder.Append("</Task>");
            arguments = objStringBuilder.ToString();

            actual = target.MakeTask(arguments);
            Assert.IsNotNull(actual);
            Assert.AreEqual("C:\\test\\d3ofmapi.dll", ((ApiCaller)actual).ApiPath);
            Assert.AreEqual("C:\\test\\OMATE.MDB", ((ApiCaller)actual).DataLocation);
            
           
            // case 2: d3mwrapi.dll, test for GetData() with new version
            //objStringBuilder.Append("<Task>" + Environment.NewLine);
            //objStringBuilder.Append("<Id>901</Id>" + Environment.NewLine);
            //objStringBuilder.Append("<Parameters>" + Environment.NewLine);
            //objStringBuilder.Append("<ApiPath>C:\\test\\d3mwrapi.dll</ApiPath>" + Environment.NewLine);
            //objStringBuilder.Append("<DataLocation>C:\\test\\RocketServiceCenter2011\\Data</DataLocation>" + Environment.NewLine);
            //objStringBuilder.Append("<Version>2007</Version>" + Environment.NewLine);
            //objStringBuilder.Append("<APIParameters>*</APIParameters>" + Environment.NewLine);
            //objStringBuilder.Append("<DateFilter>1/1/2000</DateFilter>" + Environment.NewLine);
            //objStringBuilder.Append("<OptionList>OptionActiveOnly=True</OptionList>" + Environment.NewLine);
            //objStringBuilder.Append("<DebugLevel>Debug</DebugLevel>" + Environment.NewLine);
            //objStringBuilder.Append("</Parameters>" + Environment.NewLine);
            //objStringBuilder.Append("</Task>");
            //arguments = objStringBuilder.ToString();

            //actual = target.MakeTask(arguments);
            //Assert.IsNotNull(actual);
            //Assert.AreEqual("C:\\test\\d3mwrapi.dll", ((ApiCaller)actual).ApiPath);            
            
             
            actual.Execute();
        }
    }
}
