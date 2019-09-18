using ConvertModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;

namespace ConvertLibrary
{
    public class XmlHandler
    {
        private readonly Dictionary<string, List<TestCase>> _tcList;

        private readonly List<TestCase> _allCases;

        private readonly int _tcCount;

        public XmlHandler(Dictionary<string, List<TestCase>> tcCases)
        {
            this._tcList = tcCases;
            this._allCases = new List<TestCase>();
            foreach (KeyValuePair<string, List<TestCase>> keyValuePair in _tcList)
            {
                this._allCases.AddRange(keyValuePair.Value);
            }

            this._tcCount = this._allCases.Count;

        }

        #region 之前逻辑

        /// <summary>
        /// 测试用例转为Str
        /// </summary>
        /// <returns>List String</returns>
        private Dictionary<string, List<string>> CaseToStr()
        {
            Dictionary<string, List<string>> dicCase = new Dictionary<string, List<string>>();

            foreach (KeyValuePair<string, List<TestCase>> keyValuePair in _tcList)
            {
                OutputDisplay.ShowMessage($"【{keyValuePair.Key}】数据读取......", Color.MediumVioletRed);
                Dictionary<string, List<string>> suiteTc = new Dictionary<string, List<string>>();
                List<string> tcStrList = new List<string>();
                foreach (TestCase testCase in keyValuePair.Value)
                {
                    OutputDisplay.ShowMessage(testCase.Name, Color.Chartreuse);
                    ProgressBarShow.ShowProgressValue(keyValuePair.Value.IndexOf(testCase) * 100 / keyValuePair.Value.Count);
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
                    String keywordStr = $"<keywords>";
                    foreach (string keyword in testCase.Keywords)
                    {
                        keywordStr += $"<keyword name=\"{keyword}\"><notes><![CDATA[]]></notes></keyword>";
                    }
                    fieldsStr += keywordStr + "</keywords>";
                    string tcStr = $"<testcase name=\"{testCase.Name}\">{fieldsStr}</testcase>";
                    Thread.Sleep(50);
                    tcStrList.Add(tcStr);

                    string suitenames = string.Empty;
                    testCase.TestCaseHierarchy.ForEach(item => suitenames = suitenames + "|" + item);
                    if (!suiteTc.Keys.Contains(suitenames.TrimStart('|')))
                    {
                        suiteTc.Add(suitenames.TrimStart('|'), new List<string>());
                    }
                    suiteTc[suitenames.TrimStart('|')].Add(tcStr);

                }
                ProgressBarShow.ShowProgressValue(100);
                dicCase.Add(keyValuePair.Key, tcStrList);
            }

            return dicCase;
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
                tsStr += "<testsuite name = \"测试用例集\">";
                tsStr += "<node_order><![CDATA[]]></node_order>";
                tsStr += "<details><![CDATA[]]></details>";
                sw.Write(tsStr);
                foreach (KeyValuePair<string, List<string>> casePair in this.CaseToStr())
                {
                    Dictionary<string, string> sTcs = new Dictionary<string, string>();

                    sw.Write($"<testsuite name = \"{casePair.Key}\">");
                    sw.Write("<node_order><![CDATA[]]></node_order>");
                    sw.Write("<details><![CDATA[]]></details>");
                    foreach (string str in casePair.Value)
                    {
                        sw.Write(str);
                    }
                    sw.Write("</testsuite>");
                }
                sw.Write("</testsuite>");
                sw.Close();
            }
            OutputDisplay.ShowMessage(string.Format("文件保存路勁：{0}\n", filePath), Color.Azure);
        }

        #endregion

        private string TestSuiteToStr(TestSuite ts)
        {
            string tsStr = String.Empty;
            tsStr += $"<testsuite name = \"{ts.Name}\">";
            tsStr += "<node_order><![CDATA[]]></node_order>";
            tsStr += "<details><![CDATA[]]></details>";
            if (ts.TestCases != null && ts.TestCases.Count != 0)
            {
                foreach (TestCase @case in ts.TestCases)
                {
                    tsStr += this.TestCaseToStr(@case);
                }
            }
            tsStr += ts.TestLinkStr;
            tsStr += "</testsuite>";

            return tsStr;
        }

