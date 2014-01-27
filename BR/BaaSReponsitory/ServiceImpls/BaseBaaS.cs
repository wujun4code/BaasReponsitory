using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public abstract class BaseBaaS<TKey, TRootWrapper, TEntity>
        : IBaaS<TKey, TEntity>
        where TEntity : class
        where TRootWrapper : JsonWrapper<TKey>
    {
        public BaseRestBaaS<TKey, TRootWrapper, TEntity> RestService { get; set; }
#if FRAMEWORK
        public virtual TEntity Add(TEntity entity)
        {
            var rtn = RestService.Post(entity);

            return rtn;
        }

        public virtual TEntity Get(TKey Id)
        {
            return RestService.Get(Id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return RestService.GetAll();
        }

        public TEntity Update(TEntity entity)
        {
            var key = RestService.GetEntityId<TKey>(entity);

            return RestService.Put(key, entity);
        }

        public bool Delete(TEntity entity)
        {
            var key = RestService.GetEntityId<TKey>(entity);

            return RestService.Delete(key);
        }

#endif

        public virtual void Add(TEntity entity, Action<TEntity> callback)
        {
            RestService.PostAsync(entity, callback);
        }
        public virtual void Get(TKey Id, Action<TEntity> callback)
        {
            RestService.Get(Id, callback);
        }

        public void GetAll(Action<IQueryable<TEntity>> callback)
        {
            RestService.GetAll(callback);
        }

        public void Update(TEntity entity, Action<TEntity> callback)
        {
            var key = RestService.GetEntityId<TKey>(entity);
            RestService.Put(key, entity, callback);
        }

        public void Delete(TEntity entity, Action<bool> callback)
        {
            var key = RestService.GetEntityId<TKey>(entity);

            RestService.Delete(key, callback);
        }
    }
}
