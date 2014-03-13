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
        IEnumerable<T> GetRelatedEntities<S, T>(S source)
            where S : class
            where T : class;

        IEnumerable<T> GetRelatedEntities<S, T>(S source, string ColumnName)
            where S : class
            where T : class;

        S AddOne2ManyRelation<S, T>(S source, string PropertyName, T T_entity)
            where S : class
            where T : class;

        S AddOne2ManyRelation<S, T>(S source, T T_entity)
            where S : class
            where T : class;

        S AddMany2ManyRelation<S, T>(S source, string S_PropertyName, T T_entity, string T_PropertyName)
            where S : class
            where T : class;

        S AddOneToOneRelation<S, T>(S source, string S_PropertyName, T T_entity, string T_PropertyName)
            where S : class
            where T : class;

        //S AddOneToOneRelation<S, T>(S source, T T_entity)
        //    where S : class
        //    where T : class;

        S RemoveOne2ManyRelation<S, T>(S soure, string ColumnName, T T_entity)
            where S : class
            where T : class;

        //S RemoveOne2OneRelation<S, T>(S source, string S_PropertyName, T T_entity, string T_PropertyName)
        //    where S : class
        //    where T : class;
#endif
    }
}
