using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public abstract class BaseService<TKey, TEntity> : IBaaS<TKey, TEntity>
         where TEntity : class
    {
        private IBaaSService _defaultBaaSService;
        public IBaaSService DefaultBaaSService
        {
            get
            {
                if (_defaultBaaSService == null)
                {
                    _defaultBaaSService = new SimpleService();

                }
                return _defaultBaaSService;
            }
            set
            {
                _defaultBaaSService = value;
            }
        }

#if FRAMEWORK

        public TEntity Add(TEntity entity)
        {
            return DefaultBaaSService.Add<TKey, TEntity>(entity);
        }

        public TEntity Get(TKey Id)
        {
            return DefaultBaaSService.Get<TKey, TEntity>(Id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return DefaultBaaSService.GetAll<TKey, TEntity>();
        }

        public TEntity Update(TEntity entity)
        {
            return DefaultBaaSService.Update<TKey, TEntity>(entity);
        }

        public TEntity Update(TEntity entity, object updateData)
        {
            return DefaultBaaSService.Update<TKey, TEntity>(entity, updateData);
        }

        public TEntity Update(TEntity entity, string updateString)
        {
            return DefaultBaaSService.Update<TKey, TEntity>(entity, updateString);
        }

        public bool Delete(TEntity entity)
        {
            return DefaultBaaSService.Delete<TKey, TEntity>(entity);
        }

        public IQueryable<TEntity> GetByFilter(object filterData)
        {
            return DefaultBaaSService.GetByFilter<TKey, TEntity>(filterData);
        }

#endif
        public void Add(TEntity entity, Action<TEntity> callback)
        {
            DefaultBaaSService.Add<TKey, TEntity>(entity, callback);
        }

        public void Get(TKey Id, Action<TEntity> callback)
        {
            DefaultBaaSService.Get<TKey, TEntity>(Id, callback);
        }

        public void GetAll(Action<IQueryable<TEntity>> callback)
        {
            DefaultBaaSService.GetAll<TKey, TEntity>(callback);
        }

        public void Update(TEntity entity, Action<TEntity> callback)
        {
            DefaultBaaSService.Update<TKey, TEntity>(entity, callback);
        }

        public void Delete(TEntity entity, Action<bool> callback)
        {
            DefaultBaaSService.Delete<TKey, TEntity>(entity, callback);
        }
    }
}
