using System;
using System.Data.SqlClient;

public class Database
{
    private readonly string _connectionString;

    public Database(string connectionString)
    {
        _connectionString = connectionString;
    }

    public SqlConnection GetConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public void TestConnection()
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            Console.WriteLine("Successfully connected to the SQL Server.");
        }
    }

    public void CreateUser(string name, string email, string password, string role)
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            var command = new SqlCommand("INSERT INTO Users (Name, Email, Password, Role) VALUES (@Name, @Email, @Password, @Role)", connection);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@Role", role);
            command.ExecuteNonQuery();
        }

        Console.WriteLine("User created successfully.");
    }

    public User Authenticate(string email, string password)
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            var command = new SqlCommand("SELECT * FROM Users WHERE Email = @Email AND Password = @Password", connection);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Password", password);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new User
                    {
                        UId = (long)reader["UId"],
                        Name = (string)reader["Name"],
                        Email = (string)reader["Email"],
                        Password = (string)reader["Password"],
                        Role = (string)reader["Role"]
                    };
                }
            }
        }

        return null;
    }
}
