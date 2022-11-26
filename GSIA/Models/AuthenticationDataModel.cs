using Org.BouncyCastle.Crypto.Macs;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GSIA.Models
{
    public class AuthenticationDataModel
    {


        [Display(Name = "Employee Number")]
        [Required(ErrorMessage = "Please enter your employee number")]
        public string EmpNumber { get; set; } = string.Empty;


        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid data type, please enter email address")]
        [Display(Name = "Email Address")]
        [Required]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Deployment Code")]
        [Required]
        public string Position_ { get; set; } = string.Empty;

        [Display(Name = "Latest Movement No.")]
        [Required]
        public string MovNumber { get; set; } = string.Empty;

        [Display(Name = "Sec License No.")]
        [Required]
        public string SecLicense { get; set; } = string.Empty;

        [Display(Name = "Date Hired")]
        [DataType(DataType.Date)]
        [Required]
        public string DateHired { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least 6 characters long.", MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

    }
}