        private string TestCaseToStr(TestCase tc)
        {
            Thread.Sleep(20);
            _allCases.Remove(tc);
            OutputDisplay.ShowMessage(tc.Name, Color.Chartreuse);
            ProgressBarShow.ShowProgressValue((_tcCount - _allCases.Count) * 100 / _tcCount);
            string fieldsStr = $"<node_order><![CDATA[{tc.NodeOrder}]]></node_order>";
            fieldsStr += $"<externalid><![CDATA[{tc.ExternalId}]]></externalid>";
            fieldsStr += $"<version><![CDATA[{tc.Version}]]></version>";
            fieldsStr += $"<summary><![CDATA[{tc.Summary}]]></summary>";
            fieldsStr += $"<preconditions><![CDATA[{tc.Preconditions}]]></preconditions>";
            fieldsStr += $"<execution_type><![CDATA[{tc.ExecutionType.ToString()}]]></execution_type>";
            fieldsStr += $"<importance><![CDATA[{tc.Importance.ToString()}]]></importance>";
            fieldsStr += $"<estimated_exec_duration>{tc.EstimatedExecDuration}</estimated_exec_duration>";
            fieldsStr += $"<status>{tc.Status}</status>";
            string tsStr = "";
            foreach (TestStep testStep in tc.TestSteps)
            {
                tsStr += "<step>";
                tsStr += $"<step_number><![CDATA[{testStep.StepNumber}]]></step_number>";
                tsStr += $"<actions><![CDATA[{testStep.Actions}]]></actions>";
                tsStr += $"<expectedresults><![CDATA[{testStep.ExpectedResults}]]></expectedresults>";
                tsStr += $"<execution_type><![CDATA[{testStep.ExecutionType}]]></execution_type>";
                tsStr += "</step>";
            }
            fieldsStr += $"<steps>{tsStr}</steps>";
            String keywordStr = $"<keywords>";
            foreach (string keyword in tc.Keywords)
            {
                keywordStr += $"<keyword name=\"{keyword}\"><notes><![CDATA[]]></notes></keyword>";
            }
            fieldsStr += keywordStr + "</keywords>";
            string tcStr = $"<testcase name=\"{tc.Name}\">{fieldsStr}</testcase>";
            return tcStr;
        }

        public void WriteXml2(string sTcs)
        {
            string filePath = $"{System.Environment.CurrentDirectory}\\TestCase_{DateTime.Now.ToString("yyyyMMddHHmmss")}_tree.xml";
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate);
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.Write(sTcs);
                sw.Close();
            }
            OutputDisplay.ShowMessage(string.Format("文件保存路勁：{0}\n", filePath), Color.Azure);
        }

        public string BuildStr(Dictionary<int, List<TestSuite>> tsDic)
        {
            for (int i = tsDic.Keys.Count; i > 1; i--)
            {
                foreach (TestSuite suite in tsDic[i])
                {
                    //TODO 存在测试套里面既有测试用例又有测试套的情况
                    string parenName = suite.NameHierarchy.ToArray()[i - 2];
                    string testStr = string.Empty;
                    testStr = TestSuiteToStr(suite);

                    foreach (TestSuite testSuite in tsDic[i - 1])
                    {
                        if (parenName.Equals(testSuite.Name))
                        {
                            testSuite.TestLinkStr += testStr;
                            break;
                        }
                    }
                }
            }

            string resultStr = string.Empty;
            foreach (TestSuite testSuite in tsDic[1])
            {
                resultStr += this.TestSuiteToStr(testSuite);
            }
            ProgressBarShow.ShowProgressValue(100);

            return resultStr;
        }


    }
}