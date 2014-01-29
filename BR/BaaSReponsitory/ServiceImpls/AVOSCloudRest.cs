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
            var type = typeof(TEntity);

            var properties = type.GetProperties();

            foreach (var pro in properties)
            {
                var pt = pro.PropertyType;
                if (pt == typeof(CloudPointer))
                {
                    var data = pro.GetValue(entity);
                    if (data == null)
                    {
                        var cf = pro.GetCustomAttributes(typeof(CloudFiled), true);
                        if (cf != null)
                        {
                            if (cf.Length > 0)
                            {
                                var cfInfo = (CloudFiled)cf[0];
                                if (cfInfo.RelationType == CloudFiledType.ManyToOne)
                                {
 
                                }

                                //var cp = new CloudPointer();
                                //cp.__type = "Pointer";
                                //cp.className = cfInfo.PointerTarget;

                                //var pv = type.GetProperty(cfInfo.PointerPrimaryKeyValueName);
                                //var pointerId = (string)pv.GetValue(entity);

                                //cp.objectId = pointerId;

                                //pro.SetValue(entity, cp);
                            }
                        }
                    }
                }
               
            }

            return base.SerializeEntityToPost(entity);
        }

        public override TEntity DeserializerFromResponse(IRestResponse rep)
        {

            var rtn = base.DeserializerFromResponse(rep);

            var type = typeof(TEntity);

            var properties = type.GetProperties();

            foreach (var pro in properties)
            {
                var pt = pro.PropertyType;
                if (pt == typeof(CloudPointer))
                {
                    var data = pro.GetValue(rtn);
                    if (data != null)
                    {
                        var cf = pro.GetCustomAttributes(typeof(CloudFiled), true);
                        if (cf != null)
                        {
                            if (cf.Length > 0)
                            {
                                var cfInfo = (CloudFiled)cf[0];

                                var cp = (CloudPointer)data;

                                var pv = type.GetProperty("xxx");

                                pv.SetValue(rtn, cp.objectId);
                            }
                        }
                    }
                }
                else if (pt == typeof(CloudRelation))
                {
                    var data = pro.GetValue(rtn);
                    if (data != null)
                    {
                        var cf = pro.GetCustomAttributes(typeof(CloudFiled), true);
                        if (cf != null)
                        {
                            if (cf.Length > 0)
                            {
                                var cfInfo = (CloudFiled)cf[0];

                                var cp = (CloudRelation)data;

                                var pv = type.GetProperty("xxx");
                                var v = new List<string>();
                                foreach (var o in cp.objects)
                                {
                                    v.Add(o.objectId);
                                }
                                pv.SetValue(rtn, v);
                            }
                        }
                    }
                }
            }


            return rtn;
        }
    }
}
