using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using TransferModel;

namespace TransferLibrary
{
    public class XmlHandler
    {
        //TODO 测试套数据未妥善处理
        private readonly List<TestCase> _tcList;

        public XmlHandler(List<TestCase> tcCases)
        {
            this._tcList = tcCases;
        }

        /// <summary>
        /// 测试用例转为Str
        /// </summary>
        /// <returns>List String</returns>
        private List<string> CaseToStr()
        {
            List<string> tcStrList = new List<string>();
            foreach (TestCase testCase in this._tcList)
            {
                string fieldsStr = $"<node_order><![CDATA[{testCase.NodeOrder}]]></node_order>";
                fieldsStr += $"<externalid><![CDATA[{testCase.ExternalId}]]></externalid>";
                fieldsStr += $"<version><![CDATA[{testCase.Version}]]></version>";
                fieldsStr += $"<summary><![CDATA[{testCase.Summary}]]></summary>";
                fieldsStr += $"<preconditions><![CDATA[{testCase.Preconditions}]]></preconditions>";
                fieldsStr += $"<execution_type><![CDATA[{testCase.ExecutionType.ToString()}]]></execution_type>";
                fieldsStr += $"<importance><![CDATA[{testCase.Importance.ToString()}]]></importance>";
                fieldsStr += $"<estimated_exec_duration>{testCase.EstimatedExecDuration}</estimated_exec_duration>";
                fieldsStr += $"<status>{testCase.Status}</status>";
                string tsStr = "";
                foreach (TestStep testStep in testCase.TestSteps)
                {
                    tsStr += "<step>";
                    tsStr += $"<step_number><![CDATA[{testStep.StepNumber}]]></step_number>";
                    tsStr += $"<actions><![CDATA[{testStep.Actions}]]></actions>";
                    tsStr += $"<expectedresults><![CDATA[{testStep.ExpectedResults}]]></expectedresults>";
                    tsStr += $"<execution_type><![CDATA[{testStep.ExecutionType}]]></execution_type>";
                    tsStr += "</step>";
                }
                fieldsStr += $"<steps>{tsStr}</steps>";
                string tcStr = $"<testcase name=\"{testCase.Name}\">{fieldsStr}</testcase>";

                tcStrList.Add(tcStr);
            }

            return tcStrList;
        }

        /// <summary>
        /// 写XML
        /// </summary>
        public void WriteXml()
        {
            string filePath = $"{System.Environment.CurrentDirectory}\\TestCase_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xml";
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate);
            using (StreamWriter sw = new StreamWriter(fs))
            {
                string tsStr = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
                tsStr += "<testsuite name = \"RootSuiteName\">";
                sw.Write(tsStr);
                foreach (string str in this.CaseToStr())
                {
                    sw.Write(str);
                }
                sw.Write("</testsuite>");
                sw.Close();
            }
        }
    }
}