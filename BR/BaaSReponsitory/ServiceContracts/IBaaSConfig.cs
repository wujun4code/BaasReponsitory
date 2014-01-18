using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public interface IBaaSConfig
    {
        void LoadConfig();

        IEnumerable<IBaaSHostConfig> HostConfigs { get; set; }

        IEnumerable<IBaaSCloudClassConfig> ClassConfigs { get; set; }

    }

    public interface IBaaSHostConfig
    {
         string key { get; set; }

         string name { get; set; }

         string assemblyName { get; set; }

         string targetVersion { get; set; }

         string appId { get; set; }

         string appKey { get; set; }

         string restApiAddress { get; set; }

         string apiVersion { get; set; }
    }

    public interface IBaaSCloudClassConfig
    {
         string ClassName { get; set; }

         string assemblyName { get; set; }

         string HostKey { get; set; }
    }
}
