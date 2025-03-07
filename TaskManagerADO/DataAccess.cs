using System;

using System.Collections.Generic;

using System.Data;

using System.Data.SqlClient;

using System.Linq;

using System.Runtime.Remoting.Lifetime;

using System.Security.Cryptography.X509Certificates;

using System.Text;

using System.Threading.Tasks;
using TaskMangerADO;

namespace TodaysProg

{

    public class DataAccessLayer

    {

        DataSet dsData = new DataSet();

        String ConString = "Data Source=.;Initial Catalog=MTMDB;Integrated Security=SSPI";

        public string GetUserXml()

        {

            List<UserDTO> temp = ListUsers();

            return dsData.GetXml();

        }

        public List<UserDTO> ListUsers()

        {

            List<UserDTO> users = new List<UserDTO>();

            string strCommand = "SELECT * FROM USERS";

            DataSet ds = new DataSet();

            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);

            adptr.Fill(ds, "Users");

            if (dsData.Tables.Count > 0)

            {

                DataTable dtUser = dsData.Tables["Users"];

                if (dtUser != null)

                {

                    for (int i = 0; i < dtUser.Rows.Count; i++)

                    {

                        DataRow drCurr = dtUser.Rows[i];

                        UserDTO usr = new UserDTO();

                        usr.UserId = Convert.ToInt64(dtUser.Rows[i][0]);

                        usr.Name = dtUser.Rows[i][1].ToString();

                        usr.Department = dtUser.Rows[i][2].ToString();

                        usr.RoleId = Convert.ToInt64(dtUser.Rows[i][3]);

                        users.Add(usr);

                    }

                }

            }

            return users;

        }

        public bool AddUser(UserDTO inp)

        {

            string strCommand = "SELECT * FROM USERS";

            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);

            adptr.FillSchema(dsData, SchemaType.Source, "Users");

            DataTable dtCurr = dsData.Tables["Users"];

            if (dtCurr != null)

            {

                DataRow drNew = dtCurr.NewRow();

                drNew[1] = inp.Name;

                drNew[2] = inp.Department;

                drNew[3] = inp.RoleId;

                dtCurr.Rows.Add(drNew);

                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);

                adptr.Update(dsData, "Users");

                return true;


            }

            return false;

        }

        public bool UpdateUserRole(long UserId, long newRoleId)

        {

            string strCommand = $"SELECT * FROM USERS WHERE USERID={UserId}";

            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);

            if (dsData.Tables["Users"] != null)

            {

                dsData.Tables["Users"].Clear();

            }

            adptr.Fill(dsData, "Users");

            if (dsData.Tables["Users"] != null && dsData.Tables["Users"].Rows.Count > 0)

            {

                DataRow drReq = dsData.Tables["Users"].Rows[0];

                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);

                adptr.Update(dsData, "users");

                return true;

            }

            return false;

        }

        public bool delUserRole(long UserId, long newRoleId)

        {

            string strCommand = $"SELECT * FROM USERS WHERE USERID={UserId}";

            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);

            if (dsData.Tables["Users"] != null)

            {

                dsData.Tables["Users"].Clear();

            }

            adptr.Fill(dsData, "Users");

            if (dsData.Tables["Users"] != null && dsData.Tables["Users"].Rows.Count > 0)

            {

                DataRow drReq = dsData.Tables["Users"].Rows[0];

                drReq.Delete();

                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);

                adptr.Update(dsData, "users");

                return true;

            }

            return false;

        }


    }

}




//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TaskManagerADO
//{
//    class DataAccess
//    {
//        public class DataAccessLayerDC
//        {
//            DataSet dsData = new DataSet();
//            string strCon = "Data Source=;Initial Catalog=MTMDb;Integrated Security=SSPI";
//            public List<UserDTO> ListUsers()
//            {
//                string strcommand = "Select * from users";
//                SqlDataAdapter adptr = new SqlDataAdapter(strcommand, ConString);
//                adptr.Fill(dsData, "Users");

//                if(dsData.Tables.Count>0)
//                {
//                    DataTable dtUser = dsData.Tables["Users"];
//                    if(dtUser!=null)
//                    {
//                        for (int i = 0; i < dtUser.Rows.Count; i++)
//                        {
//                            DataRow drCur = dtUser.Rows[i];
//                            UserDTO usr = new UserDTO();
//                            usr.UserId = Convert.ToInt64(dtUser.Rows[i][0]);
//                            usr.Name = dtUser.Rows[i][1].ToString();
//                            usr.Department = dtUser.Rows[i][2].ToString();
//                            usr.RoleId = Convert.ToInt64(dtUser.Rows[i][3]);
//                            users.Add(usr);

//                        }
//                    }
//                }
//                return users;

//            }

//            public bool AddUser(UserDTO inp)
//            {
//                string strCommand = "Select * from Users";
//                SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);
//                adptr.FillSchema(dsData, SchemaType.Source, "Users");
//                DataTable dtCur = dsData.Tables["Users"];
//                if(dtCur != null)
//                {
//                    DataRow drNew = dtCur.NewRow[];
//                    drNew[]
//                }


//            }
//            public bool UpdateUserRole(long UserId,long newRoleId)
//            {
//                string strcommand = $"Select * from Users where UserI={UserId}";
//                SqlDataAdapter adptr = new SqlDataAdapter(strcommand, ConString);
//                if (dsData.Tables["Users"]!=null)
//                {
//                    dsData.Tables["Users"].Clear();
//                }
//                adptr.Fill(dsData, "Users");
//                if (dsData.Tables["Users"]!=null && dsData.Tables["Users"].Rows.Count>0)
//                {
//                    DataRow 
//                }
//            }


//        }
//    }
//}
