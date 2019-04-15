using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class BacklogsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public string CalculateBacklogDoneFor(int id)
        {
            var backlog = db.Backlogs.Find(id);

            int hoursEstimated = backlog.Tasks.ToList().Sum(g =>
            {
                return g.HoursEstimated.Equals(null) ? 0 : g.HoursEstimated.Value;
            });

            int hoursDone = backlog.Tasks.ToList().Sum(g =>
            {
                return g.HoursDone.Equals(null) ? 0 : g.HoursDone.Value;
            });

            return (hoursDone.Equals(0) ? "0" : (hoursDone / hoursEstimated).ToString());
        }

        // GET: Backlogs
        public ActionResult Index()
        {
            var abc = db.Backlogs.ToList();
            var userid = db.Users.ToList().Find(u => u.UserName == User.Identity.Name).Id;
            if (User.Identity.Name.Equals("admin@admin.ru"))
            {
                return View(db.Backlogs.ToList());
            }
            return View(db.Backlogs.Where(g => g.CreatedBy.Equals(userid)).ToList());
        }

        // GET: Backlogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Backlog backlog = db.Backlogs.Find(id);
            if (backlog == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectDescription = db.Projects.Find(backlog.Project).ProjectDescription.ToString();
            return View(backlog);
        }

        // GET: Backlogs/Create
        public ActionResult Create(string projectId)
        {
            ViewBag.BacklogType = new SelectList(db.BacklogTypes.ToList(), "Type", "Type", "Backlog");
            ViewBag.BacklogState = new SelectList(db.BacklogStates.ToList(), "State", "State", "In Proccess");
            ViewBag.Projects = new SelectList(db.Projects.ToList(), "ProjectId", "ProjectDescription");

            Backlog backlog = new Backlog();
            backlog.CreatedBy = db.Users.ToList().Find(g => g.UserName == User.Identity.Name).Id;
            backlog.Project = Convert.ToInt32(projectId);
            backlog.ProjectDescription = db.Projects.First(e => e.ProjectId == backlog.Project).ProjectDescription;

            return View(backlog);
        }

        // POST: Backlogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Backlog backlog)
        {
            if (ModelState.IsValid)
            {
                db.Backlogs.Add(backlog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(backlog);
        }

        // GET: Backlogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Backlog backlog = db.Backlogs.Find(id);
            if (backlog == null)
            {
                return HttpNotFound();
            }

            // ViewBag.BacklogState = new SelectList(db.BacklogStates.ToList(), "State", "State",);
            //ViewData["BacklogState"] = new SelectList(db.BacklogStates.ToList(), "State", "State", new BacklogState { State = "In Progress" });
            //SelectList sellist = new SelectList((IEnumerable<BacklogState>)db.BacklogStates.ToList(), "State", "State");
            var States = db.BacklogStates.ToList();

            ViewBag.BacklogState = new SelectList(db.BacklogStates.ToList(), "State", "State", "In Proccess");
            ViewBag.selectedState = States.IndexOf(States.Where(g => g.State.Equals(backlog.BacklogState)).Select(g => g).First());

            ModelState.Clear();
            return View(backlog);
        }

        // POST: Backlogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Backlog backlog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(backlog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(backlog);
        }

        // GET: Backlogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Backlog backlog = db.Backlogs.Find(id);
            if (backlog == null)
            {
                return HttpNotFound();
            }
            return View(backlog);
        }

        // POST: Backlogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Backlog backlog = db.Backlogs.Find(id);
            db.Backlogs.Remove(backlog);
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
