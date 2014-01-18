using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public static class BRDic
    {
        public const string ConfigSectionKey = "BaaSConfigurationSection";

        public const string cloudObjectKey = "CloudClasses";

        public const string BaaSHostskey = "BaaSHosts";

        public const string HostRootNameKey = "Host";

        public const string ClassNameKey = "ClassName";

        public const string AssemblyNameKey = "assemblyName";

        public const string HostKeyKey = "hostKey";

        public const string keyKey = "key";
        public const string nameKey = "name";
        public const string assemblyNameKey = "assemblyName";
        public const string targetVersionKey = "targetVersion";
        public const string appIdKey = "appId";
        public const string appkeyKey = "restApiAppkey";
        public const string restApiAddressKey = "restApiAddress";
        public const string apiVersionKey = "apiVersion";

        public const string modelKey = "model"
;

        private static IBaaSConfig _runtimeConfig;
        public static IBaaSConfig RuntimeConfig
        {
            get
            {
                if (_runtimeConfig == null)
                {
#if FRAMEWORK

                    _runtimeConfig = new SampleBaaSConfig();
                   
#endif
                    
#if WINDOWS_PHONE
                    _runtimeConfig = new BaaSSampleWPConfig();
#endif


                    _runtimeConfig.LoadConfig();
                }

                return _runtimeConfig;
            }

            set
            {
                _runtimeConfig = value;
            }
        }

        public static IBaaSHostConfig GetHostByClassInfo(string className, string assemblyName)
        {
            IBaaSCloudClassConfig targetModel = null;

            IBaaSHostConfig targetHost = null;

            foreach (var model in BRDic.RuntimeConfig.ClassConfigs.AsEnumerable())
            {
                if (model.ClassName == className)
                {
                    targetModel = model;
                    break;
                }
            }

            foreach (var host in BRDic.RuntimeConfig.HostConfigs.AsEnumerable())
            {
                if (host.key == targetModel.HostKey)
                {
                    targetHost = host;
                    break;
                }
            }

            return targetHost;
        }
    }

}
