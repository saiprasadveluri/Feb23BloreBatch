
using FoodDeliveryApp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FoodDelApp
{
    public class DataAccessLayer
    {
        const string ConString = "Data Source=.;Initial Catalog=fooddelivery;Integrated Security=SSPI";
        SqlConnection con = null;
        SqlTransaction trans = null;
        public DataAccessLayer()
        {
            con = new SqlConnection(ConString);
            con.Open();
        }

        public void BeginTrans()
        {
            trans = con.BeginTransaction();
        }

        public void EndTransaction(bool commit)
        {
            if (trans != null)
            {
                if (commit)
                    trans.Commit();
                else
                    trans.Rollback();

                trans = null;
            }
        }
        public void CloseApp()
        {
            if (con != null)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        public UserDTO Login(string Email, string Password)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT * FROM Users where Email='{Email}' AND Password='{Password}'";
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                UserDTO user = new UserDTO();
                user.Userid = reader.GetInt64(0);
                user.Name = reader.GetString(1);
                user.RoleName = reader.GetString(4);
                user.Location = reader.GetString(5);
                reader.Close();
                return user;
            }
            reader.Close();
            return null;
        }

        public bool AddNewRestaurant(ResturantDTO resturant)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO Resturant (Name,Location,Ownerid) VALUES('{resturant.Name}','{resturant.Location}',{resturant.Ownerid}";
            int RowsEffected = cmd.ExecuteNonQuery();
            if (RowsEffected > 0)
            {
                return true;
            }
            else
                return false;
        }

        public bool AddNewUser(UserDTO user)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO Users (Name,Email,Password,RoleName,Location) VALUES('{user.Name}','{user.Email}','{user.Password}','{user.RoleName}','{user.Location}')";
            int RowsEffected = cmd.ExecuteNonQuery();
            if (RowsEffected > 0)
            {
                return true;
            }
            else
                return false;
        }

        public string GetUserRole(long Userid)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"Select RoleName from Users where Userid={Userid}";
            object res = cmd.ExecuteScalar();
            if (res != null)
            {
                return res.ToString();
            }
            else
            {
                return null;
            }
        }

        public List<UserDTO> GetRestaurantOwnersList()
        {
            List<UserDTO> OwnersList = new List<UserDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT * FROM Users where RoleName='{UserTypeEnum.OWNER}'";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                UserDTO user = new UserDTO();
                user.Userid = reader.GetInt64(0);
                user.Name = reader.GetString(1);
                OwnersList.Add(user);
            }
            reader.Close();
            return OwnersList;
        }

        public List<ResturantDTO> ListRestaurantsByLocation(string UserLocacation)
        {
            List<ResturantDTO> RstList = new List<ResturantDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT * FROM Resturant where Location='{UserLocacation}'";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ResturantDTO restaurant = new ResturantDTO()
                {
                    Rid = reader.GetInt64(0),
                    Name = reader.GetString(1),
                };
                RstList.Add(restaurant);
            }
            reader.Close();
            return RstList;
        }

        public List<MenuDTO> GetRestaurentMenu(long RID)
        {
            List<MenuDTO> menuList = new List<MenuDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT * FROM MenuItem where RID={RID}";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                MenuDTO mitem = new MenuDTO()
                {
                    MenuId = reader.GetInt64(0),
                    Menuname = reader.GetString(1),

                    UnitPrice = (decimal)double.Parse(reader.GetDecimal(3).ToString()),

                    FoodType = reader.GetString(4),
                };
                menuList.Add(mitem);
            }
            reader.Close();
            return menuList;
        }
        public List<ResturantDTO> ListRestaurantsByOwner(long OwnerId)
        {
            List<ResturantDTO> RstList = new List<ResturantDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT * FROM Restaurents where OwnerId={OwnerId}";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ResturantDTO restaurant = new ResturantDTO()
                {
                    Rid = reader.GetInt64(0),
                    Name = reader.GetString(1),
                };

                RstList.Add(restaurant);

            }
            reader.Close();
            return RstList;
        }

        public bool AddMenuItem(MenuDTO itm)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO MenuItem (MenuName,RID,UnitPrice,FoodType) VALUES('{itm.Menuname}',{itm.Rid},{itm.UnitPrice},'{itm.FoodType}')";
            int RowsEffected = cmd.ExecuteNonQuery();
            if (RowsEffected > 0)
            {
                return true;
            }
            else
                return false;
        }


        public bool OrderMenuItem(long OrderID, long MID, int Qty)
        {
            if (trans != null)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"INSERT INTO OrderLineItems (ORDERID,MenuId,Qty) VALUES({OrderID},{MID},{Qty})";
                cmd.Transaction = trans;
                int RowsEffected = cmd.ExecuteNonQuery();
                if (RowsEffected > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            return false;
        }

        public bool InitOrder(long RID, long UserID, out long NewOrderId)
        {
            NewOrderId = 0;
            if (trans != null)
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"INSERT INTO Orders (RID,OrderBy,Status,OrderDate) VALUES({RID},{UserID},'ORDERED','{DateTime.Now}');SELECT SCOPE_IDENTITY();";
                cmd.Transaction = trans;
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                NewOrderId = 0;
                if (sqlDataReader.Read())
                {
                    NewOrderId = Convert.ToInt64(sqlDataReader[0]);
                }
                sqlDataReader.Close();
                return NewOrderId != 0;
            }
            return false;
        }
    }
}