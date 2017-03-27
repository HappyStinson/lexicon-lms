using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lexicon_LMS.Models
{
    public class Module
    {
        public int Id { get; set; }

        [Display(Name = "Modulnamn")]
        [Required(ErrorMessage = "Du måste ange modulens namn")]
        [StringLength(50, ErrorMessage = "Namnet kan inte vara längre än 50 tecken")]
        public string Name { get; set; }

        [Display(Name = "Modulbeskrivning")]
        [StringLength(500, ErrorMessage = "Beskrivningen kan inte vara längre än 500 tecken")]
        public string Description { get; set; }

        [Display(Name = "Startdatum")]
        [Required(ErrorMessage = "Du måste ange modulens startdatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Slutdatum")]
        [Required(ErrorMessage = "Du måste ange modulens slutdatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]        
        public DateTime EndDate { get; set; }      
      
    
        public int CourseId { get; set; }

        // Navigation property    
        public virtual ICollection<Activity> Actvities { get; set; }
        public virtual Course Course { get; set; }

    }
}