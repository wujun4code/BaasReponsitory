using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public interface IRestBaaS<TKey,TEntity>
    {


#if FRAMEWORK
        TEntity Post(TEntity entity);

        TEntity Get(TKey Id);

        IQueryable<TEntity> GetAll();

        TEntity Put(TKey Id,TEntity entity);

        bool Delete(TKey Id);
#endif

        void PostAsync(TEntity entity,Action<TEntity> callback);

        void Get(TKey Id, Action<TEntity> callback);

        void GetAll(Action<IQueryable<TEntity>> callback);

        void Put(TKey Id,TEntity entity, Action<TEntity> callback);

        void Delete(TKey Id, Action<bool> callback);
        //TEntity Put<TKey, TEntity>(TKey Id,TEntity entity);

        //IList<TEntity> GetAll<TEntity>();

        //bool Delete<TKey>(TKey Id);

    }

    public enum BaaSDataFormat
    {
        Json = 0,
        Xml = 1,
    }
}
