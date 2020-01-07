using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            return _sourceNodes.Select(this.NodeToModel).ToList();
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

                if (node.Attributes != null) tc.Name = node.Attributes["name"].Value;
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
                            if (childNode.Attributes != null) tc.Keywords.Add(childNode.Attributes["name"].Value);
                        }
                        break;
                    case "custom_fields":
                        tc.CustomFileds = new Dictionary<string, string>();
                        foreach (XmlNode childNode in xmlNode.ChildNodes)
                        {
                            string name=string.Empty, value=string.Empty;
                            foreach (XmlNode nodeTmp in childNode.ChildNodes)
                            {
                                switch (nodeTmp.Name)
                                {
                                    case "name":
                                        name = nodeTmp.InnerText;
                                        break;
                                    case "value":
                                        value = nodeTmp.InnerText;
                                        break;
                                    default:
                                        break;

                                }
                            }
                            tc.CustomFileds.Add(name, value);
                        }
                        break;
                    case "requirements":
                        foreach (XmlNode childNode in xmlNode.ChildNodes)
                        {
                            string doc_id = string.Empty, title = string.Empty;
                            foreach (XmlNode nodeTemp in childNode.ChildNodes)
                            {
                                switch (nodeTemp.Name)
                                {
                                    case "doc_id":
                                        doc_id = nodeTemp.InnerText;
                                        break;
                                    case "title":
                                        title = nodeTemp.InnerText;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            tc.Requirement = $"{doc_id} {title}";
                        }
                        break;
                    default:
                        break;
                }
            }
            tc.TestCaseHierarchy = GetTestCaseHierarchy(node);

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