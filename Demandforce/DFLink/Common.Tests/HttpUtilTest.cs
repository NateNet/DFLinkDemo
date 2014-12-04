using Demandforce.DFLink.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UploadFileTest.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   UploadFileTest 
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandfore.DFLink.Common.Tests
{
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Xml;
    using Demandforce.DFLink.Common;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// UploadFileTest 
    /// </summary>
    [TestClass]
    public class HttpUtilTest
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets test context
        /// </summary>
        public TestContext TestContext { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// A test for Download
        /// </summary>
        [TestMethod]
        public void DownloadTest()
        {
            var url = "http://www.demandforce.com/_assets/images/logos/logo-intuit-df-657.png"; // TODO: Initialize to an appropriate value
            var fileName = AppDomain.CurrentDomain.BaseDirectory + "\\logo-intuit-df-657.png"; // TODO: Initialize to an appropriate value
            var expected = true; // TODO: Initialize to an appropriate value
            var actual = HttpUtil.DownloadFile(url, fileName);
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///     HttpPostData
        /// </summary>
        [TestMethod]
        public void HttpPostDataTest()
        {
            var fileName = System.Environment.CurrentDirectory + "\\upload.xml";
            var zipName = System.Environment.CurrentDirectory + "\\upload.zip";
            var license = "71C304D6-6E75-2370-0B49-8794CE8452D9";
            var fileInfo = new FileInfo(fileName);
            var fileStream = fileInfo.AppendText();
            fileStream.WriteLine(@"<?xml version=""1.0""?>");
            fileStream.WriteLine(@"<DemandForce licenseKey=""71C304D6-6E75-2370-0B49-8794CE8452D9"" dFLinkVersion=""3.7.119"" >");
            fileStream.WriteLine(@"<Business>");
            fileStream.WriteLine(@"    <Customer id=""7"" parentId=""7"" provider=""3"" firstVisit=""2009-08-04T07:26:05Z"" insuranceType=""Other"">");
            fileStream.WriteLine(@"      <Demographics firstName=""Maria"" lastName=""Cintron"" gender=""2"" birthday=""1984-04-01T00:00:00Z"" />");
            fileStream.WriteLine(@"    </Customer>");
            fileStream.WriteLine(@"</Business>");
            fileStream.WriteLine(@"</DemandForce>");
            fileStream.Flush();
            fileStream.Close();

            FileCompression.Compress(fileInfo, zipName, license, null);

            var zipInfo = new FileInfo(zipName);
            long size = zipInfo.Length;

            var url = "https://dflink.sandbox.demandforced3.com/upload/1.0/xml.jsp";
            var timeOut = 30 * 1000;
            var fileKeyName = "upload";
            var filePathName = zipName;
            NameValueCollection stringDict = new NameValueCollection();
            stringDict.Add("user", "optometry_test@demandforce.com");
            stringDict.Add("pass", string.Empty);
            stringDict.Add("license", license);

            string actual = HttpUtil.PostFormData(url, timeOut, fileKeyName, filePathName, stringDict);

            Assert.IsTrue(actual.Contains("zip with size of " + size.ToString() + " bytes.  BusinessId ="));
        }

        #endregion

        /// <summary>
        /// Test for PostJsonForXml 
        ///</summary>
        [TestMethod()]
        public void PostJsonForXmlTest()
        {
            string url = @"http://172.18.3.100/task/get";
            string paramsList = @"{'LicenseKey':'xxxxx-xxxxxx'}";
            string expected = "<Tasks></Tasks>";
            string actual = HttpUtil.PostJsonForXml(url, paramsList);
            Assert.AreEqual(expected, actual);

            paramsList = @"{'LicenseKey':'61D13359-C3A6-301C-8B17-9283A2188A9A'}";
            actual = HttpUtil.PostJsonForXml(url, paramsList);
            
            XmlDocument xmlDocument = new XmlDocument();
            bool b = true;
            try
            {
                xmlDocument.LoadXml(actual);
            }
            catch (Exception e)
            {
                b = false;
            }

            Assert.IsTrue(b);
        }
    }
}