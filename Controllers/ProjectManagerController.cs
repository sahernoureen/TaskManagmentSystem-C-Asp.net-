using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.Models;
using Microsoft.AspNet.Identity;
using TaskManagementSystem.Models.ProjectClasses;

namespace TaskManagementSystem.Controllers {
    public class ProjectManagerController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ProjectManager
        // GET: TaskManager
        public ActionResult Index() {
            var projects = db.Projects.Include("User").Include("DevTasks").ToList();
            return View(projects);
        }

        [HttpGet]
        public ActionResult AddProject() {
            return View();
        }
        [HttpPost]
        public ActionResult AddProject(ProjectViewModel model) {
            if (ModelState.IsValid) {
                var userId = User.Identity.GetUserId();
                ProjectHelper.createProject(model.Deadline, model.Description, model.Priority, model.Status, model.Title, userId);
            }
            return RedirectToAction("Index", "ProjectManager");
        }
 
        public ActionResult DeleteProject(int projectId) {
            ProjectHelper.deleteProject(projectId);
            return RedirectToAction("Index", "ProjectManager");
        }

        [HttpGet]
        public ActionResult UpdateProject(int projectId) {
            var project = ProjectHelper.createProjectViewModel(projectId);
            ViewBag.projectId = projectId;
            return View(project);
        }
        [HttpPost]
        public ActionResult UpdateProject(UpdateProjectViewModer model) {
            if (ModelState.IsValid) {
                ProjectHelper.updateProject(model.ProjectId, model.DateCompleted, model.Deadline, model.Description, model.Priority, model.Status, model.Title);
            }
            return RedirectToAction("Index", "ProjectManager");
        }

    }

}