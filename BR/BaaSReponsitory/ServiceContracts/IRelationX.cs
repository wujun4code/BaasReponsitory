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




#endif
    }

    public interface IRelation
    {
#if FRAMEWORK
        IEnumerable<T> LoadRelatedObject<S, T>(S source, string ColumnName)
            where T : class;

        IEnumerable<T> LoadRelatedObject<S, T>(S source)
           where T : class;

        S AddOne2ManyRelation<S, T>(S source, string PropertyName, T T_entity);

        S AddOne2ManyRelation<S, T>(S source, T T_entity);

        T LoadPointObject<S, T>(S source)
            where T : class
            where S : class;
#endif
    }
}
