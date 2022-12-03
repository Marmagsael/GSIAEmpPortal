
using Microsoft.Extensions.Configuration;
namespace MysqlApiLibrary.DataAccess;

public class SchemaAccess : ISchemaAccess
{
    private readonly IConfiguration _config;
    private readonly IMysqlDataAccess _sql;

    public SchemaAccess(IConfiguration config, IMysqlDataAccess sql)
    {
        _config = config;
        _sql = sql;
    }

    public string getDefaultSchema()
    {
        return _config.GetSection("Schema:Default").Value;
    }
    public string getDefaultPisSchema()
    {
        return _config.GetSection("Schema:Pis").Value;
    }
    public string getDefaultPaySchema()
    {
        return _config.GetSection("Schema:Pay").Value;
    }


    public void _1000_CreateDefaultSchema(string schema = "Main")
    {
        string sql = "CREATE DATABASE IF NOT EXISTS " + schema;
        _sql.ExecuteCmd(sql, new { });
    }

    public void _2001_CreateLoginTbl(string schema = "Main")
    {
        string sql = @"CREATE TABLE if not exists " + schema + @".Users (
                          Id        INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
                          LoginName VARCHAR(45),
                          Password  VARCHAR(80),
                          Email     VARCHAR(45),
                          Domain    VARCHAR(25),
                          PRIMARY KEY(`Id`))ENGINE = InnoDB;"; 
        _sql.ExecuteCmd(sql, new { });
    }








}
