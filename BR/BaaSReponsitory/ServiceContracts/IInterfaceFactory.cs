using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public interface IInterfaceFactory
    {
        T CreateInterface<T>(string typeName, string assemblyName);

        IBaaS<TKey, TEntity> CreateIBaaS<TKey, TEntity>(string typeName, string assemblyName) where TEntity:class;
    }
}
