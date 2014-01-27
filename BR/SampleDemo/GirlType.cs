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

        [CloudFiled(IsPointer = true, PointerTarget = "Boy", PointerPrimaryKeyValueName = "_focusBoyIds", PointerObjctValueName = "FocusdBoys")]
        [DataMember(Name = "FocusdBoys")]
        public CloudRelation _focusdBoys { get; set; }

        public List<Boy> FocusdBoys { get; set; }
        public List<string> _focusBoyIds { get; set; }



        [CloudFiled(IsPointer = true, PointerTarget = "Girl", PointerPrimaryKeyValueName = "_girlInTypeIds", PointerObjctValueName = "GirlsInType")]
        [DataMember(Name = "GirlsInType")]
        public CloudRelation _girlsInType { get; set; }

        public List<Girl> GirlsInType { get; set; }
        public List<string> _girlInTypeIds { get; set; }

    }
}
