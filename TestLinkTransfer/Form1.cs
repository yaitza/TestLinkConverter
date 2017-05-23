﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using TransferLibrary;
using TransferModel;

namespace TestLinkTransfer
{
    //TODO 链接yaitza地址
    //TODO 添加打赏功能
    //TODO 添加处理时进度条,进度条展示停止问题
    //TODO 处理完成后保存文件功能
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void getFilePathBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePathTb.Text = openFileDialog.FileName;
            }
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
//            progressBar.Style = ProgressBarStyle.Marquee;
//            Thread myThread = new Thread(DoData) {IsBackground = true};
//            myThread.Start(); //线程开始  
             
            if (this.FileChecked(filePathTb.Text)) return;
            if(xeRb.Checked)
            { 
                Thread xeThread = new Thread(XmlToExcel);
                xeThread.Start(filePathTb.Text);
            }
        }

        private void XmlToExcel(object filePath)
        {
            string fileDir = (string) filePath;
            XmlAnalysis xmlAnalysis = new XmlAnalysis(fileDir);
            XmlToModel xtm = new XmlToModel(xmlAnalysis.GetAllTestCaseNodes());
            List<TestCase> tcList = xtm.OutputTestCases();
            ExcelHandler eh = new ExcelHandler(tcList);
            eh.WriteExcel();
        }

//        DateTime dt;
//        private bool isSuccess = false;
//        private delegate void DoDataDelegate();
//        /// <summary>  
//        /// 进行循环  
//        /// </summary>  
//        private void DoData()
//        {
//            if (progressBar.InvokeRequired)
//            {
//                DoDataDelegate d = DoData;
//                progressBar.Invoke(d);
//            }
//            else
//            {
//                while (true)
//                {
//                    if (!isSuccess) continue;
//                    progressBar.Style = ProgressBarStyle.Continuous;
//                    break;
//                }
//                MessageBox.Show($"Comlpete Transfer! Time:{DateTime.Now.Subtract(dt).ToString()}.", "Info");
//            }
//        }

        /// <summary>
        /// 检查输入文件地址是否符合要求
        /// </summary>
        /// <param name="filePath">文件地址</param>
        /// <returns>isChecked</returns>
        private bool FileChecked(string filePath)
        {
            if (filePathTb.Text == string.Empty)
            {
                MessageBox.Show("请输入文件地址.", "Warning");
                return true;
            }

            if (!(filePathTb.Text.EndsWith(".xml") || filePathTb.Text.EndsWith(".xls") || filePathTb.Text.EndsWith(".xlsx")))
            {
                MessageBox.Show("输入文件要求为xml，xls或xlsx格式.", "Warning");
                return true;
            }

            if (!File.Exists(filePathTb.Text))
            {
                MessageBox.Show($"{filePathTb.Text} 已不存在，请重新输入文件地址.", "Warning");
                return true;
            }

            return false;
        }
    }
}
