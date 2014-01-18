using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public class CloudObject : Attribute
    {
        public string ClassName { get; set; }

        public string ReponsitoryName { get; set; }

        public string ReponsitoryAssemblyName { get; set; }
    }
}
