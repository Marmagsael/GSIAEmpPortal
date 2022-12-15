using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace GSIA.Models.Login
{
    public class RegisterViewModel
    {
        [Display(Name ="Employee Number")]
        [Required(ErrorMessage = "Employee Number is required.")]
        public string EmpNumber { get; set; } = string.Empty;


        [Display(Name = "Deployment Code")]
        public string DepCode { get; set; } = string.Empty;


        [Display(Name = "Latest Movement No.")]
        [Required(ErrorMessage = "Latest Movement No. is required.")]
        public string MovNumber { get; set; } = string.Empty;


        [Display(Name = "Sec License")]
        [Required(ErrorMessage = "Sec License is required.")]
        public string SecLicense { get; set; } = string.Empty;


        [Display(Name = "Date Hired")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date hired is required")]
        public string DateHired { get; set; } = string.Empty;


        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "The {0} must be at least 6 characters long.", MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;


        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "The {0} must be at least 6 characters long.", MinimumLength = 6)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
