using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public string CanManageProject(int projectid)
        {
            return Utility.CanManageProject(projectid);
        }

        public string GetUser(int projectid)
        {
            if (Utility.User == null)
            {
                Utility.DefineUserRolesForCurrentProject(projectid, User.Identity.Name);
            }

            return Utility.User;
        }


        // GET: Projects
        public ActionResult Index()
        {
            var myprojects = db.Project_User.Where(g => g.User.Equals(Utility.User)).Join(db.Projects,
                                                                                          project_user => project_user.ProjectId,
                                                                                          project => project.ProjectId,
                                                                                          (fproject_user, project) => project);
            return View(myprojects);
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            int k = 0;
            project.Team = project.UserAssigned.Count;
            //db.Backlogs.ToList().ForEach(backlog => backlog.Tasks.ToList().ForEach(task => k += (int)(task.HoursEstimated.Value)));
            project.TotalEstimate = k;

            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            Project project = new Project();

            project.CreatedBy = db.Users.ToList().Find(g => g.UserName == User.Identity.Name).Id;
            project.CreatedOn = DateTime.Now;
            ViewBag.Users = new SelectList(db.Users.ToList(), "Id", "UserName");
            return View(project);
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project project, string[] managers)
        {
            
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            CultureInfo provider = CultureInfo.InvariantCulture;
            ViewBag.Users2 = new SelectList(db.Users.ToList(), "Id", "UserName");
           // var date = project.StartDate.ToShortDateString();
           // project.StartDate = DateTime.ParseExact(date, "yyyy-MM-dd", provider);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Project project) //[Bind(Include = "ProjectId,ProjectDescription,HeadOfProject")]
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
