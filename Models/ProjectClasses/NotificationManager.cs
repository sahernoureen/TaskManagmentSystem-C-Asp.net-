using System;

namespace TaskManagementSystem.Models.ProjectClasses
{
    public class NotificationManager
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string ProjectManagerId { get; set; }
        public virtual ApplicationUser ProjectManager { get; set; }
        public string Comment { get; set; }
        public int TaskId { get; set; }
        public virtual DevTask DevTask { get; set; }

    }
}