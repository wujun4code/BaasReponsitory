﻿using BaaSReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SampleDemo
{
    [CloudObject(ClassName = "Girl")]
    [DataContract]
    public class Girl
    {
        [CloudFiled(ColumnName = "Key", IsPrimaryKey = true)]
        public string Id { get; set; }

        [DataMember(Name = "FullName")]
        public string FullName { get; set; }

        [DataMember(Name = "NickName")]
        public string NickName { get; set; }

        [CloudFiled(ColumnName = "MyStyle", IsRelation=true, RelationType = CloudFiledType.ManyToOne)]
        public GirlType MyStyle { get; set; }

    }
}
