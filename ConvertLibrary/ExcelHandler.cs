using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Threading;
using ConvertLibrary;
using ConvertModel;
using log4net;
using Excel = Microsoft.Office.Interop.Excel;

namespace ConvertLibrary
{
    public class ExcelHandler
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(ExcelHandler));
        private readonly List<TestCase> _sourceTestCases;

        public ExcelHandler(List<TestCase> outputCases)
        {
            this._sourceTestCases = outputCases;
        }

        /// <summary>
        /// 写Excel
        /// </summary>
        public void WriteExcel()
        {
            string currentDir = System.Environment.CurrentDirectory;
            string fileName = $"{currentDir}\\TestCaseTemplate.xlsx";

            Excel.Application excelApp = new Excel.ApplicationClass();

            Excel.Workbook workbook;

            if (System.IO.File.Exists(fileName))
            {
                workbook = excelApp.Workbooks.Open(fileName, 0, false, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            }
            else
            {
                workbook = excelApp.Workbooks.Add(true);
            }

            this.WriteInWorkSheet(workbook);

            excelApp.Visible = false;
            excelApp.DisplayAlerts = false;
            string saveDir = fileName.Replace("TestCaseTemplate.xlsx", $"TestCase_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
            OutputDisplay.ShowMessage($"文件保存路勁：{saveDir}\n", Color.Azure);
            workbook.SaveAs(saveDir);
            workbook.Close(false, Missing.Value, Missing.Value);
            excelApp.Quit();

            workbook = null;
            excelApp = null;
        }

        private void WriteInWorkSheet(Excel.Workbook workBook)
        {
            var workSheet = (Excel.Worksheet)workBook.Worksheets.Item[1];

            int iFlag = 2;
            foreach (TestCase node in this._sourceTestCases)
            {
                if (node.Name == null && node.TestSteps == null)
                {
                    continue;
                }
                OutputDisplay.ShowMessage(node.Name, Color.Chartreuse);
                ProgressBarShow.ShowProgressValue(this._sourceTestCases.IndexOf(node) * 100 / this._sourceTestCases.Count);
                workSheet.Cells[iFlag, 1] = node.ExternalId;
                workSheet.Cells[iFlag, 2] = CommonHelper.DelTags(node.Name);
                string keywords = string.Empty;
                if (node.Keywords != null)
                {
                    foreach (string keyword in node.Keywords)
                    {
                        keywords = keywords + keyword + ",";
                    }
                }
                workSheet.Cells[iFlag, 3] = keywords.TrimEnd(',');
                workSheet.Cells[iFlag, 4] = node.Importance.ToString();
                workSheet.Cells[iFlag, 5] = node.ExecutionType.ToString();
                workSheet.Cells[iFlag, 6] = CommonHelper.DelTags(node.Summary);
                workSheet.Cells[iFlag, 7] = CommonHelper.DelTags(node.Preconditions);
                int iMerge = 0;

                if (node.TestSteps != null && node.TestSteps.Count != 0)
                {
                    foreach (var step in node.TestSteps)
                    {
                        workSheet.Cells[iFlag, 8] = CommonHelper.DelTags(step.Actions);
                        workSheet.Cells[iFlag, 9] = CommonHelper.DelTags(step.ExpectedResults);
                        iFlag++;
                        iMerge++;
                    }
                }
                else
                {
                    iFlag++;
                    iMerge++;
                }
                this.MergeCells(workSheet, iMerge, iFlag - iMerge);
                Thread.Sleep(50);
            }
            workSheet.Cells[iFlag++, 1] = "END";
            ProgressBarShow.ShowProgressValue(100);

        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="workSheet">指定Sheet页</param>
        /// <param name="iMerge"></param>
        /// <param name="iFlag"></param>
        private void MergeCells(Excel.Worksheet workSheet, int iMerge, int iFlag)
        {
            //导出Excel前7列均需要合并单元格
            for (int i = 1; i <= 7; i++)
            {
                Excel.Range rangeLecture = workSheet.Range[workSheet.Cells[iFlag, i], workSheet.Cells[iFlag + iMerge - 1, i]];
                rangeLecture.Application.DisplayAlerts = false;
                rangeLecture.Merge(Missing.Value);
                rangeLecture.Application.DisplayAlerts = true;
            }
        }
    }
}