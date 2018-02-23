using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConvertLibrary;

namespace ConvertLibTest
{
    [TestClass]
    public class EpplusExcelAnalysisTest
    {
        private EpplusExcelAnalysis epplusExcelAnalysis;

        [TestInitialize]
        public void SetUp()
        {
            string filePath = @"E:\Github\TestLinkConverter\Resource\TestCase.xlsx";

            this.epplusExcelAnalysis = new EpplusExcelAnalysis(filePath);
        }

        [TestMethod]
        public void ReadExcelTest()
        {
            this.epplusExcelAnalysis.ReadExcel();
        }
    }
}
