using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using log4net;

namespace ConvertLibrary
{
    public class XmlAnalysis
    {
        // TODO 未实现获取测试用例并获取对应测试套
        private readonly ILog _logger = LogManager.GetLogger(typeof(XmlAnalysis));
        /// <summary>
        /// XMLDocument对象
        /// </summary>
        private readonly XmlDocument _xmlDoc;

        /// <summary>
        /// 测试用例XmlNodes
        /// </summary>
        private readonly List<XmlNode> _nodesList = new List<XmlNode>();

        public XmlAnalysis(string filePath)
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

        /// <summary>
        /// 获取所有标记为TestCase的节点数据
        /// </summary>
        /// <returns>所有TestCase节点</returns>
        public List<XmlNode> GetAllTestCaseNodes()
        {
            XmlNode xn = this._xmlDoc.SelectSingleNode("testsuite");

            if (xn == null)
            {
                this._logger.Warn(new Exception("对应导出xml无测试用例数据."));
                throw new Exception("对应导出xml无测试用例数据.");
            }
            List<XmlNode> xnList = xn.ChildNodes.Cast<XmlNode>().Where(xmlNode => xmlNode.Name.Equals("testsuite")).ToList();

            if (xnList.Count == 0)
            {
                foreach (XmlNode node in xn.ChildNodes)
                {
                    this._nodesList.Add(node);
                }
            }
            else
            {
                RecursionGetNodes(xnList);
            }

            return this._nodesList;
        }
        
        /// <summary>
        /// 递归获取测试套下所有测试用例
        /// </summary>
        /// <param name="xmlNodes">根节点下子节点集合</param>
        private void RecursionGetNodes(List<XmlNode> xmlNodes)
        {
            foreach (XmlNode node in xmlNodes)
            {
                if (node.Name.Equals("testsuite"))
                {
                    List<XmlNode> childNodes = node.ChildNodes.Cast<XmlNode>().ToList();

                    RecursionGetNodes(childNodes);
                }
                else
                {
                    if (node.Name.Equals("testcase"))
                    {
                        this._nodesList.Add(node);
                    }
                }
            }
        }
    }
}
