using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
            protected Product(int pid, string pname, double pprice, int prating, int deldays, int pquantity)

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
            private readonly List<Product> products = new List<Product>();
            private const string filePath = "C:\\Newfile\\products.txt";

            public void LoadProducts()

            {

                if (!File.Exists(filePath)) return;

                foreach (var line in File.ReadLines(filePath))

                {

                    var parts = line.Split(',');

                    var type = parts[0];

                    var pid = int.Parse(parts[1]);

                    var pname = parts[2];

                    var pprice = double.Parse(parts[3]);

                    var prating = int.Parse(parts[4]);

                    var deldays = int.Parse(parts[5]);

                    var pquantity = int.Parse(parts[6]);

                    switch (type)

                    {

                        case "Fashion":

                            products.Add(new Fashion(pid, pname, pprice, prating, deldays, pquantity, parts[7], parts[8]));

                            break;

                        case "Electronics":

                            products.Add(new Electronics(pid, pname, pprice, prating, deldays, pquantity, parts[7], parts[8]));

                            break;

                        case "Kitchen":

                            products.Add(new Kitchen(pid, pname, pprice, prating, deldays, pquantity, parts[7], parts[8], parts[9]));

                            break;

                    }

                }

            }

            public void SaveProducts()

            {

                using (var writer = new StreamWriter(filePath))

                {

                    foreach (var product in products)

                    {

                        switch (product)

                        {

                            case Fashion fashion:

                                writer.WriteLine($"Fashion,{fashion.ProductID},{fashion.ProductName},{fashion.ProductPrice},{fashion.ProductRating},{fashion.DaysOfDelivery},{fashion.ProductAvailableQuantity},{fashion.MaterialType},{fashion.Pattern}");

                                break;

                            case Electronics electronics:

                                writer.WriteLine($"Electronics,{electronics.ProductID},{electronics.ProductName},{electronics.ProductPrice},{electronics.ProductRating},{electronics.DaysOfDelivery},{electronics.ProductAvailableQuantity},{electronics.Specification},{electronics.Model}");

                                break;

                            case Kitchen kitchen:

                                writer.WriteLine($"Kitchen,{kitchen.ProductID},{kitchen.ProductName},{kitchen.ProductPrice},{kitchen.ProductRating},{kitchen.DaysOfDelivery},{kitchen.ProductAvailableQuantity},{kitchen.Color},{kitchen.Capacity},{kitchen.SpecialFeature}");

                                break;

                        }

                    }

                }

            }

            public void AddProduct(Product product)

            {

                products.Add(product);

                SaveProducts();

            }

            public void DeleteProduct(int productId)

            {

                products.RemoveAll(p => p.ProductID == productId);

                SaveProducts();

            }


            public void DisplayProducts()

            {

                if (!File.Exists(filePath)) return;

                foreach (var line in File.ReadLines(filePath))

                {

                    var parts = line.Split(',');

                    var type = parts[0];

                    var pid = int.Parse(parts[1]);

                    var pname = parts[2];

                    var pprice = double.Parse(parts[3]);

                    var prating = int.Parse(parts[4]);

                    var deldays = int.Parse(parts[5]);

                    var pquantity = int.Parse(parts[6]);

                    switch (type)

                    {

                        case "Fashion":

                            new Fashion(pid, pname, pprice, prating, deldays, pquantity, parts[7], parts[8]).Display();

                            break;

                        case "Electronics":

                            new Electronics(pid, pname, pprice, prating, deldays, pquantity, parts[7], parts[8]).Display();

                            break;

                        case "Kitchen":

                            new Kitchen(pid, pname, pprice, prating, deldays, pquantity, parts[7], parts[8], parts[9]).Display();

                            break;

                    }

                }

            }

        }

    }

}

