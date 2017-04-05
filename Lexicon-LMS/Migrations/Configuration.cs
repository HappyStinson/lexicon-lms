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
               new ActivityType {Name = "F�rel�sning" },
               new ActivityType {Name = "Inl�mningsuppgift" },
            };

            context.ActivityTypes.AddRange(activityTypes);
            /*

            foreach (var type in activityTypes)
            {
                string activityTypeName = type.Name;
                if (!context.ActivityTypes.Any(a => a.Name == activityTypeName))
                {
                    context.ActivityTypes.Add(type);
                }
            }
            */

            context.SaveChanges();                          // ID genereras n�r man sparar contexten

            int[] activityTypeIds = new int[3];
            for (int i = 0; i < activityTypeIds.Length; i++)
            {
                activityTypeIds[i] = activityTypes[i].Id; 

                // string name = activityTypes[i].Name;
                // activityTypeIds[i] = context.Courses.First(a => a.Name == name).Id;
            }

            // int activityIdLecture = context.ActivityTypes.First(a => a.Name == "F�rel�sning").Id;
            // int activityIdElearning = context.ActivityTypes.First(a => a.Name == "E-learning").Id;
            // int activityIdTask = context.ActivityTypes.First(a => a.Name == "Inl�mningsuppgift").Id;


            var courses = new Course[]
            {
               new Course {Name = "Java", Description = "Java Vidareutbildning", StartDate = new DateTime(2017, 4, 10), EndDate = new DateTime(2017, 6, 30) },
               new Course {Name = ".NET", Description = ".NET Vidareutbildning", StartDate = new DateTime(2017, 4, 24), EndDate = new DateTime(2017, 7, 14) },
            };

            context.Courses.AddRange(courses);

            /*

            foreach (var course in courses)
            {
                string courseName = course.Name;
                if (!context.Courses.Any(a => a.Name == courseName))
                {
                    context.Courses.Add(course);
                }
            }

    */

            context.SaveChanges();                          // ID genereras n�r man sparar contexten

            int[] courseIds = new int[]
            {
                courses[0].Id,
                courses[1].Id, 
                // context.Courses.First(c => c.Name == "Java").Id,
                // context.Courses.First(c => c.Name == ".NET").Id,
            };

            // int courseIdJava = context.Courses.First(c => c.Name == "Java").Id;
            // int courseIdNET = context.Courses.First(c => c.Name == ".NET").Id;         


            var modules = new Module[]
           {
               new Module {Name = "Java Programmering", Description = "Grundl�ggande programmering i Java, och genomg�ng av klassbibliotek", StartDate = new DateTime(2017, 4, 10), EndDate = new DateTime(2017, 5, 5) , CourseId = courseIds[0]},
               new Module {Name = "C# Programmering", Description = "Grundl�ggande programmering i C#, och genomg�ng av klassbibliotek", StartDate = new DateTime(2017, 4, 24), EndDate = new DateTime(2017, 5, 19), CourseId = courseIds[1]},
               new Module {Name = "Webbutveckling", Description = "HTML, CSS, och JavaScript", StartDate = new DateTime(2017, 5, 8), EndDate = new DateTime(2017, 5, 19) , CourseId = courseIds[0]},
               new Module {Name = "Webbutveckling", Description = "HTML, CSS, och JavaScript", StartDate = new DateTime(2017, 5, 22), EndDate = new DateTime(2017, 6, 2) , CourseId = courseIds[1]},
               new Module {Name = "Test", Description = "ISTQB Foundation", StartDate = new DateTime(2017, 5, 22), EndDate = new DateTime(2017, 5, 26) , CourseId = courseIds[0]},
               new Module {Name = "Test", Description = "ISTQB Foundation", StartDate = new DateTime(2017, 6, 5), EndDate = new DateTime(2017, 6, 9), CourseId = courseIds[1]},
               new Module {Name = "Java EE", Description = "Projektarbete i Java EE", StartDate = new DateTime(2017, 5, 29), EndDate = new DateTime(2017, 6, 30) , CourseId = courseIds[0]},
               new Module {Name = "ASP.NET MVC", Description = "Projektarbete i ASP.NET MVC", StartDate = new DateTime(2017, 6, 12), EndDate = new DateTime(2017, 7, 14) , CourseId = courseIds[1]},

           };

            context.Modules.AddRange(modules);

            /*

            foreach (var module in modules)
            {
                string moduleName = module.Name;
                int courseId = module.CourseId;

                if (!context.Modules.Any(m => (m.Name == moduleName) && (m.CourseId == courseId)))
                {
                    context.Modules.Add(module);
                }
            }

    */

            context.SaveChanges();                          // ID genereras n�r man sparar contexten

            int[] moduleIds = new int[8];
            for (int i = 0; i < moduleIds.Length; i++)
            {
                /*
                string moduleName = modules[i].Name;
                int courseId = modules[i].CourseId;
                moduleIds[i] = context.Modules.First(m => (m.Name == moduleName) && (m.CourseId == courseId)).Id;
                */
                moduleIds[i] = modules[i].Id;
            }

            var activities = new Activity[]
            {
                 new Activity {Name = "E-learning 1", Description = "Introduktion till Java", StartDate = new DateTime(2017, 4, 10), EndDate = new DateTime(2017, 4, 21) , ActivityTypeId = activityTypeIds[0], ModuleId = moduleIds[0]},
                 new Activity {Name = "E-learning 1", Description = "Introduktion till C#", StartDate = new DateTime(2017, 4, 24), EndDate = new DateTime(2017, 5, 5) , ActivityTypeId = activityTypeIds[0], ModuleId = moduleIds[1]},
                 new Activity {Name = "F�rel�sning 1", Description = "Introduktion till Java", StartDate = new DateTime(2017, 4, 24), EndDate = new DateTime(2017, 4, 24) , ActivityTypeId = activityTypeIds[1], ModuleId = moduleIds[0]},
                 new Activity {Name = "F�rel�sning 1", Description = "Introduktion till C#", StartDate = new DateTime(2017, 5, 8), EndDate = new DateTime(2017, 5, 8) , ActivityTypeId = activityTypeIds[1], ModuleId = moduleIds[1]},
                 new Activity {Name = "Inl�mningsuppgift 1", Description = "Inl�mningsuppgift 1, Garage 1.0", StartDate = new DateTime(2017, 4, 25), EndDate = new DateTime(2017, 4, 28) , ActivityTypeId = activityTypeIds[2], ModuleId = moduleIds[0]},
                 new Activity {Name = "Inl�mningsuppgift 1", Description = "Inl�mningsuppgift 1, Garage 1.0", StartDate = new DateTime(2017, 5, 9), EndDate = new DateTime(2017, 5, 12) , ActivityTypeId = activityTypeIds[2], ModuleId = moduleIds[1]},
                 new Activity {Name = "F�rel�sning 2", Description = "Objektorienterad Programmering", StartDate = new DateTime(2017, 5, 2), EndDate = new DateTime(2017, 5, 2) , ActivityTypeId = activityTypeIds[1], ModuleId = moduleIds[0]},
                 new Activity {Name = "F�rel�sning 2", Description = "Objektorienterad Programmering", StartDate = new DateTime(2017, 5, 15), EndDate = new DateTime(2017, 5, 15) , ActivityTypeId = activityTypeIds[1], ModuleId = moduleIds[1]},
                 new Activity {Name = "Inl�mningsuppgift 2", Description = "Inl�mningsuppgift 2, Garage 2.0", StartDate = new DateTime(2017, 5, 3), EndDate = new DateTime(2017, 5, 5) , ActivityTypeId = activityTypeIds[2], ModuleId = moduleIds[0]},
                 new Activity {Name = "Inl�mningsuppgift 2", Description = "Inl�mningsuppgift 2, Garage 2.0", StartDate = new DateTime(2017, 5, 16), EndDate = new DateTime(2017, 5, 19) , ActivityTypeId = activityTypeIds[2], ModuleId = moduleIds[1]},
                 new Activity {Name = "E-learning 1", Description = "Introduktion till Java", StartDate = new DateTime(2017, 5, 8), EndDate = new DateTime(2017, 5, 12) , ActivityTypeId = activityTypeIds[0], ModuleId = moduleIds[2]},
                 new Activity {Name = "E-learning 1", Description = "Introduktion till C#", StartDate = new DateTime(2017, 5, 22), EndDate = new DateTime(2017, 5, 26) , ActivityTypeId = activityTypeIds[0], ModuleId = moduleIds[3]},
                 new Activity {Name = "Inl�mningsuppgift 1", Description = "Inl�mningsuppgift 1, Garage 1.0", StartDate = new DateTime(2017, 5, 15), EndDate = new DateTime(2017, 5, 19) , ActivityTypeId = activityTypeIds[2], ModuleId = moduleIds[2]},
                 new Activity {Name = "Inl�mningsuppgift 1", Description = "Inl�mningsuppgift 1, Garage 1.0", StartDate = new DateTime(2017, 5, 29), EndDate = new DateTime(2017, 6, 2) , ActivityTypeId = activityTypeIds[2], ModuleId = moduleIds[3]},
                 new Activity {Name = "F�rel�sning 1", Description = "Introduktion till projektarbetet", StartDate = new DateTime(2017, 5, 29), EndDate = new DateTime(2017, 5, 29) , ActivityTypeId = activityTypeIds[1], ModuleId = moduleIds[6]},
                 new Activity {Name = "F�rel�sning 1", Description = "Introduktion till projektarbetet", StartDate = new DateTime(2017, 6, 12), EndDate = new DateTime(2017, 6, 12) , ActivityTypeId = activityTypeIds[1], ModuleId = moduleIds[7]},
                 new Activity {Name = "Projektarbete", Description = "Inl�mningsuppgift 1, Garage 1.0", StartDate = new DateTime(2017, 5, 30), EndDate = new DateTime(2017, 6, 30) , ActivityTypeId = activityTypeIds[2], ModuleId = moduleIds[6]},
                 new Activity {Name = "Projektarbete", Description = "Inl�mningsuppgift 1, Garage 1.0", StartDate = new DateTime(2017, 6, 13), EndDate = new DateTime(2017, 7, 14) , ActivityTypeId = activityTypeIds[2], ModuleId = moduleIds[7]},
            };

            context.Activities.AddRange(activities);

            /*


            foreach (var activity in activities)
            {
                string activityName = activity.Name;
                int moduleId = activity.ModuleId;

                if (!context.Activities.Any(a => (a.Name == activityName) && (a.ModuleId == moduleId)))
                {
                    context.Activities.Add(activity);
                }
            }

    */


            context.SaveChanges();                          // ID genereras n�r man sparar contexten


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

            var emails = new[] { "oscar@lxicon.se", "dimitris@lxicon.se", "jhellman@lxicon.se", "dtrump@lxicon.se", "vputin@lxicon.se" };
            var firstnames = new[] { "Oscar", "Dimitris", "John", "Donald", "Vladimir" };
            var lastnames = new[] { "Jacobsson", "Bj�rlingh", "Hellman", "Trump", "Putin" };

            int courseIndex = 0;

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
