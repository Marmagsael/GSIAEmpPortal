using LibraryMySql.Models;

namespace LibraryMySql.DataAccess.Login
{
    public interface ILoginAccess
    {
        Task<LoginOutputModel?> FetchEmployeeByEmailMain(string email);
        Task<LoginOutputModel?> LoginEmployee(LoginInputModel input);
        Task<LoginOutputModel?> LoginEmployee(string Schema, string EmpNumber);
        Task<LoginOutputModel?> LoginEmployeeByEmpnoMain(LoginInputModel input);
    }
}