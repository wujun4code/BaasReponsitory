using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDemo
{
    public class CommonTool
    {

        public static TestObject CreateA()
        {
            return new TestObject()
            {
                Age = 32,
                NonSerialize = false,
                Name = "Neal Caffrey",
            };
        }

        public static TestObject CreateB()
        {
            return new TestObject()
            {
                Age = 40,
                NonSerialize = true,
                Name = "Peter Burke",
            };
        }
    }
}
