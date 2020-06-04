using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagementSystem.Models.ProjectClasses;

namespace TaskManagementSystem.Models
{
    public class TaskHelper
    {
        public static ApplicationDbContext db = new ApplicationDbContext();

        //USER 
        public static List<ApplicationUser> getAllDeveloperUserExceptProjectManagerId(string userId)
        {
            List<ApplicationUser> listDevelopers = new List<ApplicationUser>();
            foreach (var user in db.Users)
            {
                if (AdminHelper.checkIfUserIsRole(user.Id, "Developer") && user.Id != userId)
                {
                    listDevelopers.Add(user);
                }
            }
            return listDevelopers;
        }

        //GET TASK
        public static DevTask GetTask(int id)
        {
            return db.DevTasks.FirstOrDefault(x => x.Id == id);
        }

        public static NotificationDev GetNotificationForDeveloperoftask(int taskId)
        {
            return db.NotificationDevs.FirstOrDefault(n => n.DevTaskId == taskId);
        }
        public static List<NotificationManager> GetNotificationForManageroftask(int taskId)
        {
            return db.NotificationManagers.Where(n => n.TaskId == taskId).ToList();
        }

        public static List<DevTask> getAllTasks(int id)
        {
            return db.DevTasks.Where(t => t.ProjectId == id).ToList();
        }

        //ADD TASK
        public static void AddTask(int ProjectId, string Title, string Desc, Priority pr, Status Status, DateTime DeadLine, string DevId)
        {
            Project project1 = db.Projects.FirstOrDefault(p => p.Id == ProjectId);
            DevTask devTask = new DevTask(Title, Desc, pr, Status, DateTime.Now, DeadLine, project1.Id, DevId);
            project1.DevTasks.Add(devTask);
            db.SaveChanges();
            TaskHelper.CreateDeveloperNotification(devTask);
            db.SaveChanges();

        }


        //DELETE TASK
        public static void DeleteTask(int taskId)
        {
            var DevTask = GetTask(taskId);
            var NotificationTask = GetNotificationForDeveloperoftask(taskId);
            if (NotificationTask != null)
            {
                db.DevTasks.Remove(DevTask);
                db.NotificationDevs.Remove(NotificationTask);
            }

            var NotificationManger = GetNotificationForManageroftask(taskId);
            foreach (var Notify in NotificationManger)
            {
                db.NotificationManagers.Remove(Notify);
                db.SaveChanges();
            }

            db.SaveChanges();
        }

        //UPDATE TASK
        public static void UpdateTask(int TaskId, int ProjectId, string Title, string Desc, Priority pr, Status Status, DateTime? DeadLine)
        {
            var DevTask = GetTask(TaskId);
            DevTask.Title = Title;
            DevTask.ProjectId = ProjectId;
            DevTask.Description = Desc;
            DevTask.Priority = pr;
            DevTask.Status = Status;
            DevTask.Deadline = DeadLine ?? DevTask.Deadline;
            db.SaveChanges();
        }

        public static void AssignTask(int id, string developerId)
        {
            DevTask devTask = GetTask(id);
            devTask.DeveloperId = developerId;
            db.SaveChanges();
        }

        public static UpdateTaskViewModelForPM createTaskViewModel(int TaskId)
        {
            var task = GetTask(TaskId);
            UpdateTaskViewModelForPM TaskView = new UpdateTaskViewModelForPM();
            TaskView.TaskId = TaskId;
            TaskView.ProjectId = task.ProjectId;

            TaskView.Deadline = task.Deadline;
            TaskView.Description = task.Description;
            TaskView.Priority = task.Priority;
            TaskView.Status = task.Status;
            TaskView.Title = task.Title;
            return TaskView;
        }

        public static List<DevTask> OrderbyPriority(int projectId)
        {

            var tasks = db.DevTasks.OrderByDescending(d => d.Priority == Priority.Urgent).ToList();
            return tasks;
        }

        public static void CreateDeveloperNotification(DevTask newtask)
        {
            TimeSpan diff = (newtask.Deadline).Subtract(DateTime.Now);
            int daysleft = (int)diff.TotalDays;
            if (daysleft <= 1 && newtask.NotificationDevs == null)
            {
                NotificationDev notify = new NotificationDev("One Day left for this task", DateTime.Now, newtask.Id, newtask.DeveloperId);
                db.NotificationDevs.Add(notify);
                newtask.NotificationDevs = notify;
                newtask.Developer.NotificationDev.Add(notify);
                db.SaveChanges();
            }
        }

        public static void CreateBugNotification(DevTask task, Project project)
        {
            NotificationManager NotifiMngr = new NotificationManager(project.Id, task.Id, DateTime.Now, project.UserId, "Bug Found in task");
            db.NotificationManagers.Add(NotifiMngr);
            project.NotificationManagers.Add(NotifiMngr);
            db.SaveChanges();
        }
    }

}