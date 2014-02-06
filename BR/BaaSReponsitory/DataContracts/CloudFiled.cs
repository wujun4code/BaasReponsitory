using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public class CloudFiled : Attribute
    {
        public string ColumnName { get; set; }

        public bool IsPrimaryKey { get; set; }

        public bool IsRelation { get; set; }

        public CloudFiledType RelationType { get; set; }

    }

    public enum CloudFiledType : int
    {
        GeneralField = 0,
        OneToOne = 101,//Pointer each other
        OneToMany = 102,//Relation
        ManyToOne = 103,//Pointer
    }
}
