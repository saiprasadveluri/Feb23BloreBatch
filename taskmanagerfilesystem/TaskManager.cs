using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    using System;

    using System.Collections.Generic;

    using System.IO;

    // User Base Class

    abstract class User

    {

        public string Name { get; set; }

        public string Role { get; set; }

        protected User(string name, string role)

        {

            Name = name;

            Role = role;

        }

    }

    // User Roles

    class SiteAdmin : User { public SiteAdmin(string name) : base(name, "Admin") { } }
    class ProjectManager : User { public ProjectManager(string name) : base(name, "Project Manager") { } }
    class Developer : User { public Developer(string name) : base(name, "Developer") { } }
    class QAAnalyst : User { public QAAnalyst(string name) : base(name, "QA Analyst") { } }

    // Task Class

    class Task

    {

        public string Title { get; set; }

        public string Type { get; set; } // Bug or Feature

        public string Status { get; set; }

        public List<string> Comments { get; set; }

        public Task(string title, string type)

        {

            Title = title;

            Type = type;

            Status = "Open"; // Default status

            Comments = new List<string>();

        }

        public void AddComment(string comment) => Comments.Add(comment);

        public void ChangeStatus(string status) => Status = status;

    }

    // Project Class

    class Project

    {

        public string ProjectName { get; set; }

        public List<Task> Tasks { get; set; }

        public Project(string projectName)

        {

            ProjectName = projectName;

            Tasks = new List<Task>();

        }

        public override string ToString() => ProjectName;

    }

    // Generic File Storage Class

    class DataStorage<T>

    {

        private string filePath;

        public DataStorage(string fileName)

        {
            filePath = $"{fileName}.txt";

        }

        public void SaveData(List<T> data)

        {

            using (StreamWriter writer = new StreamWriter(filePath))

            {

                foreach (var item in data)

                {
                    writer.WriteLine(item.ToString());

                }

            }

        }

        public List<string> LoadData()

        {

            List<string> data = new List<string>();

            if (File.Exists(filePath))

            {

                using (StreamReader reader = new StreamReader(filePath))

                {

                    string line;

                    while ((line = reader.ReadLine()) != null)

                    {
                        data.Add(line);

                    }

                }

            }

            return data;

        }

    }


}
