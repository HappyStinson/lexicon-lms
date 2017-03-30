using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace Lexicon_LMS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [Display(Name = "Ange en kurs")]
        [Required(ErrorMessage = "Du måste ange en kurs")]   
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Ange Förnamn")]
        [Display(Name = "Förnamn")]
        [StringLength(32)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Ange Efternamn")]
        [Display(Name = "Efternamn")]
        [StringLength(32)]
        public string LastName { get; set; }

        [Display(Name = "Fullständigt namn")]
        public string FullName { get { return FirstName + " " + LastName; } }

        // Navigation property
        public virtual Course Course { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Models.Activity> Activities { get; set;  }
        public DbSet<Models.ActivityType> ActivityTypes { get; set; }
        public DbSet<Models.Course> Courses { get; set; }
        public DbSet<Models.Module> Modules { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }    
    }
    
}