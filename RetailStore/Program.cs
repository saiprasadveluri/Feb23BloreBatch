using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

namespace Store

{

    class Program

    {

        static void Main(string[] args)

        {

            Retail.ProductManager manager = new Retail.ProductManager();

            manager.LoadProducts();

            int choice;

            do

            {

                Console.WriteLine("Menu:");

                Console.WriteLine("1. Add Product");

                Console.WriteLine("2. Delete Product");

                Console.WriteLine("3. Display Products");

                Console.WriteLine("4. Exit");

                Console.Write("Enter your choice: ");

                choice = int.Parse(Console.ReadLine());

                switch (choice)

                {

                    case 1:

                        AddProduct(manager);

                        break;

                    case 2:

                        DeleteProduct(manager);

                        break;

                    case 3:

                        manager.DisplayProducts();

                        break;

                    case 4:

                        Console.WriteLine("Exiting...");

                        break;

                    default:

                        Console.WriteLine("Invalid choice. Please try again.");

                        break;

                }

            } while (choice != 4);

        }

        static void AddProduct(Retail.ProductManager manager)

        {

            Console.WriteLine("Choose Product Type:");

            Console.WriteLine("1. Fashion");

            Console.WriteLine("2. Electronics");

            Console.WriteLine("3. Kitchen");

            Console.Write("Enter your choice: ");

            int typeChoice = int.Parse(Console.ReadLine());

            Console.Write("Enter Product ID: ");

            int pid = int.Parse(Console.ReadLine());

            Console.Write("Enter Product Name: ");

            string pname = Console.ReadLine();

            Console.Write("Enter Product Price: ");

            double pprice = double.Parse(Console.ReadLine());

            Console.Write("Enter Product Rating: ");

            int prating = int.Parse(Console.ReadLine());

            Console.Write("Enter Days of Delivery: ");

            int deldays = int.Parse(Console.ReadLine());

            Console.Write("Enter Product Available Quantity: ");

            int pquantity = int.Parse(Console.ReadLine());

            if (typeChoice == 1)

            {

                Console.Write("Enter Material Type: ");

                string mtype = Console.ReadLine();

                Console.Write("Enter Pattern: ");

                string pat = Console.ReadLine();

                manager.AddProduct(new Retail.Fashion(pid, pname, pprice, prating, deldays, pquantity, mtype, pat));

            }

            else if (typeChoice == 2)

            {

                Console.Write("Enter Specification: ");

                string spec = Console.ReadLine();

                Console.Write("Enter Model: ");

                string mod = Console.ReadLine();

                manager.AddProduct(new Retail.Electronics(pid, pname, pprice, prating, deldays, pquantity, spec, mod));

            }

            else if (typeChoice == 3)

            {

                Console.Write("Enter Color: ");

                string col = Console.ReadLine();

                Console.Write("Enter Capacity: ");

                string cap = Console.ReadLine();

                Console.Write("Enter Special Feature: ");

                string spf = Console.ReadLine();

                manager.AddProduct(new Retail.Kitchen(pid, pname, pprice, prating, deldays, pquantity, col, cap, spf));

            }

            else

            {

                Console.WriteLine("Invalid product type. Please try again.");

            }

        }

        static void DeleteProduct(Retail.ProductManager manager)

        {

            Console.Write("Enter Product ID to delete: ");

            int pid = int.Parse(Console.ReadLine());

            manager.DeleteProduct(pid);

        }

    }

}
