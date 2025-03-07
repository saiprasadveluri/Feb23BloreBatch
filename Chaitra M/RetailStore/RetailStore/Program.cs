using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RetailStore
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductManager manager = new ProductManager();
            manager.LoadFromFile();
            while (true)
            {
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Delete Product");
                Console.WriteLine("3. Update Product");
                Console.WriteLine("4. Display Products");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Choose Product Type: 1. Electronics 2. Fashion 3. Kitchen");
                        int productType = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter Product Id: ");
                        string productId = Console.ReadLine();

                        Console.Write("Enter Product Name: ");
                        string productName = Console.ReadLine();

                        Console.Write("Enter Product Price: ");
                        double price = Convert.ToDouble(Console.ReadLine());

                        Console.Write("Enter Product Rating: ");
                        int rating = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter Product Availability: ");
                        string available = Console.ReadLine();

                        Console.Write("Enter Product Quantity: ");
                        int qty = Convert.ToInt32(Console.ReadLine());

                        Product product = null;

                        if (productType == 1)
                        {
                            Console.Write("Enter Specification: ");
                            string specification = Console.ReadLine();
                            Console.Write("Enter Model: ");
                            string model = Console.ReadLine();
                            product = new Electronics
                            {
                                ProductId = productId,
                                ProductName = productName,
                                Price = price,
                                Rating = rating,
                                Available = available,
                                Qty = qty,
                                Specification = specification,
                                Model = model
                            };
                        }
                        else if (productType == 2)
                        {
                            Console.Write("Enter Material Composition: ");
                            string materialComposition = Console.ReadLine();
                            Console.Write("Enter Pattern: ");
                            string pattern = Console.ReadLine();
                            product = new Fashion
                            {
                                ProductId = productId,
                                ProductName = productName,
                                Price = price,
                                Rating = rating,
                                Available = available,
                                Qty = qty,
                                MaterialComposition = materialComposition,
                                Pattern = pattern
                            };
                        }
                        else if (productType == 3)
                        {
                            Console.Write("Enter Color: ");
                            string color = Console.ReadLine();
                            Console.Write("Enter Capacity: ");
                            double capacity = Convert.ToDouble(Console.ReadLine());
                            Console.Write("Enter Special Feature: ");
                            string specialFeature = Console.ReadLine();
                            product = new Kitchen
                            {
                                ProductId = productId,
                                ProductName = productName,
                                Price = price,
                                Rating = rating,
                                Available = available,
                                Qty = qty,
                                Color = color,
                                Capactity = capacity,
                                SpecialFeature = specialFeature
                            };
                        }
                        manager.AddProduct(product);
                        Console.WriteLine("Product Added Successfully");
                        break;

                    case 2:
                        Console.Write("Enter Product Id to delete: ");
                        string deleteId = Console.ReadLine();
                        manager.DeleteProduct(deleteId);
                        break;

                    case 3:
                        Console.Write("Enter Product Id to update: ");
                        string updateId = Console.ReadLine();
                        manager.UpdateProduct(updateId);
                        break;

                    case 4:
                        manager.DisplayProducts();
                        break;

                    case 5:
                        return;

                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }
            }
        }
    }
}
