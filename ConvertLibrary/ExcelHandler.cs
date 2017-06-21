using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;
using TransferModel;
using Excel = Microsoft.Office.Interop.Excel;

namespace TransferLibrary
{
    public class ExcelHandler
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof (ExcelHandler));
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

            if (excelApp == null)
            {
                this._logger.Warn(new Exception("Excel is not properly installed!"));
                throw new Exception("Excel is not properly installed!");
            }

            Excel.Workbook workbook;

            if (System.IO.File.Exists(fileName))
            {
                this._logger.Warn(new Exception("Template.xlsx 丢失"));
                workbook = excelApp.Workbooks.Open(fileName, 0, false, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            }
            else
            {
                workbook = excelApp.Workbooks.Add(true);
            }

            this.WriteInWorkSheet(workbook);

            excelApp.Visible = false;

            excelApp.DisplayAlerts = false;

            string saveDir = fileName.Replace("TestCaseTemplate.xlsx", $"TestCase_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx");
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
            foreach(TestCase node in this._sourceTestCases)
            {
                workSheet.Cells[iFlag, 1] = node.ExternalId;
                workSheet.Cells[iFlag, 2] = node.Name;
                workSheet.Cells[iFlag, 3] = node.Importance.ToString();
                workSheet.Cells[iFlag, 4] = node.ExecutionType.ToString();
                workSheet.Cells[iFlag, 5] = node.Summary;
                workSheet.Cells[iFlag, 6] = node.Preconditions;
                int iMerge = 0;
                foreach(TestStep step in node.TestSteps)
                {
                    workSheet.Cells[iFlag, 7] = CommonHelper.DelTags(step.Actions);
                    workSheet.Cells[iFlag, 8] = CommonHelper.DelTags(step.ExpectedResults);
                    iFlag++;
                    iMerge++;
                }

                this.MergeCells(workSheet, iMerge, iFlag - iMerge);
            }
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="workSheet">指定Sheet页</param>
        /// <param name="iMerge"></param>
        /// <param name="iFlag"></param>
        private void MergeCells(Excel.Worksheet workSheet, int iMerge, int iFlag)
        {
            //导出Excel前6列均需要合并单元格
            for(int i=1; i<=6; i++)
            {
                Excel.Range rangeLecture = workSheet.Range[workSheet.Cells[iFlag, i], workSheet.Cells[iFlag + iMerge - 1, i]];
                rangeLecture.Application.DisplayAlerts = false;
                rangeLecture.Merge(Missing.Value);
                rangeLecture.Application.DisplayAlerts = true;
            }
        }
    }
}