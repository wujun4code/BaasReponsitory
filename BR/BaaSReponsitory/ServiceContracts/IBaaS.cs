using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    /// <summary>
    /// general functions for BaaS service.
    /// </summary>
    /// <typeparam name="TKey">the type of  TEntity's primary key</typeparam>
    /// <typeparam name="TEntity">the target type to store in BaaS servier reponsitory.</typeparam>
    public interface IBaaS<TKey, TEntity>
        where TEntity : class
    {

#if FRAMEWORK

        TEntity Add(TEntity entity);

        TEntity Get(TKey Id);

        IQueryable<TEntity> GetAll();

        TEntity Update(TEntity entity);

        TEntity Update(TEntity entity, object updateData);

        TEntity Update(TEntity entity, string updateString);

        bool Delete(TEntity entity);

        IQueryable<TEntity> GetByFilter(object filterData);

#endif
        void Add(TEntity entity, Action<TEntity> callback);

        void Get(TKey Id, Action<TEntity> callback);

        void GetAll(Action<IQueryable<TEntity>> callback);

        void Update(TEntity entity, Action<TEntity> callback);

        void Delete(TEntity entity, Action<bool> callback);
    }
}
