using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GSIA.Models
{
    public class LoginViewModel
    {

        [Display(Name = "Employee Number")]
        [Required(ErrorMessage = "Please enter your employee number")]
        public string EmpNumber { get; set; } = string.Empty;

      
    }
}
