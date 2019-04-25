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
    public class Project_UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Project_User
        public ActionResult Index()
        {
            return View(db.Project_User.ToList());
        }

        // GET: Project_User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project_User project_User = db.Project_User.Find(id);
            if (project_User == null)
            {
                return HttpNotFound();
            }
            return View(project_User);
        }

        // GET: Project_User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Project_User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectId,isPM,isLead,isDev")] Project_User project_User)
        {
            if (ModelState.IsValid)
            {
                db.Project_User.Add(project_User);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project_User);
        }

        // GET: Project_User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project_User project_User = db.Project_User.Find(id);
            if (project_User == null)
            {
                return HttpNotFound();
            }
            return View(project_User);
        }

        // POST: Project_User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectId,isPM,isLead,isDev")] Project_User project_User)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project_User).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project_User);
        }

        // GET: Project_User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project_User project_User = db.Project_User.Find(id);
            if (project_User == null)
            {
                return HttpNotFound();
            }
            return View(project_User);
        }

        // POST: Project_User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project_User project_User = db.Project_User.Find(id);
            db.Project_User.Remove(project_User);
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
