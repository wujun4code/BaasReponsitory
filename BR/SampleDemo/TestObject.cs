using BaaSReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SampleDemo
{
    [CloudObject(ClassName = "Person")]
    [DataContract]
    public class TestObject
    {
        /// <summary>
        /// IMPORTANT:DO NOT add [DataMember] to Primary key property.just add  [CloudFiled(ColumnName = "Key", IsPrimaryKey = true)] is OK!
        /// </summary>
        [CloudFiled(ColumnName = "Key", IsPrimaryKey = true)]
        public string Id { get; set; }

        [DataMember]
        [CloudFiled(ColumnName = "Name")]
        public string Name { get; set; }

        [DataMember]
        [CloudFiled(ColumnName = "Age")]
        public int Age { get; set; }

        public bool NonSerialize { get; set; }

        //public DateTime LastModified { get; set; }

        //public DateTime Created { get; set; }
    }
}
