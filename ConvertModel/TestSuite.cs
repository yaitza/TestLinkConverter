using System.Collections.Generic;

namespace ConvertModel
{
    public class TestSuite
    {
        public string Name { get; set; }

        public List<string> NameHierarchy { get; set; }

        public List<TestCase> TestCases { get; set; }

        public string UnqiueCode
        {
            get
            {
                string str = string.Empty;
                foreach (string s in NameHierarchy)
                {
                    str += s + ",";
                }
                return str.TrimEnd(',');
            }
        }

        public string TestLinkStr { get; set; }
    }
}
