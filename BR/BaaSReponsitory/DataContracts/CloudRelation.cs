using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public class CloudRelationAVOSImpl
    {
        public string __op { get; set; }

        public List<CloudPointer> objects { get; set; }

        public string __type { get; set; }

        public string className { get; set; }
    }

    public class CloudRelationX<T> : IRelationX
        where T : class
    {
        public CloudRelationX()
        {

        }

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
#if FRAMEWORK
        #region interface method impls

        public string AddReltionOneToMany<S>(S source, string ColumnName, IEnumerable targets)
        {
            return AddRelation<S>(source, ColumnName, targets);
        }
        public string AddRelationManyToOne<S>(S source, string ColumnName, object target)
        {
            return DoPointer(source, ColumnName, target);
        }

        #endregion

        public List<T> RelatedObjects { get; set; }

        public T PointerObject { get; set; }

        public List<T> LoadRelatedObject<S>(S source)
        {

            var s_type = typeof(S);
            var s_column_key = "";
            var s_id = "";
            var properties = s_type.GetProperties();
            foreach (var pro in properties)
            {
                var pt = pro.PropertyType;
                if (pt == typeof(CloudRelationX<T>))
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
        public string Push<S>(S source, string ColumnName)
        {
            return DoRelation("AddRelation", source, ColumnName, this.RelatedObjects);
        }
        public string AddRelation<S>(S source, string ColumnName, IEnumerable connects)
        {
            return DoRelation("AddRelation", source, ColumnName, connects);
        }
        public string RemoveRelation<S>(S source, string ColumnName, IEnumerable disconnects)
        {
            return DoRelation("RemoveRelation", source, ColumnName, disconnects);
        }

        public string DoPointer<S>(S source, string ColumnName, object target)
        {
            var t_type = typeof(T);
            PropertyInfo target_pro = CloudObject.GetPrimaryKeyProperty<T>();
            var t_name = t_type.Name;
            var root = new CloudPointerRootWrapper();
            var cp = new CloudPointer();
            cp.__type = "Pointer";
            cp.className = t_name;

            var ro_id = target_pro.GetValue(target);
            if (ro_id != null)
            {
                cp.objectId = (string)ro_id;
            }
            else
            {
                var new_ro = this.BaaSService.Add<string, T>((T)target);
                cp.objectId = (string)target_pro.GetValue(new_ro);
            }
            root.ColumnNameX = cp;
            string rtn = JsonConvert.SerializeObject(root);
            rtn = rtn.Replace("ColumnNameX", ColumnName);
            return rtn;
        }

        public string DoRelation<S>(string OpString, S source, string ColumnName, IEnumerable tergets)
        {
            var t_type = typeof(T);
            var s_type = typeof(S);
            var s_column_key = ColumnName;
            var s_id = "";
            var s_properties = s_type.GetProperties();
            foreach (var pro in s_properties)
            {
                var pt = pro.PropertyType;

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
                var ro_id = target_pro.GetValue(ro);
                if (ro_id != null)
                {
                    cp.objectId = (string)ro_id;
                }
                else
                {
                    var new_ro = this.BaaSService.Add<string, T>((T)ro);
                    cp.objectId = (string)target_pro.GetValue(new_ro);
                }
                corw.ColumnNameX.objects.Add(cp);
            }
            //var dm = CloudObject.GetDataMember<CloudOpponentsRootWrapper>("opponents");

            //dm.Name = s_column_key;
            string corwString = JsonConvert.SerializeObject(corw);
            corwString = corwString.Replace("ColumnNameX", s_column_key);
            //this.BaaSService.Update<string, S>(source, corwString);

            return corwString;
        }

        public void LoadRelatedObject<S>(S source, Action<IQueryable<T>> OnSuccess)
        {

        }
#endif

    }
}
