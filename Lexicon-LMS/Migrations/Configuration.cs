namespace Lexicon_LMS.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Lexicon_LMS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }


      protected override void Seed(Lexicon_LMS.Models.ApplicationDbContext context)
      {          

            var courses = new Course[]
            {
            new Course {Name = "Java", Description = "Java Vidareutbildning", StartDate = new DateTime(2017, 4, 9), EndDate = new DateTime(2017, 4, 19) },
            new Course {Name = ".NET", Description = ".NET Vidareutbildning", StartDate = new DateTime(2017, 4, 20), EndDate = new DateTime(2017, 4, 30) },
            };

            context.Courses.AddRange(courses);
            context.SaveChanges();                          // ID genereras när man sparar contexten


            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var roleNames = new string[] { "teacher", "student" };
            foreach (var roleName in roleNames)
            {
                if (!context.Roles.Any(r => r.Name == roleName))
                {
                    var role = new IdentityRole { Name = roleName };
                    var result = roleManager.Create(role);
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join("\n", result.Errors));
                    }
                }
            }

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var emails = new[] { "dimitris@lxicon.se", "dtrump@lxicon.se", "vputin@lexicon.se" };
            var firstnames = new[] { "Dimitris", "Donald", "Vladimir" };
            var lastnames = new[] { "Björlingh", "Trump", "Putin" };

            for (int i = 0; i < emails.Count(); i++)
            {
                var email = emails[i];
                if (!context.Users.Any(u => u.UserName == email))
                {

                    var user = new ApplicationUser
                    {
                        UserName = emails[i],
                        Email = emails[i],
                        CourseId = context.Courses.First(c => c.Name == ".NET").Id,
                        FirstName = firstnames[i],
                        LastName = lastnames[i],
                    };
                    var result = userManager.Create(user, "foobar");
                    
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join("\n", result.Errors));

                    }
                }
            }

            context.SaveChanges();

            var teacherUser = userManager.FindByName("dimitris@lxicon.se");
            userManager.AddToRole(teacherUser.Id, "teacher");

            var studentUser1 = userManager.FindByName("dtrump@lxicon.se");
            userManager.AddToRole(studentUser1.Id, "student");

            var studentUser2 = userManager.FindByName("dtrump@lxicon.se");
            userManager.AddToRole(studentUser2.Id, "student");

        }

    }
}
