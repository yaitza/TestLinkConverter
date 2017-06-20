using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;
using log4net;
using TransferModel;

namespace TransferLibrary
{
    public class XmlToModel
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof (XmlToModel));

        private List<XmlNode> _sourceNodes; 
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
                if (node.Attributes.Count != 1)
                {
                    tc.InternalId = node.Attributes["internalid"].Value;
                }
                tc.Name = node.Attributes["name"].Value;
            }
            catch (NullReferenceException ex)
            {
                this._logger.Error("用例名称为空", ex);
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
                        tc.Summary = CommonHelper.DelTags(xmlNode.InnerText);
                        break;
                    case "preconditions":
                        tc.Preconditions = CommonHelper.DelTags(xmlNode.InnerText);
                        break;
                    case "execution_type":
                        
                        tc.ExecutionType = StrToExecType(xmlNode.InnerText);
                        break;
                    case "importance":
                        tc.Importance = StrToImportanceType(xmlNode.InnerText);
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
                        tc.TestSteps = this.GetAllSteps(xmlNode);
                        break;
                    //TODO KeyWords未解析
                    //TODO Requirements未解析
                    default:
                        break;
                }
            }
            return tc;
        }

        private ImportanceType StrToImportanceType(string innerText)
        {
            if (Regex.IsMatch(innerText, @"^[+-]?\d*[.]?\d*$"))
            {
                return (ImportanceType)int.Parse(innerText);
            }
            else
            {
                switch (innerText)
                {
                    case "高":
                    case "high":
                        return ImportanceType.高;
                    case "中":
                    case "medium":
                        return ImportanceType.中;
                    case "低":
                    case "low":
                        return ImportanceType.低;
                    default:
                        return ImportanceType.高;
                }
            }
        }

        private ExecType StrToExecType(string innerText)
        {
            if (Regex.IsMatch(innerText, @"^[+-]?\d*[.]?\d*$"))
            {
                return (ExecType) int.Parse(innerText);
            }
            else
            {
                switch (innerText)
                {
                    case "手动":
                        return ExecType.手动;
                    case "自动":
                        return ExecType.自动;
                    default:
                        return ExecType.手动; 
                }
            }
        }

        /// <summary>
        /// 获取测试步骤
        /// </summary>
        /// <param name="xmlNode">XML Node</param>
        /// <returns>List TestStep</returns>
        private List<TestStep> GetAllSteps(XmlNode xmlNode)
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
                            ts.ExecutionType = StrToExecType(xNode.InnerText);
                            break;
                        default:
                            break;
                    }
                }
                stepsList.Add(ts);
            }
            return stepsList;
        }
    }
}