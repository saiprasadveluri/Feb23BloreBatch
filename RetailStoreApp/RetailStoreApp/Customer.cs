using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailStoreApp
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public int Age {  get; set; }
    }

    public class Order
    {
       public string OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string ProductId { get; set; }
        public string CustomerId { get; set; }
        public string OrderStatus { get; set; }
    }
}
