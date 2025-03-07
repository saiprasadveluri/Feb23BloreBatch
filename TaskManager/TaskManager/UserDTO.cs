using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    class UserDTO
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public long RoleId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }

    public class TaskDTO
    {
        public long TaskId { get; set; }
        public string Title { get; set; }
        public long TaskType { get; set; }
        public long ProjId { get; set; }
        public long AssignedTo { get; set; }
    }

    public class ProjectDTO
    {
        public long ProjId { get; set; }
        public string Title { get; set; }
        public long ProjManager { get; set; }
        public string Status { get; set; }
    }

    public class CommentDTO
    {
       public long CommId { get; set; }
       public string Title { get; set; }
       public string CommText { get; set; }
       public long TaskId { get; set; }
       public long CommentedBy { get; set; }
    }

    public class ProjAssToDTO
    {
        public long Assignid { get; set; }
        public long ProjId { get; set; }
        public long Userid { get; set; }
    }
}
