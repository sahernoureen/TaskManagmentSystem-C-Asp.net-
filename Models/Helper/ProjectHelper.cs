﻿using System;
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
            ProjectHelper.CreateProjectManagerNotificationForCompleteion(project);
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


        public static void CreateProjectManagerNotification(Project proj)
        {
            TimeSpan diff =  (proj.Deadline).Subtract(DateTime.Now);
            int daysleft = (int)diff.TotalDays;
            var anyRemainingTask = proj.DevTasks.Any(t => t.Status != Status.Completed);

            if (daysleft <=1 && anyRemainingTask == true)
            {
                var CheckNotifi = proj.NotificationManagers.Any(n => n.Comment == "The Project exceeds Deadline without completetion");
                if (CheckNotifi == false)
                {
                    var dueTask = proj.DevTasks.FirstOrDefault(d => d.Status != Status.Completed && daysleft <= 1);
                    NotificationManager NotifiMngr1 = new NotificationManager(proj.Id, DateTime.Now, proj.UserId, "The Project exceeds Deadline without completetion");
                    db.NotificationManagers.Add(NotifiMngr1);
                    proj.NotificationManagers.Add(NotifiMngr1);
                    proj.User.NotificationManager.Add(NotifiMngr1);
                    db.SaveChanges();
                }
            }
          
        }

        public static void CreateProjectManagerNotificationForCompleteion(Project proj)
        {
            var checkNotification = proj.NotificationManagers.Any(n => n.Comment == "The Project is Completed");
            if(checkNotification == false)
            {
                NotificationManager Notify = new NotificationManager(proj.Id, DateTime.Now, proj.UserId, "The Project is Completed");
                db.NotificationManagers.Add(Notify);
                proj.NotificationManagers.Add(Notify);
                proj.User.NotificationManager.Add(Notify);
                db.SaveChanges();
            }
        }
    }

  
}
