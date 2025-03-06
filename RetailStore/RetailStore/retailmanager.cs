using System;
using System.Collections.Generic;
using System.Linq;

namespace RetailStoreApp
{
    public class RetailStoreManager
    {
        public List<Product> Products { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Order> Orders { get; set; }

        public RetailStoreManager()
        {
            Products = new List<Product>();
            Customers = new List<Customer>();
            Orders = new List<Order>();
        }

        //public void InitStore()
        //{
        //    Products.Add(new Fashion("FASHION_001", "Apparel", 1234, 5, "AVAILABLE", 30, "SILK", "PLAIN"));
        //    Products.Add(new Kitchen("KITCHEN_001", "Air Fryer", 4500, 5, "AVAILABLE", 30, "BLACK", 4.5, "BIG"));
        //    Products.Add(new Electronics("ELEC_001", "Laptop", 45000, 5, "AVAILABLE", 5, "Corei5", "INSPIRON"));

        //    Customers.Add(new Customer() { CustomerId = "CUST1", Name = "SAI", Age = 34 });
        //    Customers.Add(new Customer() { CustomerId = "CUST2", Name = "DURGA", Age = 22 });
        //}
        public 

        public void DisplayProductInfo()
        {
            Console.WriteLine("Product Info:");
            for (int i = 0; i < Products.Count; i++)
            {
                Products[i].DiaplyBasicInfo();
            }
        }

        public void DisplayCustInfo()
        {
            Console.WriteLine("Customer Info:");
            for (int i = 0; i < Customers.Count; i++)
            {
                Console.WriteLine($"Customer Id: {Customers[i].CustomerId} - Name: {Customers[i].Name}");
            }
        }

        public void ListOrders()
        {
            Console.WriteLine("Order List:");
            for (int i = 0; i < Orders.Count; i++)
            {
                Console.WriteLine($"OrderId: {Orders[i].OrderId} - CustId: {Orders[i].CustomerId} - ProductId: {Orders[i].ProductId}");
            }
        }

        public void OrderProduct(string custId, string prodId)
        {
            if (Orders.Count >= 2)
            {
                Console.WriteLine("Reached Max Orders");
                return;
            }
            Orders.Add(new Order()
            {
                OrderId = "ORDER_" + Orders.Count,
                OrderDate = DateTime.Now,
                OrderStatus = "ORDERED",
                CustomerId = custId,
                ProductId = prodId
            });
        }

        public void ReturnProduct(string orderId)
        {
            Order curOrd = Orders.FirstOrDefault(ord => ord.OrderId == orderId);
            if (curOrd != null)
            {
                Product prd = Products.FirstOrDefault(p => p.ProductId == curOrd.ProductId);
                if (prd != null)
                {
                    if (prd is IReturnable)
                    {
                        Console.WriteLine("Return Initiated....");
                    }
                    else
                    {
                        Console.WriteLine("You can't return this product");
                    }
                }
            }
        }

        public void SaveProductsToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var product in Products)
                {
                    writer.WriteLine($"{product.ProductId},{product.ProductName},{product.Price},{product.Rating},{product.Available},{product.Qty}");
                }
            }
        }

        public void LoadProductsFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(',');
                        if (parts.Length == 6)
                        {
                            Products.Add(new Product(parts[0], parts[1], double.Parse(parts[2]), int.Parse(parts[3]), parts[4], int.Parse(parts[5])));
                        }
                    }
                }
            }
        }
    }
}