using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using TransferModel;

namespace TransferLibrary
{
    //TODO 解析合并格式的Excel数据
    public class ExcelAnalysis
    {
        private readonly string _eFilePath;

        public ExcelAnalysis(string filePath)
        {
            this._eFilePath = filePath;
        }

        public void OpenExcel()
        {
            object missing = Missing.Value;
            Application excel = new Application();
            if (excel == null)
            {
                throw new Exception("Excel is not properly installed!");
            }
            else
            {
                excel.Visible = false;
                excel.UserControl = true;
                // 以只读的形式打开EXCEL文件
                Workbook wb = excel.Application.Workbooks.Open(this._eFilePath, missing, true, missing, missing, missing,missing, missing, missing, true, missing, missing, missing, missing, missing);
                //取得第一个工作薄
                Worksheet ws = (Worksheet)wb.Worksheets.Item[1];

                var tcList = this.GetExcelData(ws);



//                //取得总记录行数   (包括标题列)
//                int rowsint = ws.UsedRange.Cells.Rows.Count; 
//                //得到行数
//                int columnsint = ws.UsedRange.Cells.Columns.Count;//得到列数
//
//
//                //取得数据范围区域 (不包括标题列) 
//                Range rng1 = ws.Cells.get_Range("B2", "J" + rowsint);   //item
//
//
//                Range rng2 = ws.Cells.get_Range("K2", "K" + rowsint); //Customer
//                object[,] arryItem = (object[,])rng1.Value2;   //get range's value
//                object[,] arryCus = (object[,])rng2.Value2;
//                //将新值赋给一个数组
//                string[,] arry = new string[rowsint - 1, 2];
//                for (int i = 1; i <= rowsint - 1; i++)
//                {
//                    //Item_Code列
//                    arry[i - 1, 0] = arryItem[i, 1].ToString();
//                    //Customer_Name列
//                    arry[i - 1, 1] = arryCus[i, 1].ToString();
//                }
            }
            excel.Quit();
            excel = null;

           
        }

        public List<TestCase> GetExcelData(Worksheet eWorksheet)
        {
            List<TestCase> tcList = new List<TestCase>();
            int usedRows = eWorksheet.UsedRange.Cells.Rows.Count;

            if (usedRows == 0 || usedRows == 1)
            {
                throw new Exception("No TestCase!");
            }

            for (int i = 2; i <= usedRows; i++)
            {
                TestCase tc = new TestCase();
                tc.Name = ((Range) eWorksheet.Cells[i, 1]).Text.ToString();
                //tc.Importance = (ImportanceType)((Range)eWorksheet.Cells[i, 2]).Text.ToString();
                tc.ExecutionType = (ExecType) int.Parse(((Range) eWorksheet.Cells[i, 3]).Text.ToString());
                tc.Keywords = ((Range)eWorksheet.Cells[i, 4]).Text.ToString().Split(',').ToList();
                tc.Summary = ((Range)eWorksheet.Cells[i, 5]).Text.ToString();
                tc.Preconditions = ((Range)eWorksheet.Cells[i, 6]).Text.ToString();
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
    }
}