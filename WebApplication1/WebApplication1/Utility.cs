using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1
{
    public class Utility
    {

        private static ApplicationDbContext db = new ApplicationDbContext();

        public static bool isUserLead { get; set; }

        public static bool isUserManager { get; set; }
        

        public static void DefineUserRolesForCurrentProject(int projectid, string username)
        {
            var project_user = db.Project_User.Find(projectid, db.Users.Where(g => g.UserName == username).First().Id);
            isUserLead = project_user.isLead;
            isUserManager = project_user.isManager;
        }
    }
}