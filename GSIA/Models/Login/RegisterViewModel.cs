using System.ComponentModel.DataAnnotations;

namespace GSIA.Models.Login
{
    public class RegisterViewModel
    {
        [Display(Name ="Employee Number")]
        [Required(ErrorMessage = "Employee Number is required.")]
        public string EmpNumber { get; set; } = string.Empty;

        [Display(Name = "Movement Number")]
        [Required(ErrorMessage = "Movement Number is required.")]
        public string MovNumber { get; set; } = string.Empty;

        [Display(Name = "Sec License")]
        [Required(ErrorMessage = "Sec License is required.")]
        public string SecLicense { get; set; } = string.Empty;

        [Display(Name = "Date Hired")]
        [Required(ErrorMessage = "Date Hired is required.")]
        public DateTime DateHired { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
