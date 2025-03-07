using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMangerADO
{
    class DataAccessLayer
    {
        DataSet dsData = new DataSet();
        string ConString = "Data Source=.;Initial Catalog=MTMDB;Integrated Security=SSPI";
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
        public bool UpdateUserRole(long UserId,long newRoleId)
        {
            string strCommand = $"SELECT * FROM USERS WHERE USERID={UserId}";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);
            if (dsData.Tables["Users"] != null)
            {
                dsData.Tables["Users"].Clear();
            }
            adptr.Fill(dsData, "Users");
            if (dsData.Tables ["Users"]!=null&& dsData.Tables["Users"].Rows.Count>0)
            {
                DataRow drReq = dsData.Tables["Users"].Rows[0];
                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);
                adptr.Update(dsData, "users");
                return true;

            }
            return false;
        }
        public bool DelUserRole(long UserId, long newRoleId)
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
        public List<ProjectDTO> ListProjects()
        {
            List<ProjectDTO> projects = new List<ProjectDTO>();
            string strCommand = "SELECT * FROM PROJECT";
            DataSet ds = new DataSet();
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);
            adptr.Fill(ds, "Project");
            if (ds.Tables.Count > 0)
            {
                DataTable dtUser = ds.Tables["Project"];
                if (dtUser != null)
                {
                    for (int i = 0; i < dtUser.Rows.Count; i++)
                    {
                        DataRow drCurr = dtUser.Rows[i];
                        ProjectDTO pro = new ProjectDTO();
                        pro.ProjId = Convert.ToInt64(drCurr[0]);
                        pro.title = drCurr[1].ToString();
                        pro.PM = Convert.ToInt64(drCurr[2]);
                        pro.status = drCurr[3].ToString();
                        projects.Add(pro);
                    }
                }
            }
            return projects;
        }
        public bool AddProject(ProjectDTO inp)
        {
            string strCommand = "SELECT * FROM PROJECT";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);
            adptr.FillSchema(dsData, SchemaType.Source, "Project");
            DataTable dtCurr = dsData.Tables["Project"];
            if (dtCurr != null)
            {
                DataRow drNew = dtCurr.NewRow();
                drNew[1] = inp.title;
                drNew[2] = inp.PM; // Ensure PM is set
                drNew[3] = inp.status; // Ensure status is set
                dtCurr.Rows.Add(drNew);
                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);
                adptr.Update(dsData, "Project");
                return true;
            }
            return false;
        }


    }


    }
    

