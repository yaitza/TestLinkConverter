using System.Collections.Generic;
using System.Linq;
using System.Xml;
using TransferModel;

namespace TransferLibrary
{
    public class XmlToModel
    {
        private List<XmlNode> _sourceNodes; 
        public XmlToModel(List<XmlNode> xmlNodes)
        {
            this._sourceNodes = xmlNodes;
        }

        public List<TestCase> OutputTestCases()
        {
            List<TestCase> tcList = new List<TestCase>();
            foreach (XmlNode sourceNode in _sourceNodes)
            {
                tcList.Add(this.NodeToModel(sourceNode));
            }
            return tcList;
        }

        private TestCase NodeToModel(XmlNode node)
        {
            TestCase tc = new TestCase();
            tc.InternalId = node.Attributes["internalid"].Value;
            tc.Name = node.Attributes["name"].Value;
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