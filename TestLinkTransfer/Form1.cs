using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using log4net;
using TransferLibrary;
using TransferModel;
using Timer = System.Windows.Forms.Timer;

namespace TestLinkTransfer
{
    //TODO 链接yaitza地址
    //TODO 添加打赏功能
    //TODO 处理线程异步执行并添加处理时进度条
    //TODO 处理完成后保存文件功能
    //TODO 日志功能
    public partial class Form1 : Form
    {
        private ILog logger = LogManager.GetLogger(typeof(Form1));

        private DateTime starTime; 
        
        public Form1()
        {
            InitializeComponent();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string filePath = (string) e.Argument;
            if (filePath.Split('.')[1].Equals("xml"))
            {
                this.XmlToExcel(filePath);
            }
            else
            {
                this.ExcelToXml(filePath);
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.timer.Stop();
            this.progressBar.Value = this.progressBar.Maximum;
            MessageBox.Show($"Convert Success. Time : {DateTime.Now - this.starTime}.");
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (this.progressBar.Value < this.progressBar.Maximum)
            {
                this.progressBar.PerformStep();
            }
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
            this.starTime = DateTime.Now;
            CommonHelper.KillExcelProcess();
            if (this.FileChecked(filePathTb.Text)) return;
          
            this.backgroundWorker.RunWorkerAsync(filePathTb.Text);
            this.timer.Start();
        }

        private void ExcelToXml(object filePath)
        {
            try
            {
                string fileDir = (string)filePath;
                ExcelAnalysis excelAnalysis = new ExcelAnalysis(fileDir);
                List<TestCase> tcList = excelAnalysis.ReadExcel();
                XmlHandler xh = new XmlHandler(tcList);
                xh.WriteXml();
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void XmlToExcel(Object filePath)
        {
            try
            {
                string fileDir = (string)filePath;
                XmlAnalysis xmlAnalysis = new XmlAnalysis(fileDir);
                XmlToModel xtm = new XmlToModel(xmlAnalysis.GetAllTestCaseNodes());
                List<TestCase> tcList = xtm.OutputTestCases();
                ExcelHandler eh = new ExcelHandler(tcList);
                eh.WriteExcel();
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                MessageBox.Show(ex.Message);
                return;
            }
            
        }

        /// <summary>
        /// 检查输入文件地址是否符合要求
        /// </summary>
        /// <param name="filePath">文件地址</param>
        /// <returns>isChecked</returns>
        private bool FileChecked(string filePath)
        {
            if (filePathTb.Text == string.Empty)
            {
                this.logger.Info(new Exception("请输入文件地址."));
                MessageBox.Show("请输入文件地址.", "Warning");
                return true;
            }

            if (!(filePathTb.Text.EndsWith(".xml") || filePathTb.Text.EndsWith(".xls") || filePathTb.Text.EndsWith(".xlsx")))
            {
                this.logger.Info(new Exception("输入文件要求为xml，xls或xlsx格式."));
                MessageBox.Show("输入文件要求为xml，xls或xlsx格式.", "Warning");
                return true;
            }

            if (!File.Exists(filePathTb.Text))
            {
                this.logger.Info(new Exception($"{filePathTb.Text} 已不存在，请重新输入文件地址."));
                MessageBox.Show($"{filePathTb.Text} 已不存在，请重新输入文件地址.", "Warning");
                return true;
            }

            return false;
        }
    }
}
