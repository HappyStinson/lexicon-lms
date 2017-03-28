using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lexicon_LMS.Models
{
    public class ActivityType
    {      
        public int Id { get; set; }
       
        [Display(Name = "Aktivitetstypens namn")]
        [Required(ErrorMessage = "Du måste ange namnet på aktivitetstypen")]
        [StringLength(50, ErrorMessage = "Namnet på aktivitetstypen kan inte vara längre än 50 tecken")]
        public string Name { get; set; }

        // Navigation property
        public virtual ICollection<Activity> Activities { get; set; }
    }
}