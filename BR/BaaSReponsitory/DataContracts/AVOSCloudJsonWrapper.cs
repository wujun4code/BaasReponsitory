using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public class AVOSCloudJsonWrapper : JsonWrapper<string>
    {
        public override string PrimaryKey
        {
            get
            {
                return objectId;
            }
            set
            {
                PrimaryKey = value;
            }
        }

        public override string ErrorCode
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
            }
        }

        public override string ErrorMessage
        {
            get
            {
                return error;
            }
            set
            {
                error = value;
            }
        }

        public string objectId { get; set; }

        public string createdAt { get; set; }

        public string updatedAt { get; set; }

        public string code { get; set; }

        public string error { get; set; }

    }
}
