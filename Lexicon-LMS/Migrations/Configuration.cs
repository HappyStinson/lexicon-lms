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

            var activityTypes = new ActivityType[]
            {
               new ActivityType {Name = "E-learning" },
               new ActivityType {Name = "Föreläsning" },
               new ActivityType {Name = "Inlämningsuppgift" },
            };           

            context.ActivityTypes.AddRange(activityTypes);
            context.SaveChanges();                          // ID genereras när man sparar contexten                
        

            int[] activityTypeIds = new int[3];
            for (int i = 0; i < activityTypeIds.Length; i++)
            {
                activityTypeIds[i] = activityTypes[i].Id;               
            }
           

            var courses = new Course[]
            {
               new Course {Name = "Java", Description = "Java Vidareutbildning", StartDate = new DateTime(2017, 4, 10), EndDate = new DateTime(2017, 6, 30) },
               new Course {Name = ".NET", Description = ".NET Vidareutbildning", StartDate = new DateTime(2017, 4, 24), EndDate = new DateTime(2017, 7, 14) },
            };

            context.Courses.AddRange(courses);          
            context.SaveChanges();                    
                        
            int[] courseIds = new int[2]
            {
                courses[0].Id,
                courses[1].Id,                
            };            

       
            var modules = new Module[]
            {
               new Module {Name = "Java Programmering", Description = "Grundläggande programmering i Java, och genomgång av klassbibliotek", StartDate = new DateTime(2017, 4, 10), EndDate = new DateTime(2017, 5, 5) , CourseId = courseIds[0]},
               new Module {Name = "C# Programmering", Description = "Grundläggande programmering i C#, och genomgång av klassbibliotek", StartDate = new DateTime(2017, 4, 24), EndDate = new DateTime(2017, 5, 19), CourseId = courseIds[1]},
               new Module {Name = "Webbutveckling", Description = "HTML, CSS, och JavaScript", StartDate = new DateTime(2017, 5, 8), EndDate = new DateTime(2017, 5, 19) , CourseId = courseIds[0]},
               new Module {Name = "Webbutveckling", Description = "HTML, CSS, och JavaScript", StartDate = new DateTime(2017, 5, 22), EndDate = new DateTime(2017, 6, 2) , CourseId = courseIds[1]},
               new Module {Name = "Test", Description = "ISTQB Foundation", StartDate = new DateTime(2017, 5, 22), EndDate = new DateTime(2017, 5, 26) , CourseId = courseIds[0]},
               new Module {Name = "Test", Description = "ISTQB Foundation", StartDate = new DateTime(2017, 6, 5), EndDate = new DateTime(2017, 6, 9), CourseId = courseIds[1]},
               new Module {Name = "Java EE", Description = "Projektarbete i Java EE", StartDate = new DateTime(2017, 5, 29), EndDate = new DateTime(2017, 6, 30) , CourseId = courseIds[0]},
               new Module {Name = "ASP.NET MVC", Description = "Projektarbete i ASP.NET MVC", StartDate = new DateTime(2017, 6, 12), EndDate = new DateTime(2017, 7, 14) , CourseId = courseIds[1]},

            };

            context.Modules.AddRange(modules);
            context.SaveChanges();

            int[] moduleIds = new int[8];
            for (int i = 0; i < moduleIds.Length; i++)
            {       
               moduleIds[i] = modules[i].Id;
            }  
                   

            var activities = new Activity[]
            {
                 new Activity {Name = "E-learning 1", Description = "Introduktion till Java", StartDate = new DateTime(2017, 4, 10), EndDate = new DateTime(2017, 4, 21) , ActivityTypeId = activityTypeIds[0], ModuleId = moduleIds[0]},
                 new Activity {Name = "E-learning 1", Description = "Introduktion till C#", StartDate = new DateTime(2017, 4, 24), EndDate = new DateTime(2017, 5, 5) , ActivityTypeId = activityTypeIds[0], ModuleId = moduleIds[1]},
                 new Activity {Name = "Föreläsning 1", Description = "Introduktion till Java", StartDate = new DateTime(2017, 4, 24), EndDate = new DateTime(2017, 4, 24) , ActivityTypeId = activityTypeIds[1], ModuleId = moduleIds[0]},
                 new Activity {Name = "Föreläsning 1", Description = "Introduktion till C#", StartDate = new DateTime(2017, 5, 8), EndDate = new DateTime(2017, 5, 8) , ActivityTypeId = activityTypeIds[1], ModuleId = moduleIds[1]},
                 new Activity {Name = "Inlämningsuppgift 1", Description = "Inlämningsuppgift 1, Garage 1.0", StartDate = new DateTime(2017, 4, 25), EndDate = new DateTime(2017, 4, 28) , ActivityTypeId = activityTypeIds[2], ModuleId = moduleIds[0]},
                 new Activity {Name = "Inlämningsuppgift 1", Description = "Inlämningsuppgift 1, Garage 1.0", StartDate = new DateTime(2017, 5, 9), EndDate = new DateTime(2017, 5, 12) , ActivityTypeId = activityTypeIds[2], ModuleId = moduleIds[1]},
                 new Activity {Name = "Föreläsning 2", Description = "Objektorienterad Programmering", StartDate = new DateTime(2017, 5, 2), EndDate = new DateTime(2017, 5, 2) , ActivityTypeId = activityTypeIds[1], ModuleId = moduleIds[0]},
                 new Activity {Name = "Föreläsning 2", Description = "Objektorienterad Programmering", StartDate = new DateTime(2017, 5, 15), EndDate = new DateTime(2017, 5, 15) , ActivityTypeId = activityTypeIds[1], ModuleId = moduleIds[1]},
                 new Activity {Name = "Inlämningsuppgift 2", Description = "Inlämningsuppgift 2, Garage 2.0", StartDate = new DateTime(2017, 5, 3), EndDate = new DateTime(2017, 5, 5) , ActivityTypeId = activityTypeIds[2], ModuleId = moduleIds[0]},
                 new Activity {Name = "Inlämningsuppgift 2", Description = "Inlämningsuppgift 2, Garage 2.0", StartDate = new DateTime(2017, 5, 16), EndDate = new DateTime(2017, 5, 19) , ActivityTypeId = activityTypeIds[2], ModuleId = moduleIds[1]},
                 new Activity {Name = "E-learning 1", Description = "Introduktion till Java", StartDate = new DateTime(2017, 5, 8), EndDate = new DateTime(2017, 5, 12) , ActivityTypeId = activityTypeIds[0], ModuleId = moduleIds[2]},
                 new Activity {Name = "E-learning 1", Description = "Introduktion till C#", StartDate = new DateTime(2017, 5, 22), EndDate = new DateTime(2017, 5, 26) , ActivityTypeId = activityTypeIds[0], ModuleId = moduleIds[3]},
                 new Activity {Name = "Inlämningsuppgift 1", Description = "Inlämningsuppgift 1, Garage 1.0", StartDate = new DateTime(2017, 5, 15), EndDate = new DateTime(2017, 5, 19) , ActivityTypeId = activityTypeIds[2], ModuleId = moduleIds[2]},
                 new Activity {Name = "Inlämningsuppgift 1", Description = "Inlämningsuppgift 1, Garage 1.0", StartDate = new DateTime(2017, 5, 29), EndDate = new DateTime(2017, 6, 2) , ActivityTypeId = activityTypeIds[2], ModuleId = moduleIds[3]},
                 new Activity {Name = "Föreläsning 1", Description = "Introduktion till projektarbetet", StartDate = new DateTime(2017, 5, 29), EndDate = new DateTime(2017, 5, 29) , ActivityTypeId = activityTypeIds[1], ModuleId = moduleIds[6]},
                 new Activity {Name = "Föreläsning 1", Description = "Introduktion till projektarbetet", StartDate = new DateTime(2017, 6, 12), EndDate = new DateTime(2017, 6, 12) , ActivityTypeId = activityTypeIds[1], ModuleId = moduleIds[7]},
                 new Activity {Name = "Projektarbete", Description = "Inlämningsuppgift 1, Garage 1.0", StartDate = new DateTime(2017, 5, 30), EndDate = new DateTime(2017, 6, 30) , ActivityTypeId = activityTypeIds[2], ModuleId = moduleIds[6]},
                 new Activity {Name = "Projektarbete", Description = "Inlämningsuppgift 1, Garage 1.0", StartDate = new DateTime(2017, 6, 13), EndDate = new DateTime(2017, 7, 14) , ActivityTypeId = activityTypeIds[2], ModuleId = moduleIds[7]},
            };

            context.Activities.AddRange(activities);
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

            var emails = new[] { "oscar@lxicon.se", "dimitris@lxicon.se", "jhellman@lxicon.se", "dtrump@lxicon.se", "vputin@lxicon.se", "rex@lxicon.se", "rpriebus@lxicon.se", "sbannon@lxicon.se", "lende@lxicon.se", "pstore@lxicon.se"  };
            var firstnames = new[] { "Oscar", "Dimitris", "John", "Donald", "Vladimir", "Rex", "Reince", "Steve", "Leif", "Per" };
            var lastnames = new[] { "Jacobsson", "Björlingh", "Hellman", "Trump", "Putin", "Tillerson", "Priebus", "Bannon", "Den Ende", "Den Store" };

            int courseIndex = 1; 

            for (int i = 0; i < emails.Count(); i++)
            {
                var email = emails[i];
                if (!context.Users.Any(u => u.UserName == email))
                {

                    courseIndex = 1 - courseIndex;  

                    var user = new ApplicationUser
                    {
                        UserName = emails[i],
                        Email = emails[i],
                        CourseId = courses[courseIndex].Id,
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

            var teacherUser1 = userManager.FindByName("oscar@lxicon.se");
            userManager.AddToRole(teacherUser1.Id, "teacher");

            var teacherUser2 = userManager.FindByName("dimitris@lxicon.se");
            userManager.AddToRole(teacherUser2.Id, "teacher");

            var teacherUser3 = userManager.FindByName("jhellman@lxicon.se");
            userManager.AddToRole(teacherUser3.Id, "teacher");

            var studentUser1 = userManager.FindByName("dtrump@lxicon.se");
            userManager.AddToRole(studentUser1.Id, "student");

            var studentUser2 = userManager.FindByName("vputin@lxicon.se");
            userManager.AddToRole(studentUser2.Id, "student");
        }      


    }
}
