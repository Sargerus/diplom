using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class StatListController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StatList
        public ActionResult Index(string usersString, int? projectid, DateTime? today)
        {

            //TODO: validation for users

            List<StatListViewModel> entityset = new List<StatListViewModel>();
            List<UserAndProject> userWithProjects = new List<UserAndProject>();
            List<string> users = new List<string>();

            int dayOfWeek = (today == null ? ((int)DateTime.Today.DayOfWeek - 1) : (int)today.Value.DayOfWeek - 1);
            //int dayOfWeek = ((int)DateTime.Today.DayOfWeek - 1);
            var Monday = (today == null ? DateTime.Today.AddDays(-dayOfWeek) : today.Value.AddDays(-dayOfWeek));
            var Sunday = Monday.AddDays(6);

            ViewBag.Today = DateTime.Today;
            ViewBag.StartDayOfWeek = (Monday == null) ? DateTime.Today : Monday;
            ViewBag.EndDayOfWeek = (Monday == null) ? DateTime.Today.AddDays(6) : Monday.AddDays(6);
            ViewBag.ProjectName = (from g in db.Projects
                                  where g.ProjectId == projectid.Value
                                  select g.ProjectDescription).First();

            if (usersString != null)
            {
                users = System.Web.Helpers.Json.Decode<List<string>>(usersString);

                for (int i = 0; i < users.Count; i++)
                {
                    if(i == 0)
                    {
                        ViewBag.ViewedUser += users[i];
                    } else
                    {
                        ViewBag.ViewedUser += "," + users[i];
                    }
                }

            }
            if (users == null)
            {
                return View();
            }

            if (users != null)
            {
                if (!users.Any())
                {
                    return View();
                }
            }

            if (projectid == null)
            {
                userWithProjects = db.Users.Where(g => users.Contains(g.Id)).Select(g => new UserAndProject { User = g.Id, projects = g.Projects }).ToList();
                //userWithProjects = db.Users.Where(g => users.Contains(g.Id)).Select(g =>
                //{
                //    g.Projects.Select(f => f.Tasks.Select(j => j.Reports.Where(r =>
                //                                                                 (DateTime.Compare(r.ReportedOn.Date, Monday.Date) > 0 || DateTime.Compare(r.ReportedOn.Date, Monday.Date) == 0)
                //                                                              && (DateTime.Compare(r.ReportedOn.Date, Sunday.Date) < 0 || DateTime.Compare(r.ReportedOn.Date, Sunday.Date) == 0)).ToList().Count >= 0));
                //    return new UserAndProject { User = g.Id, projects = g.Projects };

                //}).ToList();

                //userWithProjects = db.Users.Where(g => users.Contains(g.Id)).Select(g => { g.Projects.All()

            }
            else
            {
                ViewBag.ProjectId = projectid.Value;
                //userWithProjects = db.Users.Where(g => users.Contains(g.Id)).Select(g => new UserAndProject { User = g.Id, projects = g.Projects }).Where(g => g.projects.All(f => f.ProjectId == projectid)).ToList();

                var userWithProjectsBuf = (from u in db.Users
                                           join p_u in db.Project_User on u.Id equals p_u.User
                                           join p in db.Projects on p_u.ProjectId equals p.ProjectId
                                           where users.Contains(u.Id) && p_u.ProjectId == projectid
                                           select new { User = u.Id, project = p }).ToList();

                foreach (var g in userWithProjectsBuf)
                {
                    try
                    {
                        var userExist = userWithProjects.First(f => f.User == g.User);
                        userWithProjects.First(f => f.User == g.User).projects.Add(g.project);
                    }
                    catch
                    {
                        userWithProjects.Add(new UserAndProject { User = g.User, projects = new List<Project> { g.project } });
                    }

                }
            }

            var orderedUserWithProjects = userWithProjects.OrderBy(g => g.User).Select(f => { f.projects.OrderBy(k => k.ProjectId); return f; });

            List<ProjectTask> pt = new List<ProjectTask>();

            foreach (var user in userWithProjects)
            {
                foreach (var project in user.projects)
                {
                    pt.Clear();

                    foreach (var task in project.Tasks)
                    {
                        if (task.UserAssigned.Equals(user.User))
                        {
                            foreach (var report in task.Reports)
                            {
                                if ((DateTime.Compare(report.ReportedOn.Date, Monday.Date) > 0 || DateTime.Compare(report.ReportedOn.Date, Monday.Date) == 0)
                                && (DateTime.Compare(report.ReportedOn.Date, Sunday.Date) < 0 || DateTime.Compare(report.ReportedOn.Date, Sunday.Date) == 0))
                                {
                                    pt.Add(task);
                                    break;
                                }
                            }
                        }
                    }

                    entityset.Add(new StatListViewModel
                    {
                        //StartDayOfWeek = Monday,
                        //EndDayOfWeek = Monday.AddDays(6),
                        ProjectName = project.ProjectDescription,
                        UserName = user.User,
                        Tasks = pt
                    });
                }
            }

            return View(entityset);
        }
    }
}