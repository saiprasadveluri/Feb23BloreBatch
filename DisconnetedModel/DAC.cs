using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DisconnetedModel
{
    public class DAC
    {
        DataSet ds= new DataSet();
        string Constring = "Data Source=.;Initial Catalog=TaskManager;Integrated Security=SSPI";
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            if (ds.Tables["Users"] != null)
            {
                ds.Tables["Users"].Clear();
            }

            SqlDataAdapter da = new SqlDataAdapter("select * from Users", Constring);
            da.Fill(ds, "Users");
            if (ds.Tables.Count > 0)
            { 
            DataTable dt = ds.Tables["Users"];
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count;i++)
                    {
                        DataRow dtcur = dt.Rows[i];
                        User user = new User();
                        user.UserId = Convert.ToInt64(dt.Rows[i][0]);
                        user.Name = dt.Rows[i][1].ToString();
                        user.Dept = dt.Rows[i][2].ToString();
                        user.RoleId = Convert.ToInt32(dt.Rows[i][3]);
                        users.Add(user);
                    }
                }
            }
            return users;
        }
        public bool Addusers(User user)
        {
            string strCommand = "select * from Users";
            SqlDataAdapter da = new SqlDataAdapter(strCommand, Constring);
            da.FillSchema(ds, SchemaType.Source, "Users");
            DataTable dt = ds.Tables["Users"];
            if (dt != null)
            {
                DataRow newRow = dt.NewRow();
                newRow[1] = user.Name;
                newRow[2] = user.Dept;
                newRow[3] = user.RoleId;
                dt.Rows.Add(newRow);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Update(ds, "Users");
                return true;
            }
            return false;
        }

        public bool UpdateUserRole(long UserId, long newRoleId)
        {
            string strCommand = "Select * from Users";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, Constring);
            if (ds.Tables["Users"] != null)
            {
                ds.Tables["Users"].Clear();
            }
            adptr.Fill(ds, "Users");
            if (ds.Tables["Users"] != null && ds.Tables["Users"].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables["Users"].Rows.Count; i++)
                {

                    if (Convert.ToInt64(ds.Tables["Users"].Rows[i][0]) == UserId)
                    {
                        DataRow drReq = ds.Tables["Users"].Rows[i];

                        drReq[3] = newRoleId;

                    }
                }
                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);
                adptr.Update(ds, "Users");
                return true;
            }
            return false;
        }

        public bool DeleteUser(long UserId)
        {
            string strCommand = "Select * from Users";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, Constring);
            if (ds.Tables["Users"] != null)
            {
                ds.Tables["Users"].Clear();
            }
            adptr.Fill(ds, "Users");
            if (ds.Tables["Users"] != null && ds.Tables["Users"].Rows.Count > 0)
            {
                var temp = ds.Tables["Users"].AsEnumerable();
                DataRow res = (from obj in temp where obj.Field<long>(0) == UserId select obj).ToList().FirstOrDefault();
                if (res != null)
                {
                    res.Delete();
                }

                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);
                adptr.Update(ds, "Users");
                return true;
            }
            return false;


        }

        public List<Project> ListProjects()
        {
            List<Project> proj = new List<Project>();
            if (ds.Tables["Projects"] != null)
            {
                ds.Tables["Projectss"].Clear();
            }

            SqlDataAdapter da = new SqlDataAdapter("select * from Projects", Constring);
            da.Fill(ds, "Projects");
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables["Projects"];
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dtcur = dt.Rows[i];
                        Project pro = new Project();
                        pro.ProjectId = Convert.ToInt64(dt.Rows[i][0]);
                        pro.ProjectName = dt.Rows[i][1].ToString();
                        pro.ProjectManagerId = Convert.ToInt64(dt.Rows[i][2]);
                        pro.PStatus = dt.Rows[i][3].ToString(); 
                        proj.Add(pro);
                    }
                }
            }
            return proj;
        }

        public bool AddProjects(Project p)
        {
            string strCommand = "select * from Projects";
            SqlDataAdapter da = new SqlDataAdapter(strCommand, Constring);
            da.FillSchema(ds, SchemaType.Source, "Projects");
            DataTable dt = ds.Tables["Projects"];
            if (dt != null)
            {
                DataRow newRow = dt.NewRow();
                newRow[1] = p.ProjectName;
                newRow[2] = p.ProjectManagerId;
                newRow[3] = p.PStatus;
                dt.Rows.Add(newRow);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Update(ds, "Projects");
                return true;
            }
            return false;
        }

        public bool UpdateProject(Project project)
        {
            string strCommand = "Select * from Projects";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, Constring);
            if (ds.Tables["Projects"] != null)
            {
                ds.Tables["Projects"].Clear();
            }
            adptr.Fill(ds, "Projects");
            if (ds.Tables["Projects"] != null && ds.Tables["Projects"].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables["Projects"].Rows.Count; i++)
                {
                    if (Convert.ToInt64(ds.Tables["Projects"].Rows[i][0]) == project.ProjectId)
                    {
                        DataRow drReq = ds.Tables["Projects"].Rows[i];
                        drReq[1] = project.ProjectName;
                        drReq[2] = project.ProjectManagerId;
                        drReq[3] = project.PStatus;
                    }
                }
                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);
                adptr.Update(ds, "Projects");
                return true;
            }
            return false;
        }

        public bool DeleteProject(long projectId)
        {
            string strCommand = "Select * from Projects";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, Constring);
            if (ds.Tables["Projects"] != null)
            {
                ds.Tables["Projects"].Clear();
            }
            adptr.Fill(ds, "Projects");
            if (ds.Tables["Projects"] != null && ds.Tables["Projects"].Rows.Count > 0)
            {
                var temp = ds.Tables["Projects"].AsEnumerable();
                DataRow res = (from obj in temp where obj.Field<long>(0) == projectId select obj).ToList().FirstOrDefault();
                if (res != null)
                {
                    res.Delete();
                }

                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);
                adptr.Update(ds, "Projects");
                return true;
            }
            return false;
        }
        public List<Task> ListTasks()
        {
            List<Task> tasks = new List<Task>();
            if (ds.Tables["Tasks"] != null)
            {
                ds.Tables["Tasks"].Clear();
            }

            SqlDataAdapter da = new SqlDataAdapter("select * from Tasks", Constring);
            da.Fill(ds, "Tasks");
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables["Tasks"];
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dtcur = dt.Rows[i];
                        Task task = new Task();
                        task.TaskId = Convert.ToInt64(dt.Rows[i][0]);
                        task.TaskTitle = dt.Rows[i][1].ToString();
                        task.TaskType = Convert.ToInt64(dt.Rows[i][2]);
                        task.ProjId = Convert.ToInt64(dt.Rows[i][3]);
                        task.AssignedTo = Convert.ToInt64(dt.Rows[i][4]);
                        tasks.Add(task);
                    }
                }
            }
            return tasks;
        }

        public List<Comments> ListComments()
        {
            List<Comments> comments = new List<Comments>();
            if (ds.Tables["Comments"] != null)
            {
                ds.Tables["Comments"].Clear();
            }

            SqlDataAdapter da = new SqlDataAdapter("select * from Comments", Constring);
            da.Fill(ds, "Comments");
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables["Comments"];
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dtcur = dt.Rows[i];
                        Comments comment = new Comments();
                        comment.CommentId = Convert.ToInt64(dt.Rows[i][0]);
                        comment.CommentTitle = dt.Rows[i][1].ToString();
                        comment.CommentText = dt.Rows[i][2].ToString();
                        comment.TaskId = Convert.ToInt64(dt.Rows[i][3]);
                        comment.CommentedBy = Convert.ToInt64(dt.Rows[i][4]);
                        comments.Add(comment);
                    }
                }
            }
            return comments;
        }

        public bool AddComment(Comments comment)
        {
            string strCommand = "select * from Comments";
            SqlDataAdapter da = new SqlDataAdapter(strCommand, Constring);
            da.FillSchema(ds, SchemaType.Source, "Comments");
            DataTable dt = ds.Tables["Comments"];
            if (dt != null)
            {
                DataRow newRow = dt.NewRow();
                newRow[1] = comment.CommentTitle;
                newRow[2] = comment.CommentText;
                newRow[3] = comment.TaskId;
                newRow[4] = comment.CommentedBy;
                dt.Rows.Add(newRow);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Update(ds, "Comments");
                return true;
            }
            return false;
        }

        public bool UpdateComment(Comments comment)
        {
            string strCommand = "Select * from Comments";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, Constring);
            if (ds.Tables["Comments"] != null)
            {
                ds.Tables["Comments"].Clear();
            }
            adptr.Fill(ds, "Comments");
            if (ds.Tables["Comments"] != null && ds.Tables["Comments"].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables["Comments"].Rows.Count; i++)
                {
                    if (Convert.ToInt64(ds.Tables["Comments"].Rows[i][0]) == comment.CommentId)
                    {
                        DataRow drReq = ds.Tables["Comments"].Rows[i];
                        drReq[1] = comment.CommentTitle;
                        drReq[2] = comment.CommentText;
                        drReq[3] = comment.TaskId;
                        drReq[4] = comment.CommentedBy;
                    }
                }
                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);
                adptr.Update(ds, "Comments");
                return true;
            }
            return false;
        }

        public bool DeleteComment(long commentId)
        {
            string strCommand = "Select * from Comments";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, Constring);
            if (ds.Tables["Comments"] != null)
            {
                ds.Tables["Comments"].Clear();
            }
            adptr.Fill(ds, "Comments");
            if (ds.Tables["Comments"] != null && ds.Tables["Comments"].Rows.Count > 0)
            {
                var temp = ds.Tables["Comments"].AsEnumerable();
                DataRow res = (from obj in temp where obj.Field<long>(0) == commentId select obj).ToList().FirstOrDefault();
                if (res != null)
                {
                    res.Delete();
                }

                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);
                adptr.Update(ds, "Comments");
                return true;
            }
            return false;
        }

        public bool AddTask(Task task)
        {
            string strCommand = "select * from Tasks";
            SqlDataAdapter da = new SqlDataAdapter(strCommand, Constring);
            da.FillSchema(ds, SchemaType.Source, "Tasks");
            DataTable dt = ds.Tables["Tasks"];
            if (dt != null)
            {
                DataRow newRow = dt.NewRow();
                newRow[1] = task.TaskTitle;
                newRow[2] = task.TaskType;
                newRow[3] = task.ProjId;
                newRow[4] = task.AssignedTo;
                dt.Rows.Add(newRow);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Update(ds, "Tasks");
                return true;
            }
            return false;
        }

        public bool UpdateTask(Task task)
        {
            string strCommand = "Select * from Tasks";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, Constring);
            if (ds.Tables["Tasks"] != null)
            {
                ds.Tables["Tasks"].Clear();
            }
            adptr.Fill(ds, "Tasks");
            if (ds.Tables["Tasks"] != null && ds.Tables["Tasks"].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables["Tasks"].Rows.Count; i++)
                {
                    if (Convert.ToInt64(ds.Tables["Tasks"].Rows[i][0]) == task.TaskId)
                    {
                        DataRow drReq = ds.Tables["Tasks"].Rows[i];
                        drReq[1] = task.TaskTitle;
                        drReq[2] = task.TaskType;
                        drReq[3] = task.ProjId;
                        drReq[4] = task.AssignedTo;
                    }
                }
                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);
                adptr.Update(ds, "Tasks");
                return true;
            }
            return false;
        }

        public bool DeleteTask(long taskId)
        {
            string strCommand = "Select * from Tasks";
            SqlDataAdapter adptr = new SqlDataAdapter(strCommand, Constring);
            if (ds.Tables["Tasks"] != null)
            {
                ds.Tables["Tasks"].Clear();
            }
            adptr.Fill(ds, "Tasks");
            if (ds.Tables["Tasks"] != null && ds.Tables["Tasks"].Rows.Count > 0)
            {
                var temp = ds.Tables["Tasks"].AsEnumerable();
                DataRow res = (from obj in temp where obj.Field<long>(0) == taskId select obj).ToList().FirstOrDefault();
                if (res != null)
                {
                    res.Delete();
                }

                SqlCommandBuilder builder = new SqlCommandBuilder(adptr);
                adptr.Update(ds, "Tasks");
                return true;
            }
            return false;
        }


    }
}
