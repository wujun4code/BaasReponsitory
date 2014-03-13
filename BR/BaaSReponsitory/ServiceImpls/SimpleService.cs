using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public class SimpleService : IBaaSService, IBaaSAuthenticate, IBaaSEntityRelation
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

        private IRelation _relationService;
        public IRelation RelationService
        {
            get
            {
                if (_relationService == null)
                {
                    _relationService = new CloudRelation();
                }
                return _relationService;
            }
            set
            {
                _relationService = value;
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

        public TEntity Update<TKey, TEntity>(TEntity entity, object updateData) where TEntity : class
        {
            return this.ReponsitoryService.Create<TKey, TEntity>().Update(entity, updateData);
        }

        public TEntity Update<TKey, TEntity>(TEntity entity, string updateString) where TEntity : class
        {
            return this.ReponsitoryService.Create<TKey, TEntity>().Update(entity, updateString);
        }

        public bool Delete<TKey, TEntity>(TEntity entity) where TEntity : class
        {
            return this.ReponsitoryService.Create<TKey, TEntity>().Delete(entity);
        }

        public IQueryable<TEntity> GetByFilter<TKey, TEntity>(object filterData) where TEntity : class
        {
            return this.ReponsitoryService.Create<TKey, TEntity>().GetByFilter(filterData);
        }

        public IQueryable<TEntity> GetByFilter<TKey, TEntity>(string filterString) where TEntity : class
        {
            return this.ReponsitoryService.Create<TKey, TEntity>().GetByFilter(filterString);
        }

        public TUser Register<TUser>(TUser newUser) where TUser : CloudUser
        {
            return this.ReponsitoryService.CreateAuthenticateService<TUser>().Register<TUser>(newUser);
        }

        public TUser Login<TUser>(TUser user) where TUser : CloudUser
        {
            return this.ReponsitoryService.CreateAuthenticateService<TUser>().Login<TUser>(user);
        }
        public IEnumerable<T> GetRelatedEntities<S, T>(S source)
            where S : class
            where T : class
        {
            return this.RelationService.LoadRelatedObject<S, T>(source);
        }

        public IEnumerable<T> GetRelatedEntities<S, T>(S source, string ColumnName)
            where S : class
            where T : class
        {
            return this.RelationService.LoadRelatedObject<S, T>(source, ColumnName);
        }

        public S AddOne2ManyRelation<S, T>(S source, T T_entity)
            where S : class
            where T : class
        {
            return this.RelationService.AddOne2ManyRelation<S, T>(source, T_entity);
        }

        public S AddOne2ManyRelation<S, T>(S source, string PropertyName, T T_entity)
            where S : class
            where T : class
        {
            return this.RelationService.AddOne2ManyRelation(source, PropertyName, T_entity);
        }

        public S AddMany2ManyRelation<S, T>(S source, string S_PropertyName, T T_entity, string T_PropertyName)
            where S : class
            where T : class
        {
            this.RelationService.AddOne2ManyRelation(T_entity, T_PropertyName, source);
            return this.RelationService.AddOne2ManyRelation(source, S_PropertyName, T_entity);
        }

        public S AddOneToOneRelation<S, T>(S source, string S_PropertyName, T T_entity, string T_PropertyName)
            where S : class
            where T : class
        {
            this.RelationService.AddMany2OneRelation(T_entity, T_PropertyName, source);
            return this.RelationService.AddMany2OneRelation(source, S_PropertyName, T_entity);
        }

        public T GetRelatedEntity<S, T>(S source)
            where T : class
            where S : class
        {
            return this.RelationService.LoadPointObject<S, T>(source);
        }

        public S RemoveOne2ManyRelation<S, T>(S soure, string ColumnName, T T_entity)
            where S : class
            where T : class
        {
            return this.RelationService.RemoveOne2ManyRelation<S, T>(soure, ColumnName, T_entity);
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

        public void RegisterAsync<TUser>(TUser newUser, Action<TUser> callback) where TUser : CloudUser
        {
            this.ReponsitoryService.CreateAuthenticateService<TUser>().RegisterAsync<TUser>(newUser, callback);
        }

        public void LoginAsync<TUser>(TUser user, Action<TUser> callback) where TUser : CloudUser
        {
            this.ReponsitoryService.CreateAuthenticateService<TUser>().LoginAsync<TUser>(user, callback);
        }

    }
}
