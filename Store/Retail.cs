using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace Store
{
    class Retail
    {
        public abstract class Product
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public double ProductPrice { get; set; }
            public int ProductRating { get; set; }
            public int DaysOfDelivery { get; set; }
            public int ProductAvailableQuantity { get; set; }

            public Product(int pid, string pname, double pprice, int prating, int deldays, int pquantity)
            {
                ProductID = pid;
                ProductName = pname;
                ProductPrice = pprice;
                ProductRating = prating;
                DaysOfDelivery = deldays;
                ProductAvailableQuantity = pquantity;
            }

            public abstract void Display();
        }

        public class Fashion : Product
        {
            public string MaterialType { get; set; }
            public string Pattern { get; set; }

            public Fashion(int pid, string pname, double pprice, int prating, int deldays, int pquantity, string mtype, string pat)
                : base(pid, pname, pprice, prating, deldays, pquantity)
            {
                MaterialType = mtype;
                Pattern = pat;
            }

            public override void Display()
            {
                Console.WriteLine($"Fashion Product - ID: {ProductID}, Name: {ProductName}, Price: {ProductPrice}, Rating: {ProductRating}, Delivery: {DaysOfDelivery} days, Quantity: {ProductAvailableQuantity}, Material: {MaterialType}, Pattern: {Pattern}");
            }
        }

        public class Electronics : Product
        {
            public string Specification { get; set; }
            public string Model { get; set; }

            public Electronics(int pid, string pname, double pprice, int prating, int deldays, int pquantity, string spec, string mod)
                : base(pid, pname, pprice, prating, deldays, pquantity)
            {
                Specification = spec;
                Model = mod;
            }

            public override void Display()
            {
                Console.WriteLine($"Electronics Product - ID: {ProductID}, Name: {ProductName}, Price: {ProductPrice}, Rating: {ProductRating}, Delivery: {DaysOfDelivery} days, Quantity: {ProductAvailableQuantity}, Specification: {Specification}, Model: {Model}");
            }
        }

        public class Kitchen : Product
        {
            public string Color { get; set; }
            public string Capacity { get; set; }
            public string SpecialFeature { get; set; }

            public Kitchen(int pid, string pname, double pprice, int prating, int deldays, int pquantity, string col, string cap, string spf)
                : base(pid, pname, pprice, prating, deldays, pquantity)
            {
                Color = col;
                Capacity = cap;
                SpecialFeature = spf;
            }

            public override void Display()
            {
                Console.WriteLine($"Kitchen Product - ID: {ProductID}, Name: {ProductName}, Price: {ProductPrice}, Rating: {ProductRating}, Delivery: {DaysOfDelivery} days, Quantity: {ProductAvailableQuantity}, Color: {Color}, Capacity: {Capacity}, Feature: {SpecialFeature}");
            }
        }

        public class ProductManager
        {
            private List<Product> products = new List<Product>();
            

            public void LoadProducts()
            {
                if (File.Exists("C:\\Users\\Administrator\\Documents\\temp\\retail.txt"))
                {
                    using (StreamReader reader = new StreamReader("C:\\Users\\Administrator\\Documents\\temp\\retail.txt"))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(',');
                            string type = parts[0];
                            int pid = int.Parse(parts[1]);
                            string pname = parts[2];
                            double pprice = double.Parse(parts[3]);
                            int prating = int.Parse(parts[4]);
                            int deldays = int.Parse(parts[5]);
                            int pquantity = int.Parse(parts[6]);

                            if (type == "Fashion")
                            {
                                string mtype = parts[7];
                                string pat = parts[8];
                                products.Add(new Fashion(pid, pname, pprice, prating, deldays, pquantity, mtype, pat));
                            }
                            else if (type == "Electronics")
                            {
                                string spec = parts[7];
                                string mod = parts[8];
                                products.Add(new Electronics(pid, pname, pprice, prating, deldays, pquantity, spec, mod));
                            }
                            else if (type == "Kitchen")
                            {
                                string col = parts[7];
                                string cap = parts[8];
                                string spf = parts[9];
                                products.Add(new Kitchen(pid, pname, pprice, prating, deldays, pquantity, col, cap, spf));
                            }
                        }
                    }
                }
            }

            public void SaveProducts()
            {
                using (StreamWriter writer = new StreamWriter("C:\\Users\\Administrator\\Documents\\temp\\retail.txt"))
                {
                    foreach (var product in products)
                    {
                        if (product is Fashion fashion)
                        {
                            writer.WriteLine($"Fashion,{fashion.ProductID},{fashion.ProductName},{fashion.ProductPrice},{fashion.ProductRating},{fashion.DaysOfDelivery},{fashion.ProductAvailableQuantity},{fashion.MaterialType},{fashion.Pattern}");
                        }
                        else if (product is Electronics electronics)
                        {
                            writer.WriteLine($"Electronics,{electronics.ProductID},{electronics.ProductName},{electronics.ProductPrice},{electronics.ProductRating},{electronics.DaysOfDelivery},{electronics.ProductAvailableQuantity},{electronics.Specification},{electronics.Model}");
                        }
                        else if (product is Kitchen kitchen)
                        {
                            writer.WriteLine($"Kitchen,{kitchen.ProductID},{kitchen.ProductName},{kitchen.ProductPrice},{kitchen.ProductRating},{kitchen.DaysOfDelivery},{kitchen.ProductAvailableQuantity},{kitchen.Color},{kitchen.Capacity},{kitchen.SpecialFeature}");
                        }
                    }
                }
            }

            public void AddProduct(Product product)
            {
                products.Add(product);
                SaveProducts();
            }

            public void DeleteProduct(int productId, Type productType)
            {
                products.RemoveAll(p => p.ProductID == productId && p.GetType() == productType);
                SaveProducts();
            }

            public void DisplayProducts()
            { 
                if (File.Exists("C:\\Users\\Administrator\\Documents\\temp\\retail.txt"))
                {
                    using (StreamReader reader = new StreamReader("C:\\Users\\Administrator\\Documents\\temp\\retail.txt"))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(',');
                            string type = parts[0];
                            int pid = int.Parse(parts[1]);
                            string pname = parts[2];
                            double pprice = double.Parse(parts[3]);
                            int prating = int.Parse(parts[4]);
                            int deldays = int.Parse(parts[5]);
                            int pquantity = int.Parse(parts[6]);

                            if (type == "Fashion")
                            {
                                string mtype = parts[7];
                                string pat = parts[8];
                                var fashion = new Fashion(pid, pname, pprice, prating, deldays, pquantity, mtype, pat);
                                fashion.Display();
                            }
                            else if (type == "Electronics")
                            {
                                string spec = parts[7];
                                string mod = parts[8];
                                var electronics = new Electronics(pid, pname, pprice, prating, deldays, pquantity, spec, mod);
                                electronics.Display();
                            }
                            else if (type == "Kitchen")
                            {
                                string col = parts[7];
                                string cap = parts[8];
                                string spf = parts[9];
                                var kitchen = new Kitchen(pid, pname, pprice, prating, deldays, pquantity, col, cap, spf);
                                kitchen.Display();
                            }
                        }
                    }
                }
            }
        }

    }
}