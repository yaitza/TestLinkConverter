using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConvertModel;

namespace ConvertLibrary
{
    class TestSuite
    {
        public string Name { get; set; }

        public List<TestSuite> TestSuites { get; set; }

        public List<TestCase> TestCases { get; set; }
    }
}
