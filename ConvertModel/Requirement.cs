using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertModel
{
    public class Requirement
    {
        public string DocId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<string> RequirementDocIdHierarchy { get; set; }

        public List<string> RequirementTitleHierarchy { get; set; }

    }
}
