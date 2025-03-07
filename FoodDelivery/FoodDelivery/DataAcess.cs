using System;
using System.Data.SqlClient;

namespace FoodDelivery.DataAccess
{
    public class DataAccess
    {
        private static SqlConnection con;

        // Establish a connection to the database
        public static void OpenConnection()
        {
            string strCon = "Data Source=.;Initial Catalog=FD;Integrated Security=SSPI";
            con = new SqlConnection(strCon);
            con.Open();
        }

        // Close the database connection
        public static void CloseConnection()
        {
            con.Close();
        }

        // Execute a query that doesn't return data (e.g., INSERT, UPDATE, DELETE)
        public static int ExecuteNonQuery(string query)
        {
            SqlCommand cmd = new SqlCommand(query, con);
            return cmd.ExecuteNonQuery();
        }

        // Execute a query that returns a single value (e.g., COUNT, SUM)
        public static object ExecuteScalar(string query)
        {
            SqlCommand cmd = new SqlCommand(query, con);
            return cmd.ExecuteScalar();
        }

        // Execute a query that returns a data reader (e.g., SELECT)
        public static SqlDataReader ExecuteReader(string query)
        {
            SqlCommand cmd = new SqlCommand(query, con);
            return cmd.ExecuteReader();
        }
    }
}
