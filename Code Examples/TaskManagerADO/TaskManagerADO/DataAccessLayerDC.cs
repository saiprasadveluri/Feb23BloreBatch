using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace TaskManagerADO
{
    public class DataAccessLayerDC
    {
        DataSet dsData = new DataSet();
        string ConString = "Data Source=.;Initial Catalog=MTMDb;Integrated Security=SSPI";

        public string GetUserJson()
        {
            List<UserDTO> temp = ListUsers();
            return JsonConvert.SerializeObject(temp);            
        }
        public string GetUserXml()
        {
            List<UserDTO> temp = ListUsers();
            
                
            return dsData.GetXml();
        }
        public List<UserDTO> ListUsers()
        {   
            List <UserDTO> users = new List <UserDTO>();

            if (dsData.Tables["Users"]!=null)
            {
                dsData.Tables["Users"].Clear();
            }

            string strCommnad = "Select * from Users";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommnad, ConString);
            adptr.Fill(dsData, "Users");
            
            if(dsData.Tables.Count>0)
            {
                DataTable dtUser = dsData.Tables["Users"];
                if(dtUser!=null)
                {
                    for (int i = 0; i<dtUser.Rows.Count;++i)
                    {
                        DataRow drCur = dtUser.Rows[i];
                        UserDTO usr= new UserDTO();
                        usr.UserId = Convert.ToInt64(dtUser.Rows[i][0]);
                        usr.Name = dtUser.Rows[i][1].ToString();
                        usr.Department = dtUser.Rows[i][2].ToString();
                        usr.RoleId= Convert.ToInt64(dtUser.Rows[i][3]);
                        users.Add(usr);
                    }
                }
            }
            return users;
        }

        public bool AddUser(UserDTO inp)
        {
            string strCommnad = "Select * from Users";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommnad, ConString);
            adptr.FillSchema(dsData, SchemaType.Source, "Users");
            DataTable dtCur = dsData.Tables["Users"];
            if (dtCur != null)
            {
                DataRow drNew = dtCur.NewRow();
                drNew[1] = inp.Name;
                drNew[2] = inp.Department;
                drNew[3]=inp.RoleId;

                dtCur.Rows.Add(drNew);

                SqlCommandBuilder builder= new SqlCommandBuilder(adptr);
                adptr.Update(dsData, "Users");
                return true;
            }
            return false;
        }

        public bool UpdateUserRole(long UserId,long newRoleId)
        {
            string strCommnad = $"Select * from Users where UserId={UserId}";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommnad, ConString);
            if (dsData.Tables["Users"] != null)
            {
                dsData.Tables["Users"].Clear();
            }
            adptr.Fill(dsData, "Users");
            if (dsData.Tables["Users"]!=null && dsData.Tables["Users"].Rows.Count>0)
            {
                DataRow drReq=dsData.Tables["Users"].Rows[0];
                drReq[3] = newRoleId;
                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);
                adptr.Update(dsData, "Users");
                return true;
            }
            return false;
        }

        public bool DleteUser(long UserId)
        {
            string strCommnad = $"Select * from Users where UserId={UserId}";
            
            SqlDataAdapter adptr = new SqlDataAdapter(strCommnad, ConString);
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
                adptr.Update(dsData, "Users");
                return true;
            }
            return false;
        }
     }
}
