using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace BaaSReponsitory
{
    public class CloudObject : Attribute
    {
        public string ClassName { get; set; }

        public string ReponsitoryName { get; set; }

        public string ReponsitoryAssemblyName { get; set; }

        public CloudObjectType ModeType { get; set; }

        public string BaaSServiceProvider { get; set; }

        public string BaaSServiceProviderAssemblyName { get; set; }

        public static PropertyInfo GetPrimaryKeyProperty<T>()
        {
            PropertyInfo target_pro = null;
            var t_type = typeof(T);
            var t_properties = t_type.GetProperties();

            foreach (var pro in t_properties)
            {
                var pt = pro.PropertyType;

                var cloud_fields = pro.GetCustomAttributes(typeof(CloudFiled), true);

                if (cloud_fields.Length > 0)
                {
                    var cloud_field = cloud_fields[0];

                    if (((CloudFiled)cloud_field).IsPrimaryKey)
                    {
                        target_pro = pro;
                        break;
                    }
                }

            }
            return target_pro;
        }

        public static DataMemberAttribute GetDataMember<T>(string proName)
        {
            DataMemberAttribute target_pro = null;
            var t_type = typeof(T);
            var pro = t_type.GetProperty(proName);


            var pt = pro.PropertyType;

            var dataMembers = pro.GetCustomAttributes(typeof(DataMemberAttribute), true);

            if (dataMembers.Length > 0)
            {
                var dm_field = dataMembers[0];
                target_pro = (DataMemberAttribute)dm_field;

            }
            return target_pro;
        }
    }

    public enum CloudObjectType : int
    {
        Normal = 0,
        User = 1,
        Role = 2
    }
}
