using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TaskManagementSystem.Models.ProjectClasses;

namespace TaskManagementSystem.Models
{
    public class ProjectHelper
    {
        public static ApplicationDbContext db = new ApplicationDbContext();

        //USER
        public static ApplicationUser getUserById(string userId) {
            return db.Users.Where(x => x.Id == userId).FirstOrDefault();
        }

        //PROJECT
        public static Project getProjectById(int id) {
            return db.Projects.FirstOrDefault(x => x.Id == id);
        }

        public static List<Project> oderByDevTaskPercent() {
            Dictionary<Project, double> projects = new Dictionary<Project, double>();
            foreach (var project in db.Projects) {
                if (project.DevTasks.Count > 0) {
                    var sum = project.DevTasks.Sum(x => x.PercentCompleted) / project.DevTasks.Count();
                    projects.Add(project, sum);
                } else {
                    projects.Add(project, 0);
                }

            }
            var projectTest = projects.OrderByDescending(x => x.Value).Select(x => x.Key).ToList();
            return projectTest;
        }

        public static List<Project> oderByDevTaskPercentExcludingComplete() {
            return oderByDevTaskPercent().Where(x => x.Status != Status.Completed).ToList();
        }

        public static List<Project> orderByPriority() {
            return db.Projects.OrderBy(
                t => t.Priority == Priority.Urgent ? 1 :
                (t.Priority == Priority.High ? 2 :
                (t.Priority == Priority.Medium ? 3 : 4))
                ).ToList();
        }

        public static List<Project> orderByIncompleByDeadline() {
            return db.Projects.Where(x => x.Status != Status.Completed && DateTime.Compare(DateTime.Now, x.Deadline) > 0).ToList();
        }


        //ADD PROJECT
        public static void createProject(DateTime deadline, string description, Priority priority, Status status, string title, string userId) {
            Project project = new Project(DateTime.Now, deadline, description, priority, status, title, userId);
            db.Projects.Add(project);
            db.SaveChanges();
        }

        //DELETE PROJECT
        public static void deleteProject(int projectid) {
            var project = getProjectById(projectid);
            db.Projects.Remove(project);
            db.SaveChanges();
        }

        //UPDATE PROJECT
        public static void updateProject(int projectId, DateTime? dateCompleted, DateTime? deadline, string description, Priority priority, Status status, string title) {
            var project = getProjectById(projectId);
            project.DateCompleted = dateCompleted;
            project.Deadline = deadline ?? project.Deadline;
            project.Description = description;
            project.Priority = priority;
            project.Status = status;
            project.Title = title;
            db.SaveChanges();
        }

        //CREATE PROJECT VIEW MODEL
        public static UpdateProjectViewModer createProjectViewModel(int projectId) {
            var project = getProjectById(projectId);
            UpdateProjectViewModer projectView = new UpdateProjectViewModer();
            projectView.ProjectId = projectId;
            projectView.DateCompleted = project.DateCompleted;
            projectView.Deadline = project.Deadline;
            projectView.Description = project.Description;
            projectView.Priority = project.Priority;
            projectView.Status = project.Status;
            projectView.Title = project.Title;
            return projectView;
        }
    }

  
}
