namespace MysqlApiLibrary.Models; 

public class LoginInputModel
{
    public string Schema { get; set; } = String.Empty;

    public string EmpNumber { get; set; } = String.Empty;

    public string Password { get; set; } = String.Empty;
}


