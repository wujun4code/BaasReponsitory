using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public interface IBaaSProvider
    {
        IBaaS<TKey, TEntity> Create<TKey, TEntity>() where TEntity : class;

        IBaaSAuthenticate CreateAuthenticateService<TUser>() where TUser : CloudUser;
    }
}
