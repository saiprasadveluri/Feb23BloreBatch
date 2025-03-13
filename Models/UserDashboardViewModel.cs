﻿using FoodDelApp.Data;
using FoodDelApp.DTOs;
using MMVCDemoApp1.Infra;

namespace MMVCDemoApp1.Models
{
    public class UserDashboardViewModel
    {
        public List<RestaurantDTO> Restaurants { get; set; }
        public List<OrderDTO> MyOrders { get; set; }
    }
}