using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public class AVOSCloud<TEntity> : BaseBaaS<string, AVOSJsonWrapper, TEntity>, IBaaS<string, TEntity> where TEntity : class
    {

        public AVOSCloud()
            : base()
        {
            this.RestService = new AVOSCloudRest<TEntity>();
        }
    }
}
