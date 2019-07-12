using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConvertLibrary;

namespace ConvertLibTest
{
    [TestClass]
    public class EpplusExcelAnalysisTest
    {
        private ExcelAnalysisByEpplus _excelAnalysisByEpplus;

        [TestInitialize]
        public void SetUp()
        {
            string filePath = @"E:\Github\TestLinkConverter\Resource\TestCase.xlsx";

            this._excelAnalysisByEpplus = new ExcelAnalysisByEpplus(filePath);
        }

        [TestMethod]
        public void ReadExcelTest()
        {
            this._excelAnalysisByEpplus.ReadExcel();
        }
    }
}
