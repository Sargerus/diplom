﻿using System;
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

        private Random random;

        public int RandomNumber()
        {
            if(random == null)
            {
                random = new Random();
            }

            return random.Next(1, 100000000);
        }

        [HttpPost]
        public ActionResult SetInvisible(int? taskid, int? projectid)
        {
            try
            {
                db.ProjectTasks.Find(taskid, projectid).notVisible = true;
                db.SaveChanges();
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            return Json("{\"message\":\"success\"}", "application/json");

        }

        public string GetUser(int projectid)
        {
            if (Utility.User == null)
            {

                Utility.DefineUserRolesForCurrentProject(projectid, User.Identity.Name);

            };

            return Utility.User;

        }

        public string CanAddTask(int projectid, string foruser)
        {
            return Utility.CanAddTask(projectid, foruser);
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

            if (task.TaskDone >= task.TaskEstimated)
            {
                color = "indicator-green";
            }

            return color;

        }
        // GET: ProjectTasks
        public ActionResult Index(int? projectId, String user)
        {
            //Define my role on this project
            Utility.DefineUserRolesForCurrentProject(projectId.Value, User.Identity.Name);

            //if no user come, i'm viewing my tasks
            if (user == null) { user = Utility.User; }

            var project = db.Projects.ToList().Find(g => g.ProjectId == projectId);
            ViewBag.ProjectName = project.ProjectDescription;

            var tasks = db.ProjectTasks.Where(g => g.ProjectKey == projectId && g.UserAssigned.Equals(user) && g.notVisible.Equals(false)).Select(g => g).ToList();

            var vmtasks = SortTasks(tasks);

            ViewBag.ViewedUser = user;
            ViewBag.ProjectId = projectId;
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
                    case "indicator-red": { outstanding.Add(new ProjectTaskViewModel { projectTask = task, EndDate = task.RequiredEndDate.ToShortDateString(), colorIndicator = "outstanding-color", TaskDoneFor = Convert.ToDouble((Convert.ToDouble(task.TaskDone) / Convert.ToDouble(task.TaskEstimated)).ToString("00.0")) }); break; }
                    case "indicator-green": { done.Add(new ProjectTaskViewModel { projectTask = task, EndDate = task.RequiredEndDate.ToShortDateString(), colorIndicator = "done-color", TaskDoneFor = Convert.ToDouble((Convert.ToDouble(task.TaskDone) / Convert.ToDouble(task.TaskEstimated)).ToString("00.0")) }); break; }
                    case "indicator-orange": { progress.Add(new ProjectTaskViewModel { projectTask = task, EndDate = task.RequiredEndDate.ToShortDateString(), colorIndicator = "progress-color", TaskDoneFor = Convert.ToDouble((Convert.ToDouble(task.TaskDone) / Convert.ToDouble(task.TaskEstimated)).ToString("00.0")) }); break; }
                    case "indicator-blue": { havetime.Add(new ProjectTaskViewModel { projectTask = task, EndDate = task.RequiredEndDate.ToShortDateString(), colorIndicator = "havetime-color", TaskDoneFor = Convert.ToDouble((Convert.ToDouble(task.TaskDone) / Convert.ToDouble(task.TaskEstimated)).ToString("00.0")) }); break; }
                }

            }

            outstanding.AddRange(progress);
            outstanding.AddRange(havetime);
            outstanding.AddRange(done);

            return outstanding;
        }

        // GET: ProjectTasks/Details/5
        public ActionResult Details(int? taskid, int? projectid, string taskofuser)
        {
            if (taskid == null || projectid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if(taskofuser == null)
            {
                taskofuser = Utility.User;
            }

            ProjectTask projectTask = db.ProjectTasks.Find(taskid, projectid);

            if (projectTask == null)
            {
                return HttpNotFound();
            }

            ViewBag.ViewedUser = taskofuser;
            return View(projectTask);
        }

        // GET: ProjectTasks/Create
        public ActionResult Create(int projectid, string vieweduser)
        {
            ProjectTask task = new ProjectTask();
            task.AssignedBy = Utility.User;
            task.UserAssigned = vieweduser;
            task.ProjectKey = projectid;

            ViewBag.ViewedUser = vieweduser;
            ViewBag.ProjectId = projectid;
            return View(task);
        }

        // POST: ProjectTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskKey,ProjectKey,Description,RequiredStartDate,RequiredEndDate,TaskDone,TaskEstimated,UserAssigned,ShortText")] ProjectTask projectTask)
        {
            if (ModelState.IsValid)
            {
                projectTask.TaskKey = db.ProjectTasks.ToList().Count + 1;
                db.ProjectTasks.Add(projectTask);

                db.Projects.Find(projectTask.ProjectKey).Tasks.Add(projectTask);

                db.SaveChanges();
                return RedirectToAction("Index", new { projectid = projectTask.ProjectKey, user = projectTask.UserAssigned });
            }

            return View(projectTask);
        }

        // GET: ProjectTasks/Edit/5
        public ActionResult Edit(int? taskid, int? projectid)
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

        // POST: ProjectTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskKey,ProjectKey,UserAssigned,ShortText,RequiredStartDate,RequiredEndDate,TaskDone,TaskEstimated")] ProjectTask projectTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { taskid = projectTask.TaskKey, projectid = projectTask.ProjectKey,taskofuser = projectTask.UserAssigned });
            }
            return View(projectTask);
        }

        // GET: ProjectTasks/Delete/5
        //public ActionResult Delete(int? taskid, int? projectid)
        //{
        //    if (taskid == null || projectid == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ProjectTask projectTask = db.ProjectTasks.Find(taskid, projectid);
        //    if (projectTask == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return RedirectToAction("Details", new { taskid = projectTask.TaskKey, projectid = projectTask.ProjectKey, taskofuser = projectTask.UserAssigned });
        //}

        // POST: ProjectTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int taskid, int projectid)
        {
            ProjectTask projectTask = db.ProjectTasks.Find(taskid, projectid);
            db.ProjectTasks.Remove(projectTask);
            db.SaveChanges();
            return RedirectToAction("Index", new { projectid = projectid, user = Utility.User });
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