using System;
using System.ComponentModel.DataAnnotations;

namespace Lexicon_LMS.Models
{
    public class CreateActivityViewModel
    {
        public string CourseName { get; set; }
        public string CourseStart { get; set; }
        public string CourseEnd { get; set; }

        // Activity model
        [Display(Name = "Aktivitetens namn")]
        [Required(ErrorMessage = "Du måste ange aktivitetens namn")]
        [StringLength(50, ErrorMessage = "Namnet kan inte vara längre än 50 tecken")]
        public string Name { get; set; }

        [Display(Name = "Aktivitetens beskrivning")]
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
        public int CourseId { get; set; }

        public static implicit operator CreateActivityViewModel(Activity activity)
        {
            return new CreateActivityViewModel
            {
                CourseName = activity.Module.Course.Name,
                CourseStart = activity.Module.Course.StartDate.ToShortDateString(),
                CourseEnd = activity.Module.Course.EndDate.ToShortDateString(),
                Name = activity.Module.Name,
                Description = activity.Module.Description,
                StartDate = activity.Module.StartDate,
                EndDate = activity.Module.EndDate,
                ActivityTypeId = activity.ActivityTypeId,
                ModuleId = activity.ModuleId,
                CourseId = activity.Module.CourseId
            };
        }

        public static implicit operator Activity(CreateActivityViewModel vm)
        {
            return new Activity
            {
                Name = vm.Name,
                Description = vm.Description,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                ActivityTypeId = vm.ActivityTypeId,
                ModuleId = vm.ModuleId
            };
        }
    }
}