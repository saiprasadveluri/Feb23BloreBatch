using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailStoreApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ReatilStoreManager manager = new ReatilStoreManager();
            manager.InitStore();
            manager.DiaplsyProductInfo();
            manager.DiaplsyCustInfo();
            for (int n = 0; n < 2; ++n)
            {
                Console.WriteLine("enter Cust Id: ");
                string custId = Console.ReadLine();
                Console.WriteLine("enter Prod Id: ");
                string prodId = Console.ReadLine();

                manager.OrderProduct(custId, prodId);
            }
            manager.ListOrders();
            Console.WriteLine("Order Id to Return: ");
            string orderId = Console.ReadLine();
            manager.ReturnProduct(orderId);
        }
    }
}

