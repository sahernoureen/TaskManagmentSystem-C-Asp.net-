using System;

namespace TaskManagementSystem.Models.ProjectClasses
{
    public class NotificationDev
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string DeveloperId { get; set; }
        public virtual ApplicationUser Developer { get; set; }
        public string Comment { get; set; }
        public int TaskId { get; set; }
        public virtual DevTask DevTask { get; set; }

    }
}