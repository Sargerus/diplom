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
    public class BacklogTasksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BacklogTasks
        public ActionResult Index()
        {
            var backlogTasks = db.BacklogTasks.Include(b => b.BacklogRef).Include(b => b.CreatedByFK);
            return View(backlogTasks.ToList());
        }

        // GET: BacklogTasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BacklogTask backlogTask = db.BacklogTasks.Find(id);
            if (backlogTask == null)
            {
                return HttpNotFound();
            }
            return View(backlogTask);
        }

        // GET: BacklogTasks/Create
        public ActionResult Create()
        {
            ViewBag.Backlog = new SelectList(db.Backlogs, "BacklogId", "Description");
            ViewBag.CreatedBy = new SelectList(db.Users, "Id", "Email");

            BacklogTask task = new BacklogTask();
            task.CreatedBy = db.Users.ToList().Find(g => g.UserName == User.Identity.Name).Id;
            return View(task);
        }

        // POST: BacklogTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskId,Description,CreatedBy,CreatedOn,HoursEstiimated,HoursDone,Backlog")] BacklogTask backlogTask)
        {
            if (ModelState.IsValid)
            {
                db.BacklogTasks.Add(backlogTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Backlog = new SelectList(db.Backlogs, "BacklogId", "Description", backlogTask.Backlog);
            ViewBag.CreatedBy = new SelectList(db.Users, "Id", "Email", backlogTask.CreatedBy);
            return View(backlogTask);
        }

        // GET: BacklogTasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BacklogTask backlogTask = db.BacklogTasks.Find(id);
            if (backlogTask == null)
            {
                return HttpNotFound();
            }
            ViewBag.Backlog = new SelectList(db.Backlogs, "BacklogId", "Description", backlogTask.Backlog);
            ViewBag.CreatedBy = new SelectList(db.Users, "Id", "Email", backlogTask.CreatedBy);
            return View(backlogTask);
        }

        // POST: BacklogTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskId,Description,CreatedBy,CreatedOn,HoursEstiimated,HoursDone,Backlog")] BacklogTask backlogTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(backlogTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Backlog = new SelectList(db.Backlogs, "BacklogId", "Description", backlogTask.Backlog);
            ViewBag.CreatedBy = new SelectList(db.Users, "Id", "Email", backlogTask.CreatedBy);
            return View(backlogTask);
        }

        // GET: BacklogTasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BacklogTask backlogTask = db.BacklogTasks.Find(id);
            if (backlogTask == null)
            {
                return HttpNotFound();
            }
            return View(backlogTask);
        }

        // POST: BacklogTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BacklogTask backlogTask = db.BacklogTasks.Find(id);
            db.BacklogTasks.Remove(backlogTask);
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
