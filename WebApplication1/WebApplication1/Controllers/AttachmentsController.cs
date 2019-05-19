using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AttachmentsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        public JsonResult Upload(int projectid, int taskid)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i]; //Uploaded file
                                                            //Use the following properties to get file's name, size and MIMEType
                int fileSize = file.ContentLength;
                string fileName = file.FileName;
                string mimeType = file.ContentType;
                System.IO.Stream fileContent = file.InputStream;
                Utility.CheckPathExist(projectid, taskid);
                file.SaveAs(Path.Combine(Environment.CurrentDirectory, @"Attachments\" + projectid + @"\" + taskid + @"\" + fileName));

                //Attacments attacment = new Attacments();
                //attacment.AttacmentId = (from g in db.Attacments
                //                         where g.ProjectId == projectid && g.TaskId == taskid
                //                         select g).Count() + 1;
                //attacment.CreatedBy = Utility.User;
                //attacment.CreatedOn = DateTime.Now;
                //attacment.Description = fileName;
                //attacment.PathToFile = Path.Combine(Environment.CurrentDirectory, @"Attachments\" + projectid + @"\" + taskid + @"\" + fileName);
                //attacment.ProjectId = projectid;
                //attacment.TaskId = taskid;

                db.Attacments.Add(new Attacments
                {
                    AttacmentId = (from g in db.Attacments
                                   where g.ProjectId == projectid && g.TaskId == taskid
                                   select g).Count() + 1,
                    CreatedBy = Utility.User,
                    CreatedOn = DateTime.Now,
                    Description = fileName,
                    PathToFile = Path.Combine(Environment.CurrentDirectory, @"Attachments\" + projectid + @"\" + taskid + @"\" + fileName),
                    ProjectId = projectid,
                    TaskId = taskid

                });

            }
            db.SaveChanges();
            return Json("Uploaded " + Request.Files.Count + " files");
    }

}
}