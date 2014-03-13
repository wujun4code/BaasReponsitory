using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public interface IRelationX
    {

#if FRAMEWORK
        string AddReltionOneToMany<S>(S source, string ColumnName, IEnumerable targets);

        string AddRelationManyToOne<S>(S source, string ColumnName, object target);

        string RemoveRelationOneToMany<S>(S source, string ColumnName, IEnumerable targets);

#endif
    }

    public interface IRelation
    {
#if FRAMEWORK
        IEnumerable<T> LoadRelatedObject<S, T>(S source, string ColumnName)
            where S : class
            where T : class;

        IEnumerable<T> LoadRelatedObject<S, T>(S source)
            where S : class
            where T : class;

        S AddOne2ManyRelation<S, T>(S source, string PropertyName, T T_entity)
            where S : class
            where T : class;

        S AddOne2ManyRelation<S, T>(S source, T T_entity)
            where S : class
            where T : class;

        S AddMany2OneRelation<S, T>(S source, string PropertyName, T T_entity)
            where S : class
            where T : class;

        S RemoveOne2ManyRelation<S, T>(S source, string PropertyName, T T_entity)
            where S : class
            where T : class;

        T LoadPointObject<S, T>(S source)
            where T : class
            where S : class;
#endif
    }
}
