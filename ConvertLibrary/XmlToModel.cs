using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                tc.InternalId = node.Attributes["internalid"].Value;
                tc.Name = node.Attributes["name"].Value;
            }
            catch (NullReferenceException ex)
            {
                this._logger.Error(node.InnerText, ex);
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
                        tc.ExecutionType = (ExecType) int.Parse(xmlNode.InnerText);
                        break;
                    case "importance":
                        tc.Importance = (ImportanceType) int.Parse(xmlNode.InnerText);
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
                            ts.ExecutionType = (ExecType) int.Parse(xNode.InnerText);
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