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
    public class ReportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reports
        public ActionResult Index()
        {
            //var reports = db.Reports.Include(r => r.TaskFK).Include(r => r.ReportedByFK);
            //return View(reports.ToList());
            return View();
        }

        // GET: Reports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // GET: Reports/Create
        public ActionResult Create(string id)
        {
            //ViewBag.Backlog = new SelectList(db.Backlogs, "BacklogId", "Description");
            //ViewBag.ReportedBy = new SelectList(db.Users, "Id", "Email");
            Report report = new Report();
            //report.Task = Convert.ToInt32(id);
            //ViewBag.TaskDesc = db.BacklogTasks.Find(Convert.ToInt32(id)).Description.ToString(); 
            report.ReportedBy = db.Users.ToList().Find(g => g.UserName == User.Identity.Name).Id;
            return View(report);
        }

        // POST: Reports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Report report)
        {
            if (ModelState.IsValid)
            {
                db.Reports.Add(report);

                //BacklogTask task = db.BacklogTasks.ToList().Where(g => g.TaskId == report.Task).First();
                //task.Reports.Add(report);
                //task.HoursDone += report.HoursReported;

                //db.BacklogTasks.Find(report.TaskFK.TaskId).HoursDone += report.HoursReported;
                db.SaveChanges();
                return RedirectToAction("Index", "Backlogs");
            }

            //ViewBag.Backlog = new SelectList(db.Backlogs, "TaskId", "Description", report.Task);
            ViewBag.ReportedBy = new SelectList(db.Users, "Id", "Email", report.ReportedBy);
            return View(report);
        }

        // GET: Reports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            //ViewBag.Backlog = new SelectList(db.Backlogs, "TaskId", "Description", report.Task);
            ViewBag.ReportedBy = new SelectList(db.Users, "Id", "Email", report.ReportedBy);
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Report report)
        {
            if (ModelState.IsValid)
            {
                db.Entry(report).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","BacklogTasks");
            }
            //ViewBag.Backlog = new SelectList(db.Backlogs, "TaskId", "Description", report.Task);
            ViewBag.ReportedBy = new SelectList(db.Users, "Id", "Email", report.ReportedBy);
            return View(report);
        }

        // GET: Reports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Report report = db.Reports.Find(id);
            db.Reports.Remove(report);
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
