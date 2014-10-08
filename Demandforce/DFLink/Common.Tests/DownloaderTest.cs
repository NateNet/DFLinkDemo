// -----------------------------------------------------------------------
// <copyright file="DownloaderTest.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandfore.DFLink.Common.Tests
{
    using System;
    using Demandforce.DFLink.Common;
    using Microsoft.VisualStudio.TestTools.UnitTesting;        
    
    /// <summary>
    /// This is a test class for DownloaderTest and is intended
    /// to contain all DownloaderTest Unit Tests
    /// </summary>
    [TestClass]
    public class DownloaderTest
    {
        /// <summary>
        /// A test for Download
        /// </summary>
        [TestMethod]
        public void DownloadTest()
        {
            Downloader target = new Downloader(); // TODO: Initialize to an appropriate value
            string url = "http://www.demandforce.com/_assets/images/logos/logo-intuit-df-657.png"; // TODO: Initialize to an appropriate value
            string fileName = AppDomain.CurrentDomain.BaseDirectory + "\\logo-intuit-df-657.png"; ; // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Download(url, fileName);
            Assert.AreEqual(expected, actual);            
        }
    }
}
