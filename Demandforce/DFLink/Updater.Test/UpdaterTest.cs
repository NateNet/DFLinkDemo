// -----------------------------------------------------------------------
// <copyright file="UpdaterTest.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Updater.Test
{
    using System;
    using System.Text;
    using Demandforce.DFLink.Controller;
    using Demandforce.DFLink.Controller.Task;
    using Demandforce.DFLink.Updater;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    
    /// <summary>
    /// This is a test class for UpdaterTest and is intended
    /// to contain all UpdaterTest Unit Tests
    /// </summary>
    [TestClass]
    public class UpdaterTest
    {
        /// <summary>
        /// A test for Execute
        /// </summary>
        [TestMethod]
        public void ExecuteTest()
        {
            Updater target = new Updater(); // TODO: Initialize to an appropriate value
            target.Execute();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        /// A test for MakeTask
        /// </summary>
        [TestMethod]
        public void MakeTaskTest()
        {
            Updater target = new Updater(); // TODO: Initialize to an appropriate value
            string arguments = string.Empty; // TODO: Initialize to an appropriate value
            ITask actual;
            bool fileexists = false; 
            StringBuilder objStringBuilder = new StringBuilder();

            objStringBuilder.Append("<Tasks>" + Environment.NewLine);
            objStringBuilder.Append("<Parameters>" + Environment.NewLine);
            objStringBuilder.Append("<Mode>1</Mode>" + Environment.NewLine);
            objStringBuilder.Append("<BusinessConfigFileLocation>1</BusinessConfigFileLocation>" + Environment.NewLine);
            objStringBuilder.Append("<FileName>DFUpdateInstruction.xml</FileName>" + Environment.NewLine);
            objStringBuilder.Append("<FileServerPath>http://localhost/</FileServerPath>" + Environment.NewLine);
            objStringBuilder.Append("<ApiName>d3123api.dll</ApiName>" + Environment.NewLine);
            objStringBuilder.Append("</Parameters>" + Environment.NewLine);
            objStringBuilder.Append("</Tasks>");
            arguments = objStringBuilder.ToString();
            
            actual = target.MakeTask(arguments);
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, ((Updater)actual).Mode);
            Assert.AreEqual("DFUpdateInstruction.xml", ((Updater)actual).FileName);
            
            actual.Execute();

            string spath = AppDomain.CurrentDomain.BaseDirectory;
            spath = spath.Remove(spath.IndexOf("Debug"), 5);
            fileexists = System.IO.File.Exists(spath + "\\DebugDFUpdateInstruction.xml");
            Assert.IsTrue(fileexists);            
        }
    }
}
