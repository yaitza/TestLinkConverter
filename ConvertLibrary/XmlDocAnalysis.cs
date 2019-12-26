using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Aspose.Words;
using ConvertModel;

namespace ConvertLibrary
{
    public class XmlDocAnalysis
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(XmlDocAnalysis));
        /// <summary>
        /// XMLDocument对象
        /// </summary>
        private readonly XmlDocument _xmlDoc;

        /// <summary>
        /// 测试需求XmlNodes
        /// </summary>
        private readonly List<XmlNode> _nodesList = new List<XmlNode>();

        

        public XmlDocAnalysis(string filePath)
        {
            if (File.Exists(filePath))
            {
                this._xmlDoc = new XmlDocument();
                this._xmlDoc.Load(filePath);
            }
            else
            {
                string message = $"{filePath}文件已不存在.";
                this._logger.Error(new Exception(message));
                throw new Exception(message);
            }
        }

        public List<Requirement> XmlToDoc()
        {
            List<Requirement> requirementsList = new List<Requirement>();
            XmlNode xn = this._xmlDoc.SelectSingleNode("requirement-specification");
            if (xn != null) this.RecursionGetNodes(xn.ChildNodes.Cast<XmlNode>().ToList());
            foreach (XmlNode node in _nodesList)
            {
                Requirement require = new Requirement();
                require.RequirementTitleHierarchy = GetRequirementHierarchy(node, "title");
                require.RequirementDocIdHierarchy = GetRequirementHierarchy(node, "doc_id");

                foreach (XmlNode nodeChildNode in node.ChildNodes.Cast<XmlNode>().ToList())
                {
                    if (nodeChildNode.Name.Equals("docid"))
                    {
                        require.DocId = nodeChildNode.InnerText;
                    }

                    if (nodeChildNode.Name.Equals("title"))
                    {
                        require.Title = " " + nodeChildNode.InnerText;
                    }

                    if (nodeChildNode.Name.Equals("description"))
                    {
                        require.Description = nodeChildNode.InnerText;
                    }
                }

                requirementsList.Add(require);
                OutputDisplay.ShowMessage($"{require.DocId} {require.Title}", Color.Chartreuse);
                Thread.Sleep(100);
            }

            return requirementsList;
        }

        public void WriteDoc(List<Requirement> requirementsList)
        {
            Document doc = new Document();
            DocumentBuilder builder = new DocumentBuilder(doc);
            List<string> hierarchyArray = new List<string>();
            foreach (Requirement requirement in requirementsList)
            {
                if (hierarchyArray.Count == 0)
                {
                    hierarchyArray.AddRange(requirement.RequirementDocIdHierarchy);
                    for (int i = 0; i < requirement.RequirementDocIdHierarchy.Count; i++)
                    {
                        builder.InsertHtml($"<h{i + 1 }>{requirement.RequirementDocIdHierarchy[i]} {requirement.RequirementTitleHierarchy[i]}</h{i + 1}>");
                    }
                }
                else
                {
                    for (int i = 0; i < requirement.RequirementDocIdHierarchy.Count; i++)
                    {
                        if (!hierarchyArray[i].Equals(requirement.RequirementDocIdHierarchy[i]))
                        {
                            hierarchyArray[i] = requirement.RequirementDocIdHierarchy[i];
                            builder.InsertHtml($"<h{i + 1 }>{requirement.RequirementDocIdHierarchy[i]} {requirement.RequirementTitleHierarchy[i]}</h{i + 1}>");
                        }
                    }

                }

                builder.InsertHtml($"<h{requirement.RequirementDocIdHierarchy.Count + 1}>{requirement.DocId} {requirement.Title}</h{requirement.RequirementDocIdHierarchy.Count + 1}>");
                builder.InsertHtml(requirement.Description);
            }
            string filePath = $"{System.Environment.CurrentDirectory}\\Requirement_{DateTime.Now.ToString("yyyyMMddHHmmss")}.doc";
            doc.Save(filePath);
            OutputDisplay.ShowMessage($"文件保存路勁：{filePath}\n", Color.Azure);
        }

        /// <summary>
        /// 递归获取测试套下所有测试用例
        /// </summary>
        /// <param name="xmlNodes">根节点下子节点集合</param>
        private void RecursionGetNodes(List<XmlNode> xmlNodes)
        {
            foreach (XmlNode node in xmlNodes)
            {
                if (node.Name.Equals("req_spec"))
                {
                    List<XmlNode> childNodes = node.ChildNodes.Cast<XmlNode>().ToList();

                    RecursionGetNodes(childNodes);
                }
                else
                {
                    if (node.Name.Equals("requirement"))
                    {
                        this._nodesList.Add(node);
                    }
                }
            }
        }

        private List<string> GetRequirementHierarchy(XmlNode xmlNode, string type)
        {
            List<string> suiteNames = new List<string>();
            RecursionGetRequirementParentNodes(xmlNode, suiteNames, type);

            return suiteNames;
        }

        private void RecursionGetRequirementParentNodes(XmlNode xmlNode, List<string> suiteNames, string type)
        {
            if (xmlNode.ParentNode.Name.Equals("req_spec"))
            {
                RecursionGetRequirementParentNodes(xmlNode.ParentNode, suiteNames, type);
            }

            if (xmlNode.Name.Equals("req_spec"))
            {
                suiteNames.Add(xmlNode.Attributes[type].Value);
            }
        }
    }
}
