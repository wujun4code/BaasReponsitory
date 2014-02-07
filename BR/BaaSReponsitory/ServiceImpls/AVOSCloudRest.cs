using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public class AVOSCloudRest<TEntity> : BaseRestBaaS<string, AVOSJsonWrapper, TEntity>
        where TEntity : class
    {
        public AVOSCloudRest()
            : base()
        {
            var type = typeof(TEntity);

            var className = type.Name;

            var hostInfo = BRDic.GetHostByClassInfo(className, type.Assembly.FullName);

            this.CustomHeaders = new Dictionary<string, string>();

            this.CustomHeaders.Add("X-AVOSCloud-Application-Id", hostInfo.appId);
            this.CustomHeaders.Add("X-AVOSCloud-Application-Key", hostInfo.appKey);

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

        public override string SerializeEntityToPost(TEntity entity)
        {
            return base.SerializeEntityToPost(entity);
        }

        public override TEntity DeserializerFromResponse(IRestResponse rep)
        {

            var rtn = base.DeserializerFromResponse(rep);

            var type = typeof(TEntity);
            JObject rtnJObj = JObject.Parse(rep.Content);
            var properties = type.GetProperties();

            foreach (var pro in properties)
            {
                var pt = pro.PropertyType;
                var cf = pro.GetCustomAttributes(typeof(CloudFiled), true);
                if (cf != null)
                {
                    if (cf.Length > 0)
                    {
                        var cfInfo = (CloudFiled)cf[0];

                        if (cfInfo.RelationType == CloudFiledType.ManyToOne
                            || cfInfo.RelationType == CloudFiledType.OneToOne)
                        {
                            var pointerInfo = rtnJObj[cfInfo.ColumnName]["objectId"];

                            var pointerObj = Activator.CreateInstance(pt);
                            SetTEntityId(pointerObj, (string)pointerInfo);
                            pro.SetValue(rtn, pointerObj);
                        }
                    }
                }

            }


            return rtn;
        }
    }
}
