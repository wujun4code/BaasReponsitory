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
        S AddOne2ManyRelation<S, T>(S source, string PropertyName, T T_entity);
        S AddOne2ManyRelation<S, T>(S source,T T_entity);
#endif
    }
}
