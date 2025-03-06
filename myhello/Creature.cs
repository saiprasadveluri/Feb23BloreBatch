using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myhello
{
    public class Creature
    {
        public void eat()
        {
            Console.WriteLine("Creature is eating");
        }

        public void sleep()
        {
            Console.WriteLine("Creature is sleeping");
        }
    }

    public class Birds : Creature
    {
        public void fly()
        {
            Console.WriteLine("Birds are flying");
        }
    }

    public class Fish : Creature
    {
        public void swim()
        {
            Console.WriteLine("Fish are swimming");
        }
    }

    public class Special : Creature
    {
        public void fly()
        {
            Console.WriteLine("Special are flying");
        }
        public void swim()
        {
            Console.WriteLine("Special are swimming");
        }
    }
    
}
