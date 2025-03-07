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
            ReatilStoreManager manager= new ReatilStoreManager();
            
            manager.InitStore();

            while (true)
            {
                Console.WriteLine("Choose Option: 1 - Display Products, 2 -  Add Product, 3- Delete Product, 4 - Print Product Data, 5- Quit");
                string strOption = Console.ReadLine();
                int Option=int.Parse(strOption);
                switch (Option)
                {
                    case 1:
                        manager.DiaplsyProductInfo();
                        break;
                    case 2:
                        manager.AddProduct();
                        break;
                    case 3:

                        break;
                    case 4:

                        break;
                    case 5:
                        manager.SaveStore();
                        return;
                    break;
                }
            }
        }
    }
}
