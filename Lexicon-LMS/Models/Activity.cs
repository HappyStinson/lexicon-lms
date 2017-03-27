using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lexicon_LMS.Models
{
    public class Activity
    {
        public int Id { get; set; }

        [Display(Name = "Aktivitetsnamn")]
        [Required(ErrorMessage = "Du måste ange aktivitetens namn")]
        [StringLength(50, ErrorMessage = "Namnet kan inte vara längre än 50 tecken")]
        public string Name { get; set; }

        [Display(Name = "Aktivitetsbeskrivning")]
        [StringLength(500, ErrorMessage = "Beskrivningen kan inte vara längre än 500 tecken")]
        public string Description { get; set; }

        [Display(Name = "Startdatum")]
        [Required(ErrorMessage = "Du måste ange aktivitetens startdatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]      
        public DateTime StartDate { get; set; }

        [Display(Name = "Slutdatum")]
        [Required(ErrorMessage = "Du måste ange aktivitetens slutdatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]     
        public DateTime EndDate { get; set; }

        public int ActivityTypeId { get; set; }
        public int ModuleId { get; set; }

        // Navigation properties        
        public virtual ActivityType Type { get; set; }
        public virtual Module Module { get; set; }      

    }
}