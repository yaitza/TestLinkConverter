using System;
using System.Text;
using System.Collections.Generic;
using ConvertLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConvertLibTest
{
    /// <summary>
    /// XmlHandlerTest 的摘要说明
    /// </summary>
    [TestClass]
    public class XmlHandlerTest
    {
        private Dictionary<string, List<ConvertModel.TestCase>> _tcDic;
        public XmlHandlerTest()
        {
            string fileDir = @"D:\github\res\TestCase1.xlsx";
            ExcelAnalysisByEpplus excelAnalysis = new ExcelAnalysisByEpplus(fileDir);
            _tcDic = excelAnalysis.ReadExcel();
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性: 
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void BuildStrTest()
        {
            TestSuiteGenerator tsg = new TestSuiteGenerator(_tcDic);
            var testSuite = tsg.BuildTestSuite();
            XmlHandler xh = new XmlHandler(_tcDic);
            xh.BuildStr(testSuite);

        }
    }
}
