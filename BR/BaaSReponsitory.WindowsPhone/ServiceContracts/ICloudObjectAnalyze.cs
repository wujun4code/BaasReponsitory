using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public interface ICloudObjectAnalyze
    {
        string GetOne2ManyPropertyName<S, T>(S source);
    }
}
