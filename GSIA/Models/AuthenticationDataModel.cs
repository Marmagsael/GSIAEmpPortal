using Org.BouncyCastle.Crypto.Macs;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GSIA.Models
{
    public class AuthenticationDataModel
    {
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid data type, please enter email address")]
        [Display(Name = "Email Address")]
        [Required]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Employee Number")]
        [Required(ErrorMessage = "Please enter your employee number")]
        public string EmployeeNumber { get; set; } = string.Empty;


    }
}
