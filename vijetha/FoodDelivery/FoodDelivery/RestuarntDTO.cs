﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery
{
    public class RestuarntDTO
    {
        public int RestuarntId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int? OwnerId { get; set; }
    }
}
