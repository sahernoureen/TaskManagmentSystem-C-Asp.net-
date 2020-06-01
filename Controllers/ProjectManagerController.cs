using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using TaskManagementSystem.Models;
using TaskManagementSystem.Models.ProjectClasses;


namespace TaskManagementSystem.Controllers {
    public class ProjectManagerController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index() {
            var projects = db.Projects.Include("User").Include("DevTasks").ToList();
            return View(projects);
        }

            return View();
        }

        [HttpGet]
        public ActionResult AddProject()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProject(ProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                ProjectHelper.createProject(model.Deadline, model.Description, model.Priority, model.Status, model.Title, userId);
            }
            return RedirectToAction("Index", "ProjectManager");
        }
 
        public ActionResult DeleteProject(int projectId) {
            ProjectHelper.deleteProject(projectId);
            return RedirectToAction("Index", "ProjectManager");
        }

        public ActionResult OrderByPercent() {
            Dictionary<Project, double> dicProjects = new Dictionary<Project, double>();
            foreach (var project in db.Projects) {
                if (project.DevTasks.Count > 0) {
                    var sum = project.DevTasks.Sum(x => x.PercentCompleted) / project.DevTasks.Count();
                    dicProjects.Add(project, sum);
                } else {
                    dicProjects.Add(project, 0);
                }
            }
            var projects = dicProjects.OrderByDescending(x => x.Value).Select(x => x.Key).ToList();
            return View(projects);
        }

        public ActionResult OrderByPercentExcludingComplete() {
            Dictionary<Project, double> dicProjects = new Dictionary<Project, double>();
            foreach (var project in db.Projects) {
                if (project.DevTasks.Count > 0) {
                    var sum = project.DevTasks.Sum(x => x.PercentCompleted) / project.DevTasks.Count();
                    dicProjects.Add(project, sum);
                } else {
                    dicProjects.Add(project, 0);
                }
            }
            var projects = dicProjects.OrderByDescending(x => x.Value).Select(x => x.Key).Where(x => x.Status != Status.Completed).ToList();
            return View(projects);
        }

        public ActionResult OrderByPriority() {
            var projects = db.Projects.OrderBy(
                 t => t.Priority == Priority.Urgent ? 1 :
                 (t.Priority == Priority.High ? 2 :
                 (t.Priority == Priority.Medium ? 3 : 4))
                 ).ToList();
            return View(projects);
        }

        public ActionResult OrderByIncompleByDeadline() {
            var devTasks = db.DevTasks.Where(x => x.Status != Status.Completed && DateTime.Compare(DateTime.Now, x.Deadline) > 0).ToList();
            return View(devTasks);
        }

        [HttpGet]
        public ActionResult UpdateProject(int projectId)
        {
            var project = ProjectHelper.createProjectViewModel(projectId);
            ViewBag.projectId = projectId;
            return View(project);
        }
        [HttpPost]
        public ActionResult UpdateProject(UpdateProjectViewModer model)
        {
            if (ModelState.IsValid)
            {
                ProjectHelper.updateProject(model.ProjectId, model.DateCompleted, model.Deadline, model.Description, model.Priority, model.Status, model.Title);
            }
            return RedirectToAction("Index", "ProjectManager");
        }
    }
}