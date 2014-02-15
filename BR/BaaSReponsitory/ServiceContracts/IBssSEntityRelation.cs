using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public interface IBaaSEntityRelation
    {
#if FRAMEWORK
        IEnumerable<T> GetRelatedEntities<S, T>(S source) where T : class;
        IEnumerable<T> GetRelatedEntities<S, T>(S source, string ColumnName) where T : class;
#endif
    }
}
