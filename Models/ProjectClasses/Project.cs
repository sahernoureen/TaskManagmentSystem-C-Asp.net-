using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManagementSystem.Models.ProjectClasses;


namespace TaskManagementSystem.Models
{
    public class Project
    {
        public Project()
        {

        }
        public Project(DateTime dateCreated, DateTime deadline, string description, float budget, Priority priority, Status status, string title, string userId)
        {
            DateCompleted = null;
            DateCreated = dateCreated;
            Deadline = deadline;
            Description = description;
            Budget = budget;
            Priority = priority;
            Status = status;
            Title = title;
            UserId = userId;
            DevTasks = new HashSet<DevTask>();
            NotificationManagers = new HashSet<NotificationManager>();
        }

        public int Id { get; set; }
        public float Budget { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? DateCompleted { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime DateCreated { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime Deadline { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public string Title { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<DevTask> DevTasks { get; set; }

        public virtual ICollection<NotificationManager> NotificationManagers{get;set;}
    }
}