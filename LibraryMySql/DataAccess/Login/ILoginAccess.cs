using LibraryMySql.Models;

namespace LibraryMySql.DataAccess.Login
{
    public interface ILoginAccess
    {
        Task<LoginOutputModel?> FetchEmployeeInMainByEmpNo(LoginInputModel input);
        Task<LoginOutputModel?> FetchEmployeeInMainByEmpNoAndPassword(LoginInputModel input);
        Task<LoginOutputModel?> LoginEmployee(LoginInputModel input);
        Task<LoginOutputModel?> LoginEmployee(string Schema, string EmpNumber, string Password);
    }
}