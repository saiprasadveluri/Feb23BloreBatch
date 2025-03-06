using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Retail
{
    public abstract class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double Rating { get; set; }
        public int DaysOfDelivery { get; set; }
        public int AvailableQuantity { get; set; }

        public override string ToString()
        {
            return $"{GetType().Name},{ProductId},{Name},{Price},{Rating},{DaysOfDelivery},{AvailableQuantity}";
        }
    }

    public class Electronics : Product
    {
        public string Specifications { get; set; }
        public string Model { get; set; }

        public override string ToString()
        {
            return base.ToString() + $",{Specifications},{Model}";
        }
    }

    public class Fashion : Product
    {
        public string MaterialComposition { get; set; }
        public string Pattern { get; set; }

        public override string ToString()
        {
            return base.ToString() + $",{MaterialComposition},{Pattern}";
        }
    }

    public class Kitchen : Product
    {
        public string Color { get; set; }
        public string Capacity { get; set; }
        public string SpecialFeature { get; set; }

        public override string ToString()
        {
            return base.ToString() + $",{Color},{Capacity},{SpecialFeature}";
        }
    }

    

    public class Store
    {
        private List<Product> products = new List<Product>();

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public void DeleteProduct(int productId)
        {
            products.RemoveAll(p => p.ProductId == productId);
        }

        public void UpdateProduct(Product updatedProduct)
        {
            var product = products.Find(products => products.ProductId == updatedProduct.ProductId);
            if (product != null)
            {
                product.Name = updatedProduct.Name;
                product.Price = updatedProduct.Price;
                product.Rating = updatedProduct.Rating;
                product.DaysOfDelivery = updatedProduct.DaysOfDelivery;
                product.AvailableQuantity = updatedProduct.AvailableQuantity;

                if (product is Electronics electronics && updatedProduct is Electronics updatedElectronics)
                {
                    electronics.Specifications = updatedElectronics.Specifications;
                    electronics.Model = updatedElectronics.Model;
                }
                else if (product is Fashion fashion && updatedProduct is Fashion updatedFashion)
                {
                    fashion.MaterialComposition = updatedFashion.MaterialComposition;
                    fashion.Pattern = updatedFashion.Pattern;
                }
                else if (product is Kitchen kitchen && updatedProduct is Kitchen updatedKitchen)
                {
                    kitchen.Color = updatedKitchen.Color;
                    kitchen.Capacity = updatedKitchen.Capacity;
                    kitchen.SpecialFeature = updatedKitchen.SpecialFeature;
                }
            }
        }

        public void PrintProducts()
        {
            if (products.Count == 0)
            {
                Console.WriteLine("No products available");
                return;
            }
            else
            {
                foreach (var product in products)
                {
                    Console.WriteLine(product);
                }
            }
        }

        public void LoadProductsFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    var type = parts[0];
                    var productId = int.Parse(parts[1]);
                    var name = parts[2];
                    var price = decimal.Parse(parts[3]);
                    var rating = double.Parse(parts[4]);
                    var daysOfDelivery = int.Parse(parts[5]);
                    var availableQuantity = int.Parse(parts[6]);

                    

                    if (type == nameof(Fashion))
                    {
                        var materialComposition = parts[7];
                        var pattern = parts[8];
                        products.Add(new Fashion
                        {
                            ProductId = productId,
                            Name = name,
                            Price = price,
                            Rating = rating,
                            DaysOfDelivery = daysOfDelivery,
                            AvailableQuantity = availableQuantity,
                            MaterialComposition = materialComposition,
                            Pattern = pattern
                        });
                    }
                    else if (type == nameof(Kitchen))
                    {
                        var color = parts[7];
                        var capacity = parts[8];
                        var specialFeature = parts[9];
                        products.Add(new Kitchen
                        {
                            ProductId = productId,
                            Name = name,
                            Price = price,
                            Rating = rating,
                            DaysOfDelivery = daysOfDelivery,
                            AvailableQuantity = availableQuantity,
                            Color = color,
                            Capacity = capacity,
                            SpecialFeature = specialFeature
                        });
                    }
                    else if (type == nameof(Electronics))
                    {
                        var specifications = parts[7];
                        var model = parts[8];
                        products.Add(new Electronics
                        {
                            ProductId = productId,
                            Name = name,
                            Price = price,
                            Rating = rating,
                            DaysOfDelivery = daysOfDelivery,
                            AvailableQuantity = availableQuantity,
                            Specifications = specifications,
                            Model = model
                        });
                    }
                    
                }
            }
            else
            {
                Console.WriteLine("Files/Products not found");
            }
        }

        public void SaveProductsToFile(string filePath)
        {
            var lines = new List<string>();
            foreach (var product in products)
            {
                lines.Add(product.ToString());
            }
            File.WriteAllLines(filePath, lines);
        }
    }
}
