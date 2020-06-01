using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementSystem.Models.ProjectClasses
{
    public class DevTask
    {
        public DevTask() {

        }
        public DevTask(string Title, string Description, Priority Priority, Status Status, DateTime DateCreated, DateTime DeadLine,
            int ProjectId, string devId) {
            this.Title = Title;
            this.Description = Description;
            this.Priority = Priority;
            this.Status = Status;
            this.DateCreated = DateCreated;
            this.Deadline = DeadLine;
            this.ProjectId = ProjectId;
            this.DeveloperId = devId;      

        }

        public int Id { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? DateCompleted { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime DateCreated { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime Deadline { get; set; }
        public string Comment { get; set; }
        public string Description { get; set; }
        public int PercentCompleted { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public string Title { get; set; }

        public string DeveloperId { get; set; }
        public virtual ApplicationUser Developer { get; set; }


        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}