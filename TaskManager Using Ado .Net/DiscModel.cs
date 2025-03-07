using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL
{

    internal class DiscModel
    {
        static SqlConnection con;
        public static void main(string[] args)
        {
            try
            {
                string conString = "Data Source=.;Initial Catalog=testdb;Integrated Security=SSPI";
                string strCommand1 = "SELECT * FROM User";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(strCommand1, conString);
                da.Fill(ds, "User");
                DataTable dtUsers = ds.Tables["User"];
            }
            finally
            {

            }
        }
    }
}
