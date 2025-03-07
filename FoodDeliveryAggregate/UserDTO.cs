using System.Collections.Generic;
using System;

public class UserDTO
{
    public long UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string RoleName { get; set; }
}

public class RestaurantDTO
{
    public int RId { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public long UserId { get; set; }
}

public class MenuItemDTO
{
    public int RestaurantId { get; set; }
    public string MenuName { get; set; }
    public int Price { get; set; }
    public string Category { get; set; }
    
}

public class OrderDTO
{
    public long OrderId { get; set; }
    public long UserId { get; set; }
    public long RestaurantId { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }
    public List<OrderItemDTO> OrderItems { get; set; }
}

public class OrderItemDTO
{
    public long OrderItemId { get; set; }
    public long OrderId { get; set; }
    public int MenuItemId { get; set; }
    public int Quantity { get; set; }
}
