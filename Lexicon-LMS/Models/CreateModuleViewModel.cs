using System;
using System.ComponentModel.DataAnnotations;

namespace Lexicon_LMS.Models
{
    public class CreateModuleViewModel
    {
        public string CourseName { get; set; }
        public string CourseStart { get; set; }
        public string CourseEnd { get; set; }

        // Module model
        [Display(Name = "Modulnamn")]
        [Required(ErrorMessage = "Du måste ange kursens namn")]
        [StringLength(50, ErrorMessage = "Namnet kan inte vara längre än 50 tecken")]
        public string Name { get; set; }

        [Display(Name = "Modulbeskrivning")]
        [StringLength(500, ErrorMessage = "Beskrivningen kan inte vara mer än 500 tecken")]
        public string Description { get; set; }

        [Display(Name = "Startdatum")]
        [Required(ErrorMessage = "Du måste ange modulens startdatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Slutdatum")]
        [Required(ErrorMessage = "Du måste ange modulens slutdatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public int CourseId { get; set; }

        public static implicit operator CreateModuleViewModel(Module module)
        {
            return new CreateModuleViewModel
            {
                CourseName = module.Course.Name,
                CourseStart = module.Course.StartDate.ToShortDateString(),
                CourseEnd = module.Course.EndDate.ToShortDateString(),
                Name = module.Name,
                Description = module.Description,
                StartDate = module.StartDate,
                EndDate = module.EndDate,
                CourseId = module.CourseId
            };
        }

        public static implicit operator Module(CreateModuleViewModel vm)
        {
            return new Module
            {
                Name = vm.Name,
                Description = vm.Description,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                CourseId = vm.CourseId
            };
        }
    }
}