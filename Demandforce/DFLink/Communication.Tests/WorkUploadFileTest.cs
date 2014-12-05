using Demandforce.DFLink.Communication.WebAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Demandforce.DFLink.Communication.Tests
{
    using System.IO;
    using System.Linq;
    using System.Text;

    using Demandforce.DFLink.Common.Configuration;
    using Demandforce.DFLink.Common;
    using Demandforce.DFLink.Communication.Model;

    [TestClass()]
    public class WorkUploadFileTest
    {


        private TestContext testContextInstance;


        public TestContext TestContext { get; set; }


        [TestMethod()]
        public void UploadFileTest()
        {
            
            IServerSettings serverSettings = new ServerSettings(new XmlSettings());

            var target = new WorkUploadFile();
            string url = string.Empty;
            string param = string.Empty;

            target.Call = new TestCall(
                (p1, p2) =>
                {
                    url = p1;
                    param = p2;
                    return string.Empty;
                });


            int taskId = 12345;
            string data = "jionfje33jr3r fw9893r \nfwefjwww";
            string filePathName = System.AppDomain.CurrentDomain.BaseDirectory + "test.txt";
            var fileInfo = new FileInfo(filePathName);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            var streamWriter = fileInfo.AppendText();
            streamWriter.WriteLine(data);
            streamWriter.Flush();
            streamWriter.Close();

            target.UploadFile(taskId, filePathName);

            var model = JsonUtils.UnSerializeObject<UploadFileModel>(param);
            var base64String = Convert.ToBase64String(Encoding.ASCII.GetBytes(data + "\r\n"));


            Assert.AreEqual(url, serverSettings.AddressUrl + serverSettings.CommandLogUpload);
            Assert.AreEqual(model.FileContent, base64String);
            Assert.AreEqual(model.FileName, filePathName);
            Assert.AreEqual(model.TaskId, taskId);
        }
    }
}
