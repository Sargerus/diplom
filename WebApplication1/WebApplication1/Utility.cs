using System;
using System.Collections.Generic;
using System.IO;
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

        public static bool CheckPathExist(int projectid, int taskid)
        {
            try
            {
                if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, @"Attachments\" + projectid + @"\" + taskid)))//("\\Attachments\\" + projectid + "\\" + taskid))
                {
                    if(!Directory.Exists(Path.Combine(Environment.CurrentDirectory, @"Attachments\" + projectid)))//Exists("\\Attachments\\" + projectid))
                    {
                        var asd = Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, @"Attachments\" + projectid));
                    }
                    if(!Directory.Exists(Path.Combine(Environment.CurrentDirectory, @"Attachments\" + projectid + @"\" + taskid)))
                    {
                        var dsa = Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, @"Attachments\" + projectid + @"\" + taskid));
                    }
                    
                }
            }
            catch
            {
                return false;
            }

            return true;


        }

        public static string CanReport(int taskid, int projectid)
        {
            string answer = "0";

            var user_1 = from g in db.ProjectTasks
                         where g.TaskKey == taskid && g.ProjectKey == projectid && g.UserAssigned == Utility.User
                         select g;

            if (user_1.Any())
            {
                answer = "1";
            }

            return answer;
        }

        public static bool CheckIfLead(string leadid, int projectid)
        {
            bool answer = false;

            var lead = db.Project_User.Find(projectid, leadid);
            if (lead == null)
            {
                answer = false;
            }
            else
            {
                if (lead.isLead || lead.isManager)
                {
                    answer = true;
                }
            }

            return answer;
        }

        public static string CanManageProject(int projectid)
        {
            string answer = string.Empty;

            if (db.Project_User.Where(g => g.ProjectId.Equals(projectid) && g.User.Equals(User) && g.isManager.Equals(true)).Any())
            {
                answer = "1";
            }
            else
            {
                answer = "0";
            }

            return answer;

        }

        public static void SetUser(string username)
        {
            User = username;
            //User = db.Users.Where(g => g.UserName.Equals(email)).Select(g => g.Id).First();
        }

        public static void DefineUserRolesForCurrentProject(int projectid, string username)
        {

            var project_user = db.Project_User.Find(projectid, db.Users.Where(g => g.UserName == username).First().Id);
            isUserLead = project_user.isLead;
            isUserManager = project_user.isManager;
            myLead = project_user.myLead;
            myManager = project_user.myManager;
            User = project_user.User;

            isDefined = true;
        }

        public static string CanAddTask(int projectid, string foruser)
        {
            if (!isDefined) { return ""; }
            string answer = "0";

            if(db.Project_User.Find(projectid,Utility.User).isManager == true)
            {
                answer = "1";
            }
            var dependentUser = db.Project_User.Where(g => g.ProjectId.Equals(projectid) && (g.User.Equals(foruser) && (g.myLead.Equals(User))));
            if (dependentUser.Any())
            {
                answer = "1";
            }
            else if (foruser.Equals(Utility.User))
            {


                dependentUser = from g in db.Project_User
                                where (g.ProjectId == projectid && g.User.Equals(Utility.User)) && (g.isLead == true || g.isManager == true)
                                select g;

                if (dependentUser.Any())
                {
                    answer = "1";
                }
                else
                {
                    answer = "0";
                }

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