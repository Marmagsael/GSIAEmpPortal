using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GsiaLibrary.Models.UI.Login;

public class RegisterInputModel
{
    public string Schema { get; set; } = string.Empty;

    public string EmpNumber { get; set; } = string.Empty;

    public string DepCode { get; set; } = string.Empty;

    public string MovNumber { get; set; } = string.Empty;

    public string SecLicense { get; set; } = string.Empty;

    //public DateTime? DateHired { get; set; }
    public string DateHired { get; set; } = string.Empty;

    public string Email { get; set; } = null;

    public string Password { get; set; } = string.Empty;

    public string ConfirmPassword { get; set; } = string.Empty;
}
