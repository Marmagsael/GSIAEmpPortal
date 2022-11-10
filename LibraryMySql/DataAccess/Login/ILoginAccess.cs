using LibraryMySql.Models;

namespace LibraryMySql.DataAccess.Login
{
    public interface ILoginAccess
    {
        Task<LoginOutputModel?> LoginEmployee(LoginInputModel input);
        Task<LoginOutputModel?> LoginEmployee(string Schema, string EmpNumber, string Password);
    }
}