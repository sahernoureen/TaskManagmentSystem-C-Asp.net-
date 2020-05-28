using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models.Helper
{
    public class UserInfoHolder
    {
        public string Name { set; get; }

        public double Salary { set; get; }
        public string Id { set; get; }
        public List<IdentityRole> RolesInfo = new List<IdentityRole>();
    }
}