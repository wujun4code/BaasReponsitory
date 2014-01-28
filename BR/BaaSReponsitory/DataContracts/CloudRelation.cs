using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public class CloudRelation
    {
        public string __op { get; set; }

        public List<CloudPointer> objects { get; set; }

        public string __type { get; set; }

        public string className { get; set; }
    }

    public class CloudRelationX<S, T>
        where S : class
        where T : class
    {
        public CloudRelationX()
        {

        }

        public List<T> RelatedObjects { get; set; }

        private IBaaSService _baaSService;
        public IBaaSService BaaSService
        {
            get
            {
                if (_baaSService == null)
                {
                    _baaSService = new SimpleService();
                }
                return _baaSService;
            }
            set
            {
                _baaSService = value;
            }
        }

        public List<T> LoadRelatedObject(S source)
        {

            var s_type = typeof(S);
            var s_column_key = "";
            var s_id = "";
            var properties = s_type.GetProperties();
            foreach (var pro in properties)
            {
                var pt = pro.PropertyType;
                if (pt == typeof(CloudRelationX<S, T>))
                {
                    s_column_key = pro.Name;
                }
                else
                {
                    var cloud_fields = pro.GetCustomAttributes(typeof(CloudFiled), true);

                    if (cloud_fields.Length > 0)
                    {
                        var cloud_field = cloud_fields[0];

                        if (((CloudFiled)cloud_field).IsPrimaryKey)
                        {
                            s_id = (string)pro.GetValue(source);
                        }
                    }
                }
            }


            AVOSRelationFiter arf = new AVOSRelationFiter();
            arf.key = s_column_key;
            var s_name = typeof(S).Name;

            arf.relatedTo = new AVOSRelatedTo()
            {
                className = s_name,
                __type = "Pointer",
                objectId = s_id
            };
            AVOSRelatedToRootWrapper artro = new AVOSRelatedToRootWrapper();
            artro.relatedTo = arf;

            var rtnQuery = this.BaaSService.GetByFilter<string, T>(artro);
            RelatedObjects = rtnQuery.ToList();
            return rtnQuery.ToList();
        }

        public void Push(S source)
        {
            DoRelation("AddRelation", source, this.RelatedObjects);
        }

        public void RemoveRelation(S source, IList<T> disconnects)
        {
            DoRelation("RemoveRelation", source, disconnects);
        }

        public void DoRelation(string OpString, S source, IList<T> tergets)
        {
            var t_type = typeof(T);
            var s_type = typeof(S);
            var s_column_key = "";
            var s_id = "";
            var s_properties = s_type.GetProperties();
            foreach (var pro in s_properties)
            {
                var pt = pro.PropertyType;
                if (pt == typeof(CloudRelationX<S, T>))
                {
                    s_column_key = pro.Name;
                }
                else
                {
                    var cloud_fields = pro.GetCustomAttributes(typeof(CloudFiled), true);

                    if (cloud_fields.Length > 0)
                    {
                        var cloud_field = cloud_fields[0];

                        if (((CloudFiled)cloud_field).IsPrimaryKey)
                        {
                            s_id = (string)pro.GetValue(source);
                        }
                    }
                }
            }

            PropertyInfo target_pro = CloudObject.GetPrimaryKeyProperty<T>();
            CloudOpponentsRootWrapper corw = new CloudOpponentsRootWrapper();
            corw.ColumnNameX = new CloudOpponents();
            corw.ColumnNameX.__op = OpString;
            corw.ColumnNameX.objects = new List<AVOSRelatedTo>();

            var t_name = t_type.Name;
            foreach (var ro in tergets)
            {
                var cp = new AVOSRelatedTo();
                cp.__type = "Pointer";
                cp.className = t_name;
                cp.objectId = (string)target_pro.GetValue(ro);
                corw.ColumnNameX.objects.Add(cp);
            }
            //var dm = CloudObject.GetDataMember<CloudOpponentsRootWrapper>("opponents");

            //dm.Name = s_column_key;
            string corwString = JsonConvert.SerializeObject(corw);
            corwString = corwString.Replace("ColumnNameX", s_column_key);
            this.BaaSService.Update<string, S>(source, corwString);
        }

        public void LoadRelatedObject(S source, Action<IQueryable<T>> OnSuccess)
        {

        }

    }
}
