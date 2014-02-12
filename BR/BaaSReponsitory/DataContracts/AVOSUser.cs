using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
#if FRAMEWORK
    [CloudObject(BaaSServiceProvider = "AVOSCloudRest", BaaSServiceProviderAssemblyName = "BaaSReponsitory", ModeType = CloudObjectType.User)]
#endif
#if WINDOWS_PHONE
    [CloudObject(BaaSServiceProvider = "AVOSCloudRest", BaaSServiceProviderAssemblyName = "BaaSReponsitory.WindowsPhone", ModeType = CloudObjectType.User)]
#endif
    [DataContract]
    public class AVOSUser : CloudUser
    {
        private string _userName;

        [DataMember(Name = "username")]
        public override string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }
        private string _password;

        [DataMember(Name = "password")]
        public override string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        private string _email;

        [DataMember(Name = "email")]
        public override string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        public string sessionToken { get; set; }
    }
}
