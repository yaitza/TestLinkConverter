using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using log4net;
using Microsoft.Office.Interop.Excel;
using TransferModel;

namespace TransferLibrary
{
    //TODO 解析合并格式的Excel数据
    public class ExcelAnalysis
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof (ExcelAnalysis));
        private readonly string _eFilePath;

        public ExcelAnalysis(string filePath)
        {
            this._eFilePath = filePath;
        }

        /// <summary>
        /// 读取Excel数据
        /// </summary>
        /// <returns>TestCase</returns>
        public List<TestCase> ReadExcel()
        {
            List<TestCase> tcList;
            object missing = Missing.Value;
            Application excel = new Application();
            if (excel == null)
            {
                _logger.Warn(new Exception("Excel is not properly installed!"));
                throw new Exception("Excel is not properly installed!");
            }
            else
            {
                excel.Visible = false;
                excel.UserControl = true;
                // 以只读的形式打开EXCEL文件
                Workbook wb = excel.Application.Workbooks.Open(this._eFilePath, missing, true, missing, missing, missing,
                    missing, missing, missing, true, missing, missing, missing, missing, missing);
                //取得第一个工作薄
                Worksheet ws = (Worksheet) wb.Worksheets.Item[1];
                tcList = this.GetExcelData(ws);
            }
            excel.Quit();
            excel = null;
            CommonHelper.KillExcelProcess();

            return tcList;
        }

        /// <summary>
        /// 获取Excel数据并转为TestCase
        /// </summary>
        /// <param name="eWorksheet">Sheet页</param>
        /// <returns>Model类型测试用例</returns>
        private List<TestCase> GetExcelData(Worksheet eWorksheet)
        {
            List<TestCase> tcList = new List<TestCase>();
            int usedRows = eWorksheet.UsedRange.Cells.Rows.Count;

            if (usedRows == 0 || usedRows == 1)
            {
                this._logger.Warn(new Exception("No TestCase!"));
                throw new Exception("No TestCase!");
            }

            for (int i = 2; i <= usedRows; i++)
            {
                TestCase tc = new TestCase();
                tc.Name = ((Range) eWorksheet.Cells[i, 1]).Text.ToString();
                tc.Importance = this.ConvertToImportanceType(((Range) eWorksheet.Cells[i, 2]).Text.ToString());
                tc.ExecutionType = (ExecType) int.Parse(((Range) eWorksheet.Cells[i, 3]).Text.ToString());
                tc.Keywords = ((Range) eWorksheet.Cells[i, 4]).Text.ToString().Split(',').ToList();
                tc.Summary = ((Range) eWorksheet.Cells[i, 5]).Text.ToString();
                tc.Preconditions = ((Range) eWorksheet.Cells[i, 6]).Text.ToString();
                TestStep ts = new TestStep
                {
                    StepNumber = 1,
                    ExecutionType = ExecType.手动,
                    Actions = ((Range) eWorksheet.Cells[i, 7]).Text.ToString(),
                    ExpectedResults = ((Range) eWorksheet.Cells[i, 8]).Text.ToString()
                };
                tc.TestSteps = new List<TestStep> {ts};

                tcList.Add(tc);
            }

            return tcList;
        }

        /// <summary>
        /// 测试用例优先级类型转换
        /// </summary>
        /// <param name="impType">优先级</param>
        /// <returns>ImportanceType</returns>
        private ImportanceType ConvertToImportanceType(string innerText)
        {
            if (Regex.IsMatch(innerText, @"^[+-]?\d*[.]?\d*$"))
            {
                return (ImportanceType) int.Parse(innerText);
            }
            else
            {
                switch (innerText.ToLower())
                {
                    case "高":
                    case "high":
                        return ImportanceType.高;
                    case "中":
                    case "medium":
                        return ImportanceType.中;
                    case "低":
                    case "low":
                        return ImportanceType.低;
                    default:
                        return ImportanceType.高;
                }
            }
        }
    }
}