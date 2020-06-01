using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.Models;
using TaskManagementSystem.Models.ProjectClasses;

namespace TaskManagementSystem.Controllers
{
    public class DeveloperController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles = "Developer")]
        public ActionResult Index() {
            var userId = User.Identity.GetUserId();
            var devTasks = db.DevTasks.Where(x => x.DeveloperId == userId).Include("Project").ToList();
            return View(devTasks);
        }

        [HttpGet]
        public ActionResult UpdateDeveloperTask(int devTaskId) {
            var devtask = db.DevTasks.FirstOrDefault(x => x.Id == devTaskId);
            return View(devtask);
        }

        [HttpPost]
        public ActionResult UpdateDeveloperTask(DevTask devTask) {
            if (ModelState.IsValid) {
                var updateDevTask = db.DevTasks.FirstOrDefault(x => x.Id == devTask.Id);
                if (devTask.PercentCompleted == 100) {
                    updateDevTask.DateCompleted = DateTime.Now;
                    updateDevTask.PercentCompleted = devTask.PercentCompleted;
                    updateDevTask.Status = Status.Completed;
                } else {
                    updateDevTask.Comment = null;
                    updateDevTask.DateCompleted = null;
                    updateDevTask.PercentCompleted = devTask.PercentCompleted;
                    if (updateDevTask.Status == Status.Completed) {
                        updateDevTask.Status = Status.Assigned;
                    }
                }
                db.SaveChanges();
            }
            return Redirect("Index");
        }

        [HttpGet]
        public ActionResult AddComment(int devTaskId) {
            var devtask = db.DevTasks.FirstOrDefault(x => x.Id == devTaskId);
            return View(devtask);
        }
        [HttpPost]
        public ActionResult AddComment(DevTask devTask) {
            if (ModelState.IsValid) {
                var updateDevTask = db.DevTasks.FirstOrDefault(x => x.Id == devTask.Id);
                updateDevTask.Comment = devTask.Comment;
                db.SaveChanges();
            }
            return Redirect("Index");
        }
    }
}