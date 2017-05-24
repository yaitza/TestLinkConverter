using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransferLibrary;

namespace TransferLibTest
{
    [TestClass]
    public class ExcelAnalysisUnitTest
    {
        private string _efilePath;

        [TestInitialize]
        public void SetUp()
        {
            this._efilePath = @"F:\Code\TestLinkTransfer\trunk\Resource\TestCase.xlsx";
        }

        [TestMethod]
        public void OpenExcelTest()
        {
            ExcelAnalysis ea = new ExcelAnalysis(this._efilePath);
            XmlHandler mx = new XmlHandler(ea.ReadExcel());
            mx.writeXml();

            Assert.AreEqual(0, 0);
        }
    }
}
