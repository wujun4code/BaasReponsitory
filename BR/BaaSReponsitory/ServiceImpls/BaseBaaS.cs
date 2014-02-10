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

        public virtual void RealtionHandlerAfterPost(TEntity entity)
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
                                var targetValue = pro.GetValue(entity);
                                if (cf_info.IsRelation && targetValue != null)
                                {

                                    Type t_type = null;
                                    object target = null;
                                    if (cf_info.RelationType == CloudFiledType.OneToMany)
                                    {
                                        t_type = pt.GetGenericArguments()[0];
                                        target = (IEnumerable)pro.GetValue(entity);
                                       
                                    }
                                    else if (cf_info.RelationType == CloudFiledType.ManyToOne || cf_info.RelationType == CloudFiledType.OneToOne)
                                    {
                                        t_type = pt;
                                        target = pro.GetValue(entity);
                                    }

                                    if (target == null)
                                        return;

                                    string columnName = "";
                                    if (string.IsNullOrEmpty(cf_info.ColumnName))
                                    {
                                        columnName = pro.Name;
                                    }
                                    else
                                    {
                                        columnName = cf_info.ColumnName;
                                    }

                                    Type[] CloudRelationXParams = new Type[] { t_type };
                                    Type targetType = typeof(CloudRelationX<>);
                                    Type constructed = targetType.MakeGenericType(CloudRelationXParams);
                                    var cloudRelation = (IRelationX)Activator.CreateInstance(constructed);


                                    string updateString = "";


                                    if (cf_info.RelationType == CloudFiledType.OneToMany)
                                    {
                                        if (target != null)
                                        updateString = cloudRelation.AddReltionOneToMany<TEntity>(entity, columnName, (IEnumerable)target);
                                    }

                                    if (cf_info.RelationType == CloudFiledType.ManyToOne)
                                    {
                                        if (target != null)
                                        updateString = cloudRelation.AddRelationManyToOne<TEntity>(entity, columnName, target);
                                    }

                                    this.Update(entity, updateString);
                                }

                            }
                        }
                    }
                }
            }
        }

        public virtual void RealtionHandlerAfterGet()
        {
 
        }

        public virtual TEntity Add(TEntity entity)
        {
            var rtn = RestService.Post(entity);
            RealtionHandlerAfterPost(rtn);
            return rtn;
        }

        public virtual TEntity Get(TKey Id)
        {
            var rtn = RestService.Get(Id);

            return rtn;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return RestService.GetAll();
        }

        public virtual TEntity Update(TEntity entity)
        {
            var key = RestService.GetEntityId<TKey>(entity);
            RealtionHandlerAfterPost(entity);
            return RestService.Put(key, entity);
        }

        public virtual TEntity Update(TEntity entity, object updateData)
        {
            var key = RestService.GetEntityId<TKey>(entity);

            return RestService.Put(key, entity, updateData);
        }

        public virtual TEntity Update(TEntity entity, string updateString)
        {
            var key = RestService.GetEntityId<TKey>(entity);

            return RestService.Put(key, entity, updateString);
        }

        public virtual bool Delete(TEntity entity)
        {
            var key = RestService.GetEntityId<TKey>(entity);

            return RestService.Delete(key);
        }

        public virtual IQueryable<TEntity> GetByFilter(object filterData)
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

        public virtual void GetAll(Action<IQueryable<TEntity>> callback)
        {
            RestService.GetAll(callback);
        }

        public virtual void Update(TEntity entity, Action<TEntity> callback)
        {
            var key = RestService.GetEntityId<TKey>(entity);
            RestService.Put(key, entity, callback);
        }

        public virtual void Delete(TEntity entity, Action<bool> callback)
        {
            var key = RestService.GetEntityId<TKey>(entity);

            RestService.Delete(key, callback);
        }
    }
}
