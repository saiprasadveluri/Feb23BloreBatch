using FoodDelApp.Data;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;

namespace FoodDelApp
{
    public class DataAccessLayer
    {
        const string ConString = "Data Source=.;Initial Catalog=MFoodDelDB;Integrated Security=SSPI; Trust Server Certificate=true;";
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
            if(trans!=null)
            {
                if (commit)
                    trans.Commit();
                else
                    trans.Rollback();

                trans = null;
            }
        }
        public void CloseSession()
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
            cmd.CommandText = $"SELECT * FROM UserInfo where Email='{Email}' AND Password='{Password}'";
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                UserDTO user = new UserDTO();
                user.UserId = reader.GetInt64(0);
                user.Name = reader.GetString(1);
                user.RoleName = reader.GetString(4);
                user.Location = reader.GetString(5);
                reader.Close();
                return user;
            }
            reader.Close();
            return null;
        }

        public bool EditUser(UserDTO user)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = @$"UPDATE USERINFO
                        SET DISPLAYNAME='{user.Name}',
                            ROLENAME='{user.RoleName}',
                            LOCATION='{user.Location}'
                            WHERE USERID={user.UserId}
                        ";
            int RowsEffected=cmd.ExecuteNonQuery();
            return RowsEffected > 0;
        }
        public UserDTO GetUserById(long UserId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT * FROM UserInfo where UserId={UserId}";
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                UserDTO user = new UserDTO()
                {
                    UserId = reader.GetInt64(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    RoleName = reader.GetString(4),
                    Location = reader.GetString(5)
                };
                reader.Close();
                return user;
            }
            reader.Close();
            return null;
        }
        public List<UserDTO> GetAllUsers()
        {
            List<UserDTO> UserList=new List<UserDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT * FROM UserInfo";
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while(reader.Read())
                {
                    UserDTO user = new UserDTO();
                    user.UserId = reader.GetInt64(0);
                    user.Name = reader.GetString(1);
                    user.RoleName = reader.GetString(4);
                    user.Location = reader.GetString(5);
                    
                    UserList.Add(user);
                }
                //return UserList;                
            }
            reader.Close();
            cmd.Clone();
            return UserList;
        }
        public bool AddNewRestaurant(RestaurantDTO restaurant)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO Restaurents (RName,Location,OwnerId) VALUES('{restaurant.Name}','{restaurant.Location}',{restaurant.OwnerId})";
            int RowsEffected=cmd.ExecuteNonQuery();
            if (RowsEffected > 0)
            {
                return true;
            }
            else
                return false;
        }


        public bool DeleteRestaurant(long RID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"DELETE Restaurents WHERE RID={RID}";
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
            cmd.CommandText = $"INSERT INTO UserInfo (DisplayName,Email,Password,RoleName,Location) VALUES('{user.Name}','{user.Email}','{user.Password}','{user.RoleName}','{user.Location}')";
            int RowsEffected = cmd.ExecuteNonQuery();
            if (RowsEffected > 0)
            {
                return true;
            }
            else
                return false;
        }

        public bool DeleteUser(long UserId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"DELETE UserInfo UserId={UserId}";
            int RowsEffected = cmd.ExecuteNonQuery();
            if (RowsEffected > 0)
            {
                return true;
            }
            else
                return false;
        }

        public string GetUserRole(long UserId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"Select RoleName from UserInfo where UserId={UserId}";
            object res=cmd.ExecuteScalar();
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
            List<UserDTO> OwnersList= new List<UserDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT * FROM UserInfo where RoleName='{UserTypeEnum.OWNER}'";
            SqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
               
                UserDTO user = new UserDTO();
                user.UserId = reader.GetInt64(0);
                user.Name = reader.GetString(1);
                user.Location = reader.GetString(5);
                OwnersList.Add(user);
            }
            reader.Close();
            return OwnersList;
        }

        public List<RestaurantDTO> ListRestaurantsByLocation(string UserLocacation)
        {
            List<RestaurantDTO> RstList = new List<RestaurantDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT * FROM Restaurents where Location='{UserLocacation}'";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                RestaurantDTO restaurant = new RestaurantDTO()
                {
                    RID= reader.GetInt64(0),
                    Name= reader.GetString(1),
                };
                RstList.Add(restaurant);
            }
            reader.Close();
            return RstList;
        }


        public List<RestaurantDTO> ListRestaurants()
        {
            List<RestaurantDTO> RstList = new List<RestaurantDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = @$"SELECT R.RID,R.RName,R.Location,U.DisplayName FROM Restaurents R
                                    JOIN UserInfo U on R.OwnerId=U.UserId";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                RestaurantDTO restaurant = new RestaurantDTO()
                {
                    RID = reader.GetInt64(0),
                    Name = reader.GetString(1),
                    Location = reader.GetString(2),
                    OwnerName = reader.GetString(3)
                };
                RstList.Add(restaurant);
            }
            reader.Close();
            cmd.Clone();
            return RstList;
        }
        public List<MenuItemDTO> GetRestaurentMenu(long RID)
        {
            List<MenuItemDTO> menuList = new List<MenuItemDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT * FROM MenuItem where RID={RID}";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                MenuItemDTO mitem = new MenuItemDTO()
                {
                    MID = reader.GetInt64(0),
                    MenuName = reader.GetString(1),
                    
                    UnitPrice = double.Parse(reader.GetDecimal(3).ToString()),

                    FoodType = reader.GetString(4),
                };
                menuList.Add(mitem);
            }
            reader.Close();
            return menuList;
        }
        public List<RestaurantDTO> ListRestaurantsByOwner(long OwnerId)
        {
            List<RestaurantDTO> RstList = new List<RestaurantDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT * FROM Restaurents where OwnerId={OwnerId}";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                RestaurantDTO restaurant = new RestaurantDTO()
                {
                    RID = reader.GetInt64(0),
                    Name = reader.GetString(1),
                    Location = reader.GetString(2),
                };

                RstList.Add(restaurant);

            }
            reader.Close();
            return RstList;
        }

        public bool AddMenuItem(MenuItemDTO itm)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO MenuItem (MenuName,RID,UnitPrice,FoodType) VALUES('{itm.MenuName}',{itm.RID},{itm.UnitPrice},'{itm.FoodType}')";
            int RowsEffected = cmd.ExecuteNonQuery();
            if (RowsEffected > 0)
            {
                return true;
            }
            else
                return false;
        }


        public bool OrderMenuItem(long OrderID,long MID,int Qty)
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

        public bool InitOrder(long RID, long UserID,out long NewOrderId)
        {
            NewOrderId = 0;
            if (trans != null)
            {

                string today = DateTime.Now.ToString("MM-dd-yyyy");

            SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"INSERT INTO Orders (RID,OrderBy,Status,OrderDate) VALUES({RID},{UserID},'{AppConstants.ORDERED}','{today}');SELECT SCOPE_IDENTITY();";
                cmd.Transaction= trans;
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

        public List<OrderDTO> GetOrdersByUser(long UserId)
        {
            List<OrderDTO> OrderList = new ();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = @$"SELECT O.ORDERID,R.RNAME,U.DISPLAYNAME,O.ORDERDATE,O.STATUS
                                FROM ORDERS O 
                                JOIN Restaurents R ON O.RID=R.RID 
                                JOIN USERINFO U ON O.ORDERBY=U.USERID
                                where O.OrderBy={UserId}";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                OrderDTO order = new()
                {
                   OID= reader.GetInt64(0),
                    RestaurantName= reader.GetString(1),
                    OrderedUserName= reader.GetString(2),
                    OrderDate=reader.GetDateTime(3),
                    OrderStatus=reader.GetString(4),
                };

                OrderList.Add(order);

            }
            reader.Close();
            return OrderList;
        }

        public List<OrderDTO> GetOrdersByRestaurant(long RID)
        {
            List<OrderDTO> OrderList = new();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = @$"SELECT O.ORDERID,R.RNAME,U.DISPLAYNAME,O.ORDERDATE,O.STATUS
                                FROM ORDERS O 
                                JOIN Restaurents R ON O.RID=R.RID 
                                JOIN USERINFO U ON O.ORDERBY=U.USERID
                                where O.rid={RID}";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                OrderDTO order = new()
                {
                    OID = reader.GetInt64(0),
                    RestaurantName = reader.GetString(1),
                    OrderedUserName = reader.GetString(2),
                    OrderDate = reader.GetDateTime(3),
                    OrderStatus = reader.GetString(4),
                };

                OrderList.Add(order);

            }
            reader.Close();
            return OrderList;
        }
        public bool UpdateOrderStatus(long OrderID, string Status)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = @$"UPDATE Orders SET STATUS='{Status}' WHERE ORDERID={OrderID}";
            int RE = cmd.ExecuteNonQuery();
            return RE > 0;
        }
            
    }
}
