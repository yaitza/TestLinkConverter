using System;
using System.IO;
using System.Drawing;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using log4net;
using ConvertLibrary;
using ConvertModel;

namespace ConvertLibrary
{
    public class ExcelAnalysisByEpplus
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(ExcelAnalysisByEpplus));
        private readonly ExcelPackage _excelPackage;


        public ExcelAnalysisByEpplus(string excelFilePath)
        {
            if (string.IsNullOrEmpty(excelFilePath))
            {
                OutputDisplay.ShowMessage("传入文件地址有误！", Color.Red);
                return;
            }

            if (!File.Exists(excelFilePath))
            {
                OutputDisplay.ShowMessage("文件不存在!", Color.Red);
                return;
            }

            try
            {
                FileInfo fiExcel = new System.IO.FileInfo(excelFilePath);
                this._excelPackage = new ExcelPackage(fiExcel);
            }catch (Exception ex)
            {
                OutputDisplay.ShowMessage(ex.Message, Color.Red);
                return;
            }
        }

        public Dictionary<string, List<TestCase>> ReadExcel()
        {
            Dictionary<string, List<TestCase>> dicAllTestCases = new Dictionary<string, List<TestCase>>();
            int iCount = this._excelPackage.Workbook.Worksheets.Count;

            for(int iFlag = 1; iFlag <= iCount; iFlag++)
            {
                ExcelWorksheet excelWorksheet = this._excelPackage.Workbook.Worksheets[iFlag];
                var testCase = this.GetExcelSheetData(excelWorksheet);

                if(testCase.Count == 0)
                {
                    OutputDisplay.ShowMessage($"页签:{excelWorksheet.Name}无任何可转换用例数据.", Color.GreenYellow);
                    continue;
                }

                dicAllTestCases.Add(excelWorksheet.Name, testCase);
            }
            this._excelPackage.Dispose();
            return dicAllTestCases;
        }

        public List<TestCase> GetExcelSheetData(ExcelWorksheet eWorksheet)
        {
            List<TestCase> tcList = new List<TestCase>();
            int usedRows, usedCols;

            if(eWorksheet.Dimension == null)
            {
                this._logger.Warn(new Exception("No TestCase, this Sheet is new!"));
                return new List<TestCase>();
            }
            else
            {
                usedRows = eWorksheet.Dimension.End.Row;
                usedCols = eWorksheet.Dimension.End.Column;
            }

            if(usedRows == 0 || usedRows == 1)
            {
                this._logger.Warn(new Exception("No TestCase!"));
                return tcList;
            }

            List<string> testSuitesName = new List<string>();
            for (int i = 2; i <= usedRows; i++)
            {
                var currentCell = eWorksheet.Cells[i, 1];
                //设置单元格格式为文本格式，防止为自定义格式时读取单元格报错
                for (int j = 2; j <= usedCols; j++)
                {
                    eWorksheet.Cells[i, j].Style.Numberformat.Format = "@";
                }

                TestCase tcTemp = new TestCase();
                TestStep ts = new TestStep { StepNumber = 1, ExecutionType = ExecType.手动 };
                Dictionary<String, String> custormFiled = new Dictionary<string, string>();
                for (int j = 1; j <= usedCols; j++)
                {
                    String titleName = eWorksheet.Cells[1, j].Value.ToString();
                    var cellObject = eWorksheet.Cells[i, j].Value;
                    if (titleName.Contains("级") && cellObject != null && !titleName.Contains("优先") )
                    {
                        if (testSuitesName.Count < j)
                        {
                            testSuitesName.Add(cellObject.ToString());
                        }
                        else
                        {
                            testSuitesName[j - 1] = cellObject.ToString();
                        }

                        for (int k = j; k < testSuitesName.Count; k++)
                        {
                            testSuitesName.RemoveAt(k);
                        }
                        continue;
                    }


                    if (titleName.Contains("用例编号"))
                    {
                        if (cellObject != null)
                        {
                            tcTemp.ExternalId =
                                string.Format($"{cellObject.ToString()}");
                        }
                        continue;
                    }

                    if (titleName.Contains("用例标题"))
                    {
                        if (cellObject != null)
                        {
                            tcTemp.Name = cellObject.ToString();
                            continue;
                        }
                    }

                    if (titleName.Contains("预置条件"))
                    {
                        if (cellObject != null)
                        {
                            tcTemp.Preconditions = CommonHelper.DelExcelTags(cellObject.ToString());
                            continue;
                        }
                    }

                    if (titleName.Contains("操作步骤"))
                    {
                        if (cellObject != null)
                        {
                            ts.Actions = CommonHelper.DelExcelTags(cellObject.ToString());
                            continue;
                        }
                    }

                    if (titleName.Contains("期望结果"))
                    {
                        if (cellObject != null)
                        {
                            ts.ExpectedResults = CommonHelper.DelExcelTags(cellObject.ToString());
                            continue;
                        }
                    }

                    if (titleName.Contains("测试环境"))
                    {
                        if (cellObject != null)
                        {
                            custormFiled.Add(titleName, cellObject.ToString());
                        }
                        else
                        {
                            custormFiled.Add(titleName, String.Empty);
                        }
                        continue;
                    }

                    if (titleName.Contains("机型"))
                    {
                        if (cellObject != null)
                        {
                            custormFiled.Add(titleName, cellObject.ToString());
                        }
                        else
                        {
                            custormFiled.Add(titleName, String.Empty);
                        }
                        continue;
                    }

                    if (titleName.Contains("优先级"))
                    {
                        if (cellObject != null)
                        {
                            tcTemp.Importance = CommonHelper.StrToImportanceType(cellObject.ToString());
                        }
                        continue;
                    }

                    if (titleName.Contains("需求编号"))
                    {
                        if (cellObject != null)
                        {
                            tcTemp.Requirement = cellObject.ToString();
                        }
                    }
                }
                tcTemp.TestSteps = new List<TestStep> {ts};
                tcTemp.CustomFileds = custormFiled;

                List<String> testSuitesNameList = new List<String>();
                foreach (string s in testSuitesName)
                {
                    if (!String.IsNullOrEmpty(s))
                    {
                        testSuitesNameList.Add(s);
                    }
                }
                tcTemp.TestCaseHierarchy = testSuitesNameList;
        
                if (String.IsNullOrEmpty(tcTemp.Name))
                {
                    continue;
                }

                tcList.Add(tcTemp);
            }
            

            return tcList;
        }


    }
}
