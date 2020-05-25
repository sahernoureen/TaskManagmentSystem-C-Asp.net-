using System;

namespace TaskManagementSystem.Models.ProjectClasses
{
    public class DevTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateCompleted { get; set; }
        public int PercentCompleted { get; set; }


        public string DeveloperId { get; set; }
        public virtual ApplicationUser Developer { get; set; }


        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}