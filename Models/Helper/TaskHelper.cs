using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManagementSystem.Models.ProjectClasses;

namespace TaskManagementSystem.Models
{
    public class TaskHelper
    {
        public static ApplicationDbContext db = new ApplicationDbContext();

        //USER 
        public static List<ApplicationUser> getAllDeveloperUserExceptProjectManagerId(string userId) {
            List<ApplicationUser> listDevelopers = new List<ApplicationUser>();
            foreach (var user in db.Users) {
                if (AdminHelper.checkIfUserIsRole(user.Id, "Developer") && user.Id != userId) {
                    listDevelopers.Add(user);
                }
            }
            return listDevelopers;
        }

        //GET TASK
        public static DevTask GetTask(int id) {
            return db.DevTasks.FirstOrDefault(x => x.Id == id);
        }

        public static List<DevTask> getAllTasks(int id) {
            return db.DevTasks.Where(t => t.ProjectId == id).ToList();
        }

        //ADD TASK
        public static void AddTask(int ProjectId, string Title, string Desc, Priority pr, Status Status, DateTime DeadLine, string DevId) {
            Project project1 = db.Projects.FirstOrDefault(p => p.Id == ProjectId);
            DevTask devTask = new DevTask(Title, Desc, pr, Status, DateTime.Now, DeadLine, project1.Id, DevId);
            project1.DevTasks.Add(devTask);
            db.SaveChanges();
        }

        //DELETE TASK
        public static void DeleteTask(int taskId) {
            var DevTask = GetTask(taskId);
            db.DevTasks.Remove(DevTask);
            db.SaveChanges();
        }

        //UPDATE TASK
        public static void UpdateTask(int TaskId, int ProjectId, string Title, string Desc, Priority pr, Status Status, DateTime? DeadLine) {
            var DevTask = GetTask(TaskId);
            DevTask.Title = Title;
            DevTask.ProjectId = ProjectId;
            DevTask.Description = Desc;
            DevTask.Priority = pr;
            DevTask.Status = Status;
            DevTask.Deadline = DeadLine ?? DevTask.Deadline;
            //DevTask.DateCompleted = (DateTime)DateCompleted;
            //DevTask.PercentCompleted = (int)percentcomplete;
            //DevTask.Comment = comment;

            db.SaveChanges();
        }

        public static void AssignTask(int id, string developerId) {
            DevTask devTask = GetTask(id);
            devTask.DeveloperId = developerId;
            db.SaveChanges();
        }

        public static UpdateTaskViewModelForPM createTaskViewModel(int TaskId) {
            var task = GetTask(TaskId);
            UpdateTaskViewModelForPM TaskView = new UpdateTaskViewModelForPM();
            TaskView.TaskId = TaskId;
            TaskView.ProjectId = task.ProjectId;

            TaskView.Deadline = task.Deadline;
            TaskView.Description = task.Description;
            TaskView.Priority = task.Priority;
            TaskView.Status = task.Status;
            TaskView.Title = task.Title;
            //TaskView.PercentCompleted = task.PercentCompleted;
            //TaskView.Comment = task.Comment;
            //TaskView.DateCompleted = task.DateCompleted;
            return TaskView;
        }

        public static List<DevTask> OrderbyPriority(int projectId) {

            var tasks = db.DevTasks.OrderByDescending(d => d.Priority == Priority.Urgent).ToList();
            return tasks;



        }
    }

}