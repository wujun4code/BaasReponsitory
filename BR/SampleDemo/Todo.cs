using BaaSReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SampleDemo
{
    [CloudObject(ClassName = "Todo")]
    [DataContract]
    public class Todo
    {
        /// <summary>
        /// IMPORTANT:DO NOT add [DataMember] to Primary key property.just add  [CloudFiled(ColumnName = "Key", IsPrimaryKey = true)] is OK!
        /// </summary>
        [CloudFiled(ColumnName = "Key", IsPrimaryKey = true)]
        public string ItemId { get; set; }


        [DataMember]
        [CloudFiled(ColumnName = "Title")]
        public string Title { get; set; }

        [DataMember]
        [CloudFiled(ColumnName = "Status")]
        public int Status { get; set; }

        [DataMember]
        [CloudFiled(ColumnName = "StartTime")]
        public DateTime StartTime { get; set; }

        [DataMember]
        [CloudFiled(ColumnName = "Content")]
        public string Content { get; set; }

        [DataMember]
        [CloudFiled(ColumnName = "From")]
        public string From { get; set; }

        public bool NonSerialize { get; set; }

    }
}
