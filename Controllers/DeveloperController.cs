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

            ViewBag.UserId = userId;
            ViewBag.NotificationCount = GetNotificationsCount(devTasks);
            return View(devTasks);
        }

        public int GetNotificationsCount(List<DevTask> UserTasks)
        {  
            var userNotificationCount = 0;
            foreach (var newtask in UserTasks)
            {
                TaskHelper.CreateDeveloperNotification(newtask); 
                if(newtask.NotificationDevs != null)
                {
                    if (newtask.NotificationDevs.IsRead == false)
                    {
                        userNotificationCount++;
                    }
                }
            }
            return userNotificationCount;
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

                    NotificationManager NotifiManager = new NotificationManager(
                        updateDevTask.ProjectId, 
                        updateDevTask.Id, 
                        DateTime.Now, 
                        updateDevTask.Project.UserId,
                        "The Task is Completed");

                    db.NotificationManagers.Add(NotifiManager);
                    updateDevTask.Project.NotificationManagers.Add(NotifiManager);
                    updateDevTask.Project.User.NotificationManager.Add(NotifiManager);
                    db.SaveChanges();

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

        [HttpGet]
        public ActionResult ReportABug(int devTaskId)
        {
            var devtask = db.DevTasks.FirstOrDefault(x => x.Id == devTaskId);
            return View(devtask);
        }
        [HttpPost]
        public ActionResult ReportABug(DevTask devTask)
        {
            if (ModelState.IsValid)
            {
                var BugReportDevTask = db.DevTasks.FirstOrDefault(x => x.Id == devTask.Id);
                BugReportDevTask.BugReport = devTask.BugReport;
            //    var project = db.Projects.FirstOrDefault(p => p.);
                TaskHelper.CreateBugNotification(BugReportDevTask, BugReportDevTask.Project);
             
            }
            return Redirect("Index");
        }




        public ActionResult DeveloperNotifications(string userId)
        {
            var Result = db.NotificationDevs.Where(n => n.DeveloperId == userId).ToList();
            return View(Result);
        }

        public ActionResult GetDetailOfANotification(int NotifiId)
        {
            var Result = db.NotificationDevs.FirstOrDefault(d => d.Id == NotifiId);
            if(Result!= null)
            {
                Result.IsRead = true;
            }
          
            db.SaveChanges();
            return View(Result);
        }
    }
}