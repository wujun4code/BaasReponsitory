using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
    
    public abstract class CloudUser
    {
       
        public abstract string UserName { get; set; }

       
        public abstract string Password { get; set; }

       
        public abstract string Email { get; set; }

    }
}
