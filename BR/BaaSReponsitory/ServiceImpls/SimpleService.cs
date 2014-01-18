using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public class SimpleService : IBaaSService
    {

        public void InjectServiceProvider(IBaaSProvider reponsitoryService)
        {
            this._reponsitoryService = reponsitoryService;
        }
        private IBaaSProvider _reponsitoryService;
        public IBaaSProvider ReponsitoryService
        {
            get
            {
                if (_reponsitoryService == null)
                {
                    _reponsitoryService = new BaseBssSProvider();
                }
                return _reponsitoryService;
            }
            set
            {
                _reponsitoryService = value;
            }
        }
#if FRAMEWORK

        public TEntity Add<TKey, TEntity>(TEntity entity) where TEntity : class
        {
            return this.ReponsitoryService.Create<TKey, TEntity>().Add(entity);
        }

        public TEntity Get<TKey, TEntity>(TKey Id) where TEntity : class
        {
            return this.ReponsitoryService.Create<TKey, TEntity>().Get(Id);
        }

        public IQueryable<TEntity> GetAll<TKey, TEntity>() where TEntity : class
        {
            return this.ReponsitoryService.Create<TKey, TEntity>().GetAll();
        }

        public TEntity Update<TKey, TEntity>(TEntity entity) where TEntity : class
        {
            return this.ReponsitoryService.Create<TKey, TEntity>().Update(entity);
        }

        public bool Delete<TKey, TEntity>(TEntity entity) where TEntity : class
        {
            return this.ReponsitoryService.Create<TKey, TEntity>().Delete(entity);
        }
#endif
        public void Add<TKey, TEntity>(TEntity entity, Action<TEntity> callback) where TEntity : class
        {
            this.ReponsitoryService.Create<TKey, TEntity>().Add(entity, callback);
        }

        public void Get<TKey, TEntity>(TKey Id, Action<TEntity> callback) where TEntity : class
        {
            this.ReponsitoryService.Create<TKey, TEntity>().Get(Id, callback);
        }

        public void GetAll<TKey, TEntity>(Action<IQueryable<TEntity>> callback) where TEntity : class
        {
            this.ReponsitoryService.Create<TKey, TEntity>().GetAll(callback);
        }

        public void Update<TKey, TEntity>(TEntity entity, Action<TEntity> callback) where TEntity : class
        {
            this.ReponsitoryService.Create<TKey, TEntity>().Update(entity, callback);
        }

        public void Delete<TKey, TEntity>(TEntity entity, Action<bool> callback) where TEntity : class
        {
            this.ReponsitoryService.Create<TKey, TEntity>().Delete(entity, callback);
        }

    }
}
