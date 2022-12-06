
using Microsoft.Extensions.Configuration;
using MysqlApiLibrary.Models.Login;
using MysqlApiLibrary.Models.Main;

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


    public void _1001_CreateDefaultSchema(string schema = "Main")
    {
        string sql = "CREATE DATABASE IF NOT EXISTS " + schema;
        _sql.ExecuteCmd(sql, new { });
    }

    public void _1002_CreateLoginTbl(string schema = "Main")
    {
        string sql = @"CREATE TABLE if not exists " + schema + @".Users (
                          Id        INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
                          LoginName VARCHAR(45),
                          Password  VARCHAR(80),
                          Email     VARCHAR(45),
                          Domain    VARCHAR(25),
                          status    Char(1)         DEFAULT 'A',
                          PRIMARY KEY(`Id`))ENGINE = InnoDB;";
        _sql.ExecuteCmd(sql, new { });
    }

    // --- Create User TABLE and User's Schema ------------------------------- 


    public async Task<SchemaStructureModel?> _0001_CheckIfTableExists(string schema, string tableName)
    {
        string sql = @"SELECT TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE FROM information_schema.TABLES 
                        WHERE TABLE_SCHEMA = @Schema and TABLE_NAME = @TableName;";
        var data = await _sql.FetchData<SchemaStructureModel, dynamic>(sql, new { Schema = schema, TableName = tableName });
        return data.FirstOrDefault();


    }





}
