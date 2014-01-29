using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public class CloudOpponents
    {
        public string __op { get; set; }
        public List<AVOSRelatedTo> objects { get; set; }
    }

    [DataContract]
    public class CloudOpponentsRootWrapper
    {
        public string ColumnName { get; set; }

        [DataMember]
        public CloudOpponents ColumnNameX { get; set; }

        [OnSerializing]
        void OnSerializing(StreamingContext context)
        {
            
        }
    }
}
