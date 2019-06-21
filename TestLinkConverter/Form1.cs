using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ConvertLibrary;
using log4net;
using TransferLibrary;
using TransferModel;

namespace TestLinkConverter
{
    //TODO 处理完成后保存文件功能
    public partial class Form : System.Windows.Forms.Form
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(Form));

        private DateTime _starTime;

        private Dictionary<string, List<TestCase>> _tcDic = new Dictionary<string, List<TestCase>>();

        private delegate void DisplayMessage(string msg, Color color);


        public Form()
        {
            InitializeComponent();
            OutputDisplay.ShowMethod += this.OutputRichTextBox;
        }

        private void OutputRichTextBox(string msg, Color color)
        {
            if (this.outputRtb.InvokeRequired)
            {
                DisplayMessage dm = new DisplayMessage(OutputRichTextBox);
                this.Invoke(dm, new object[] { msg, color});
            }
            else
            {
                this.outputRtb.SelectionColor = color;
                this.outputRtb.AppendText( $"{DateTime.Now.ToString("u")} {msg} {Environment.NewLine}");
                this.outputRtb.Focus();
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string filePath = (string)e.Argument;
            if (filePath.EndsWith("xml"))
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
            TimeSpan consume = DateTime.Now - this._starTime;

            string showMsg = $"转换用例数: {_tcDic.Sum(keyValuePair => keyValuePair.Value.Count)}. 耗时: {consume.Minutes.ToString("D2")}:{consume.Seconds.ToString("D2")}.\n用例生成目录: {System.Environment.CurrentDirectory.ToString()}";
            MessageBox.Show(showMsg);
            OutputDisplay.ShowMessage(showMsg, Color.Azure);
            this.progressBar.Value = this.progressBar.Minimum;
            this.filePathTb.Text = string.Empty;
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
            this._starTime = DateTime.Now;
            CommonHelper.KillExcelProcess();
            if (this.FileChecked(filePathTb.Text)) return;

            this.backgroundWorker.RunWorkerAsync(filePathTb.Text);
            this.timer.Start();
        }

        /// <summary>
        /// Excel转换为XML
        /// </summary>
        /// <param name="fileDir">文件路径</param>
        private void ExcelToXml(string fileDir)
        {
            try
            {
                EpplusExcelAnalysis excelAnalysis = new EpplusExcelAnalysis(fileDir);
                _tcDic = excelAnalysis.ReadExcel();
                XmlHandler xh = new XmlHandler(_tcDic);
                xh.WriteXml();
            }
            catch (Exception ex)
            {
                this._logger.Error(ex);
                OutputDisplay.ShowMessage(ex.ToString(), Color.Red);
                return;
            }
        }

        /// <summary>
        /// XML转换为Excel
        /// </summary>
        /// <param name="fileDir">文件路径</param>
        private void XmlToExcel(string fileDir)
        {
            try
            {
                XmlAnalysis xmlAnalysis = new XmlAnalysis(fileDir);
                XmlToModel xtm = new XmlToModel(xmlAnalysis.GetAllTestCaseNodes());
                List<TestCase> tcList = xtm.OutputTestCases();
                this._tcDic = new Dictionary<string, List<TestCase>>();
                _tcDic.Add("TestCase", tcList);

                ExcelHandler eh = new ExcelHandler(tcList);
                eh.WriteExcel();
            }
            catch (Exception ex)
            {
                this._logger.Error(ex);
                OutputDisplay.ShowMessage(ex.ToString(), Color.Red);
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
                this._logger.Info(message: new Exception(message: "请输入文件地址."));
                OutputDisplay.ShowMessage(msg: "请输入文件地址.", color: Color.Red);
                return true;
            }

            if (!(filePathTb.Text.EndsWith(value: ".xml") || filePathTb.Text.EndsWith(value: ".xls") || filePathTb.Text.EndsWith(value: ".xlsx")))
            {
                this._logger.Info(message: new Exception(message: "输入文件要求为xml，xls或xlsx格式."));
                OutputDisplay.ShowMessage(msg: "输入文件要求为xml，xls或xlsx格式.", color: Color.Red);
                return true;
            }

            if (!File.Exists(path: filePathTb.Text))
            {
                this._logger.Info(message: new Exception(message: string.Format(format: "{0} 已不存在，请重新输入文件地址.", arg0: filePathTb.Text)));
                MessageBox.Show(text: string.Format(format: "{0} 已不存在，请重新输入文件地址.", arg0: filePathTb.Text), caption: "Warning");
                return true;
            }

            return false;
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://yaitza.github.io/");
        }

        private void DonateLab_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Form donateForm = new DonateForm();
            donateForm.Show();
        }

        private void xeRb_CheckedChanged(object sender, EventArgs e)
        {
            this.openFileDialog.Filter = "xml|*.xml|Excel 2007Plus|*.xlsx";
        }

        private void exRb_CheckedChanged(object sender, EventArgs e)
        {
            this.openFileDialog.Filter = "Excel 2007Plus|*.xlsx|xml|*.xml";
        }


        private void downloadlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("IExplore.exe", "https://raw.githubusercontent.com/yaitza/TestLinkConverter/master/Resource/TestCaseTemplate.xlsx");
        }
    }
}
