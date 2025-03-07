using System;
using System.Data.SqlClient;

public class AuthenticationService
{
    private readonly Database _database;

    public AuthenticationService(Database database)
    {
        _database = database;
    }

    public User Authenticate(string email, string password)
    {
        using (var connection = _database.GetConnection())
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
