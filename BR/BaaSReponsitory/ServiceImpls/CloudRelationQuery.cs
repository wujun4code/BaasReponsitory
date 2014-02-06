using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public static class CloudRelationQuery
    {
        public static void GetObjectsInRelation<TTarget, TSource>(this CloudRelationAVOSImpl cloudRelation)
        {
            var Source_className = typeof(TSource).Name;

        }
    }
}
