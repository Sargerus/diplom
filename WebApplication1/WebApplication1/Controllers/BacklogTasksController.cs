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
    public class BacklogTasksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public string CalculateTaskDoneFor(int id)
        {
            var task = db.BacklogTasks.ToList().Find(g => g.TaskId.Equals(id));
            if (task.HoursDone.Equals(null) || task.HoursEstimated.Equals(null))
                return 0.ToString();
            if (task.HoursEstimated.Value.Equals(0))
                return 0.ToString();

            return Math.Round((Convert.ToDouble(task.HoursDone.Value) / Convert.ToDouble(task.HoursEstimated.Value)) * 100 ).ToString();
        }

        // GET: BacklogTasks
        public ActionResult Index()
        {
            var backlogTasks = db.BacklogTasks.Include(b => b.BacklogRef).Include(b => b.CreatedByFK);
            List<BacklogTask> entities = new List<BacklogTask>();
            var userid = db.Users.ToList().Find(u => u.UserName == User.Identity.Name).Id;
            if (User.Identity.Name.Equals("admin@admin.ru"))
            {
                 entities = db.BacklogTasks.ToList();
            }
                entities = db.BacklogTasks.Where(g => g.CreatedBy.Equals(userid)).ToList();

           // var entities = backlogTasks.ToList();
            foreach(var entity in entities)
            {
                foreach(var report in entity.Reports)
                {
                    if (entity.HoursDone == null)
                        entity.HoursDone = new int();
                    entity.HoursDone += report.HoursReported;
                }
            }
            return View(entities);
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
            foreach(var report in backlogTask.Reports)
            {
                backlogTask.HoursDone += report.HoursReported;   
            }
            return View(backlogTask);
        }

        // GET: BacklogTasks/Create
        public ActionResult Create(string id)
        {
            var userid = db.Users.ToList().Find(u => u.UserName == User.Identity.Name).Id;
            ViewBag.Backlog = new SelectList(db.Backlogs.Where(g => g.CreatedBy.Equals(userid)), "BacklogId", "Description");
            ViewBag.CreatedBy = new SelectList(db.Users, "Id", "Email");

            BacklogTask task = new BacklogTask();
            task.CreatedBy = db.Users.ToList().Find(g => g.UserName == User.Identity.Name).Id;
            task.Backlog = Convert.ToInt32(id);
            if(id != null)
            ViewBag.BacklogDesc = db.Backlogs.Find(Convert.ToInt32(id)).BacklogDescription;

            return View(task);
        }

        // POST: BacklogTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([System.Web.Http.FromBody]BacklogTask backlogTask)
        {
            if (backlogTask.CreatedBy == null || backlogTask.Equals(String.Empty))
            {
                backlogTask.CreatedBy = db.Users.ToList().Find(g => g.UserName.Equals(User.Identity.Name)).Id;
                ModelState.Clear();
            }

            if (ModelState.IsValid || TryValidateModel(backlogTask))
            {

                db.BacklogTasks.Add(backlogTask);
                db.Backlogs.Find(backlogTask.Backlog).Tasks.Add(backlogTask);
                db.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
                //return RedirectToAction("Index");
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
