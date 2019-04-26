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

        private static bool isDefined;

        public static String User { get; private set; }

        public static bool isUserLead { get; private set; }

        public static bool isUserManager { get; private set; }
        
        public static String myLead { get; private set; }

        public static String myManager { get; private set; }

        public static void SetUser(string email)
        {
            User = db.Users.Where(g => g.Email.Equals(email)).Select(g => g.Id).First();
        }

        public static void DefineUserRolesForCurrentProject(int projectid, string username)
        {

            var project_user = db.Project_User.Find(projectid, db.Users.Where(g => g.UserName == username).First().Id);
            isUserLead = project_user.isLead;
            isUserManager = project_user.isManager;
            myLead = project_user.myLead;
            myManager = project_user.myManager;
            //User = project_user.User;

            isDefined = true;
        }

        public static string CanAddTask()
        {
            if (!isDefined) { return ""; }
            string answer = "0";
            if (isUserLead || isUserManager)
            {
                answer = "1";
            }

            return answer;
        }

        public static List<ApplicationUser> GetMyTeam()
        {
            if (!isDefined) { return null; }

            return db.Project_User.Where(g => g.myLead.Equals(User)).Join(db.Users,
                                                                           f => f.User,
                                                                           s => s.Id, (f, s) => s).ToList();
        }
    }
}