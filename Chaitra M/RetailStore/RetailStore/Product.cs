using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailStore
{
    abstract class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }
        public int DaysToDeliver { get; set; }
        public int AvailableQuantity { get; set; }

        public abstract string GetProductDetails();
        public virtual string GetProductType()
        {
            return "Product";
        }
    }

    public class Electronics : Product
    {
        public string Specification { get; set; }
        public string Model { get; set; }

        public override string GetProductType() => "Electronics";

        public override string GetProductDetails()
        {
            return $"{ProductId},{Name},{Price},{Rating},{DaysToDeliver},{AvailableQuantity},{GetProductType()},{Specification},{Model}";
        }
    }

    public class Kitchen : Product
    {
        public string Color { get; set; }
        public string Capacity { get; set; }
        public string SpecialFeature { get; set; }

        public override string GetProductType() => "Kitchen";

        public override string GetProductDetails()
        {
            return $"{ProductId},{Name},{Price},{Rating},{DaysToDeliver},{AvailableQuantity},{GetProductType()},{Color},{Capacity},{SpecialFeature}";
        }
    }

    class ProductManager
    {
        private List<Product> products = new List<Product>();
        private string filePath = "products.txt";

        public void AddProduct(Product product)
        {
            products.Add(product);
            SaveToFile();
        }

        public void DeleteProduct(int productId)
        {
            products.RemoveAll(p => p.ProductId == productId);
            SaveToFile();
        }

        public void UpdateProduct(int productId)
        {
            Product product = products.Find(p => p.ProductId == productId);
            if (product != null)
            {
                Console.Write("Enter the new price: ");
                product.Price = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter new Available Quantity: ");
                product.AvailableQuantity = Convert.ToInt32(Console.ReadLine());

                SaveToFile();
            }
            else
            {
                Console.WriteLine("Product not found");
            }
        }

        public void DisplayProducts()
        {
            if (products.Count == 0)
            {
                Console.WriteLine("No products to display");
                return;
            }

            foreach (Product product in products)
            {
                Console.WriteLine(product.GetProductDetails());
            }
        }

        public void SaveToFile()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (Product product in products)
                {
                    writer.WriteLine(product.GetProductDetails());
                }
            }
        }

        public void LoadFromFile()
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    int id = int.Parse(parts[0]);
                    string name = parts[1];
                    double price = double.Parse(parts[2]);
                    double rating = double.Parse(parts[3]);
                    int daysToDeliver = int.Parse(parts[4]);
                    int availableQuantity = int.Parse(parts[5]);
                    string productType = parts[6];

                    if (productType == "Electronics")
                    {
                        products.Add(new Electronics
                        {
                            ProductId = id,
                            Name = name,
                            Price = price,
                            Rating = rating,
                            DaysToDeliver = daysToDeliver,
                            AvailableQuantity = availableQuantity,
                            Specification = parts[7],
                            Model = parts[8]
                        });
                    }
                    else if (productType == "Kitchen")
                    {
                        products.Add(new Kitchen
                        {
                            ProductId = id,
                            Name = name,
                            Price = price,
                            Rating = rating,
                            DaysToDeliver = daysToDeliver,
                            AvailableQuantity = availableQuantity,
                            Color = parts[7],
                            Capacity = parts[8],
                            SpecialFeature = parts[9]
                        });
                    }
                }
            }
        }
    }
}
