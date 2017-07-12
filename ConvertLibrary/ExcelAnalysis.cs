using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;
using Microsoft.Office.Interop.Excel;
using TransferLibrary;
using TransferModel;

namespace ConvertLibrary
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

            for (int i = 1; i < usedRows; i++)
            {
                if (!((Range)eWorksheet.Cells[i, 1]).Text.ToString().Equals("END")) continue;
                usedRows = i;
                break;
            }

            for (int i = 2; i < usedRows; i++)
            {
                TestCase tc = new TestCase();

                Range currentCell1 = (Range)eWorksheet.Cells[i, 1];
                int icount = currentCell1.MergeArea.Count;
                tc.ExternalId = currentCell1.Text.ToString().ToString();

                Range currentCell2 = (Range) eWorksheet.Cells[i, 2];
                tc.Name = currentCell2.Text.ToString();

                Range currentCell3 = (Range)eWorksheet.Cells[i, 3];
                tc.Importance = CommonHelper.StrToImportanceType(currentCell3.Text.ToString());

                Range currentCell4 = (Range)eWorksheet.Cells[i, 4];
                tc.ExecutionType = CommonHelper.StrToExecType(currentCell4.Text.ToString());

                Range currentCell5 = (Range)eWorksheet.Cells[i, 5];
                tc.Summary = currentCell5.Text.ToString();

                Range currentCell6 = (Range)eWorksheet.Cells[i, 6];
                tc.Preconditions = currentCell6.Text.ToString();

                tc.TestSteps = new List<TestStep>();
                for (int j = i; j < i+icount; j++)
                {
                    TestStep ts = new TestStep();
                    ts.StepNumber = 1;
                    ts.ExecutionType = ExecType.手动;
                    ts.Actions = ((Range) eWorksheet.Cells[j, 7]).Text.ToString();
                    ts.ExpectedResults = ((Range) eWorksheet.Cells[j, 8]).Text.ToString();
                    tc.TestSteps.Add(ts);
                }
                tcList.Add(tc);
                i = i + icount - 1;
            }

            return tcList;
        }
    }
}