using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lexicon_LMS.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "E-post")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
     
        [Required]
        [Display(Name = "E-post")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {       
        [Required(ErrorMessage = "Du måste ange din E-postadress")]
        [Display(Name = "E-post")]       
        [EmailAddress(ErrorMessage = "Du har angett en ogiltig E-postadress")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Du måste ange ditt lösenord")]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Du måste ange en e-postaddress")]
        [EmailAddress]
        [Display(Name = "E-post")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Du måste ange ett lösenord")]
        [StringLength(100, ErrorMessage = "Lösenordet måste vara minst {2} tecken långt", MinimumLength = 2)]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bekräfta lösenordet")]
        [Compare("Password", ErrorMessage = "Lösenorden är inte likadana")]
        public string ConfirmPassword { get; set; }

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

        [Display(Name = "Registrera som lärare?")]
        public bool IsTeacher { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-post")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bekräfta lösenordet")]
        [Compare("Lösenord", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-post")]
        public string Email { get; set; }
    }

    public class UserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Kurs")]
        public string CourseName { get; set; }

        [Display(Name = "Namn")]
        public string FullName { get; set; }

        [Display(Name = "E-post")]
        public string Email { get; set; }

        [Display(Name = "Roll")]
        public string Role { get; set; }
    }
}
