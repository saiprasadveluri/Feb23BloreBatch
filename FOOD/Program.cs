using System;
using System.Data.SqlClient;

class Program
{
    static void Main(string[] args)
    {
        var database = new Database("Data Source=.;Initial Catalog=FOODDELIVERY;Integrated Security=SSPI;");

        
        try
        {
            database.TestConnection();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to connect to the SQL Server: {ex.Message}");
            return;
        }

        
        User user = null;
        while (user == null)
        {
            Console.WriteLine("Enter your email:");
            var email = Console.ReadLine();

            Console.WriteLine("Enter your password:");
            var password = Console.ReadLine();

            user = database.Authenticate(email, password);

            if (user == null)
            {
                Console.WriteLine("Invalid credentials. Do you want to create a new user? (yes/no)");
                var response = Console.ReadLine();
                if (response?.ToLower() == "yes")
                {
                    CreateUser(database, email, password);
                }
            }
        }

        Console.WriteLine($"Welcome, {user.Name}!");

        bool continueRunning = true;
        while (continueRunning)
        {
            switch (user.Role.ToUpper())
            {
                case "ADMIN":
                    ShowAdminFunctions(database);
                    break;
                case "OWNER":
                    ShowOwnerFunctions(database);
                    break;
                case "CUSTOMER":
                    ShowUserFunctions(database);
                    break;
                default:
                    Console.WriteLine("Unknown role.");
                    break;
            }

            Console.WriteLine("Do you want to perform another function? (yes/no)");
            var continueResponse = Console.ReadLine();
            continueRunning = continueResponse?.ToLower() == "yes";
        }
    }

    static void CreateUser(Database database, string email, string password)
    {
        Console.WriteLine("Enter your name:");
        var name = Console.ReadLine();

        Console.WriteLine("Enter your role (Admin/Owner/Customer):");
        var role = Console.ReadLine();

        database.CreateUser(name, email, password, role);
    }

