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
                userManager.AddToRole(user.Id, role2.Name);

                context.BacklogStates.AddRange(new List<BacklogState> {
                 new BacklogState { State = "None" },
                 new BacklogState { State = "In Progress" },
                 new BacklogState { State = "Done" },
                });

                

                context.SaveChanges();
            }
        }
    }
}