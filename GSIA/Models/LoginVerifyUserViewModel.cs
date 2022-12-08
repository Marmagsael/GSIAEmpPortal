using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GSIA.Models
{
    public class LoginVerifyUserViewModel
    {
        [Display(Name = "Employee Number")]
        [Required(ErrorMessage = "Please enter your employee number")]
        public string EmpNumber { get; set; } = string.Empty;


        [Display(Name = "Deployment Code")]
        [Required(ErrorMessage = "Deployment Code is required")]
        public string Position_ { get; set; } = string.Empty;

        [Display(Name = "Latest Movement No.")]
        [Required(ErrorMessage = "Latest Movement No. is required")]
        public string MovNumber { get; set; } = string.Empty;

        [Display(Name = "Sec License No.")]
        [Required(ErrorMessage = "Sec. License is required")]
        public string SecLicense { get; set; } = string.Empty;

        [Display(Name = "Date Hired")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date Hired is required")]
        public string DateHired { get; set; } = string.Empty;


        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least 6 characters long.", MinimumLength = 6)]
        public string vPassword { get; set; } = string.Empty;
    }
}
