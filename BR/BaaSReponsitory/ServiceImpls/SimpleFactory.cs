using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public class SimpleFactory : IInterfaceFactory
    {
        public T CreateInterface<T>(string typeName, string assemblyName)
        {
            try
            {
                var t = typeof(T);

                var gtas = t.GenericTypeArguments;

                Assembly asse = Assembly.Load(assemblyName);

                var types = asse.GetTypes().AsEnumerable();

                Type targetType = null;

                foreach (var ty in types)
                {
                    if (ty.Name.Contains(typeName))
                    {
                        targetType = ty;
                        break;
                    }
                }

                Type constructed = targetType.MakeGenericType(gtas[1]);

                var rtn = Activator.CreateInstance(constructed);

                return (T)rtn;
            }
            catch
            {
                throw;
            }

        }

        public IBaaS<TKey, TEntity> CreateIBaaS<TKey, TEntity>(string typeName, string assemblyName) where TEntity : class
        {
            try
            {
                IBaaS<TKey, TEntity> rtn = null;

                var cloudObjType = typeof(TEntity);

                Assembly asse = Assembly.Load(assemblyName);

                var types = asse.GetTypes().AsEnumerable();

                Type targetType = null;

                foreach (var ty in types)
                {
                    if (ty.Name.Contains(typeName))
                    {
                        targetType = ty;
                        break;
                    }
                }

                Type constructed = targetType.MakeGenericType(cloudObjType);

                var result = Activator.CreateInstance(constructed);

                rtn = (IBaaS<TKey, TEntity>)result;

                return rtn;
            }
            catch
            {
                throw;
            }
        }
    }
}
