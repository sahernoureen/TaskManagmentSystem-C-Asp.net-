using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManagementSystem.Models.ProjectClasses {
    public class TaskViewModel {

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Deadline")]
        public DateTime Deadline { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Priority")]
        public Priority Priority { get; set; }
        [Required]
        [Display(Name = "Status")]
        public Status Status { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "ProjectId")]
        public int ProjectId { get; set; }
        [Required]
        [Display(Name = "DeveloperId")]
        public string DeveloperId { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
    }

    public class UpdateTaskViewModelForPM {
        public int TaskId { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Deadline")]
        public DateTime? Deadline { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public string Title { get; set; }
        public int ProjectId { get; set; }
        //public string Comment { get; set; }
        //public int? PercentCompleted { get; set; }
        //[DataType(DataType.Date)]
        //[Display(Name = "DateCompleted")]
        //public DateTime? DateCompleted { get; set; }
    }

}