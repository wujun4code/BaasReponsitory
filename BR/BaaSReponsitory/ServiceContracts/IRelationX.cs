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

        string AddReltion<S>(S source,string ColumnName, IEnumerable targets);
    }
}
