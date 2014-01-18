using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public abstract class BaaSServiceHost
    {
        public string CompanyName { get; set; }

        public string RestApiAddress { get; set; }

        public string Version { get; set; }


    }
}
