using BaaSReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SampleDemo
{
    [CloudObject(ClassName = "GirlType")]
    [DataContract]
    public class GirlType
    {
        [CloudFiled(ColumnName = "Key", IsPrimaryKey = true)]
        public string Id { get; set; }

        [DataMember(Name = "TypeName")]
        public string TypeName { get; set; }

        [CloudFiled(ColumnName = "FocusdBoys", RelationType = CloudFiledType.OneToMany)]
        public List<Boy> FocusdBoys { get; set; }

        [CloudFiled(ColumnName = "GirlsMatchType", IsRelation = true, RelationType = CloudFiledType.OneToMany)]
        public List<Girl> GirlsMatchType { get; set; }

    }
}
