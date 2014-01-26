using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory.DataContracts
{
    public class CloudRelation
    {
        public string __type { get; set; }

        public string className { get; set; }

        public List<CloudPointer> objects { get; set; }
    }
}
