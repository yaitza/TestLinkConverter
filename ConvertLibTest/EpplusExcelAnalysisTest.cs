using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConvertLibrary;

namespace ConvertLibTest
{
    [TestClass]
    public class EpplusExcelAnalysisTest
    {
        [TestInitialize]
        public void SetUp()
        {
            string filePath = @"E:\Github\TestLinkConverter\Resource\TestCase.xlsx";

            EpplusExcelAnalysis epplusExcelAnalysis = new EpplusExcelAnalysis(filePath);
        }

        [TestMethod]
        public void ReadExcelTest()
        {

        }
    }
}
