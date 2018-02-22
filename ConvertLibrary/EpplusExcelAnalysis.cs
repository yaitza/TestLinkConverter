using System;
using System.IO;
using System.Drawing;
using OfficeOpenXml;
using System.Collections.Generic;
using TransferModel;

namespace ConvertLibrary
{
    class EpplusExcelAnalysis
    {
        private ExcelPackage excelPackage;


        public EpplusExcelAnalysis(string excelFilePath)
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
                this.excelPackage = new ExcelPackage(fiExcel);
            }catch (Exception ex)
            {
                OutputDisplay.ShowMessage(ex.Message, Color.Red);
                return;
            }
        }

        public void ReadExcel()
        {
            int iCount = this.excelPackage.Workbook.Worksheets.Count;

            if(iCount == 0)
            {
                OutputDisplay.ShowMessage("表中无Sheet页！", Color.Red);
                return;
            }

            for(int iFlag =0; iFlag<iCount; iCount++)
            {
                ExcelWorksheet excelWorksheet = this.excelPackage.Workbook.Worksheets[iFlag];

            }
        }


    }
}
