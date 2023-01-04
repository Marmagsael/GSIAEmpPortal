

using MysqlApiLibrary.Models.Main;

namespace MysqlApiLibrary.DataAccess._100Main
{
    public class MenuUsersAccess : IMenuUsersAccess
    {
        private readonly IMysqlDataAccess _sql;

        public MenuUsersAccess(IMysqlDataAccess sql)
        {
            _sql = sql;
        }

        public Task<List<MenuUsersModel?>> GetMenuUsers(string schema)
        {
            string sql = $@"select * from {schema}.Menus10User order by odr, id ";

            return _sql.FetchData<MenuUsersModel?, dynamic>(sql, new { });
        }


    }
}
