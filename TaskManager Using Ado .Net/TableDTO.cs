using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL
{
    public class UserDTO
    {
        public long UserId { get; set; }
        public string UName { get; set; }
        public string Dept { get; set; }
        public long RoleId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }

    public class ProjectDTO
    {
        public long ProjectId { get; set; }
        public string ProjectName { get; set; }
        public long ProjectManagerId { get; set; }
        public string PStatus { get; set; }
    }

    public class TaskDTO
    {
        public long TaskId { get; set; }
        public string Title { get; set; }
        public int TType { get; set; }
        public long ProjectId { get; set; }
        public long AssignedTo { get; set; }
        public string TStatus { get; set; }
    }

    public class CommentDTO
    {
        public long CommentId { get; set; }
        public string Title { get; set; }
        public string CommentText { get; set; }
        public long TaskId { get; set; }
        public long CommentedBy { get; set; }
    }

    public class ProjectAssignedToDTO
    {
        public long id { get; set; }
        public long projid { get; set; }
        public long userid { get; set; }
    }

}
