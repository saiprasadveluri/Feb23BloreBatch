using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FoodDelApp.DTOs;

public class DataAccessLayer
{
    private const string ConString = "Data Source=.;Initial Catalog=MFoodDelDB;Integrated Security=SSPI";
    private SqlConnection con;
    private SqlTransaction trans;

    public DataAccessLayer()
    {
        con = new SqlConnection(ConString);
    }

    public void BeginTrans()
    {
        con.Open();
        trans = con.BeginTransaction();
    }

    public void EndTransaction(bool commit)
    {
        if (commit)
        {
            trans.Commit();
        }
        else
        {
            trans.Rollback();
        }
        con.Close();
    }

    public void CloseApp()
    {
        con.Close();
    }

    public UserDTO Login(string Email, string Password)
    {
        UserDTO user = null;
        string query = "SELECT * FROM Users WHERE Email = @Email AND Password = @Password";
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@Email", Email);
        cmd.Parameters.AddWithValue("@Password", Password);

        con.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            user = new UserDTO
            {
                UserId = (int)reader["UserId"],
                Name = (string)reader["Name"],
                Email = (string)reader["Email"],
                Password = (string)reader["Password"],
                RoleName = (string)reader["RoleName"],
                Location = (string)reader["Location"]
            };
        }
        con.Close();
        return user;
    }

    public bool AddNewRestaurant(RestaurantDTO restaurant)
    {
        try
        {
            string query = "INSERT INTO Restaurants (Name, Location, OwnerId) VALUES (@Name, @Location, @OwnerId)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Name", restaurant.Name);
            cmd.Parameters.AddWithValue("@Location", restaurant.Location);
            cmd.Parameters.AddWithValue("@OwnerId", restaurant.OwnerId);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        catch (Exception)
        {
            con.Close();
            return false;
        }
    }

    public bool AddNewUser(UserDTO user)
    {
        try
        {
            string query = "INSERT INTO Users (Name, Email, Password, RoleName, Location) VALUES (@Name, @Email, @Password, @RoleName, @Location)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@RoleName", user.RoleName);
            cmd.Parameters.AddWithValue("@Location", user.Location);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        catch (Exception)
        {
            con.Close();
            return false;
        }
    }

    public string GetUserRole(int UserId)
    {
        string role = null;
        string query = "SELECT RoleName FROM Users WHERE UserId = @UserId";
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@UserId", UserId);

        con.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            role = (string)reader["RoleName"];
        }
        con.Close();
        return role;
    }

    public List<UserDTO> GetRestaurantOwnersList()
    {
        List<UserDTO> owners = new List<UserDTO>();
        string query = "SELECT * FROM Users WHERE RoleName = 'OWNER'";
        SqlCommand cmd = new SqlCommand(query, con);

        con.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            UserDTO owner = new UserDTO
            {
                UserId = (int)reader["UserId"],
                Name = (string)reader["Name"],
                Email = (string)reader["Email"],
                Password = (string)reader["Password"],
                RoleName = (string)reader["RoleName"],
                Location = (string)reader["Location"]
            };
            owners.Add(owner);
        }
        con.Close();
        return owners;
    }

    public List<RestaurantDTO> ListRestaurantsByLocation(string UserLocacation)
    {
        List<RestaurantDTO> restaurants = new List<RestaurantDTO>();
        string query = "SELECT * FROM Restaurants WHERE Location = @Location";
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@Location", UserLocacation);

        con.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            RestaurantDTO restaurant = new RestaurantDTO
            {
                RID = (int)reader["RestaurantId"],
                Name = (string)reader["Name"],
                Location = (string)reader["Location"],
                OwnerId = (int)reader["OwnerId"]
            };
            restaurants.Add(restaurant);
        }
        con.Close();
        return restaurants;
    }

    public List<MenuItemDTO> GetRestaurentMenu(int RID)
    {
        List<MenuItemDTO> menuItems = new List<MenuItemDTO>();
        string query = "SELECT * FROM MenuItems WHERE RestaurantId = @RestaurantId";
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@RestaurantId", RID);

        con.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            MenuItemDTO menuItem = new MenuItemDTO
            {
                MID = (int)reader["MenuItemId"],
                MenuName = (string)reader["MenuName"],
                UnitPrice = (int)reader["UnitPrice"],
                FoodType = (string)reader["FoodType"],
                RID = (int)reader["RestaurantId"]
            };
            menuItems.Add(menuItem);
        }
        con.Close();
        return menuItems;
    }

    // DataAccessLayer.cs

    public List<RestaurantDTO> ListRestaurantsByOwner(int OwnerId)
    {
        List<RestaurantDTO> restaurants = new List<RestaurantDTO>();
        string query = "SELECT RestaurantId, Name, Location, OwnerId FROM Restaurants WHERE OwnerId = @OwnerId";
        con.Open(); // Ensure the connection is open
        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            cmd.Parameters.AddWithValue("@OwnerId", OwnerId);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    RestaurantDTO restaurant = new RestaurantDTO()
                    {
                        RID = reader.GetInt32(0), // Ensure correct type casting
                        Name = reader.GetString(1),
                        Location = reader.GetString(2),
                        OwnerId = reader.GetInt32(3) // Ensure correct type casting
                    };
                    restaurants.Add(restaurant);
                }
            }
        }
        con.Close(); // Ensure the connection is closed
        return restaurants;
    }





    public bool AddMenuItem(MenuItemDTO itm)
    {
        try
        {
            string query = "INSERT INTO MenuItems (MenuName, UnitPrice, FoodType, RestaurantId) VALUES (@MenuName, @UnitPrice, @FoodType, @RestaurantId)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@MenuName", itm.MenuName);
            cmd.Parameters.AddWithValue("@UnitPrice", itm.UnitPrice);
            cmd.Parameters.AddWithValue("@FoodType", itm.FoodType);
            cmd.Parameters.AddWithValue("@RestaurantId", itm.RID);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        catch (Exception)
        {
            con.Close();
            return false;
        }
    }

    public bool OrderMenuItem(int OrderID, int MID, int Qty)
    {
        try
        {
            string query = "INSERT INTO OrderItems (OrderId, MenuItemId, Quantity) VALUES (@OrderId, @MenuItemId, @Quantity)";
            SqlCommand cmd = new SqlCommand(query, con, trans);
            cmd.Parameters.AddWithValue("@OrderId", OrderID);
            cmd.Parameters.AddWithValue("@MenuItemId", MID);
            cmd.Parameters.AddWithValue("@Quantity", Qty);

            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool InitOrder(int RID, int UserID, out int NewOrderId)
    {
        NewOrderId = 0;
        try
        {
            string query = "INSERT INTO Orders (UserId, RestaurantId, Status) OUTPUT INSERTED.OrderId VALUES (@UserId, @RestaurantId, @Status)";
            SqlCommand cmd = new SqlCommand(query, con, trans);
            cmd.Parameters.AddWithValue("@UserId", UserID);
            cmd.Parameters.AddWithValue("@RestaurantId", RID);
            cmd.Parameters.AddWithValue("@Status", "PENDING");

            NewOrderId = (int)cmd.ExecuteScalar();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
