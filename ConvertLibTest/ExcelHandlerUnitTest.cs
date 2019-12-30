using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Xml;
using ConvertLibrary;
using ConvertModel;

namespace TransferLibTest
{
    [TestClass]
    public class ExcelHandlerUnitTest
    {
        private List<TestCase> _testCaseList;


        [TestInitialize]
        public void SetUp()
        {
            string filepath = @"G:\Code\TestLinkTransfer\trunk\Resource\CRM.xml";

            XmlAnalysis xmlAnalysis = new XmlAnalysis(filepath);
            XmlToModel xtm = new XmlToModel(xmlAnalysis.GetAllTestCaseNodes());
            this._testCaseList = xtm.OutputTestCases();
        }

        [TestMethod]
        public void WriteExcelTest()
        {
            Assert.AreEqual(0,0);
        }
    }
}