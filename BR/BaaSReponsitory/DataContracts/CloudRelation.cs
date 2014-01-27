using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public class CloudRelation
    {
        public string __op { get; set; }

        public List<CloudPointer> objects { get; set; }
    }
}
