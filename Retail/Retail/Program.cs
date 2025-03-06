using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail
{
    class Program
    {
        static void Main(string[] args)
        {
            Store store = new Store();
            string filePath = "C:/Users/Administrator/Desktop/products.csv";
            store.LoadProductsFromFile(filePath);

            while (true)
            {
                Console.WriteLine("Retail Store Management\n");
                Console.WriteLine("1. Update Product");
                Console.WriteLine("2. Add Product");
                Console.WriteLine("3. Delete Product");
                Console.WriteLine("4. Print Products");
                Console.WriteLine("5. Save and Exit");
                Console.Write("Select an option: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        UpdateProduct(store);
                        Console.Write("\n\n");
                        break;

                    case "2":
                        AddProduct(store);
                        Console.Write("\n\n");
                        break;
                    case "3":
                        DeleteProduct(store);
                        Console.Write("\n\n");
                        break;
                    case "4":
                        store.PrintProducts();
                        Console.Write("\n\n");
                        break;
                    case "5":
                        store.SaveProductsToFile(filePath);
                        Console.Write("\n\n");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void UpdateProduct(Store store)
        {
            Console.WriteLine("Select Product Type: 1. Fashion 2. Kitchen 3. Electronics");
            var type = Console.ReadLine();

            Console.WriteLine("Enter Product Id to update: ");
            int productId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Updated Product Details: ");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Price: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Rating: ");
            double rating = double.Parse(Console.ReadLine());
            Console.Write("Days of Delivery: ");
            int daysofdelivery = int.Parse(Console.ReadLine());
            Console.Write("Available Quantity: ");
            int availablequantity = int.Parse(Console.ReadLine());

            if (type == "1")
            {
                Console.Write("Material Composition (COTTON/PURE_SILK/SYNTHETIC): ");
                string materialcomposition = Console.ReadLine();
                Console.Write("Pattern (PLAIN/CHECKED/STRIPES): ");
                string pattern = Console.ReadLine();

                store.UpdateProduct(new Fashion
                {
                    ProductId = productId,
                    Name = name,
                    Price = price,
                    Rating = rating,
                    DaysOfDelivery = daysofdelivery,
                    AvailableQuantity = availablequantity,
                    MaterialComposition = materialcomposition,
                    Pattern = pattern
                });
            }
            else if (type == "2")
            {
                Console.Write("Color: ");
                string color = Console.ReadLine();
                Console.Write("Capacity: ");
                string capacity = Console.ReadLine();
                Console.Write("Special Feature: ");
                string specialfeature = Console.ReadLine();

                store.UpdateProduct(new Kitchen
                {
                    ProductId = productId,
                    Name = name,
                    Price = price,
                    Rating = rating,
                    DaysOfDelivery = daysofdelivery,
                    AvailableQuantity = availablequantity,
                    Color = color,
                    Capacity = capacity,
                    SpecialFeature = specialfeature
                });
            }
            else if (type == "3")
            {
                Console.Write("Specifications: ");
                string specifications = Console.ReadLine();
                Console.Write("Model: ");
                string model = Console.ReadLine();

                store.UpdateProduct(new Electronics{
                    ProductId = productId,
                    Name = name,
                    Price = price,
                    Rating = rating,
                    DaysOfDelivery = daysofdelivery,
                    AvailableQuantity = availablequantity,
                    Specifications = specifications,
                    Model = model
                });



            }
        }
        static void AddProduct(Store store)
        {
            Console.WriteLine("Select Product Type: 1. Fashion 2. Kitchen 3. Electronics");
            var type = Console.ReadLine();

            Console.Write("Product Id: ");
            int productId = int.Parse(Console.ReadLine());
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Price: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Rating: ");
            double rating = double.Parse(Console.ReadLine());
            Console.Write("Days of Delivery: ");
            int daysOfDelivery = int.Parse(Console.ReadLine());
            Console.Write("Available Quantity: ");
            int availableQuantity = int.Parse(Console.ReadLine());

            if (type == "1")
            {
                Console.Write("Material Composition (COTTON/PURE_SILK/SYNTHETIC): ");
                string materialComposition = Console.ReadLine();
                Console.Write("Pattern (PLAIN/CHECKED/STRIPES): ");
                string pattern = Console.ReadLine();

                store.AddProduct(new Fashion
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
            else if (type == "2")
            {
                Console.Write("Color: ");
                string color = Console.ReadLine();
                Console.Write("Capacity: ");
                string capacity = Console.ReadLine();
                Console.Write("Special Feature: ");
                string specialFeature = Console.ReadLine();

                store.AddProduct(new Kitchen
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
            else if (type == "3")
            {
                Console.Write("Specifications: ");
                string specifications = Console.ReadLine();
                Console.Write("Model: ");
                string model = Console.ReadLine();

                store.AddProduct(new Electronics
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
            else
            {
                Console.WriteLine("Invalid product type.");
            }
        }

        static void DeleteProduct(Store store)
        {
            Console.Write("Enter Product Id to delete: ");
            int productId = int.Parse(Console.ReadLine());
            store.DeleteProduct(productId);
        }
    }
}