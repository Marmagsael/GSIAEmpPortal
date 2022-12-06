using MysqlApiLibrary.Models;
using MysqlApiLibrary.Models.Login;

namespace MysqlApiLibrary.DataAccess.Login
{
    public interface ILoginAccess
    {
        Task<LoginOutputModel?> LoginEmployee(string Schema, string EmpNumber, string Password);
        Task<List<LoginOutputModel?>> LoginEmployee(string schema);
        Task<UserMainModel?> _1000_Login(string loginname, string password, string schema = "Main");
        Task<UserMainModel?> _1001_GetUserMainById(int id, string schema = "Main");
        Task<UserMainModel?> _1002_GetUserMainByLoginName(string loginName, string schema = "Main");
        Task<UserMainModel?> _1003_GetUserMainByEmail(string email, string schema = "Main");
        Task _1004_InsertUserMain(UserMainModel user, string schema = "Main");
        Task _1005_Update(int id, UserMainModel user, string schema = "Main");
        Task _1006_Delete(int id, string schema = "Main");
        Task<UserMainModel?> _1007_ChangeUserStatus(int id, string status, string schema = "Main");
        Task<string[]> _1008_CreateUserSchema(int id, string connName = "MySqlConn");
    }
}