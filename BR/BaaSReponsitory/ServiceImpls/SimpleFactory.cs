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

                var rtn = Activator.CreateInstance(targetType);

                return (T)rtn;
            }
            catch
            {
                throw;
            }

        }

        public IBaaSAuthenticate CreateIBaaSAuthenticate<TUser>(string typeName, string assemblyName) where TUser : CloudUser
        {
            try
            {
                IBaaSAuthenticate rtn = null;

                var cloudObjType = typeof(TUser);

                Assembly asse = Assembly.Load(assemblyName);

                var types = asse.GetTypes().AsEnumerable();

                var singleType = asse.GetType(typeName);

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

                rtn = (IBaaSAuthenticate)result;

                return rtn;
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

                var singleType = asse.GetType(typeName);

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
