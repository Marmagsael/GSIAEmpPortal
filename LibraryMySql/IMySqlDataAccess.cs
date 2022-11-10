using System.Data;

namespace LibraryMySql
{
    public interface IMySqlDataAccess
    {
        Task ExecuteCmd<T>(string sql, T parameters, string connString = "MySqlConn");
        Task<List<T>> FetchData<T, U>(string sql, U parameters, string connString = "MySqlConn");
        IDbConnection GetConnString(string connString = "MySqlConn");
    }
}