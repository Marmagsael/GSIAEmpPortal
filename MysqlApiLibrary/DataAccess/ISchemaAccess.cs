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
        void _1003_CreateMainTable(string schema = "Main");
        void _1003_CreatePayTable(string schema = "Main");
        void _1003_CreatePisTable(string schema = "Main");
        void _1004_InsertDefaultValuesMain(string schema = "Main");
        void _1004_InsertDefaultValuesPay(string schema = "Main");
        void _1004_InsertDefaultValuesPis(string schema = "Main");
    }
}