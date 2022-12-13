using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GSIA.Models.Login
{
    public class LoginViewModel
    {

        [Display(Name = "Employee Number")]
        [Required(ErrorMessage = "Please enter your employee number")]
        public string EmpNumber { get; set; } = string.Empty;


        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least 6 characters long.", MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

    }
}
