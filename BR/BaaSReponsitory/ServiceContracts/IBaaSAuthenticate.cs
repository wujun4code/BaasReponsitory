using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    public interface IBaaSAuthenticate
    {
#if FRAMEWORK
        TUser Register<TUser>(TUser newUser) where TUser : CloudUser;

        TUser Login<TUser>(TUser user) where TUser : CloudUser;
#endif
        void RegisterAsync<TUser>(TUser newUser, Action<TUser> callback) where TUser : CloudUser;

        void LoginAsync<TUser>(TUser user, Action<TUser> callback) where TUser : CloudUser;
    }
}
