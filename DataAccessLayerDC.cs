using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TaskManagerADO
{
    class DataAccessLayerDC //DC means Disconnected
    {
        DataSet dsData = new DataSet();

        string ConString = "Data Source=.;Initial Catalog=MTMDB;Integrated Security=SSPI";

        public string GetUserJson()
        {
            List<UserDTO> temp = ListUsers();
            return JsonConvert.SerializeObject(temp);
            
        }

        public string GeUserXml()
        {
            List<UserDTO> users = ListUsers();
            return dsData.GetXml();
        }

        public List<UserDTO> ListUsers()
        {
            List<UserDTO> users = new List<UserDTO>();
            if (dsData.Tables["Users"] != null)
            {
                dsData.Tables["Users"].Clear();
            }
            string strCommand = "SELECT * FROM USERS";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);
            adptr.Fill(dsData, "Users");

            if (dsData.Tables.Count > 0)
            {
                DataTable dtUser = dsData.Tables["Users"];
                if (dtUser != null)
                {
                    for (int i = 0; i < dtUser.Rows.Count; i++)
                    {
                        DataRow drCur = dtUser.Rows[i];

                        UserDTO usr = new UserDTO();
                        usr.UserId = Convert.ToInt64(dtUser.Rows[i][0]);
                        usr.Name = dtUser.Rows[i][1].ToString();
                        usr.Dept = dtUser.Rows[i][2].ToString();
                        usr.RoleId = Convert.ToInt64(dtUser.Rows[i][3]);
                        users.Add(usr);
                    }

                }

            }
            return users;

        }

        public bool AddUser(UserDTO inp)
        {
            string strCommand = "Select * from Users";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);
            adptr.FillSchema(dsData, SchemaType.Source, "Users");
            DataTable dtCur = dsData.Tables["Users"];
            if (dtCur != null)
            {
                DataRow drNew = dtCur.NewRow();
                drNew[1] = inp.Name;
                drNew[2] = inp.Dept;
                drNew[3] = inp.RoleId;
                dtCur.Rows.Add(drNew);
                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);
                adptr.Update(dsData, "Users");
                return true;
            }

            return false;

        }
        public bool UpdateUserRole(long UserId, long newRoleId)
        {
            string strCommand =$"Select * from Users where UserId={UserId}";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);
            if (dsData.Tables["Users"] != null)
            {
                dsData.Tables["Users"].Clear();
            }
            adptr.Fill(dsData, "Users");
            if (dsData.Tables["Users"] != null && dsData.Tables["Users"].Rows.Count > 0) {
                DataRow drReq = dsData.Tables["Users"].Rows[0];
                drReq[3] = newRoleId;
                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);
                adptr.Update(dsData, "Users");
                return true;

            }
            return false;
        }


        public bool DeleteUser(long UserId)
        {
            string strCommand = $"Select * from Users where UserId={UserId}";
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
                adptr.Update(dsData, "Users");
                return true;

            }
            return false;
        }

        public bool AddProject(ProDTO inp)
        {
            string strCommand = "Select * from Project";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);
            adptr.FillSchema(dsData, SchemaType.Source, "Project");
            DataTable dtCur = dsData.Tables["Project"];
            if (dtCur != null)
            {
                DataRow drNew = dtCur.NewRow();
                drNew[1] = inp.PMID;
                drNew[2] = inp.PTITLE;
                drNew[3] = inp.PSTATUS;
                dtCur.Rows.Add(drNew);
                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);
                adptr.Update(dsData, "Project");
                return true;
            }

            return false;

        }

        public List<ProDTO> ListProjects()
        {
            List<ProDTO> pro = new List<ProDTO>();
            if (dsData.Tables["Project"] != null)
            {
                dsData.Tables["Project"].Clear();
            }
            string strCommand = "SELECT * FROM PROJECT";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);
            adptr.Fill(dsData, "Project");

            if (dsData.Tables.Count > 0)
            {
                DataTable dtPro = dsData.Tables["Project"];
                if (dtPro != null)
                {
                    for (int i = 0; i < dtPro.Rows.Count; i++)
                    {
                        DataRow drCur = dtPro.Rows[i];

                        ProDTO proj = new ProDTO();
                        proj.ProjectId = Convert.ToInt64(dtPro.Rows[i][0]);
                        proj.PMID = Convert.ToInt64(dtPro.Rows[i][1]);
                        proj.PTITLE = dtPro.Rows[i][2].ToString();
                        proj.PSTATUS = dtPro.Rows[i][3].ToString();
                        pro.Add(proj);
                    }

                }

            }
            return pro;

        }








    }
    }

