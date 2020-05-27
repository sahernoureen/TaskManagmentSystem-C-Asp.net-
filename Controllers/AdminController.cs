using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.Models;
namespace TaskManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        //get
        public ActionResult AddUser()
        {
            return View();
        }
        //post
        [HttpPost]
        public ActionResult AddUser(string email)
        {
            var result = AdminHelper.addUser(email);
            return View(result);
        }
        //get
        public ActionResult AddRole()
        {
            return View();
        }
        //post
        [HttpPost]
        public ActionResult AddRole(string roleName)
        {
            var result = AdminHelper.addRole(roleName);
            return View(result);
        }
        //get
        public ActionResult AddUserToRole()
        {
            return View();
        }
        //post
        [HttpPost]
        public ActionResult AddUserToRole(string userId, string role)
        {
            var result = AdminHelper.addUserToRole(userId, role);
            return View(result);
        }
    }
}