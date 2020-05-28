using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using TaskManagementSystem.Models.Helper;

namespace TaskManagementSystem.Models
{
    public static class AdminHelper
    {
        public static ApplicationDbContext db = new ApplicationDbContext();
        public static UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        public static RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
        //USER
        public static bool addUser(RegisterViewModel user)
        {

            if (userManager.FindByEmail(user.Email) == null)
            {
                ApplicationUser appUser = new ApplicationUser() { Email = user.Email, UserName = user.Email };
                userManager.Create(appUser, user.Password);
                return true;
            }
            return false;
        }


        public static bool deleteUser(string userId)
        {
            if (userManager.FindById(userId) != null)
            {
                var user = db.Users.Find(userId);
                userManager.Delete(user);
                return true;
            }
            return false;
        }
        public static List<UserInfoHolder> getAllUsersInfo()
        {
            var users = db.Users.ToList();

            var userInfo = new List<UserInfoHolder>();

            foreach (var u in users) 
            {
                var ui = new UserInfoHolder();
                ui.Id = u.Id;
                ui.Name = u.UserName;

                foreach(var r in u.Roles) 
                {
                    var role = db.Roles.Find(r.RoleId);
                    ui.RolesInfo.Add(role);
                }
                userInfo.Add(ui);
            }
            return userInfo.Where(u=>u.RolesInfo.All(r=>r.Name!="Admin")).ToList();
        }

        public static List<ApplicationUser> getAllUsers() 
        {
            return db.Users.ToList();
        }
        public static List<IdentityRole> getAllRoles()
        {
            return db.Roles.Where(r=>r.Name!="Admin").ToList();
        }
        public static ApplicationUser getUserById(string userId)
        {
            return db.Users.FirstOrDefault(x => x.Id == userId);
        }
        public static List<ApplicationUser> getAllUserExceptById(string userId)
        {
            return db.Users.Where(x => x.Id != userId).ToList();
        }
        //ADD ROLE TO USER
        public static bool addUserToRole(string userId, string role)
        {
            if (checkIfUserIsRole(userId, role))
            {
                return false;
            }
            else
            {
                userManager.AddToRole(userId, role);
                return true;
            }
        }
        //REMOVE ROLE TO USER
        public static bool removeUserFromRole(string userId, string role)
        {
            if (!checkIfUserIsRole(userId, role))
            {
                return false;
            }
            else
            {
                userManager.RemoveFromRole(userId, role);
                return true;
            }
        }
        //ADD ROLE
        public static bool addRole(string roleName)
        {
            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole { Name = roleName });
                return true;
            }
            return false;
        }
        //REMOVE ROLE
        public static void removeRole(string roleName)
        {
            if (roleManager.RoleExists(roleName))
            {
                roleManager.Delete(new IdentityRole { Name = roleName });
            }
        }
        //CHECKING
        public static bool checkIfUserIsRole(string userId, string role)
        {
            var result = userManager.IsInRole(userId, role);
            return result;
        }
        //+ addUser()
        //+ deleteUser()
        //+ updateUser()
        //+ addRole()
        //+ deleteRole()
        //+ updateRole()
        //+ assignRole()
    }
}