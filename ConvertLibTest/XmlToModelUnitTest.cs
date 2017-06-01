using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransferLibrary;
using TransferModel;

namespace TransferLibTest
{
    [TestClass]
    public class XmlToModelUnitTest
    {
        private List<XmlNode> _xmlNodes;

        private XmlNode _node;


        [TestInitialize]
        public void SetUp()
        {
            string filepath = @"F:\Code\TestLinkTransfer\trunk\Resource\CRM.xml";

            XmlAnalysis xmlAnalysis = new XmlAnalysis(filepath);
            this._xmlNodes = xmlAnalysis.GetAllTestCaseNodes();
            this._node = this._xmlNodes[0];
        }

        [TestMethod]
        public void OutputTestCasesTest()
        {
            XmlToModel xtm = new XmlToModel(this._xmlNodes);
            List<TestCase> tcList = xtm.OutputTestCases();

            Assert.AreEqual(tcList.Count, _xmlNodes.Count);
        }


//        [TestMethod]
//        public void NodeToModelTest()
//        {
//            XmlToModel xtm = new XmlToModel(this._xmlNodes);
//
//            var tc = xtm.NodeToModel(this._node);
//
//            Assert.AreEqual(0,0);
//        }

//        [TestMethod]
//        public void GetAllStepsTest()
//        {
//            XmlNode stepNode = null;
//            foreach (XmlNode node in this._node.ChildNodes)
//            {
//                if (node.Name.Equals("steps"))
//                {
//                    stepNode = node;
//                }
//            }
//
//            XmlToModel xtm = new XmlToModel(this._xmlNodes);
//            if (stepNode != null)
//            {
//                xtm.GetAllSteps(stepNode);
//            }
//
//            Assert.AreEqual(0, 0);
//
//        }


    }
}
