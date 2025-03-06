using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myhello
{
    class Retail
    {
        public abstract class product
        {
            public int productid
                { get; set; }
            public string productname
                { get; set; }
            public double productprice
                { get; set; }
            public int productrating
                { get; set; }
            public int daysofdelivery
                { get; set; }
            public int prodctAvailableQuantity
                { get; set; }

            public product(int pid, string pname, double pprice, int prating, int deldays, int pquantity)
            {
                productid = pid;
                productname = pname;
                productprice = pprice;
                productrating = prating;
                daysofdelivery = deldays;
                prodctAvailableQuantity = pquantity;
            }

            public void display()
            {

            }

        }

        public class customer
        {
            public int custid
            { get; set; }
            public int custage
            { get; set; }
            public string custname
            { get; set; }
            public string custaddress
            { get; set; }

            public customer(int cid, int cage, string cname, string caddress)
            {
                custid = cid;
                custage = cage;
                custname = cname;
                custaddress = caddress;
            }
        }

        public class order
        {
            public int orderid
            { get; set; }
            public int orderquantity
            { get; set; }
            public string customerid
            { get; set; }
            public string productid
            { get; set; }
            public string orderdate
            { get; set; }
            public string orderstatus
            { get; set; }

            public order(int oid, int oq, string cid, string pid, string od, string os)
            {
                orderid = oid;
                orderquantity = oq;
                customerid = cid;
                productid = pid;
                orderdate = od;
                orderstatus = os;
            }
        }

        public class Fashion : product
        {
            public string materialtype
            { get; set; }
            public string pattern
            { get; set; }

            public Fashion(int pid, string pname, double pprice, int prating, int deldays, int pquantity, string mtype, string pat) : base(pid, pname, pprice, prating, deldays, pquantity)
            {
                materialtype = mtype;
                pattern = pat;
            }

        }

        public class Electronics : product
        {
            public string specification
            { get; set; }
            public string model
            { get; set; }

            public Electronics(int pid, string pname, double pprice, int prating, int deldays, int pquantity, string spec, string mod) : base(pid, pname, pprice, prating, deldays, pquantity)
            {
                specification = spec;
                model = mod;
            }
        }

        public class  Kitchen : product
        {
            public string color
            { get; set; }
            public string capacity
                { get; set; }
            public string specialfeature
            { get; set; }

            public Kitchen(int pid, string pname, double pprice, int prating, int deldays, int pquantity, string col, string cap, string spf) : base(pid, pname, pprice, prating, deldays, pquantity)
            {
                color = col;
                capacity = cap;
                specialfeature = spf;
            }
        }
    }
}
