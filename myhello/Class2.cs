using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myhello
{
    public class ride
    {
        public int rideno;
        public string cname;
        public string drivername;
        public int fare;
        public int distance;
        public ride(int rno, string cn, string dn, int f)
        {
            rideno = rno;
            cname = cn;
            drivername = dn;
            fare = f;
            
        }
        public virtual void display() {
            Console.WriteLine("Ride No: " + rideno);
            Console.WriteLine("Customer Name: " + cname);
            Console.WriteLine("Driver Name: " + drivername);
            Console.WriteLine("Fare: " + fare);
        }
    }
    public class regular : ride
    {
        public int startloc;
        public int destloc;
        public regular(int rno, string cn, string dn, int f,  int sl, int dl) : base(rno, cn, dn, f)
        {
            startloc = sl;
            destloc = dl;
            distance = Math.Abs(sl - dl);
        }
        public override void display()
        {
            Console.WriteLine("Ride No: " + rideno);
            Console.WriteLine("Customer Name: " + cname);
            Console.WriteLine("Driver Name: " + drivername);
            Console.WriteLine("Fare: " + fare);
            Console.WriteLine("Distance: " + distance);
            Console.WriteLine("Start Location: " + startloc);
            Console.WriteLine("Destination Location: " + destloc);
            Console.WriteLine("total fare: " + fare * distance);
        }
    }
    public class rental : ride
    {
        public int mintravfare;
        public int costperhr;
        public int hours;
        public int minfare;


        public rental(int rno, string cn, string dn, int f, int mtf, int cph, int h) : base(rno, cn, dn, f)
        {
            mintravfare = mtf;
            costperhr = cph;
            hours = h;
        }
        public override void display()
        {
            Console.WriteLine("Ride No: " + rideno);
            Console.WriteLine("Customer Name: " + cname);
            Console.WriteLine("Driver Name: " + drivername);
            Console.WriteLine("Fare: " + fare);
            Console.WriteLine("Minimum Travel Fare: " + mintravfare);
            Console.WriteLine("Cost per Hour: " + costperhr);
            Console.WriteLine("Hours: " + hours);
            Console.WriteLine("Total Fare: " + mintravfare + (costperhr * hours));
        }
    }
}
