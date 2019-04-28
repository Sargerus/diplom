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
        public ActionResult Index(List<string> users, int? projectid)
        {


            //TODO: validation for users

            List<StatListViewModel> entityset = new List<StatListViewModel>();
            List<UserAndProject> userWithProjects = new List<UserAndProject>();

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

            int dayOfWeek = ((int)DateTime.Today.DayOfWeek - 1);
            var Monday = DateTime.Today.AddDays(-dayOfWeek);
            var Sunday = Monday.AddDays(6);

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
                userWithProjects = db.Users.Where(g => users.Contains(g.Id)).Select(g => new UserAndProject { User = g.Id, projects = g.Projects }).Where(g => g.projects.All(f => f.ProjectId == projectid)).ToList();
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
                                }
                            }
                        }
                    }

                    entityset.Add(new StatListViewModel
                    {
                        StartDayOfWeek = Monday,
                        EndDayOfWeek = Monday.AddDays(6),
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