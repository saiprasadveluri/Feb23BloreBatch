using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myhello
{
    public static class Util
    {
        public static void swap<T>( T a,  T b)
        {
            Console.WriteLine("before swap\na = " + a + " b = " + b);
            T temp = a;
            a = b;
            b = temp;
            Console.WriteLine("after swap\na = " + a + " b = " + b);
        }
    }
    public class IntHolder
    {
        int val;
        public void set(int val)
        {
            this.val = val;
        }
        public void print()
        {
            Console.WriteLine(val);
        }
    }
}
