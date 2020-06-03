using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementSystem.Models.ProjectClasses
{
    public class NotificationDev
    {
        public NotificationDev()
        {

        }
        public NotificationDev(string Comment, DateTime notifytime, int taskId, string DevelopId)
        {
            this.Comment = Comment;
            this.DateCreated = notifytime;
            this.DevTaskId = taskId;
            this.DeveloperId = DevelopId;
            this.IsRead = false;
        }

     
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string DeveloperId { get; set; }
        public virtual ApplicationUser Developer { get; set; }
        public string Comment { get; set; }

        public bool IsRead { get; set; }

        [Required]
        public int DevTaskId { get; set; }

    }
}