    static void ShowAdminFunctions(Database database)
    {
        Console.WriteLine("Admin Functions:");
        Console.WriteLine("1. Add new restaurant");
        Console.WriteLine("2. Remove restaurant");
        Console.WriteLine("3. Assign owner to restaurant");

        Console.WriteLine("Choose the function you want to perform:");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                AddRestaurant(database);
                break;
            case "2":
                RemoveRestaurant(database);
                break;
            case "3":
                AssignOwnerToRestaurant(database);
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    static void AddRestaurant(Database database)
    {
        Console.WriteLine("Enter the restaurant name:");
        var name = Console.ReadLine();

        Console.WriteLine("Enter the restaurant location:");
        var location = Console.ReadLine();

        Console.WriteLine("Enter the user ID:");
        var userId = Console.ReadLine();

        using (var connection = database.GetConnection())
        {
            connection.Open();
            var command = new SqlCommand("INSERT INTO Restaurants (RName, RLocation, UserId) VALUES (@Name, @Location, @UserId)", connection);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Location", location);
            command.Parameters.AddWithValue("@UserId", userId);

            try
            {
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Restaurant added successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to add the restaurant.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    static void RemoveRestaurant(Database database)
    {
        Console.WriteLine("Enter the restaurant ID to remove:");
        var restaurantId = Console.ReadLine();

        using (var connection = database.GetConnection())
        {
            connection.Open();
            var command = new SqlCommand("DELETE FROM Restaurants WHERE RId = @RestaurantId", connection);
            command.Parameters.AddWithValue("@RestaurantId", restaurantId);
            command.ExecuteNonQuery();
        }

        Console.WriteLine("Restaurant removed successfully.");
    }

    static void AssignOwnerToRestaurant(Database database)
    {
        Console.WriteLine("Enter the restaurant ID:");
        var restaurantId = Console.ReadLine();

        Console.WriteLine("Enter the owner ID:");
        var ownerId = Console.ReadLine();

        using (var connection = database.GetConnection())
        {
            connection.Open();
            var command = new SqlCommand("UPDATE Restaurants SET UserId = @UserId WHERE RId = @RestaurantId", connection);
            command.Parameters.AddWithValue("@UserId", ownerId);
            command.Parameters.AddWithValue("@RestaurantId", restaurantId);
            command.ExecuteNonQuery();
        }

        Console.WriteLine("Owner assigned to restaurant successfully.");
    }

    static void ShowOwnerFunctions(Database database)
    {
        Console.WriteLine("Owner Functions:");
        Console.WriteLine("1. Add menu items to restaurant");

        Console.WriteLine("Choose the function you want to perform:");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                AddMenuItem(database);
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    static void AddMenuItem(Database database)
    {
        Console.WriteLine("Enter the restaurant ID:");
        var restaurantId = Console.ReadLine();

        Console.WriteLine("Enter the menu item name:");
        var itemName = Console.ReadLine();

        Console.WriteLine("Enter the menu item price:");
        var itemPrice = Console.ReadLine();

        Console.WriteLine("Enter the menu item category:");
        var itemCategory = Console.ReadLine();

        using (var connection = database.GetConnection())
        {
            connection.Open();
            var command = new SqlCommand("INSERT INTO MenuItems (ResId, MName, Price, Category) VALUES (@ResId, @MName, @Price, @Category)", connection);
            command.Parameters.AddWithValue("@ResId", restaurantId);
            command.Parameters.AddWithValue("@MName", itemName);
            command.Parameters.AddWithValue("@Price", itemPrice);
            command.Parameters.AddWithValue("@Category", itemCategory);

            try
            {
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Menu item added successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to add the menu item.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }




    static void ShowUserFunctions(Database database)
    {
        Console.WriteLine("Customer Functions:");
        Console.WriteLine("1. Search for restaurant based on location");
        Console.WriteLine("2. Filter items based on food preferences/dish");
        Console.WriteLine("3. Place order for given items for a given restaurant");

        Console.WriteLine("Choose the function you want to perform:");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                SearchRestaurant(database);
                break;
            case "2":
                FilterItems(database);
                break;
            case "3":
                PlaceOrder(database);
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    static void SearchRestaurant(Database database)
    {
        Console.WriteLine("Enter the location to search for restaurants:");
        var location = Console.ReadLine();

        using (var connection = database.GetConnection())
        {
            connection.Open();
            var command = new SqlCommand("SELECT * FROM Restaurants WHERE RLocation = @Location", connection);
            command.Parameters.AddWithValue("@Location", location);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Restaurant: {reader["RName"]}, Location: {reader["RLocation"]}");
                }
            }
        }
    }

    static void FilterItems(Database database)
    {
        Console.WriteLine("Enter your food preference or dish to filter items:");
        var preference = Console.ReadLine();

        using (var connection = database.GetConnection())
        {
            connection.Open();
            var command = new SqlCommand("SELECT * FROM MenuItems WHERE MName LIKE @Preference", connection);
            command.Parameters.AddWithValue("@Preference", "%" + preference + "%");

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Item: {reader["MName"]}, Price: {reader["Price"]}, Restaurant: {reader["ResId"]}");
                }
            }
        }
    }

    static void PlaceOrder(Database database)
    {
        Console.WriteLine("Enter the restaurant ID where you want to place the order:");
        var restaurantId = Console.ReadLine();

        Console.WriteLine("Enter the user ID:");
        var userId = Console.ReadLine();

        Console.WriteLine("Enter the menu item IDs (comma-separated) you want to order:");
        var itemIds = Console.ReadLine().Split(',');

       
        var total =100.0; 
        var status = "Pending"; 

        using (var connection = database.GetConnection())
        {
            connection.Open();
            var command = new SqlCommand("INSERT INTO Orders (ResId, UserId, ItemIds, Total, Status) VALUES (@ResId, @UserId, @ItemIds, @Total, @Status)", connection);
            command.Parameters.AddWithValue("@ResId", restaurantId);
            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@ItemIds", string.Join(",", itemIds));
            command.Parameters.AddWithValue("@Total", total);
            command.Parameters.AddWithValue("@Status", status);

            try
            {
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Order placed successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to place the order.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }


}
