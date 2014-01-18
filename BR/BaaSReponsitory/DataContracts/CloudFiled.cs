using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public class CloudFiled:Attribute
    {
        public string ColumnName { get; set; }

        public bool IsPrimaryKey { get; set; }

    }
}
