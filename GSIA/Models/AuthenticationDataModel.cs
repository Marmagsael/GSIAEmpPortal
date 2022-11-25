using Org.BouncyCastle.Crypto.Macs;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GSIA.Models
{
    public class AuthenticationDataModel
    {


        [Display(Name = "Employee Number")]
        [Required(ErrorMessage = "Please enter your employee number")]
        public string EmployeeNumber { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least 8 characters long.", MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid data type, please enter email address")]
        [Display(Name = "Email Address")]
        [Required]
        public string Email { get; set; } = string.Empty;

    }
}
