using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TaskManagementSystem.Models;
namespace TaskManagementSystem.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        //get
        public ActionResult AddUser()
        {
            ViewBag.AddStatus = null;
            return View();
        }
        //get
        public ActionResult AdminHomePage()
        {
            return View();
        }
        //post
        [HttpPost]
        public ActionResult AddUser(RegisterViewModel user)
        {
            ViewBag.AddStatus = null;
            if (ModelState.IsValid) 
            {
                ViewBag.AddStatus = AdminHelper.addUser(user);
                return View();
            }
            return View();
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
            var users = AdminHelper.getAllUsersInfo();
            var roles = AdminHelper.getAllRoles();
            ViewBag.userId = new SelectList(users, "Id", "Name");
            ViewBag.role = new SelectList(roles, "Name", "Name");
            return View();
        }
        //post
        [HttpPost]
        public ActionResult AddUserToRole(string userId, string role)
        {
            var users = AdminHelper.getAllUsersInfo();
            var roles = AdminHelper.getAllRoles();
            ViewBag.userId = new SelectList(users, "Id", "Name");
            ViewBag.role = new SelectList(roles, "Name", "Name");
            var result = AdminHelper.addUserToRole(userId, role);
            return RedirectToAction("GetAllUsersInfo");
        }


       

        //get
        public ActionResult GetAllUsersInfo()
        {
            var result = AdminHelper.getAllUsersInfo();
            return View(result);
        }

        //get
        public ActionResult GetAllRoles()
        {
            var result = AdminHelper.getAllRoles();
            return View(result);
        }

        public ActionResult DeleteUser(string userId) 
        {
            var result = AdminHelper.deleteUser(userId);
            if (result) 
            { 
                return RedirectToAction("GetAllUsers");
            }
            return View();
        }

        //get
        public ActionResult RemoveUserFromRole(string userId, string role) 
        {
            AdminHelper.removeUserFromRole(userId, role);
            return RedirectToAction("GetAllUsersInfo");
        } 

    }
}