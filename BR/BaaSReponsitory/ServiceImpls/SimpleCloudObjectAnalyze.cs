using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
