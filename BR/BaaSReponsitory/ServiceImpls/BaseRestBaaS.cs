using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public abstract class BaseRestBaaS<TKey, TRootWrapper, TEntity> : IRestBaaS<TKey, TEntity>
        where TRootWrapper : JsonWrapper<TKey>
    {

        #region abstract properties and methods for logic.

        public abstract string RestApiRootAddress { get; set; }

        public abstract string RestApiVersion { get; set; }

        public abstract Dictionary<string, string> CustomHeaders { get; set; }

        public abstract string ListJsonRootNodeName { get; set; }

        public abstract IRestClient Client { get; set; }

        private BaaSDataFormat _defaultDataFormat;
        public virtual BaaSDataFormat DefaultDataFormat
        {
            get
            {
                return _defaultDataFormat;
            }
            set
            {
                _defaultDataFormat = value;
            }
        }

        public abstract string CreateResource();

        #endregion

        #region interface methods implements

        public virtual TEntity Post(TEntity entity)
        {
            ProcessClientBeforeSend();

            var req = CreatePostReuqest(entity);

            IRestResponse rep = null;

#if FRAMEWORK
            rep = Client.Execute(req);
#endif

            entity = ProcessResponseAfterPost<TEntity>(entity, rep);

            return entity;

        }

        public virtual void PostAndPushRelation(TEntity entity, Action<TEntity> pushRelation)
        {
            var after_entity = Post(entity);

            pushRelation(after_entity);

        }

        public virtual TEntity Get(TKey Id)
        {
            ProcessClientBeforeSend();

            var req = CreateGetRequest(Id);

            IRestResponse rep = null;

#if FRAMEWORK
            rep = Client.Execute(req);
#endif
            var rtn = DeserializerFromResponse(rep);

            SetTEntityId(rtn, Id);

            return rtn;
        }
        public IQueryable<TEntity> GetAll()
        {
            ProcessClientBeforeSend();

            var req = CreateGetAllRequest();

            IRestResponse rep = null;

#if FRAMEWORK
            rep = Client.Execute(req);
#endif
            return GetQueryableByResponse(rep);

        }
        public TEntity Put(TKey Id, TEntity entity)
        {
            ProcessClientBeforeSend();

            var req = CreatePutRequest(Id, entity);

            IRestResponse rep = null;
#if FRAMEWORK
            rep = Client.Execute(req);
#endif
            return ProcessResponseAfterPut(entity, rep);
        }

        public TEntity Put(TKey Id, TEntity entity, object updateData)
        {
            string updateString = JsonConvert.SerializeObject(updateData);

            return this.Put(Id, entity, updateString);

        }
        public TEntity Put(TKey Id, TEntity entity, string updateString)
        {
            ProcessClientBeforeSend();

            var req = CreatePutRequestByUpdatString(Id, updateString);

            IRestResponse rep = null;
#if FRAMEWORK
            rep = Client.Execute(req);
#endif
            return ProcessResponseAfterPut(entity, rep);
        }

        public bool Delete(TKey Id)
        {
            ProcessClientBeforeSend();

            var req = CreateDeleteRequest(Id);

            IRestResponse rep = null;
#if FRAMEWORK
            rep = Client.Execute(req);
#endif

            return ProcessResponseAfterDelete(Id, rep);
        }

        public IQueryable<TEntity> GetByFilter(object filterData)
        {
            ProcessClientBeforeSend();
            var req = CreateGetByFilterRequest(filterData);
            IRestResponse rep = null;
#if FRAMEWORK
            rep = Client.Execute(req);
#endif

            return GetQueryableByResponse(rep);
        }



        public virtual void PostAsync(TEntity entity, Action<TEntity> callback)
        {
            ProcessClientBeforeSend();

            var req = CreatePostReuqest(entity);

            Client.ExecuteAsync(req, new Action<IRestResponse, RestRequestAsyncHandle>
                (
                (repp, rrayh) =>
                {
                    entity = ProcessResponseAfterPost<TEntity>(entity, repp);
                    callback(entity);
                })
                );
        }

        public virtual void Get(TKey Id, Action<TEntity> callback)
        {
            ProcessClientBeforeSend();

            var req = CreateGetRequest(Id);

            Client.ExecuteAsync(req, new Action<IRestResponse, RestRequestAsyncHandle>(
                (repp, rrayh) =>
                {
                    var rtn = DeserializerFromResponse(repp);

                    SetTEntityId(rtn, Id);

                    callback(rtn);
                })
                );
        }

        public void GetAll(Action<IQueryable<TEntity>> callback)
        {
            ProcessClientBeforeSend();

            var req = CreateGetAllRequest();

            Client.ExecuteAsync(req, new Action<IRestResponse, RestRequestAsyncHandle>(
               (repp, rrayh) =>
               {
                   var rtn = GetQueryableByResponse(repp);

                   callback(rtn);
               })
               );
        }

        public void Put(TKey Id, TEntity entity, Action<TEntity> callback)
        {
            ProcessClientBeforeSend();

            var req = CreatePutRequest(Id, entity);

            Client.ExecuteAsync(req, new Action<IRestResponse, RestRequestAsyncHandle>(
               (repp, rrayh) =>
               {
                   var rtn = ProcessResponseAfterPut(entity, repp);

                   callback(rtn);
               })
               );
        }

        public void Delete(TKey Id, Action<bool> callback)
        {
            ProcessClientBeforeSend();

            var req = CreateDeleteRequest(Id);

            Client.ExecuteAsync(req, new Action<IRestResponse, RestRequestAsyncHandle>(
               (repp, rrayh) =>
               {
                   var rtn = ProcessResponseAfterDelete(Id, repp);

                   callback(rtn);
               })
               );
        }
        #endregion

        #region Create request logic
        public virtual IRestRequest CreatePostReuqest(TEntity entity)
        {
            var req = new RestRequest();

            req.Resource = CreateResource();

            req.Method = Method.POST;

            SetRequest(req);

            var BodyDataString = SerializeEntityToPost(entity);

            req.AddParameter("application/json", BodyDataString, ParameterType.RequestBody);

            return req;
        }

        public virtual IRestRequest CreateGetRequest(TKey Id)
        {
            var req = new RestRequest();

            req.Method = Method.GET;

            req.Resource = CreateResource() + "/" + Id;

            SetRequest(req);


            return req;

        }

        public virtual IRestRequest CreateGetAllRequest()
        {
            var req = new RestRequest();

            req.Method = Method.GET;

            req.Resource = CreateResource();

            SetRequest(req);

            return req;
        }

        public virtual IRestRequest CreatePutRequest(TKey Id, TEntity entity)
        {
            var req = new RestRequest();

            req.Method = Method.PUT;

            req.Resource = CreateResource() + "/" + Id;

            SetRequest(req);


            var BodyDataString = SerializeEntityToPost(entity);

            req.AddParameter("application/json", BodyDataString, ParameterType.RequestBody);

            return req;
        }

        public virtual IRestRequest CreatePutRequestByUpdatString(TKey Id, string updateString)
        {
            var req = new RestRequest();

            req.Method = Method.PUT;

            req.Resource = CreateResource() + "/" + Id;

            SetRequest(req);

            var BodyDataString = updateString;

            req.AddParameter("application/json", BodyDataString, ParameterType.RequestBody);

            return req;
        }

        public virtual IRestRequest CreateDeleteRequest(TKey Id)
        {
            var req = new RestRequest();

            req.Method = Method.DELETE;

            req.Resource = CreateResource() + "/" + Id;

            SetRequest(req);

            return req;
        }

        public virtual IRestRequest CreateGetByFilterRequest(object filterData)
        {
            var req = new RestRequest();

            req.Resource = CreateResource();

            req.Method = Method.GET;

            //var filterString = req.JsonSerializer.Serialize(filterData);
            var filterString = JsonConvert.SerializeObject(filterData);

            req.AddHeader("Content-Type", "data-urlencode");

            req.AddParameter("where", filterString);

            //SetRequest(req);
            SetNetCredentials(req);

            return req;
        }


        public virtual void SetRequest(IRestRequest req)
        {
#if FRAMEWORK
            SetContentType(req, DefaultDataFormat);

            req.RequestFormat = (DataFormat)((int)DefaultDataFormat);

            SetNetCredentials(req);
#endif
        }

        #endregion

        #region process request before send request
        public virtual void ProcessClientBeforeSend()
        {

#if FRAMEWORK

            Client.Proxy = WebRequest.DefaultWebProxy;
            Client.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
#endif
            foreach (var chkey in CustomHeaders.Keys)
            {
                Client.AddDefaultHeader(chkey, CustomHeaders[chkey]);
            }

        }

        public virtual void SetNetCredentials(IRestRequest req)
        {

        }

        public virtual void SetContentType(IRestRequest req, BaaSDataFormat format)
        {
            switch (format)
            {
                case BaaSDataFormat.Json:
                    req.AddHeader("Content-Type", "application/json");
                    break;

                case BaaSDataFormat.Xml:
                    req.AddHeader("Content-Type", "application/xml");
                    break;
            }
        }

        public virtual string SerializeEntityToPost(TEntity entity)
        {
            var rtn = "";
            Newtonsoft.Json.JsonSerializerSettings _jsonSetting = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
            };
            rtn = JsonConvert.SerializeObject(entity, Formatting.Indented, _jsonSetting);
            return rtn;
        }

        #endregion

        #region process response after do request logic business

        public virtual void ErrorCatcher(IRestResponse rep)
        {
            var result = JObject.Parse(rep.Content);
            var wrapper = result.ToObject<TRootWrapper>();

            if (String.IsNullOrEmpty(wrapper.ErrorCode))
            {
#if FRAMEWORK
                var exception = new BRException(wrapper.ErrorMessage);
                exception.Data.Add("error", wrapper.ErrorCode);
#endif
            }
        }

        public virtual T SetTEntityId<T>(T entity, TKey objectId)
        {
            var type = typeof(T);

            var pro_infos = type.GetProperties();
            foreach (var pi in pro_infos)
            {
                var cloud_fields = pi.GetCustomAttributes(typeof(CloudFiled), true);

                if (cloud_fields.Length > 0)
                {
                    var cloud_field = cloud_fields[0];

                    if (((CloudFiled)cloud_field).IsPrimaryKey)
                    {
                        pi.SetValue(entity, objectId);
                        break;
                    }
                }
            }

            return entity;
        }
        public virtual void SetTEntityId(object entity, TKey objectId)
        {
            var type = entity.GetType();
            var pro_infos = type.GetProperties();
            foreach (var pi in pro_infos)
            {
                var cloud_fields = pi.GetCustomAttributes(typeof(CloudFiled), true);

                if (cloud_fields.Length > 0)
                {
                    var cloud_field = cloud_fields[0];

                    if (((CloudFiled)cloud_field).IsPrimaryKey)
                    {
                        pi.SetValue(entity, objectId);
                        break;
                    }
                }
            }
        }

        public virtual K GetEntityId<K>(TEntity entiy)
        {
            var rtn = default(K);
            var type = typeof(TEntity);

            var pro_infos = type.GetProperties();
            foreach (var pi in pro_infos)
            {
                var cloud_fields = pi.GetCustomAttributes(typeof(CloudFiled), true);

                if (cloud_fields.Length > 0)
                {
                    var cloud_field = cloud_fields[0];

                    if (((CloudFiled)cloud_field).IsPrimaryKey)
                    {
                        rtn = (K)pi.GetValue(entiy);
                        break;
                    }
                }
            }
            return rtn;
        }

        public virtual TKey GetObjectIdByResponseAfterPost(IRestResponse rep)
        {
            var result = JObject.Parse(rep.Content);
            var wrapper = result.ToObject<TRootWrapper>();
            return wrapper.PrimaryKey;
        }

        public virtual T ProcessResponseAfterPost<T>(T entity, IRestResponse rep)
        {
            TKey objectId = GetObjectIdByResponseAfterPost(rep);

            SetTEntityId(entity, objectId);


            return entity;
        }
        public virtual TEntity ProcessResponseAfterPut(TEntity entity, IRestResponse rep)
        {
            var rtn = entity;
            JObject o = JObject.Parse(rep.Content);
            var wrapper = o.ToObject<TRootWrapper>();
            if (wrapper == null)
            {
                rtn = default(TEntity);
            }
            return rtn;
        }

        public virtual IQueryable<TEntity> GetQueryableByResponse(IRestResponse rep)
        {
            JsonConvert.DeserializeObject(rep.Content);

            JObject o = JObject.Parse(rep.Content);

            JArray a = (JArray)o[this.ListJsonRootNodeName];

            IList<TEntity> l = a.ToObject<IList<TEntity>>();

            var wrapperList = a.ToObject<IList<TRootWrapper>>();

            for (int i = 0; i < l.Count; i++)
            {
                SetTEntityId(l[i], wrapperList[i].PrimaryKey);
            }
            var rtn = l.AsQueryable<TEntity>();
            return rtn;
        }

        public virtual bool ProcessResponseAfterDelete(TKey Id, IRestResponse rep)
        {
            var rtn = false;
            if (rep.StatusCode == HttpStatusCode.OK)
            {
                rtn = true;
            }
            return rtn;
        }


        public virtual TEntity DeserializerFromResponse(IRestResponse rep)
        {
            return Deserializer<TEntity>(rep, DefaultDataFormat);
        }

        public virtual string SerializeToPost<T>(T obj)
        {
            var rtn = "";
            Newtonsoft.Json.JsonSerializerSettings _jsonSetting = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
            };
            rtn = JsonConvert.SerializeObject(obj, Formatting.Indented, _jsonSetting);
            return rtn;
        }

        public virtual T Deserializer<T>(IRestResponse response, BaaSDataFormat format)
        {
            T rtn = default(T);
            switch (format)
            {
                case BaaSDataFormat.Json:
                    {

                        JsonDeserializer dezer = new JsonDeserializer();
                        rtn = dezer.Deserialize<T>(response);
                    }
                    break;
                case BaaSDataFormat.Xml:
                    {
                        XmlDeserializer dezer = new XmlDeserializer();
                        rtn = dezer.Deserialize<T>(response);
                    }
                    break;
            }

            return rtn;
        }

        #endregion


    }
}
