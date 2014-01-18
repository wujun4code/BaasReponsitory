using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaaSReponsitory
{
#if FRAMEWORK
    [Serializable]

    public class BRException : Exception
    {
        public BRException() { }
        public BRException(string message) : base(message) { }
        public BRException(string message, Exception inner) : base(message, inner) { }
        protected BRException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

#endif

#if WINDOWS_PHONE
    public class BRException : Exception
    {
       
    }
#endif
}
