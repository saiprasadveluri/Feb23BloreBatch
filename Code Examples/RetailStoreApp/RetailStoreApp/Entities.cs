using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailStoreApp
{
    public abstract class Product
    {       
        public string ProductId{get; set;}        
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Rating { get; set; }
        public string Available { get; set; }
        public int Qty { get; set;  }

        const int PID_INDEX = 1, PNAME_INDEX = 2, PRICE_INDEX = 3,RATING_INDEX=4,AVAIL_INDEX=5,QTY_INDEX=6;

        public Product(string productId, string productName, double price
            ):this(productId, productName, price, 0,
                "AVAILABLE",25)
        {
            
        }
        public Product(string productId, string productName, double price,
            int rating,string available,int qty)
        {
            ProductId = productId;
            ProductName = productName;
            Price = price;
            Rating = rating;
            Available = available;
            Qty = qty;
        }

        public Product()
        {

        }
        public void DiaplyBasicInfo()
        {
            Console.WriteLine($"productId: {ProductId} - ProductName:{ProductName} - Qty: {Qty}");
        }
        public abstract void DisplayData();
        public virtual void ParseData(string[] parts)
        {
            ProductId = parts[PID_INDEX];
            ProductName = parts[PNAME_INDEX];
            Price=double.Parse(parts[PRICE_INDEX]);
            Rating=int.Parse(parts[RATING_INDEX]);
            Available = parts[AVAIL_INDEX];
            Qty = int.Parse(parts[QTY_INDEX]);
        }        
    }

    public class Fashion: Product
    {
        public string MatType { get; set; }
        public string Pattern { get; set; }
        public int MAT_INDEX=7, PATTERN_INDEX=8;
        public Fashion(string productId, string productName, double price,
            int rating, string available, int qty,string matType, string pattern):
            base(productId, productName, price, rating, available, qty)
        {
            MatType = matType;
            Pattern = pattern;
        }

        public Fashion()
        {

        }

        public override void ParseData(string[] parts)
        {
            base.ParseData(parts);
            MatType=parts[MAT_INDEX];
            Pattern = parts[PATTERN_INDEX];
        }
        public override void DisplayData()
        {
            Console.WriteLine($"MatType: {MatType} - Pattern:{Pattern}");
        }

        public override string ToString()
        {
            return $"{1},{ProductId},{ProductName},{Price},{Rating},{Available},{Qty},{MatType},{Pattern}";
        }
    }

    public class Kitchen:Product, IReturnable
    {
        public string Color { get; set; }
        public double Capacity { get; set; }
        public string SpecialFeature { get; set; }

        private const int COLOR_INDEX = 7, CAP_INDEX = 8, FEATURE_INDEX = 9; 

        public Kitchen(string productId, string productName, double price,
            int rating, string available, int qty,string color, double capacity, 
            string specialFeature): base(productId, productName, price, rating, available, qty)
        {
            Color = color;
            Capacity = capacity;
            SpecialFeature = specialFeature;
        }

        public Kitchen()
        {

        }

        public override void DisplayData()
        {
            Console.WriteLine($"Color: {Color} - Capacity:{Capacity} - SpecialFeature: {SpecialFeature}");
        }

        public void ReturnProduct()
        {

        }

        public override void ParseData(string[] parts)
        {
            base.ParseData(parts);
            Color = parts[COLOR_INDEX];
            Capacity = double.Parse(parts[CAP_INDEX]);
            SpecialFeature = parts[FEATURE_INDEX];
        }

        public override string ToString()
        {
            return $"{2},{ProductId},{ProductName},{Price},{Rating},{Available},{Qty},{Color},{Capacity},{SpecialFeature}";
        }
    }

    public class Electronics : Product, IReturnable
    {
        public string Specification { get; set; }
        public string Model { get; set; }

        const int SPEC_INDEX=7, MODEL_INDEX=8;
        public override void ParseData(string[] parts)
        {
            base.ParseData(parts);
            Specification = parts[SPEC_INDEX];
            Model = parts[MODEL_INDEX];            
        }




        public Electronics(string productId, string productName, double price,
            int rating, string available, int qty,string spec,string model):base(productId, productName, price, rating, available, qty)
        {
            Model = model;
            Specification = spec;
        }

        public Electronics()
        {

        }
        public override void DisplayData()
        {
            Console.WriteLine($"Model: {Model} - Specification:{Specification}");
        }
        public void ReturnProduct()
        {

        }

        public override string ToString()
        {
            return $"{3},{ProductId},{ProductName},{Price},{Rating},{Available},{Qty},{Model},{Specification}";
        }
    }
    public interface IReturnable
    {
        void ReturnProduct();
    }

    public enum ProductType
    {
        FASHION=1,
        KITCHEN=2,
        ELECTRONICS=3
    }
}
