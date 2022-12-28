using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
namespace MysqlMvc.Models.Login;

public class LoginInputModel
{
    [Display(Name = "Email Address")]
    [Required(ErrorMessage = "Invalid Email Address / Login Name")]
    public string EmpNumber { get; set; } = string.Empty;

    [Display(Name ="Password")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

}
