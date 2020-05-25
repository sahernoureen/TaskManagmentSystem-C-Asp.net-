using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaskManagementSystem.Models.ProjectClasses;


namespace TaskManagementSystem.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateCompleted { get; set; }
        public float Budget { get; set; }
        public virtual ICollection<DevTask> DevTasks { get; set; }

        public Project()
        {
            DevTasks = new HashSet<DevTask>();
        }
    }
}