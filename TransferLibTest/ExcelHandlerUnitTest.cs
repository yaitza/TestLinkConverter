using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Xml;
using TransferLibrary;
using TransferModel;

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
            var eh = new ExcelHandler(this._testCaseList);
            eh.WriteExcel();

            Assert.AreEqual(0,0);
        }
    }
}