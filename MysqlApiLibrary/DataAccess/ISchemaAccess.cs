namespace MysqlApiLibrary.DataAccess
{
    public interface ISchemaAccess
    {
        
        string getDefaultPaySchema();
        string getDefaultPisSchema();
        string getDefaultSchema();

        void _1000_CreateDefaultSchema(string schema = "Main");
        void _2001_CreateLoginTbl(string schema = "Main");

    }
}