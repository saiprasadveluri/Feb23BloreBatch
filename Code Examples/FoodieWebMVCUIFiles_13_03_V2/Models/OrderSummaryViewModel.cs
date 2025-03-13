using FoodDelApp.Data;
using System.Reflection;

namespace MMVCDemoApp1.Models
{
    public class OrderSummaryViewModel
    {
        public List<OrderLineData> OrderdLineItems{get;set;}
        public long RID { get; set; }
        public double OrderTotal { get; set; }
    }
}
