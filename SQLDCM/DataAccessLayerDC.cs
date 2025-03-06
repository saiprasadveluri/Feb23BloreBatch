using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQL;
using Newtonsoft.Json;

namespace SQL2
{
    public class DataAccessLayerDC
    {
        DataSet dsData = new DataSet();
        string ConString = "Data Source=.;Initial Catalog=testdb;Integrated Security=SSPI";

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
                if(dtUser != null)
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
                        ProjectDTO project = new ProjectDTO();
                        project.ProjectId = Convert.ToInt64(row[0]);
                        project.ProjectName = Convert.ToString(row[1]);
                        project.ProjectManagerId = Convert.ToInt64(row[2]);
                        project.PStatus = Convert.ToString(row[3]);
                        projects.Add(project);
                    }
                }
            }
            return projects;
        }

        public List<TaskDTO> ListTasks()
        {
            string strCommand = "Select * From Tasks";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);
            List<TaskDTO> tasks = new List<TaskDTO>();
            if (dsData.Tables["Tasks"] != null)
            {
                dsData.Tables["Tasks"].Clear();
            }
            adptr.Fill(dsData, "Tasks");
            if (dsData.Tables.Count > 0)
            {
                DataTable dtTask = dsData.Tables["Tasks"];
                if (dtTask != null)
                {
                    Console.WriteLine($"Total Rows: {dtTask.Rows.Count}");
                    foreach (DataRow row in dtTask.Rows)
                    {
                        TaskDTO task = new TaskDTO();
                        task.TaskId = Convert.ToInt64(row[0]);
                        task.Title = Convert.ToString(row[1]);
                        task.TType = Convert.ToInt32(row[2]);
                        task.ProjectId = Convert.ToInt64(row[3]);
                        task.AssignedTo = Convert.ToInt64(row[4]);
                        tasks.Add(task);
                    }
                }
            }
            return tasks;
        }

        public List<CommentDTO> ListComments()
        {
            string strCommand = "Select * From Comments";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);
            List<CommentDTO> comments = new List<CommentDTO>();
            if (dsData.Tables["Comments"] != null)
            {
                dsData.Tables["Comments"].Clear();
            }
            adptr.Fill(dsData, "Comments");
            if (dsData.Tables.Count > 0)
            {
                DataTable dtComment = dsData.Tables["Comments"];
                if (dtComment != null)
                {
                    Console.WriteLine($"Total Rows: {dtComment.Rows.Count}");
                    foreach (DataRow row in dtComment.Rows)
                    {
                        CommentDTO comment = new CommentDTO();
                        comment.CommentId = Convert.ToInt64(row[0]);
                        comment.Title = Convert.ToString(row[1]);
                        comment.CommentText = Convert.ToString(row[2]);
                        comment.TaskId = Convert.ToInt64(row[3]);
                        comment.CommentedBy = Convert.ToInt64(row[4]);
                        comments.Add(comment);
                    }
                }
            }
            return comments;
        }

        public bool AddUser(UserDTO user)
        {
            string strCommand = "Select * from Users";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);
            if (dsData.Tables["Users"] != null)
            {
                dsData.Tables["Users"].Clear();
            }
            adptr.Fill(dsData, "Users");
            DataTable dtUser = dsData.Tables["Users"];
            if (dtUser != null) {
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

        public bool AddProject(ProjectDTO project)
        {
            string strCommand = "Select * from Projects";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);
            if (dsData.Tables["Projects"] != null)
            {
                dsData.Tables["Projects"].Clear();
            }
            adptr.Fill(dsData, "Projects");
            DataTable dtProject = dsData.Tables["Projects"];
            if (dtProject != null)
            {
                DataRow newRow = dtProject.NewRow();
                newRow[1] = project.ProjectName;
                newRow[2] = project.ProjectManagerId;
                newRow[3] = project.PStatus;
                dtProject.Rows.Add(newRow);
                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);
                adptr.Update(dsData, "Projects");
                return true;
            }
            else return false;
        }

        public bool AddTask(TaskDTO task)
        {
            string strCommand = "Select * from Tasks";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);
            if (dsData.Tables["Tasks"] != null)
            {
                dsData.Tables["Tasks"].Clear();
            }
            adptr.Fill(dsData, "Tasks");
            DataTable dtTask = dsData.Tables["Tasks"];
            if (dtTask != null)
            {
                DataRow newRow = dtTask.NewRow();
                newRow[1] = task.Title;
                newRow[2] = task.TType;
                newRow[3] = task.ProjectId;
                newRow[4] = task.AssignedTo;
                dtTask.Rows.Add(newRow);
                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);
                adptr.Update(dsData, "Tasks");
                return true;
            }
            else return false;
        }

        public bool AddComment(CommentDTO comment)
        {
            string strCommand = "Select * from Comments";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);
            if (dsData.Tables["Comments"] != null)
            {
                dsData.Tables["Comments"].Clear();
            }
            adptr.Fill(dsData, "Comments");
            DataTable dtComment = dsData.Tables["Comments"];
            if (dtComment != null)
            {
                DataRow newRow = dtComment.NewRow();
                newRow[1] = comment.Title;
                newRow[2] = comment.CommentText;
                newRow[3] = comment.TaskId;
                newRow[4] = comment.CommentedBy;
                dtComment.Rows.Add(newRow);
                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);
                adptr.Update(dsData, "Comments");
                return true;
            }
            else return false;
        }

        public bool UpdateUserRole(long UserId,long newRoleId)
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
                for (int i = 0; i < dsData.Tables["Users"].Rows.Count; i++) {

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

        public bool UpdateProjStatus(long proid,string stat)
        {
            string strCommand = $"Select 8 from Projects where ProjectId = {proid} ";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);
            if (dsData.Tables["Projects"] != null)
            {
                dsData.Tables["Projects"].Clear();
            }
            adptr.Fill(dsData, "Projects");
            if (dsData.Tables["Projects"] != null && dsData.Tables["Projects"].Rows.Count > 0)
            {
                DataRow drReq = dsData.Tables["Projects"].Rows[0];

                drReq[3] = stat;
                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);
                adptr.Update(dsData, "Projects");
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

        public bool DeleteProject(long ProjectId)
        {
            string strCommand = "Select * from Projects";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);
            if (dsData.Tables["Projects"] != null)
            {
                dsData.Tables["Projects"].Clear();
            }
            adptr.Fill(dsData, "Projects");
            if (dsData.Tables["Projects"] != null && dsData.Tables["Projects"].Rows.Count > 0)
            {
                var temp = dsData.Tables["Projects"].AsEnumerable();
                DataRow res = (from obj in temp where obj.Field<long>(0) == ProjectId select obj).ToList().FirstOrDefault();
                if (res != null)
                {
                    res.Delete();
                }
                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);
                adptr.Update(dsData, "Projects");
                return true;
            }
            return false;
        }

        public bool DeleteTask(long TaskId)
        {
            string strCommand = "Select * from Tasks";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);
            if (dsData.Tables["Tasks"] != null)
            {
                dsData.Tables["Tasks"].Clear();
            }
            adptr.Fill(dsData, "Tasks");
            if (dsData.Tables["Tasks"] != null && dsData.Tables["Tasks"].Rows.Count > 0)
            {
                var temp = dsData.Tables["Tasks"].AsEnumerable();
                DataRow res = (from obj in temp where obj.Field<long>(0) == TaskId select obj).ToList().FirstOrDefault();
                if (res != null)
                {
                    res.Delete();
                }
                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);
                adptr.Update(dsData, "Tasks");
                return true;
            }
            return false;
        }

        public bool DeleteComment(long CommentId)
        {
            string strCommand = "Select * from Comments";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, ConString);
            if (dsData.Tables["Comments"] != null)
            {
                dsData.Tables["Comments"].Clear();
            }

            adptr.Fill(dsData, "Comments");

            if (dsData.Tables["Comments"] != null && dsData.Tables["Comments"].Rows.Count > 0)
            {
                var temp = dsData.Tables["Comments"].AsEnumerable();
                DataRow res = (from obj in temp where obj.Field<long>(0) == CommentId select obj).ToList().FirstOrDefault();
                if (res != null)
                {
                    res.Delete();
                }
                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);
                adptr.Update(dsData, "Comments");
                return true;
            }return false;
        }

        public string GetUserXML()
        {
            List<UserDTO> temp = ListUsers();
            return dsData.GetXml();
        }

    }
}
