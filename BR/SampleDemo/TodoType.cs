using BaaSReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SampleDemo
{
    [CloudObject(ClassName = "TodoType")]//ClassName means the table name in Parse.com
    [DataContract]//for json serialize
    public class TodoType
    {
        [CloudFiled(ColumnName = "Key", IsPrimaryKey = true)]
        public string ItemId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

    }
}
