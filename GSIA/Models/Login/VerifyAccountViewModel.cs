using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GSIA.Models.Login
{
    public class VerifyAccountViewModel
    {
        [Display(Name = "Employee Number")]
        public string vEmpNumber { get; set; } = string.Empty;

        [Display(Name = "Deployment Code")]
        [Required(ErrorMessage = "Deployment Code is required")]
        public string DeploymentCode { get; set; } = string.Empty;

        [Display(Name = "Latest Movement No.")]
        [Required(ErrorMessage = "Latest Movement No. is required")]
        public string MovNumber { get; set; } = string.Empty;

        [Display(Name = "Sec License No.")]
        [Required(ErrorMessage = "Sec License No. is required")]
        public string SecLicense { get; set; } = string.Empty;

        [Display(Name = "Date Hired")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date hired is required")]
        public string DateHired { get; set; } = string.Empty;

        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, ErrorMessage = "The {0} must be at least 6 characters long.", MinimumLength = 6)]
        public string vPassword { get; set; } = string.Empty;

        [Compare("vPassword", ErrorMessage = "Confirm password doesn't match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(20, ErrorMessage = "The {0} must be at least 6 characters long.", MinimumLength = 6)]
        public string vConfirmPassword { get; set; } = string.Empty;

    }
}
