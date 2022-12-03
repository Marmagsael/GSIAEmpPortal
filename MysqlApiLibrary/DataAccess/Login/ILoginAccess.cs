using MysqlApiLibrary.Models;

namespace MysqlApiLibrary.DataAccess.Login
{
    public interface ILoginAccess
    {
        Task<LoginOutputModel?> LoginEmployee(string Schema, string EmpNumber, string Password);
        Task<List<LoginOutputModel?>> LoginEmployee(string schema);
    }
}