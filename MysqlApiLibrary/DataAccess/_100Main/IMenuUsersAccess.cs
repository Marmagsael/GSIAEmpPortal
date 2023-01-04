using MysqlApiLibrary.Models.Main;

namespace MysqlApiLibrary.DataAccess._100Main
{
    public interface IMenuUsersAccess
    {
        Task<List<MenuUsersModel?>> GetMenuUsers(string schema);
    }
}