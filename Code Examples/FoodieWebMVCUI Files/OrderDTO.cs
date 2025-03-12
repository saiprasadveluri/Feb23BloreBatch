using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelApp.Data
{
    public class OrderDTO
    {
        public long OID {  get; set; }
        public long RID {  get; set; }
        public long OrderBy {  get; set; }
        public DateTime OrderDate { get; set; }
        public long OrderStatus { get; set; }
        public string RestaurantName {  get; set; }
        public string OrderedUserName { get; set; }
    }
}
