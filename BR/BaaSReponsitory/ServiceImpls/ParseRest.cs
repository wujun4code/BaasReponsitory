using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public class ParseRest<TEntity> : BaseRestBaaS<string, AVOSCloudJsonWrapper, TEntity>
         where TEntity : class
    {
        public ParseRest()
            : base()
        {
            var type = typeof(TEntity);

            var className = type.Name;

            var hostInfo = BRDic.GetHostByClassInfo(className, type.Assembly.FullName);

            this.CustomHeaders = new Dictionary<string, string>();

            this.CustomHeaders.Add("X-Parse-Application-Id", hostInfo.appId);
            this.CustomHeaders.Add("X-Parse-REST-API-Key", hostInfo.appKey);

            this.RestApiRootAddress = hostInfo.restApiAddress;

            this.RestApiVersion = hostInfo.apiVersion;

            this.ListJsonRootNodeName = "results";

            this.Client = new RestClient(this.RestApiRootAddress + "/" + this.RestApiVersion);

        }

        public override string RestApiRootAddress { get; set; }

        public override string RestApiVersion { get; set; }

        public override Dictionary<string, string> CustomHeaders { get; set; }

        public override string ListJsonRootNodeName { get; set; }

        public override IRestClient Client { get; set; }

        public override string CreateResource()
        {
            var rtn = "";

            var type = typeof(TEntity);

            var cloudObjectAttribute = type.GetCustomAttributes(typeof(CloudObject), true);

            if (cloudObjectAttribute.Length > 0)
            {
                var cloudOA = cloudObjectAttribute[0];

                rtn = "classes/" + ((CloudObject)cloudOA).ClassName;
            }

            return rtn;
        }
    }
}
