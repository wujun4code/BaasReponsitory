using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    [DataContract]
    public class AVOSRelationFiter
    {
        [DataMember(Name="object")]
        public AVOSRelatedTo relatedTo { get; set; }

        [DataMember]
        public string key { get; set; }

    }

    [DataContract]
    public class AVOSRelatedTo
    {
        [DataMember]
        public string __type { get; set; }
        [DataMember]
        public string className { get; set; }
        [DataMember]
        public string objectId { get; set; }
    }

    [DataContract]
    public class AVOSRelatedToRootWrapper
    {
        [DataMember(Name = "$relatedTo")]
        public AVOSRelationFiter relatedTo { get; set; }
    }
}
