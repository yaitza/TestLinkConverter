using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransferModel;
using TransferLibrary;

namespace TestLinkTransfer
{
    //TODO 链接yaitza地址
    //TODO 添加打赏功能
    //TODO 处理线程异步执行并添加处理时进度条
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
            if (this.FileChecked(filePathTb.Text)) return;
            if(xeRb.Checked)
            { 
                Thread xtThread = new Thread(XmlToExcel);
                xtThread.Start(filePathTb.Text);
            }

        }

        private void XmlToExcel(Object filePath)
        {
            string fileDir = (string) filePath;
            XmlAnalysis xmlAnalysis = new XmlAnalysis(fileDir);
            XmlToModel xtm = new XmlToModel(xmlAnalysis.GetAllTestCaseNodes());
            List<TestCase> tcList = xtm.OutputTestCases();
            ExcelHandler eh = new ExcelHandler(tcList);
            eh.WriteExcel();
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
                MessageBox.Show("请输入文件地址.", "Warning");
                return true;
            }

            if (!(filePathTb.Text.EndsWith(".xml") || filePathTb.Text.EndsWith(".xls") || filePathTb.Text.EndsWith(".xlsx")))
            {
                MessageBox.Show("输入文件要求为xml，xls或xlsx格式.", "Warning");
                return true;
            }

            if (!System.IO.File.Exists(filePathTb.Text))
            {
                MessageBox.Show($"{filePathTb.Text} 已不存在，请重新输入文件地址.", "Warning");
                return true;
            }

            return false;
        }
    }
}
