using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    public class DataAccessLayer
    {
        SqlConnection con;
        string strCon = "Data Source=.;Initial Catalog=MTMDb;Integrated Security=SSPI";

        public DataAccessLayer()
        {
            con = new SqlConnection(strCon);
        }

        public void OpenConnection()
        {
            con.Open();
        }

        public void CloseConnection()
        {
            con.Close();
        }

        public bool AddUser(UserDTO inp)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO USERS(Name,Dept,RoleId,email,password) VALUES('{inp.Name}','{inp.Department}',{inp.RoleId},'{inp.Email}',{inp.Password}')";
            int RowsEffected = cmd.ExecuteNonQuery();//Executing command
            if (RowsEffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
