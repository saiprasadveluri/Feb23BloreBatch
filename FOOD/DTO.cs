public class User
{
    public long UId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}

public class Restaurant
{
    public long Rid { get; set; }
    public string Rname { get; set; }
    public string Rlocation { get; set; }
    public long UserId { get; set; }
}

public class Order
{
    public long Oid { get; set; }
    public long UsrId { get; set; }
    public long ResId { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; }
}

public class MenuItem
{
    public long MId { get; set; }
    public long ResId { get; set; }
    public string Mname { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
}

public class OrderItem
{
    public long OrderItemId { get; set; }
    public long OrderId { get; set; }
    public long MenuId { get; set; }
    public int Quantity { get; set; }
}
