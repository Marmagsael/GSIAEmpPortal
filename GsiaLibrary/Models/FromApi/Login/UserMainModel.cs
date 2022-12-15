namespace GsiaLibrary.Models.FromApi.Login;

public class UserMainModel
{
    public int Id { get; set; }
    public string? LoginName { get; set; }
    public string? Password { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? Domain { get; set; }
    public string? Status { get; set; } = "A";

}
