using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public class SimpleCloudObjectAnalyze : ICloudObjectAnalyze
    {
        public string GetOne2ManyPropertyName<S, T>()
        {
            string rtn = "";
            var s_type = typeof(S);
            var t_type = typeof(T);
            var properties = s_type.GetProperties();
            foreach (var pro in properties)
            {
                var pt = pro.PropertyType;
                var cloud_fields = pro.GetCustomAttributes(typeof(CloudFiled), true);
                if (cloud_fields.Length > 0)
                {
                    var cloud_field = (CloudFiled)cloud_fields[0];

                    if (cloud_field.IsRelation)
                    {
                        if (cloud_field.RelationType == CloudFiledType.OneToMany)
                        {
                            if (pt.GenericTypeArguments[0] == t_type)
                            {
                                rtn = pro.Name;
                                break;
                            }
                        }
                    }
                }

            }
            return rtn;
        }

        public string GetOne2OnePropertyName<S, T>()
        {
            string rtn = "";
            var s_type = typeof(S);
            var t_type = typeof(T);
            var properties = s_type.GetProperties();
            foreach (var pro in properties)
            {
                var pt = pro.PropertyType;
                var cloud_fields = pro.GetCustomAttributes(typeof(CloudFiled), true);
                if (cloud_fields.Length > 0)
                {
                    var cloud_field = (CloudFiled)cloud_fields[0];

                    if (cloud_field.IsRelation)
                    {
                        if (cloud_field.RelationType == CloudFiledType.OneToOne)
                        {
                            if (pt == t_type)
                            {
                                rtn = pro.Name;
                                break;
                            }
                        }
                    }
                }

            }
            return rtn;
        }

        public CloudFiled GetRealtionInfo<S, T>(out string PropertyName)
        {
            CloudFiled rtn = null;
            PropertyName = string.Empty;
            var s_type = typeof(S);
            var t_type = typeof(T);
            var properties = s_type.GetProperties();
            foreach (var pro in properties)
            {
                var pt = pro.PropertyType;
                var cloud_fields = pro.GetCustomAttributes(typeof(CloudFiled), true);
                if (cloud_fields.Length > 0)
                {
                    var cloud_field = (CloudFiled)cloud_fields[0];

                    if (cloud_field.IsRelation)
                    {
                        if (cloud_field.RelationType == CloudFiledType.OneToOne || cloud_field.RelationType == CloudFiledType.ManyToOne)
                        {
                            if (pt == t_type)
                            {
                                rtn = cloud_field;
                                PropertyName = pro.Name;
                                break;
                            }
                            else if (pt.GenericTypeArguments != null)
                            {
                                if (pt.GenericTypeArguments.Length > 0)
                                {
                                    if (pt.GenericTypeArguments[0] == t_type)
                                    {
                                        rtn = cloud_field;
                                        PropertyName = pro.Name;
                                        break;
                                    }
                                }
                            }


                        }
                    }
                }

            }
            return rtn;
        }

        public CloudFiled GetRealtionInfo<S, T>(string PropertyName)
        {
            CloudFiled rtn = null;
            var s_type = typeof(S);
            var t_type = typeof(T);
            var propertity = s_type.GetProperty(PropertyName);
            var pt = propertity.PropertyType;
            var cloud_fields = propertity.GetCustomAttributes(typeof(CloudFiled), true);
            if (cloud_fields.Length > 0)
            {
                var cloud_field = (CloudFiled)cloud_fields[0];

                if (cloud_field.IsRelation)
                {
                    rtn = cloud_field;
                }
            }

            return rtn;
        }

        public IEnumerable<PropertyInfo> GetAllCloudFiledProperties(Type S)
        {
            var rtn = new List<PropertyInfo>();

            var s_type = S;
            var properties = s_type.GetProperties();
            foreach (var pro in properties)
            {
                var pt = pro.PropertyType;
                var cloud_fields = pro.GetCustomAttributes(typeof(CloudFiled), true);
                if (cloud_fields.Length > 0)
                {
                    var cloud_field = (CloudFiled)cloud_fields[0];
                    if (cloud_field != null)
                    {
                        rtn.Add(pro);
                    }
                }

            }
            return rtn;
        }

        public PropertyInfo GetPrimaryTypeText(Type S)
        {
            PropertyInfo rtn = null;
            var s_type = S;
            var properties = s_type.GetProperties();

            foreach (var pro in properties)
            {
                var pt = pro.PropertyType;
                var cloud_fields = pro.GetCustomAttributes(typeof(CloudFiled), true);
                if (cloud_fields.Length > 0)
                {
                    var cloud_field = (CloudFiled)cloud_fields[0];
                    if (cloud_field != null)
                    {
                        if (cloud_field.IsPrimaryKey)
                        {
                            rtn = pro;
                            break;
                        }
                    }
                }

            }
            return rtn;
        }
    }
}
