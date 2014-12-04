// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileCompressionTest.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   FileCompressionTest 
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandfore.DFLink.Common.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Demandforce.DFLink.Common;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     FileCompressionTest
    /// </summary>
    [TestClass]
    public class FileCompressionTest
    {
        #region Public Properties

        /// <summary>
        /// text context
        /// </summary>
        public TestContext TestContext { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Compress
        /// </summary>
        [TestMethod]
        public void CompressTest()
        {
            string fileName = Environment.CurrentDirectory + "\\" + "TestFile.txt";
            var fileInfo = new FileInfo(fileName);
            StreamWriter streamWriter = fileInfo.CreateText();
            streamWriter.WriteLine("AAAAA");
            streamWriter.Flush();
            streamWriter.Close();

            string zipFileName = Environment.CurrentDirectory + "\\" + "TestFile.zip";
            string password = "a";
            bool expected = true;
            bool actual;
            actual = FileCompression.Compress(fileInfo, zipFileName, password, null);
            Assert.AreEqual(expected, actual);

            fileInfo = new FileInfo(zipFileName);
            Assert.IsTrue(fileInfo.Exists);
        }

        /// <summary>
        ///     Compress
        /// </summary>
        [TestMethod]
        public void CompressTest1()
        {
            var fileNames = new List<FileInfo>();
            string filePath = Environment.CurrentDirectory + "\\";
            IEnumerable<int> nums = Enumerable.Range(1, 20);
            foreach (int i in nums)
            {
                string fileName = filePath + i + ".txt";
                var fileInfo = new FileInfo(fileName);
                StreamWriter streamWriter = fileInfo.CreateText();
                streamWriter.WriteLine(i.ToString());
                streamWriter.Flush();
                streamWriter.Close();
                fileNames.Add(fileInfo);
            }

            string zipFileName = Environment.CurrentDirectory + "\\" + "outData.zip";
            string password = "abcde";
            int? compressionLevel = 9;
            int? sleepTimer = 200;
            bool expected = true; 
            bool actual;
            actual = FileCompression.Compress(fileNames, zipFileName, password, compressionLevel, sleepTimer);
            Assert.AreEqual(expected, actual);

            var file = new FileInfo(zipFileName);
            Assert.IsTrue(file.Exists);
        }

        #endregion
    }
}