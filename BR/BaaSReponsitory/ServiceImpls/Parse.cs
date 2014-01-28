using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public class Parse<TEntity> : BaseBaaS<string, AVOSJsonWrapper, TEntity>, IBaaS<string, TEntity> where TEntity : class
    {
        public Parse()
            : base()
        {
            this.RestService = new ParseRest<TEntity>();
        }
    }
}
