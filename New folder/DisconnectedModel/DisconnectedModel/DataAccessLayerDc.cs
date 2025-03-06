using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using SQL;

namespace SQL2
{
    public class DataAccessLayerDC
    {
        DataSet dsData = new DataSet();
        string ConString = "Data Source=.;Initial Catalog=tmdb;Integrated Security=SSPI";
        public List<UserDTO> ListUsers()
        {
            string strCommand = "Select * From Users";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);
            List<UserDTO> users = new List<UserDTO>();
            if (dsData.Tables["Users"] != null)
            {
                dsData.Tables["Users"].Clear();
            }
            adptr.Fill(dsData, "Users");

            if (dsData.Tables.Count > 0)
            {
                DataTable dtUser = dsData.Tables["Users"];
                if (dtUser != null)
                {
                    Console.WriteLine($"Total Rows: {dtUser.Rows.Count}");


                    foreach (DataRow row in dtUser.Rows)
                    {
                        UserDTO user = new UserDTO();
                        user.UserId = Convert.ToInt64(row[0]);
                        user.UName = Convert.ToString(row[1]);
                        user.Dept = Convert.ToString(row[2]);
                        user.RoleId = Convert.ToInt64(row[3]);
                        users.Add(user);
                    }
                }
            }
            return users;
        }

        public bool AddUser(UserDTO user)
        {
            string strCommand = "Select * from Users";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);
            adptr.Fill(dsData, "Users");
            DataTable dtUser = dsData.Tables["Users"];
            if (dtUser != null)
            {
                DataRow newRow = dtUser.NewRow();
                newRow[1] = user.UName;
                newRow[2] = user.Dept;
                newRow[3] = user.RoleId;

                dtUser.Rows.Add(newRow);
                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);
                adptr.Update(dsData, "Users");
                return true;
            }
            else return false;
        }

        public bool UpdateUserRole(long UserId, long newRoleId)
        {
            string strCommand = "Select * from Users";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);
            if (dsData.Tables["Users"] != null)
            {
                dsData.Tables["Users"].Clear();
            }
            adptr.Fill(dsData, "Users");
            if (dsData.Tables["Users"] != null && dsData.Tables["Users"].Rows.Count > 0)
            {
                for (int i = 0; i < dsData.Tables["Users"].Rows.Count; i++)
                {
                    if (Convert.ToInt64(dsData.Tables["Users"].Rows[i][0]) == UserId)
                    {
                        DataRow drReq = dsData.Tables["Users"].Rows[i];
                        drReq[3] = newRoleId;
                    }
                }
                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);
                adptr.Update(dsData, "Users");
                return true;
            }
            return false;
        }


        public bool DeleteUser(long UserId)
        {
            string strCommand = "Select * from Users";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);
            if (dsData.Tables["Users"] != null)
            {
                dsData.Tables["Users"].Clear();
            }
            adptr.Fill(dsData, "Users");
            if (dsData.Tables["Users"] != null && dsData.Tables["Users"].Rows.Count > 0)
            {
                var temp = dsData.Tables["Users"].AsEnumerable();
                DataRow res = (from obj in temp where obj.Field<long>(0) == UserId select obj).ToList().FirstOrDefault();
                if (res != null)
                {
                    res.Delete();
                }

                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);
                adptr.Update(dsData, "Users");
                return true;
            }
            return false;


        }
        public List<ProjectDTO> ListProjects()
        {
            string strCommand = "Select * From Projects";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);
            List<ProjectDTO> projects = new List<ProjectDTO>();
            if (dsData.Tables["Projects"] != null)
            {
                dsData.Tables["Projects"].Clear();
            }
            adptr.Fill(dsData, "Projects");

            if (dsData.Tables.Count > 0)
            {
                DataTable dtProject = dsData.Tables["Projects"];
                if (dtProject != null)
                {
                    Console.WriteLine($"Total Rows: {dtProject.Rows.Count}");

                    foreach (DataRow row in dtProject.Rows)
                    {
                        ProjectDTO proj = new ProjectDTO
                        {
                            Projectid = Convert.ToInt64(row[0]),
                            ProjectName = Convert.ToString(row[1]),
                            ProjectManagerid = Convert.ToInt64(row[2]),
                            Status = Convert.ToString(row[3])
                        };
                        projects.Add(proj);
                    }
                }
            }
            return projects;
        }
        public bool AddProject(ProjectDTO project)
        {
            string strCommand = "Select * from Projects";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);
            adptr.Fill(dsData, "Projects");
            DataTable dtUser = dsData.Tables["Projects"];
            if (dtUser != null)
            {
                DataRow newRow = dtUser.NewRow();
                newRow[1] = project.ProjectName;
                newRow[2] = project.ProjectManagerid;
                newRow[3] = project.Status;

                dtUser.Rows.Add(newRow);
                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);
                adptr.Update(dsData, "Projects");
                return true;
            }
            else return false;
        }
    }
}