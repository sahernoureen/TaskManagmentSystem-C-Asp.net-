using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Models.ProjectClasses
{
    public class NotificationManager
    {
        public NotificationManager()
        {

        }

        public NotificationManager(int ProjId, int taskId, DateTime DateCreated, string ManagerId, string comment)
        {
            this.ProjectId = ProjId;
            this.TaskId = taskId;
            this.DateCreated = DateCreated;
            this.ProjectManagerId = ManagerId;
            this.Comment = comment;
            this.IsChecked = false;
        }

        public NotificationManager(int ProjId, DateTime DateCreated, string ManagerId, string comment)
        {
            this.ProjectId = ProjId;        
            this.DateCreated = DateCreated;
            this.ProjectManagerId = ManagerId;
            this.Comment = comment;
            this.IsChecked = false;
        }


        [Key]
        public int Id { set; get; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public int TaskId { get; set; }
        public virtual DevTask DevTask { get; set; }

        public DateTime DateCreated { get; set; }
        public string ProjectManagerId { get; set; }
        public virtual ApplicationUser ProjectManager { get; set; }
        public string Comment { get; set; }
        public bool IsChecked { get; set; }
    }
}