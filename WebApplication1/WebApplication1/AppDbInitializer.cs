using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
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

                userManager.Create(admin,"!Kemp111");
                userManager.Create(user, "!Kemp111");
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(user.Id, role2.Name);

                context.BacklogStates.AddRange(new List<BacklogState> {
                 new BacklogState { State = "None" },
                 new BacklogState { State = "In Progress" },
                 new BacklogState { State = "Done" },
                });

                context.Projects.Add(new Project
                {
                    CreatedBy = "administrator",
                    CreatedOn = DateTime.Now,
                    ProjectDescription = "First project",
                    ProjectId = 1
                });

                context.Backlogs.Add(new Backlog
                {
                    CreatedOn = DateTime.Now,
                    Project = 1,
                    Description = "First Backlog",
                    BacklogState = "In Progress",
                    BacklogId = 1,
                    CreatedBy = "administrator"
                });

                BacklogTask taskk = new BacklogTask
                {
                    Backlog = 1,
                    CreatedBy = "administrator",
                    Description = "First task",
                    CreatedOn = DateTime.Now,
                    TaskId = 1,
                    HoursEstiimated = 8
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
        }
    }
}