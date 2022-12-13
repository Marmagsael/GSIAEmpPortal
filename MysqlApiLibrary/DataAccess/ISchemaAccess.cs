using MysqlApiLibrary.Models.Main;

namespace MysqlApiLibrary.DataAccess
{
    public interface ISchemaAccess
    {
        string _0000getDefaultPaySchema();
        string _0000getDefaultPisSchema();
        string _0000getDefaultSchema();
        Task<SchemaStructureModel?> _0001_CheckIfTableExists(string schema, string tableName);
        void _1001_CreateDefaultSchema(string schema = "Main");
        void _1002_CreateLoginTbl(string schema = "Main");
    }
}