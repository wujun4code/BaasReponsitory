using BaaSReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SampleDemo
{
    /// <summary>
    /// a demo class for BR.
    /// </summary>
    [CloudObject(ClassName = "Todo")]//ClassName means the table name in Parse.com
    [DataContract]//for json serialize
    public class Todo
    {
        /// <summary>
        /// IMPORTANT:DO NOT add [DataMember] to Primary key property.just add  [CloudFiled(ColumnName = "Key", IsPrimaryKey = true)] is OK!
        /// IsPrimaryKey= true means the logic PrimaryKey for your business,
        /// because Parse.com will create a Primary Key for you named "objectId", it looks like "xyk7Gd1",
        /// but when you get a object from server ,this Property will be replaced by objectId by BR.
        /// </summary>
        [CloudFiled(ColumnName = "Key", IsPrimaryKey = true)]
        public string ItemId { get; set; }


        /// <summary>
        /// general property.
        /// </summary>
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


        /// <summary>
        /// without [DataMember] attribute, it will NOT be sent to server.
        /// </summary>
        public bool NonSerialize { get; set; }

    }
}
