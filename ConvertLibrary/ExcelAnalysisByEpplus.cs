using System;
using System.IO;
using System.Drawing;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Linq;
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

            for(int i=1; i < eWorksheet.Dimension.End.Row; i++)
            {
                if(eWorksheet.Cells[i,1].Text != null || eWorksheet.Cells[i,1].Text.ToString() != string.Empty ||
                    !eWorksheet.Cells[i, 1].Text.ToString().Equals("END"))
                {
                    continue;
                }
                usedRows = i;
                break;
            }

            TestCase tc = new TestCase();

            for (int i = 2; i < usedRows; i++)
            {
                var currentCell = eWorksheet.Cells[i, 1];
                //设置单元格格式为文本格式，防止为自定义格式时读取单元格报错
                for (int j = 2; j <= 9; j++)
                {
                    eWorksheet.Cells[i, j].Style.Numberformat.Format = "@";
                }

                if (currentCell.Value == null)
                {
                    TestStep ts = new TestStep
                    {
                        StepNumber = tc.TestSteps.Count + 1,
                        ExecutionType = ExecType.手动,
                        Actions = eWorksheet.Cells[i, 7].Text.ToString(),
                        ExpectedResults = eWorksheet.Cells[i, 8].Text.ToString()
                    };

                    tc.TestSteps.Add(ts);
                    continue;
                }
                else
                {
                    if(tc.ExternalId != null)
                    {
                        tcList.Add(tc);
                    }

                    tc = new TestCase
                    {
                        ExternalId = string.Format($"{currentCell.Text.ToString()}_{new Random().Next(0, 10000)}"),
                        Name = eWorksheet.Cells[i, 2].Text.ToString(),
                        Keywords = eWorksheet.Cells[i, 3].Text.ToString().Split(',').ToList(),
                        Importance = CommonHelper.StrToImportanceType(eWorksheet.Cells[i, 4].Text.ToString()),
                        ExecutionType = CommonHelper.StrToExecType(eWorksheet.Cells[i, 5].Text.ToString()),
                        Summary = eWorksheet.Cells[i, 6].Text.ToString(),
                        Preconditions = eWorksheet.Cells[i, 7].Text.ToString()
                    };
                    
                    TestStep tsOne = new TestStep
                    {
                        StepNumber = 1,
                        ExecutionType = ExecType.手动,
                        Actions = eWorksheet.Cells[i, 8].Text.ToString(),
                        ExpectedResults = eWorksheet.Cells[i, 9].Text.ToString()
                    };

                    tc.TestSteps = new List<TestStep> {tsOne};
                }
            }

            return tcList;
        }


    }
}
