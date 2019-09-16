using ConvertModel;
using System.Collections.Generic;
using System.Linq;

namespace ConvertLibrary
{
    public class TestSuiteGenerator
    {
        private readonly Dictionary<string, List<TestCase>> _tcList;

        public TestSuiteGenerator(Dictionary<string, List<TestCase>> tcCases)
        {
            this._tcList = tcCases;
        }

        public void BuildTestSuite()
        {
            Dictionary<string, List<TestCase>> suiteTc = new Dictionary<string, List<TestCase>>();
            Dictionary<int, List<TestSuite>> tsDic = new Dictionary<int, List<TestSuite>>();

            foreach (KeyValuePair<string, List<TestCase>> keyValuePair in _tcList)
            {
                foreach (TestCase @case in keyValuePair.Value)
                {
                    string suitenames = string.Empty;
                    @case.TestCaseHierarchy.ForEach(item => suitenames = suitenames + "|" + item);
                    if (!suiteTc.Keys.Contains(suitenames.TrimStart('|')))
                    {
                        suiteTc.Add(suitenames.TrimStart('|'), new List<TestCase>());
                    }
                    suiteTc[suitenames.TrimStart('|')].Add(@case);
                }
            }

            foreach (KeyValuePair<string, List<TestCase>> keyValuePair in suiteTc)
            {
                string[] suiteArray = keyValuePair.Key.Split('|');
                int countSuite = suiteArray.Length;
                for (int i = 1; i < countSuite; i++)
                {
                    TestSuite tsTemp = new TestSuite { Name = suiteArray[i - 1], NameHierarchy = suiteArray.Take(i).ToList() };
                   
                    if (!tsDic.ContainsKey(i))
                    {
                        tsDic.Add(i, new List<TestSuite>());
                    }

                    if (!tsDic[i].Exists(item => item.Name == tsTemp.Name))
                    {
                        tsDic[i].Add(tsTemp);
                    }
                }

                string suiteName = suiteArray[countSuite - 1];
                TestSuite ts = new TestSuite { Name = suiteName, TestCases = keyValuePair.Value, NameHierarchy = suiteArray.ToList() };
                MyTree<TestSuite> tsTree = new MyTree<TestSuite>(ts);
                if (!tsDic.ContainsKey(countSuite))
                {
                    tsDic.Add(countSuite, new List<TestSuite>());
                }

                if (!tsDic[countSuite].Exists(item => item.Name == ts.Name))
                {
                    tsDic[countSuite].Add(ts);
                }
            }
        }

        public void BuildTree(Dictionary<int, List<TestSuite>> tsDic)
        {
            TestSuite ts = new TestSuite();
            ts.Name = "mySheet";
            TreeNode myTreeNodes = new TreeNode(ts);

            foreach (KeyValuePair<int, List<TestSuite>> pair in tsDic)
            {
                foreach (TestSuite testSuite in pair.Value)
                {
                    var item = myTreeNodes.AddChildNode(testSuite);
                }

            }

        }
    }
}
