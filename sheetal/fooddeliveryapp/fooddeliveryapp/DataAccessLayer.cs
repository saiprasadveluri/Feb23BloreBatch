using FoodDelApp.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FoodDelApp
{
    public class DataAccessLayer
    {
        const string ConString = "Data Source=.;Initial Catalog=MFoodDelDB;Integrated Security=SSPI";
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

        public bool AddNewRestaurant(RestaurantDTO restaurant)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO Restaurents (RName,Location,OwnerId) VALUES('{restaurant.Name}','{restaurant.Location}',{restaurant.OwnerId}";
            int RowsEffected = cmd.ExecuteNonQuery();
            if (RowsEffected > 0)
            {
                return true;
            }
            else
                return false;
        }

        public void AddNewUser(UserDTO user)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO UserInfo (DisplayName,Email,Password,RoleName,Location) VALUES('{user.Name}','{user.Email}','{user.Password}','{user.RoleName}','{user.Location}')";
            int RowsEffected = cmd.ExecuteNonQuery();
            
        }

        public string GetUserRole(long UserId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"Select RoleName from UserInfo where UserId={UserId}";
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
            cmd.CommandText = $"SELECT * FROM UserInfo where RoleName='{UserTypeEnum.OWNER}'";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                UserDTO user = new UserDTO();
                user.UserId = reader.GetInt64(0);
                user.Name = reader.GetString(1);
                OwnersList.Add(user);
            }
            reader.Close();
            return OwnersList;
        }

        public List<RestaurantDTO> ListRestaurantsByLocation(string UserLocation)
        {
            List<RestaurantDTO> RstList = new List<RestaurantDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT * FROM Restaurents where Location='{UserLocation}'";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                RestaurantDTO restaurant = new RestaurantDTO()
                {
                    RID = reader.GetInt64(0),
                    Name = reader.GetString(1),
                };
                RstList.Add(restaurant);
            }
            reader.Close();
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

        public bool UpdateUser(UserDTO updatedUser)
        {
            if (trans != null)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Transaction = trans;

                cmd.CommandText = $"UPDATE Users SET " +
                                  $"Name = COALESCE(NULLIF(@Name, ''), Name), " +
                                  $"Password = COALESCE(NULLIF(@Password, ''), Password), " +
                                  $"Location = COALESCE(NULLIF(@Location, ''), Location), " +
                                  $"RoleName = COALESCE(NULLIF(@RoleName, ''), RoleName) " +
                                  $"WHERE UserID = @UserID";

                cmd.Parameters.AddWithValue("@UserID", updatedUser.UserId);
                cmd.Parameters.AddWithValue("@Name", (object)updatedUser.Name ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Password", (object)updatedUser.Password ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Location", (object)updatedUser.Location ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@RoleName", (object)updatedUser.RoleName ?? DBNull.Value);

                return cmd.ExecuteNonQuery() > 0;
            }
            return false;
        }

        public bool UpdateRestaurant(RestaurantDTO updatedRestaurant)
        {
            if (trans != null)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Transaction = trans;

                cmd.CommandText = $"UPDATE Restaurents SET " +
                                  $"RName = COALESCE(NULLIF(@Name, ''), RName), " +
                                  $"Location = COALESCE(NULLIF(@Location, ''), Location) " +
                                  $"WHERE RID = @RID";

                cmd.Parameters.AddWithValue("@RID", updatedRestaurant.RID);
                cmd.Parameters.AddWithValue("@Name", (object)updatedRestaurant.Name ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Location", (object)updatedRestaurant.Location ?? DBNull.Value);

                return cmd.ExecuteNonQuery() > 0;
            }
            return false;
        }

        public bool UpdateMenu(long MID, string newMenuName)
        {
            if (trans != null)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Transaction = trans;

                cmd.CommandText = $"UPDATE MenuItem SET " +
                                  $"MenuName = COALESCE(NULLIF(@MenuName, ''), MenuName) " +
                                  $"WHERE MID = @MID";

                cmd.Parameters.AddWithValue("@MID", MID);
                cmd.Parameters.AddWithValue("@MenuName", (object)newMenuName ?? DBNull.Value);

                return cmd.ExecuteNonQuery() > 0;
            }
            return false;
        }

        public bool UpdateMenuItem(MenuItemDTO updatedItem)
        {
            if (trans != null)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Transaction = trans;

                cmd.CommandText = $"UPDATE MenuItem SET " +
                                  $"MenuName = COALESCE(NULLIF(@MenuName, ''), MenuName), " +
                                  $"UnitPrice = COALESCE(NULLIF(@UnitPrice, 0), UnitPrice), " +
                                  $"FoodType = COALESCE(NULLIF(@FoodType, ''), FoodType) " +
                                  $"WHERE MID = @MID";

                cmd.Parameters.AddWithValue("@MID", updatedItem.MID);
                cmd.Parameters.AddWithValue("@MenuName", (object)updatedItem.MenuName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@UnitPrice", updatedItem.UnitPrice > 0 ? updatedItem.UnitPrice : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FoodType", (object)updatedItem.FoodType ?? DBNull.Value);

                return cmd.ExecuteNonQuery() > 0;
            }
            return false;
        }

        public bool UpdateOrder(long OrderID, string newStatus)
        {
            if (trans != null)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Transaction = trans;

                cmd.CommandText = $"UPDATE Orders SET " +
                                  $"Status = COALESCE(NULLIF(@Status, ''), Status) " +
                                  $"WHERE OrderID = @OrderID";

                cmd.Parameters.AddWithValue("@OrderID", OrderID);
                cmd.Parameters.AddWithValue("@Status", (object)newStatus ?? DBNull.Value);

                return cmd.ExecuteNonQuery() > 0;
            }
            return false;
        }

        public bool DeleteUser(long UserId)
        {
            if (trans != null)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Transaction = trans;
                cmd.CommandText = "DELETE FROM UserInfo WHERE UserId = @UserId";
                cmd.Parameters.AddWithValue("@UserId", UserId);

                return cmd.ExecuteNonQuery() > 0;
            }
            return false;
        }

        public bool DeleteRestaurant(long RestaurantId)
        {
            if (trans != null)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Transaction = trans;
                cmd.CommandText = "DELETE FROM Restaurents WHERE RID = @RestaurantId";
                cmd.Parameters.AddWithValue("@RestaurantId", RestaurantId);

                return cmd.ExecuteNonQuery() > 0;
            }
            return false;
        }
        public bool DeleteOrder(long OrderID)
        {
            if (trans != null)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Transaction = trans;
                cmd.CommandText = "DELETE FROM Orders WHERE OrderID = @OrderID";
                cmd.Parameters.AddWithValue("@OrderID", OrderID);

                return cmd.ExecuteNonQuery() > 0;
            }
            return false;
        }
        public bool DeleteMenuItem(long MenuID)
        {
            if (trans != null)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Transaction = trans;
                cmd.CommandText = "DELETE FROM MenuItem WHERE MID = @MenuID";
                cmd.Parameters.AddWithValue("@MenuID", MenuID);

                return cmd.ExecuteNonQuery() > 0;
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