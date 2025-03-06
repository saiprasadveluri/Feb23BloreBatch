using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailStoreApp
{
    public abstract class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Rating { get; set; }
        public string Available { get; set; }
        public int Qty { get; set; }

        public Product(string productId, string productName, double price,
            int rating) : this(productId, productName, price, rating,
                "AVAILABLE", 25)
        {

        }
        public Product(string productId, string productName, double price,
            int rating, string available, int qty)
        {
            ProductId = productId;
            ProductName = productName;
            Price = price;
            Rating = rating;
            Available = available;
            Qty = qty;
        }

        public void DisplayBasicInfo()
        {
            Console.WriteLine($"ProductId: {ProductId} - ProductName: {ProductName} - Qty: {Qty}");
        }
        public abstract void DisplayData();
    }

    public class Fashion : Product
    {
        public string MatType { get; set; }
        public string Pattern { get; set; }
        public Fashion(string productId, string productName, double price,
            int rating, string available, int qty, string matType, string pattern) :
            base(productId, productName, price, rating, available, qty)
        {
            MatType = matType;
            Pattern = pattern;
        }

        public override void DisplayData()
        {
            Console.WriteLine($"MatType: {MatType} - Pattern: {Pattern}");
        }
    }

    public class Kitchen : Product, IReturnable
    {
        public string Color { get; set; }
        public double Capacity { get; set; }
        public string SpecialFeature { get; set; }

        public Kitchen(string productId, string productName, double price,
            int rating, string available, int qty, string color, double capacity,
            string specialFeature) : base(productId, productName, price, rating, available, qty)
        {
            Color = color;
            Capacity = capacity;
            SpecialFeature = specialFeature;
        }

        public override void DisplayData()
        {
            Console.WriteLine($"Color: {Color} - Capacity: {Capacity} - SpecialFeature: {SpecialFeature}");
        }

        public void ReturnProduct()
        {
            Console.WriteLine($"Product {ProductName} has been returned.");
        }
    }

    public class Electronics : Product, IReturnable
    {
        public string Specification { get; set; }
        public string Model { get; set; }
        public Electronics(string productId, string productName, double price,
            int rating, string available, int qty, string spec, string model) : base(productId, productName, price, rating, available, qty)
        {
            Model = model;
            Specification = spec;
        }

        public override void DisplayData()
        {
            Console.WriteLine($"Model: {Model} - Specification: {Specification}");
        }
        public void ReturnProduct()
        {
            Console.WriteLine($"Product {ProductName} has been returned.");
        }
    }
    public interface IReturnable
    {
        void ReturnProduct();
    }
}
