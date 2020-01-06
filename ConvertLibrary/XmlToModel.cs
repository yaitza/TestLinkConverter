using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Xml;
using ConvertModel;
using log4net;

namespace ConvertLibrary
{
    public class XmlToModel
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof (XmlToModel));

        private readonly List<XmlNode> _sourceNodes; 
        public XmlToModel(List<XmlNode> xmlNodes)
        {
            this._sourceNodes = xmlNodes;
        }

        public List<TestCase> OutputTestCases()
        {
            List<TestCase> tcList = new List<TestCase>();
            foreach (XmlNode node in _sourceNodes)
            {
                tcList.Add(NodeToModel(node));
                ProgressBarShow.ShowProgressValue(this._sourceNodes.IndexOf(node) * 100 / (this._sourceNodes.Count-1));
            }

            return tcList;
        }

        /// <summary>
        /// Node转为Model
        /// </summary>
        /// <param name="node">XML节点Node</param>
        /// <returns>TestCase Model</returns>
        private TestCase NodeToModel(XmlNode node)
        {
            TestCase tc = new TestCase();

            try
            {
                if (node.Attributes != null && node.Attributes.Count == 0)
                {
                    return tc;
                }
                if (node.Attributes != null && node.Attributes.Count != 1)
                {
                    tc.InternalId = node.Attributes["internalid"].Value;
                }

                if (node.Attributes != null)
                {
                    tc.Name = node.Attributes["name"].Value;
                }
            }
            catch (NullReferenceException ex)
            {
                this._logger.Error("用例名称为空", ex);
                OutputDisplay.ShowMessage("用例名称为空", Color.Red);
            }
            
               
            foreach (XmlNode xmlNode in node)
            {
                switch (xmlNode.Name)
                {
                    case "node_order":
                        tc.NodeOrder = xmlNode.InnerText;
                        break;
                    case "externalid":
                        tc.ExternalId = xmlNode.InnerText;
                        break;
                    case "version":
                        tc.Version = xmlNode.InnerText;
                        break;
                    case "summary":
                        tc.Summary = xmlNode.InnerText;
                        break;
                    case "preconditions":
                        tc.Preconditions = xmlNode.InnerText;
                        break;
                    case "execution_type":
                        tc.ExecutionType = CommonHelper.StrToExecType(xmlNode.InnerText);
                        break;
                    case "importance":
                        tc.Importance = CommonHelper.StrToImportanceType(xmlNode.InnerText);
                        break;
                    case "estimated_exec_duration":
                        if (xmlNode.InnerText.Equals(""))
                        {
                            tc.EstimatedExecDuration = 0.0;
                            break;
                        }
                        tc.EstimatedExecDuration = double.Parse(xmlNode.InnerText);
                        break;
                    case "status":
                        tc.Status = (StatusType) int.Parse(xmlNode.InnerText);
                        break;
                    case "steps":
                        tc.TestSteps = GetAllSteps(xmlNode);
                        break;
                    case "keywords":
                        tc.Keywords = new List<string>();
                        foreach (XmlNode childNode in xmlNode.ChildNodes)
                        {
                            if (childNode.Attributes != null)
                            {
                                tc.Keywords.Add(childNode.Attributes["name"].Value);
                            }
                        }
                        break;
                    case "requirement":
                        tc.Requirement = xmlNode.InnerText;
                        break;
                    default:
                        break;
                }
            }
            tc.TestCaseHierarchy = GetTestCaseHierarchy(node);
            OutputDisplay.ShowMessage(tc.Name, Color.Chartreuse);
            Thread.Sleep(100);
            return tc;
        }

        /// <summary>
        /// 获取测试步骤
        /// </summary>
        /// <param name="xmlNode">XML Node</param>
        /// <returns>List TestStep</returns>
        private static List<TestStep> GetAllSteps(XmlNode xmlNode)
        {
            List<TestStep> stepsList = new List<TestStep>();
            foreach (XmlNode node in xmlNode.ChildNodes)
            {
                TestStep ts = new TestStep();
                foreach (XmlNode xNode in node)
                {
                    switch (xNode.Name)
                    {
                        case "step_number":
                            ts.StepNumber = int.Parse(xNode.InnerText);
                            break;
                        case "actions":
                            ts.Actions = CommonHelper.DelTags(xNode.InnerText);
                            break;
                        case "expectedresults":
                            ts.ExpectedResults = CommonHelper.DelTags(xNode.InnerText);
                            break;
                        case "execution_type":
                            ts.ExecutionType = CommonHelper.StrToExecType(xNode.InnerText);
                            break;
                        default:
                            break;
                    }
                }
                stepsList.Add(ts);
            }
            return stepsList;
        }

        private List<string> GetTestCaseHierarchy(XmlNode xmlNode)
        {
            List<string> suiteNames = new List<string>();
            RecursionGetNodes(xmlNode, suiteNames);

            return suiteNames;
        }

        private void RecursionGetNodes(XmlNode xmlNode, List<string> suiteNames)
        {
            if (xmlNode.ParentNode.Name.Equals("testsuite"))
            {
                RecursionGetNodes(xmlNode.ParentNode, suiteNames);
            }

            if (xmlNode.Name.Equals("testsuite"))
            {
                suiteNames.Add(xmlNode.Attributes["name"].Value);
            }
        }
    }
}