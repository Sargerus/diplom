using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1
{
    public static class AppDbInitializer
    {


        public static void FillInitialValues()
        {

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                try
                {
                    var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
                    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                    // создаем две роли
                    var role1 = new IdentityRole { Name = "admin" };
                    var role2 = new IdentityRole { Name = "user" };

                    // добавляем роли в бд
                    roleManager.Create(role1);
                    roleManager.Create(role2);

                    ApplicationUser admin = new ApplicationUser
                    {

                        Email = "admin@admin.ru",
                        Id = "administrator",
                        UserName = "admin@admin.ru",
                    };

                    ApplicationUser user = new ApplicationUser
                    {

                        Email = "user@user.ru",
                        Id = "user",
                        UserName = "user@user.ru",
                    };

                    userManager.Create(admin, "!Kemp111");
                    userManager.Create(user, "!Kemp111");
                    userManager.AddToRole(admin.Id, role1.Name);
                    userManager.AddToRole(user.Id, role2.Name);

                    var project1 = context.Projects.Add(new Project
                    {
                        CreatedBy = "administrator",
                        CreatedOn = DateTime.Now,
                        ProjectDescription = "First project",
                        ProjectId = 1,
                        HeadOfProject = "administrator",
                        StartDate = DateTime.Now
                    });

                    context.Projects.Add(new Project
                    {
                        CreatedBy = "administrator",
                        CreatedOn = DateTime.Now,
                        ProjectDescription = "Second project",
                        ProjectId = 2,
                        HeadOfProject = "administrator",
                        StartDate = DateTime.Now
                    });

                    context.Projects.Add(new Project
                    {
                        CreatedBy = "administrator",
                        CreatedOn = DateTime.Now,
                        ProjectDescription = "Third project",
                        ProjectId = 3,
                        HeadOfProject = "administrator",
                        StartDate = DateTime.Now
                    });

                    var task = context.ProjectTasks.Add(new ProjectTask
                    {
                        ProjectKey = 1,
                        AssignedBy = "administrator",
                        UserAssigned = "user",
                        TaskEstimated = 24,
                        TaskDone = 0,
                        TaskKey = 1,
                        Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                        ShortText = "Change Fonts",
                        RequiredStartDate = DateTime.Now.AddDays(-2),
                        RequiredEndDate = DateTime.Now.AddDays(2)
                    });

                    project1.Tasks.Add(task);
                    user.Projects.Add(project1);

                    project1.Tasks.Add(context.ProjectTasks.Add(new ProjectTask
                    {
                        ProjectKey = 1,
                        AssignedBy = "administrator",
                        UserAssigned = "user",
                        TaskEstimated = 32,
                        TaskDone = 0,
                        TaskKey = 2,
                        ShortText = "Backend govno",
                        Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                        RequiredStartDate = DateTime.Now.AddDays(-2),
                        RequiredEndDate = DateTime.Now.AddDays(2)

                    }));

                    project1.Tasks.Add(context.ProjectTasks.Add(new ProjectTask
                    {
                        ProjectKey = 1,
                        AssignedBy = "administrator",
                        UserAssigned = "user",
                        TaskEstimated = 20,
                        TaskDone = 0,
                        TaskKey = 3,
                        ShortText = "Simple task",
                        Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                        RequiredStartDate = DateTime.Now,
                        RequiredEndDate = DateTime.Now.AddDays(-5)
                    }));

                    project1.Tasks.Add(context.ProjectTasks.Add(new ProjectTask
                    {
                        ProjectKey = 1,
                        AssignedBy = "administrator",
                        UserAssigned = "administrator",
                        TaskEstimated = 20,
                        TaskDone = 0,
                        TaskKey = 4,
                        ShortText = "Simple task",
                        Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                        RequiredStartDate = DateTime.Now,
                        RequiredEndDate = DateTime.Now.AddDays(-5)
                    }));

                    //task.Reports.Add(context.Reports.Add(new Report
                    //{
                    //    ReportId = 1,
                    //    Comment = "Govno",
                    //    ReportedBy = "user",
                    //    ReportedOn = DateTime.Now,
                    //    HoursReported = 4
                    //}));

                    //task.Reports.Add(context.Reports.Add(new Report
                    //{
                    //    ReportId = 2,
                    //    Comment = "Govno",
                    //    ReportedBy = "user",
                    //    ReportedOn = DateTime.Now,
                    //    HoursReported = 4
                    //}));

                    context.Project_User.Add(new Project_User
                    {
                        ProjectId = 1,
                        User = "administrator",
                        isLead = true,
                        isManager = true
                    });
                    context.Project_User.Add(new Project_User
                    {
                        ProjectId = 2,
                        User = "administrator",
                        isDev = true
                    });

                    context.Project_User.Add(new Project_User
                    {
                        ProjectId = 1,
                        User = "user",
                        isDev = true,
                        myLead = "administrator"
                    });

                    context.SaveChanges();

                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }

            }
        }
    }
}