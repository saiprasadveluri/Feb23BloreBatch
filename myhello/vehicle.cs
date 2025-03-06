using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myhello
{
    public class Vehicle
    {
        private string make;

        private int MaxSpeed;

        public Vehicle(string mk, int speed)
        {
            make = mk;
            MaxSpeed = speed;
        }

        public void Drive()
        {
            Console.WriteLine("Driving the vehicle");
        }

        public void ShowData()
        {
            Console.WriteLine($"{make}-{MaxSpeed}");
        }

    }
    public class Car : Vehicle
    {

        private string NoofGears;
        private int capacity;
        public Car(string mk, int speed, string gears, int cap) : base(mk, speed)
        {
            NoofGears = gears;
            capacity = cap;
        }
        public void ShowNOofGears()
        {
            Console.WriteLine("No of Gears: " + NoofGears);
        }

        public new void  Drive()
        {
            Console.WriteLine("change the gear");
            Console.WriteLine("Driving the vehicle");
        }
    }
    public class Truck : Vehicle
    {
        private int loadCapacity;
        public Truck(string mk, int speed, int cap) : base(mk, speed)
        {
            loadCapacity = cap;
        }
        public void ShowLoadCapacity()
        {
            Console.WriteLine("Load Capacity: " + loadCapacity);
        }
        public new void Drive()
        {
            Console.WriteLine("Driving the truck");
        }
    }
}