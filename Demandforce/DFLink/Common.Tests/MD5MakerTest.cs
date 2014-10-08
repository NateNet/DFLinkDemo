// -----------------------------------------------------------------------
// <copyright file="MD5MakerTest.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandfore.DFLink.Common.Tests
{
    using System;
    using Demandforce.DFLink.Common;
    using Microsoft.VisualStudio.TestTools.UnitTesting;    
    
    /// <summary>
    /// This is a test class for MD5MakerTest and is intended
    /// to contain all MD5MakerTest Unit Tests
    /// </summary>
    [TestClass]
    public class MD5MakerTest
    {
        /// <summary>
        /// A test for GetMD5ForFile
        /// </summary>
        [TestMethod]
        public void GetMD5ForFileTest()
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + "\\MD5 test.txt"; // TODO: Initialize to an appropriate value
            string expected = "E26AC8BB85B704AC5F85DEA83A5ED99B"; // TODO: Initialize to an appropriate value
            string actual;
            actual = MD5Maker.GetMD5ForFile(fileName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for GetMD5ForString
        /// </summary>
        [TestMethod]
        public void GetMD5ForStringTest()
        {
            string value = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = MD5Maker.GetMD5ForString(value);
            Assert.AreEqual(expected, actual);

            value = "md5 test";
            expected = "2E5F9458BCD27E3C2B5908AF0B91551A";
            actual = MD5Maker.GetMD5ForString(value);
            Assert.AreEqual(expected, actual);
        }
    }
}
