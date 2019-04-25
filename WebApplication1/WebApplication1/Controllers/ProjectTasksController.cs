using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class ProjectTasksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public string CanAddTask()
        {
            string answer = "0";
            if(Utility.isUserLead == true || Utility.isUserManager == true)
            {
                answer = "1";
            }

            return answer;
        }

        public string DefineColorOfTask(int taskid, int projectid)
        {
            string color = "";
            ProjectTask task = db.ProjectTasks.Find(taskid, projectid);
            TimeSpan span = task.RequiredEndDate - DateTime.Now;
            double criticalValue = Convert.ToDouble(((span.Days + 1) * 8)) / Convert.ToDouble((task.TaskEstimated - task.TaskDone));
            if (criticalValue >= 2)
            {
                color = "indicator-blue";
            }
            else if (criticalValue >= 1 && criticalValue <= 2)
            {
                color = "indicator-orange";
            }
            else if (criticalValue < 1)
            {
                color = "indicator-red";
            }

            if (task.TaskDone == task.TaskEstimated)
            {
                color = "indicator-green";
            }

            return color;

        }
        // GET: ProjectTasks
        public ActionResult Index(int projectId)
        {

            Utility.DefineUserRolesForCurrentProject(projectId, User.Identity.Name);

            var project = db.Projects.ToList().Find(g => g.ProjectId == projectId);
            ViewBag.ProjectName = project.ProjectDescription;

            var tasks = db.ProjectTasks.Where(g => g.ProjectKey == projectId).Select(g => g).ToList();

            var vmtasks = SortTasks(tasks);
            return View(vmtasks);
        }

        public List<ProjectTaskViewModel> SortTasks(List<ProjectTask> tasks)
        {
            ProjectTaskViewModel vmtask = new ProjectTaskViewModel();
            List<ProjectTaskViewModel> outstanding = new List<ProjectTaskViewModel>();
            List<ProjectTaskViewModel> progress = new List<ProjectTaskViewModel>();
            List<ProjectTaskViewModel> havetime = new List<ProjectTaskViewModel>();
            List<ProjectTaskViewModel> done = new List<ProjectTaskViewModel>();

            foreach (var task in tasks)
            {
                switch (DefineColorOfTask(task.TaskKey, task.ProjectKey))
                {
                    case "indicator-red": { outstanding.Add(new ProjectTaskViewModel { projectTask = task, colorIndicator = "outstanding-color" }); break; }
                    case "indicator-green": { done.Add(new ProjectTaskViewModel { projectTask = task, colorIndicator = "done-color" }); break; }
                    case "indicator-orange": { progress.Add(new ProjectTaskViewModel { projectTask = task, colorIndicator = "progress-color" }); break; }
                    case "indicator-blue": { havetime.Add(new ProjectTaskViewModel { projectTask = task, colorIndicator = "havetime-color" }); break; }
                }

            }

            outstanding.AddRange(progress);
            outstanding.AddRange(havetime);
            outstanding.AddRange(done);

            return outstanding;
        }

        // GET: ProjectTasks/Details/5
        public ActionResult Details(int? taskid, int? projectid)
        {
            if (taskid == null || projectid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTask projectTask = db.ProjectTasks.Find(taskid, projectid);
            if (projectTask == null)
            {
                return HttpNotFound();
            }
            return View(projectTask);
        }

        // GET: ProjectTasks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskKey,ProjectKey,Description,RequiredStartDate,RequiredEndDate,TaskDone,TaskEstimated")] ProjectTask projectTask)
        {
            if (ModelState.IsValid)
            {
                db.ProjectTasks.Add(projectTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(projectTask);
        }

        // GET: ProjectTasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTask projectTask = db.ProjectTasks.Find(id);
            if (projectTask == null)
            {
                return HttpNotFound();
            }
            return View(projectTask);
        }

        // POST: ProjectTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskKey,ProjectKey,Description,RequiredStartDate,RequiredEndDate,TaskDone,TaskEstimated")] ProjectTask projectTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(projectTask);
        }

        // GET: ProjectTasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTask projectTask = db.ProjectTasks.Find(id);
            if (projectTask == null)
            {
                return HttpNotFound();
            }
            return View(projectTask);
        }

        // POST: ProjectTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectTask projectTask = db.ProjectTasks.Find(id);
            db.ProjectTasks.Remove(projectTask);
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
