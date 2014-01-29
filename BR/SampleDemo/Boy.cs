using BaaSReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SampleDemo
{
    [CloudObject(ClassName = "Boy")]
    [DataContract]
    public class Boy
    {
        [CloudFiled(ColumnName = "Key", IsPrimaryKey = true)]
        public string Id { get; set; }

        [DataMember(Name = "FullName")]
        public string FullName { get; set; }

        [DataMember(Name = "NickName")]
        public string NickName { get; set; }

        [CloudFiled(ColumnName = "FocusdGirlType", RelationType = CloudFiledType.OneToMany)]
        public List<GirlType> FocusdGirlType { get; set; }
    }
}
