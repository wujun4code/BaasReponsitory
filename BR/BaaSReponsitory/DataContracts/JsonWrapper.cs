using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public abstract class JsonWrapper<TKey>
    {
        public abstract TKey PrimaryKey { get; set; }

    }
}
