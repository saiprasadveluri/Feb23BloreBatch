﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryAggregateApp
{
    class MenuItemDTO
    {
        public long MenuId { get; set; }
        public string MenuName { get; set; }
        public long RID { get; set; }
        public decimal UnitPrice { get; set; }
        public string FoodType { get; set; }
    }
}


