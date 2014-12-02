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
    using System.Collections.Specialized;
    using System.Globalization;
    using System.IO;

    using Demandforce.DFLink.Common;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     UploadFileTest 
    /// </summary>
    [TestClass]
    public class UploadFileTest
    {
        #region Public Properties

        /// <summary>
        ///     test context
        /// </summary>
        public TestContext TestContext { get; set; }

        #endregion

        // 编写测试时，还可使用以下特性:
        // 使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext)
        // {
        // }
        // 使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup()
        // {
        // }
        // 使用 TestInitialize 在运行每个测试前先运行代码
        // [TestInitialize()]
        // public void MyTestInitialize()
        // {
        // }
        // 使用 TestCleanup 在运行完每个测试后运行代码
        // [TestCleanup()]
        // public void MyTestCleanup()
        // {
        // }
        #region Public Methods and Operators

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

            string actual;
            actual = UploadFile.HttpPostData(url, timeOut, fileKeyName, filePathName, stringDict);

            Assert.IsTrue(actual.Contains("zip with size of " + size.ToString() + " bytes.  BusinessId ="));
        }

        #endregion
    }
}