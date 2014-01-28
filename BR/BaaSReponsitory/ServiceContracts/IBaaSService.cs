using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public interface IBaaSService
    {

        void InjectServiceProvider(IBaaSProvider reponsitoryService);
        IBaaSProvider ReponsitoryService { get; set; }
#if FRAMEWORK

        TEntity Add<TKey, TEntity>(TEntity entity) where TEntity : class;

        TEntity Get<TKey, TEntity>(TKey Id) where TEntity : class;

        IQueryable<TEntity> GetAll<TKey, TEntity>() where TEntity : class;

        TEntity Update<TKey, TEntity>(TEntity entity) where TEntity : class;

        TEntity Update<TKey, TEntity>(TEntity entity,object updateData) where TEntity : class;

        TEntity Update<TKey, TEntity>(TEntity entity, string updateString) where TEntity : class;

        bool Delete<TKey, TEntity>(TEntity entity) where TEntity : class;

        IQueryable<TEntity> GetByFilter<TKey, TEntity>(object filterData) where TEntity : class;

#endif
        void Add<TKey, TEntity>(TEntity entity, Action<TEntity> callback) where TEntity : class;

        void Get<TKey, TEntity>(TKey Id, Action<TEntity> callback) where TEntity : class;

        void GetAll<TKey, TEntity>(Action<IQueryable<TEntity>> callback) where TEntity : class;

        void Update<TKey, TEntity>(TEntity entity, Action<TEntity> callback) where TEntity : class;

        void Delete<TKey, TEntity>(TEntity entity, Action<bool> callback) where TEntity : class;
    }
}
