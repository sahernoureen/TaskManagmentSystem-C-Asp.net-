using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace TaskManagementSystem.Models
{
    public static class AdminHelper
    {
        public static ApplicationDbContext db = new ApplicationDbContext();
        public static UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        public static RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

        //USER
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
        public static bool removeUserToRole(string userId, string role)
        {
            if (checkIfUserIsRole(userId, role))
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