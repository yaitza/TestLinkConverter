using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransferLibrary;

namespace TransferLibTest
{
    [TestClass]
    public class XmlAnalysisUnitTest
    {
        private XmlAnalysis xmlAnalysis;

        [TestInitialize]
        public void SetUp()
        {
            string filepath = @"F:\Code\TestLinkTransfer\trunk\Resource\CRM.xml";
//            string filepath = @"F:\Code\TestLinkTransfer\trunk\Resource\幻视项目.xml";

            this.xmlAnalysis = new XmlAnalysis(filepath);
        }


        [TestMethod]
        public void TestGetAllTestCaseNodes()
        {
            Assert.AreEqual(this.xmlAnalysis.GetAllTestCaseNodes().Count, 20);
        }

        [TestCleanup]
        public void TearDown()
        {
            this.xmlAnalysis = null;
        }
    }
}
