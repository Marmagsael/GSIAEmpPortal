
namespace LibraryMySql.Models;

public class VerifyUserInputModel
{

    public string Schema { get; set; } = String.Empty;
    public string EmpNumber { get; set; } = String.Empty;
    public string Position { get; set; } = String.Empty;
    public string MovNumber { get; set; } = String.Empty;
    public string SecLicense { get; set; } = String.Empty;
    public DateTime? DateHired { get; set; }
    public string Email { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
}
