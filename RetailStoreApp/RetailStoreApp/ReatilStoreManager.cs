using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;

namespace RetailStoreApp
{
    public class ReatilStoreManager
    {
        //public Product[] products { get; set; }
        //public Customer[] customers { get; set; }
        //public Order[] orders { get; set; }
        List<Product> products;
        List<Customer> customers;
        List<Order> orders;
        const string PROD_FILE_PATH = "d:\\temp\\store_products.txt";
        const string CUST_FILE_PATH = "d:\\temp\\store_cust.txt";
        //int CurOrderIndex = 0;
        public ReatilStoreManager()
        {
            //products = new Product[3];
            //customers = new Customer[2];
            //orders= new Order[2];

            products = new List<Product>();
            customers = new List<Customer>();
            orders = new List<Order>();

        }

        public void SaveStore()
        {
            StreamWriter sw = new StreamWriter(PROD_FILE_PATH);
            foreach(Product product in products)
            {
                sw.WriteLine(product.ToString());
            }
            sw.Close();
        }
        public void InitStore()
        {
            if(File.Exists(PROD_FILE_PATH))
            {
                StreamReader sr = new StreamReader(PROD_FILE_PATH);
                while (true)
                {
                    string strProdLine = sr.ReadLine();
                    if (strProdLine == null)
                        break;
                    if (strProdLine.Length == 0)
                        continue;
                    //Product Line is available...
                    Product product = null;
                    string[] parts = strProdLine.Split(new char[] { ',' });
                    int prodType=int.Parse(parts[0]);
                    switch ((ProductType)prodType)
                    {
                        case ProductType.FASHION:
                            product = new Fashion();
                            break;
                        case ProductType.KITCHEN:
                            product = new Kitchen();
                            break;
                        case ProductType.ELECTRONICS:
                            product = new Electronics();
                            break;
                    }
                    product.ParseData(parts);
                    products.Add(product);
                }
                sr.Close();
            }

            if(File.Exists(CUST_FILE_PATH))
            {
                StreamReader sr = new StreamReader(CUST_FILE_PATH);
                while (true)
                {
                    string strCustLine = sr.ReadLine();
                    if (strCustLine == null)
                        break;
                    string[] parts = strCustLine.Split(new char[] { ',' });
                    Customer customer = new Customer()
                    {
                        CustomerId = parts[0],
                        Name = parts[1],
                        Age=int.Parse(parts[2])
                    };
                    customers.Add(customer);
                }
                sr.Close();
            }            
            
        }

        public void AddProduct()
        {
            Product p = null;
            Console.WriteLine("Product Type: 1 - Fashion, 2 - Kitchen, 3 - Electronics");
            int ProdType = int.Parse(Console.ReadLine());
            Console.WriteLine("ProdId: ");
            string ProdId = Console.ReadLine();
            Console.WriteLine("ProdName: ");
            string ProdName = Console.ReadLine();
            Console.WriteLine("Price: ");
            double Price = double.Parse(Console.ReadLine());
            int Rating = 0;
            string Avialable = "AVAILABLE";
            Console.WriteLine("Quantity: ");
            int Qty = int.Parse(Console.ReadLine());

            
            switch (ProdType)
            {
                case 1:
                    Console.WriteLine("Material Type: ");
                    string MatType= Console.ReadLine();
                    Console.WriteLine("Pattern: ");
                    string Pattern = Console.ReadLine();
                    p = new Fashion(ProdId, ProdName, Price, Rating, Avialable, Qty, MatType, Pattern);
                    products.Add(p);
                    break;
                    case 2:
                    Console.WriteLine("Color: ");
                    string color= Console.ReadLine();
                    Console.WriteLine("Capacity: ");
                    double cap=double.Parse(Console.ReadLine());
                    Console.WriteLine("Special Feature: ");
                    string special=Console.ReadLine();
                    p = new Kitchen(ProdId, ProdName, Price, Rating, Avialable, Qty, color, cap, special);
                    products.Add(p);
                    break;
                    case 3:
                    Console.WriteLine("Specification: ");
                    string spec = Console.ReadLine();
                    Console.WriteLine("Model: ");
                    string model = Console.ReadLine();
                    p = new Electronics(ProdId, ProdName, Price, Rating, Avialable, Qty,spec,model);
                    products.Add(p);
                    break;
                default:
                    Console.WriteLine("Invalid Product Type....");
                    break;
            }
        }
        public void DiaplsyProductInfo()
        {
            Console.WriteLine("Product Info:");
            if(products.Count==0)
            {
                Console.WriteLine("No prodcuts found...");
                return;
            }
            for (int i = 0; i < products.Count; i++)
            {
                products[i].DiaplyBasicInfo();
            }
        }

        public void DiaplsyCustInfo()
        {
            Console.WriteLine("Cust Info:");
            for (int i = 0; i < customers.Count; i++)
            {
                Console.WriteLine($"Cust Id: {customers[i].CustomerId} - Name: {customers[i].Name}");
            }
        }

        public void ListOrders()
        {
            Console.WriteLine("Order List: ");
            //for (int i = 0; i < CurOrderIndex; i++)
            //{
            //    Console.WriteLine($"OrderId: {orders[i].OrderId} - CustId:{orders[i].CustomerId} - ProductId:{orders[i].ProductId}");
            //}

            for (int i = 0; i < orders.Count; i++)
            {
                Console.WriteLine($"OrderId: {orders[i].OrderId} - CustId:{orders[i].CustomerId} - ProductId:{orders[i].ProductId}");
            }
        }
        public void OrderProduct(string custId,string prodId)
        {
            //if(CurOrderIndex>1)
            //{
            //    Console.WriteLine("Reached Max Orders");
            //    return;
            //}
            Order order = new Order()
            {
                OrderId = "ORDER_" + orders.Count + 1,
                OrderDate = DateTime.Now,
                OrderStatus = "ORDERD",
                CustomerId = custId,
                ProductId = prodId
            };
            orders.Add(order);
        }

        public void ReturnProduct(string orderId)
        {
            //LINQ
            /*Order curOrd=orders.FirstOrDefault(ord=>ord.OrderId==orderId);
            Product prd = products.FirstOrDefault(p => p.ProductId == curOrd.OrderId);
            if (prd!=null)
            {
                if (prd is IReturnable)
                {
                    Console.WriteLine("Return Initiated....");
                }
                else
                {
                    Console.WriteLine("You cant return this product");
                }
            }
            */
            //Longer Way...
            Order CurOrder = null;
            foreach (Order order in orders)
            {
                if(order.OrderId== orderId)
                {
                    CurOrder = order;
                    break;
                }
            }
            if (CurOrder != null)
            {
                string ProdId = CurOrder.ProductId;
                Product Curprod = null;
                foreach (Product prod in products)
                {
                    if (prod.ProductId == ProdId)
                    {
                        Curprod = prod;
                        break;
                    }
                }
                if (Curprod != null)
                {
                    //Return....
                    if(Curprod is IReturnable)
                    {
                        Console.WriteLine("Return Initiated....");
                    }
                    else
                    {
                        Console.WriteLine("You cant return this product");
                    }
                }
            }
        }
    }
}
