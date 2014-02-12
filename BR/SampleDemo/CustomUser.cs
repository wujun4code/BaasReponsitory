using BaaSReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SampleDemo
{
    [DataContract]
    public class CustomUser : AVOSUser
    {
        [DataMember]
        public string mobilePhone { get; set; }
        [DataMember]
        public DateTime birthday { get; set; }
    }
}
