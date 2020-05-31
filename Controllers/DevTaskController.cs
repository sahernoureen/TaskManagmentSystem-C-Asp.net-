using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.Models;
using TaskManagementSystem.Models.ProjectClasses;

namespace TaskManagementSystem.Controllers
{
    public class DevTaskController : Controller
    {
        [HttpGet]
        public ActionResult AddTask(int projectId) {
            ViewBag.projectId = projectId;
            var userId = User.Identity.GetUserId();
            var users = TaskHelper.getAllDeveloperUserExceptProjectManagerId(userId);
            ViewBag.DeveloperId = new SelectList(users, "Id", "UserName");
            return View();
        }
        [HttpPost]
        public ActionResult AddTask(TaskViewModel model) {
            if (ModelState.IsValid) {
                TaskHelper.AddTask(model.ProjectId, model.Title, model.Description, model.Priority, model.Status, model.Deadline, model.DeveloperId);
            }
            var userId = User.Identity.GetUserId();
            var users = TaskHelper.getAllDeveloperUserExceptProjectManagerId(userId);
            ViewBag.DeveloperId = new SelectList(users, "Id", "UserName");
            return RedirectToAction("Index", "ProjectManager");
        }

        public ActionResult DeleteTask(int devTaskId) {
            TaskHelper.DeleteTask(devTaskId);
            return RedirectToAction("Index", "ProjectManager");
        }

        public ActionResult UpdateTask(int devTaskId, int projectId) {
            ViewBag.projectId = projectId;
            var task = TaskHelper.createTaskViewModel(devTaskId);
            ViewBag.TaskId = devTaskId;
            return View(task);
        }

        [HttpPost]
        public ActionResult UpdateTask(UpdateTaskViewModelForPM model) {
            if (ModelState.IsValid) {
                TaskHelper.UpdateTask(model.TaskId, model.ProjectId, model.Title, model.Description, model.Priority, model.Status, model.Deadline);
            }
            return RedirectToAction("Index", "ProjectManager");
        }
    }
}