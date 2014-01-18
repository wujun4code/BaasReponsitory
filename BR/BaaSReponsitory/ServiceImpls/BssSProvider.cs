using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public abstract class BssSProvider : IBaaSProvider
    {

        public virtual void InjectFactory(IInterfaceFactory iInterfaceFactory)
        {
            this._interfaceFactory = iInterfaceFactory;
        }
        private IInterfaceFactory _interfaceFactory;

        public IInterfaceFactory InterfaceFactory
        {
            get
            {
                if (_interfaceFactory == null)
                {
                    _interfaceFactory = new SimpleFactory();
                }
                return _interfaceFactory;
            }
            set
            {
                _interfaceFactory = value;
            }
             
        }
        public IBaaS<TKey, TEntity> Create<TKey, TEntity>() where TEntity : class
        {

            var type = typeof(TEntity);

            var className = type.Name;

            var config = BRDic.RuntimeConfig;

            IBaaSCloudClassConfig targetModel = null;

            IBaaSHostConfig targetHost = null;
            

            foreach (var model in config.ClassConfigs.AsEnumerable())
            {
                if (model.ClassName == className)
                {
                    targetModel = model;
                    break;
                }
            }

            foreach (var host in config.HostConfigs.AsEnumerable())
            {
                if (host.key == targetModel.HostKey)
                {
                    targetHost = host;
                    break;
                }
            }

            IBaaS<TKey, TEntity> rtn = this.InterfaceFactory.CreateIBaaS < TKey,TEntity>(targetHost.name, targetHost.assemblyName);

            return rtn; 
        }
    }
}
