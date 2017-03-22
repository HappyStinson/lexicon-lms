using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lexicon_LMS.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Display(Name = "Kursnamn")]
        [Required(ErrorMessage = "Du måste ange kursens namn")]
        [StringLength(20, ErrorMessage = "Namnet kan inte vara längre än 20 tecken")]
        public string Name { get; set; }

        [Display(Name = "Kursbeskrivning")]
        [StringLength(500, ErrorMessage = "Beskrivningen kan inte vara mer än 500 tecken")]
        public string Description { get; set; }

        [Display(Name = "Startdatum")]
        [Required(ErrorMessage = "Du måste ange kursens startdatum")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Slutdatum")]
        [Required(ErrorMessage = "Du måste ange kursens slutdatum")]
        public DateTime EndDate { get; set; }

        // Navigation properties
        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<Module> Modules { get; set; }
    }
}