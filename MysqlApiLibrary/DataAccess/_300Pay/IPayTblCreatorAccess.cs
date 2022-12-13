namespace MysqlApiLibrary.DataAccess._300Pay
{
    public interface IPayTblCreatorAccess
    {
        Task<string> _1101PayrollGrp(string schema, string connName = "MySqlConn");
    }
}