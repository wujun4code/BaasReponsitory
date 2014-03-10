using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public interface ICloudObjectAnalyze
    {
        CloudFiled GetRealtionInfo<S, T>(out string PropertyName);
        CloudFiled GetRealtionInfo<S, T>(string PropertyName);
        IEnumerable<PropertyInfo> GetAllCloudFiledProperties<S>();

        PropertyInfo GetPrimaryTypeText<S>();
    }
}
