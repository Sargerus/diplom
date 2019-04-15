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
                        isManager = true
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

                    context.BacklogStates.AddRange(new List<BacklogState> {
                 new BacklogState { State = "None" },
                 new BacklogState { State = "In Progress" },
                 new BacklogState { State = "Done" },
                });

                    context.BacklogTypes.AddRange(new List<BacklogType>
                {
                 new BacklogType { Type = "Backlog" },
                 new BacklogType { Type = "Defect" },
                 new BacklogType { Type = "Sprint Backlog" },
                });

                    context.Projects.Add(new Project
                    {
                        CreatedBy = "administrator",
                        CreatedOn = DateTime.Now,
                        ProjectDescription = "First project",
                        ProjectId = 1,
                        HeadOfProject = "administrator",
                        StartDate = DateTime.Now
                    });

                    context.Backlogs.Add(new Backlog
                    {
                        CreatedOn = DateTime.Now,
                        Project = 1,
                        BacklogDescription = "First Backlog",
                        BacklogState = "In Progress",
                        BacklogId = 1,
                        CreatedBy = "administrator",
                        BacklogType = "Backlog",
                        ProjectDescription = "First project"
                    });

                    BacklogTask taskk = new BacklogTask
                    {
                        Backlog = 1,
                        CreatedBy = "administrator",
                        Description = "First task",
                        CreatedOn = DateTime.Now,
                        TaskId = 1,
                        HoursEstimated = 8
                    };

                    context.BacklogTasks.Add(taskk);

                    context.Products.AddRange(new List<Products> {

                   new Products {ProductId = 1, Description = "First" },
                   new Products { ProductId = 2, Description = "Second" },
                   new Products {ProductId = 3, Description = "Third"}
                });

                    context.SaveChanges();

                    Backlog fff = context.Backlogs.Select(g => g).First();
                    fff.Tasks.Add(taskk);

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