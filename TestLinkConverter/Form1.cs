using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ConvertLibrary;
using ConvertModel;
using log4net;

namespace TestLinkConverter
{
    //TODO 处理完成后保存文件功能
    public partial class Form : System.Windows.Forms.Form
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(Form));

        private DateTime _starTime;

        private Dictionary<string, List<TestCase>> _tcDic = new Dictionary<string, List<TestCase>>();

        private delegate void DisplayMessage(string msg, Color color);

        private delegate void SetPos(int ipos);


        public Form()
        {
            InitializeComponent();
            OutputDisplay.ShowMethod += this.OutputRichTextBox;
            ProgressBarShow.SetProgressValue += this.SetProgressValue;
            _ = GoogleAnalyticsTracker.Tracker("specificForm", "Initialize");
        }


        private void startBtn_Click(object sender, EventArgs e)
        {
            if (this.FileChecked(filePathTb.Text)) return;
            if (cB_testcase.Checked)
            {
                this._starTime = DateTime.Now;
                CommonHelper.KillExcelProcess();
                this.backgroundWorker.RunWorkerAsync(filePathTb.Text);
                this.timer.Start();
            }

            if (cB_requirement.Checked)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(XmlToDoc));
                thread.Start(filePathTb.Text);
            }
            
        }

        private void XmlToDoc(object obj)
        {
            string fileDir = obj as string;
            _ = GoogleAnalyticsTracker.Tracker("specificWork", "XmlToDoc");
            XmlDocAnalysis xda = new XmlDocAnalysis(fileDir);
            List<Requirement> requirements = xda.XmlToDoc();
            xda.WriteDoc(requirements);
            string showMsg = $"转换需求数: {requirements.Count} .\n用例生成目录: {System.Environment.CurrentDirectory.ToString()}";
            MessageBox.Show(showMsg);
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
            TimeSpan consume = DateTime.Now - this._starTime;
            string showMsg = $"转换用例数: {_tcDic.Sum(keyValuePair => keyValuePair.Value.Count)}. 耗时: {consume.Minutes.ToString("D2")}:{consume.Seconds.ToString("D2")}.\n用例生成目录: {System.Environment.CurrentDirectory.ToString()}";
            MessageBox.Show(showMsg);
            OutputDisplay.ShowMessage(showMsg, Color.Azure);
            this.filePathTb.Text = string.Empty;
            this.progressBar.Value = this.progressBar.Minimum;
        }

        private void getFilePathBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePathTb.Text = openFileDialog.FileName;
            }
        }

        /// <summary>
        /// Excel转换为XML
        /// </summary>
        /// <param name="fileDir">文件路径</param>
        private void ExcelToXml(string fileDir)
        {
            _ = GoogleAnalyticsTracker.Tracker("specificWork", "ExcelToXml");
            try
            {
                ExcelAnalysisByEpplus excelAnalysis = new ExcelAnalysisByEpplus(fileDir);
                _tcDic = excelAnalysis.ReadExcel();

                var tsg = new TestSuiteGenerator(_tcDic);
                var tsDic = tsg.BuildTestSuite();

                XmlHandler xh = new XmlHandler(_tcDic);
                xh.WriteXml2(xh.BuildStr(tsDic));
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
            _ = GoogleAnalyticsTracker.Tracker("specificWork", "XmlToExcel");
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
            _ = GoogleAnalyticsTracker.Tracker("specificForm", "Donate");
            System.Windows.Forms.Form donateForm = new DonateForm();
            donateForm.Show();
        }

        private void downloadlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _ = GoogleAnalyticsTracker.Tracker("specificForm", "DownloadTemplate");
            System.Diagnostics.Process.Start("IExplore.exe", "https://raw.githubusercontent.com/yaitza/TestLinkConverter/master/Resource/TestCaseTemplate.xlsx");
        }

        private void OutputRichTextBox(string msg, Color color)
        {
            if (this.outputRtb.InvokeRequired)
            {
                DisplayMessage dm = new DisplayMessage(OutputRichTextBox);
                this.Invoke(dm, new object[] { msg, color });
            }
            else
            {
                this.outputRtb.SelectionColor = color;
                this.outputRtb.AppendText($"{DateTime.Now.ToString("u")} {msg} {Environment.NewLine}");
                this.outputRtb.Focus();
            }
        }

        private void SetProgressValue(int ipos)
        {
            if (this.InvokeRequired)
            {
                SetPos setPos = new SetPos(SetProgressValue);
                this.Invoke(setPos, new object[] {ipos});
            }
            else
            {
                this.progressBar.Value = Convert.ToInt32(ipos);
            }
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _ = GoogleAnalyticsTracker.Tracker("specificForm", "ClickHomeSiteUrl");
            System.Diagnostics.Process.Start("https://github.com/yaitza/TestLinkConverter/");
        }

        private void CB_requirement_Click(object sender, EventArgs e)
        {
            this.cB_testcase.CheckState = CheckState.Unchecked; ;
            this.cB_requirement.CheckState = CheckState.Checked;
        }

        private void CB_testcase_Click(object sender, EventArgs e)
        {
            this.cB_requirement.CheckState = CheckState.Unchecked;
            this.cB_testcase.CheckState = CheckState.Checked;

        }
    }
}
