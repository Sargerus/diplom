using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class RecordController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Record
        public ActionResult Index(int? days)
        {
            var currentuser = db.Users.ToList().Find(g => g.UserName == User.Identity.Name);

            //try
            //{
            //    List<Report> records = new List<Report>();
            //    if(currentuser.Id == "administrator")
            //    {
            //        records = db.Reports.ToList();
            //    } else
            //    records = currentuser.Reports.ToList();
            //    if (records.Count == 0)
            //    {
            //        return View(new List<Report>());
            //    }
            //    else
            //    {
            //        return View(records.ToList().Where(g => g.ReportedOn > new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - (int)days)).OrderBy(g => g.ReportedOn));
            //    }
            //}
            //catch
            //{
            //    return View(new List<Report>());
            //}
            return View();
        }

        // GET: Record/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Record/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Record/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult CreateRecords(int? days)
        {
            var currentuser = db.Users.ToList().Find(g => g.UserName == User.Identity.Name);

            //try
            //{
            //    var records = currentuser.Reports;
            //    if (records.Count == 0)
            //    {
            //        return View(new List<Report>());
            //    }
            //    else
            //    {
            //        return View(records.ToList().Where(g => g.ReportedOn > new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day - (int)days)).OrderBy(g => g.ReportedOn));
            //    }
            //}
            //catch
            //{
            //    return View(new List<Report>());
            //}
            return View();
        }
    }
}
