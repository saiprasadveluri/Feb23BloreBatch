using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myhello
{
    public class MyvalueHolder<T>
    {
        T val;
        public void set(T val)
        {
            this.val = val;
        }
        public void print()
        {
            Console.WriteLine(val);
        }
    }
}
