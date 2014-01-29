using System;
using System.Collections;
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

        public virtual void AnalyzeTEntity(TEntity entity)
        {
            var s_type = typeof(TEntity);

            var properties = s_type.GetProperties();

            foreach (var pro in properties)
            {
                var pt = pro.PropertyType;
                var cf = pro.GetCustomAttributes(typeof(CloudFiled), true);
                {
                    if (cf != null)
                    {
                        if (cf.Length > 0)
                        {
                            var cf_info = (CloudFiled)cf[0];
                            if (!cf_info.IsPrimaryKey)
                            {
                                if (cf_info.RelationType == CloudFiledType.OneToMany)
                                {
                                    var t_type = pt.GetGenericArguments()[0];
                                    Type[] CloudRelationXParams = new Type[] { t_type };
                                    Type targetType = typeof(CloudRelationX<>);

                                    Type constructed = targetType.MakeGenericType(CloudRelationXParams);

                                    var cloudRelation = (IRelationX)Activator.CreateInstance(constructed);

                                    var targets = (IEnumerable)pro.GetValue(entity);

                                    if (targets != null)
                                    {
                                        string updateString = cloudRelation.AddReltion<TEntity>(entity, cf_info.ColumnName, targets);

                                        this.Update(entity, updateString);
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }

        public virtual TEntity Add(TEntity entity)
        {
            var rtn = RestService.Post(entity);
            AnalyzeTEntity(rtn);
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

        public TEntity Update(TEntity entity,object updateData)
        {
            var key = RestService.GetEntityId<TKey>(entity);

            return RestService.Put(key,entity, updateData);
        }

        public TEntity Update(TEntity entity, string updateString)
        {
            var key = RestService.GetEntityId<TKey>(entity);

            return RestService.Put(key,entity, updateString);
        }

        public bool Delete(TEntity entity)
        {
            var key = RestService.GetEntityId<TKey>(entity);

            return RestService.Delete(key);
        }

        public IQueryable<TEntity> GetByFilter(object filterData)
        {
            return RestService.GetByFilter(filterData);
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
