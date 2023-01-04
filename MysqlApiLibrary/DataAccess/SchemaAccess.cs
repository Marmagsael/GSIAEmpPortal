
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

    public string _0000getDefaultSchema()
    {
        return _config.GetSection("Schema:Default").Value;
    }
    public string _0000getDefaultPisSchema()
    {
        return _config.GetSection("Schema:Pis").Value;
    }
    public string _0000getDefaultPaySchema()
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
                          Password  VARCHAR(150),
                          Email     VARCHAR(60),
                          Domain    VARCHAR(25),
                          status    Char(1)         DEFAULT 'A',
                          PRIMARY KEY(`Id`))ENGINE = InnoDB;";
        _sql.ExecuteCmd(sql, new { });
    }

    public async void _1003_CreateMainTable(string schema="Main")
    {
        string sql = string.Empty;

        // --- MenuUser ----------------------------------
        _1003_1_CreateMenus("Menus10User",schema); 
        _1003_1_CreateMenus("Menus20Employment",schema); 
        _1003_1_CreateMenus("Menus30AMS",schema); 
        _1003_1_CreateMenus("Menus40Pis",schema); 
        _1003_1_CreateMenus("Menus50Pay",schema); 
        
        

        // --- Country ----------------------------------
        sql = @$"CREATE TABLE if not exists {schema}.Country ( 
                    Id INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
                    Code        CHAR(10) NULL,
                    Name        CHAR(60) NULL,
                    PRIMARY KEY (`Id`)) Engine = InnoDB;";
        await _sql.ExecuteCmd(sql, new { });

        // --- Currency ----------------------------------
        sql = @$"CREATE TABLE if not exists  {schema}.Currency
            (
                Id INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
                Code   NVARCHAR (5)  NULL,
                Name   NVARCHAR (45) NULL,
                Symbol NVARCHAR (5)  NULL,
                PRIMARY KEY (`Id`)) Engine = InnoDB;";
        await _sql.ExecuteCmd(sql, new { });

        // --- ProvinceState-------------------------------------
        sql = $@"CREATE TABLE if not exists  {schema}.ProvinceState
            (
                Id INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
                Code      CHAR (10) NOT NULL,
                Name      CHAR (60) NOT NULL,
                CountryId INT       NOT NULL DEFAULT 0,
                PRIMARY KEY (`Id`)) Engine = InnoDB;";
        await _sql.ExecuteCmd(sql, new { });

        // --- UserCompany ---------------------------------------
        sql = @$"CREATE TABLE if not exists  {schema}.UsersCompany
                (
                    Id INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
                    OwnerId         INT          NULL,
                    CompanySName    VARCHAR (15) NULL,
                    CompanyName     VARCHAR (120) NULL,
                    CurencyId       INT          NULL,
                    StorageId       VARCHAR (120) NULL,
                    PISSchema       VARCHAR (60) NULL,
                    PaySchema       VARCHAR (60) NULL,
                    AMSSchema       VARCHAR (60) NULL,
                    ApplicantSchema VARCHAR (60) NULL,
                    PRIMARY KEY (`Id`)) Engine = InnoDB;";
        await _sql.ExecuteCmd(sql, new { });


    }

    public void _1003_CreatePisTable(string schema = "Main")
    {

    }
    public void _1003_CreatePayTable(string schema = "Main")
    {

    }

    private async void _1003_1_CreateMenus(string menuName, string schema="Main") {
        string sql = @$"CREATE TABLE if not exists {schema}.{menuName}
        (
            Id INTEGER UNSIGNED NOT NULL, 
                            IdParent     INT NULL, 
                            Indent       INT NULL, 
                            Type         NCHAR(10) NULL, 
                            Code         NCHAR(10) NULL, 
                            Icon1        VARCHAR(80) NULL, 
                            Icon2        VARCHAR(80) NULL, 
                            DispText     VARCHAR(80) NULL, 
                            IsWithChild  SMALLINT NULL, 
                            Controller   VARCHAR(50) NULL, 
                            Action       VARCHAR(50) NULL, 
                            Odr          SMALLINT NULL DEFAULT 0,
                            PRIMARY KEY (`Id`)) Engine = InnoDB;";
        await _sql.ExecuteCmd(sql, new { });
    }
    public async void _1004_InsertDefaultValuesMain(string schema = "Main")
    {
        // --- Users --------------------------------
        string sql  = $"select * from {schema}.users limit 1";
        var user    = await _sql.FetchData<UserMainModel, dynamic>(sql, new { }); 
        if(user.Count == 0)
        {
            sql = @$"insert into {schema}.users 
                  (LoginName,    Password,           Email) values 
                  ('marmagsael', sha2('635421',512), 'marmagsael@gmail.com'), 
                  ('judith',     sha2('123456',512), 'jreyes@gmail.com') ";
            await _sql.ExecuteCmd(sql, new { });
        }
        _1004_1_InsertMenu10User(schema); 


    }
    private async void _1004_1_InsertMenu10User(string schema="Main"){
        string sql  = $"select * from {schema}.menus10User limit 1";
        var menu    = await _sql.FetchData<UserMainModel, dynamic>(sql, new { }); 
        if(menu.Count == 0)
        {
            sql = @$"insert into {schema}.menus10user
                    (Odr,   Id,     IdParent,	Indent,     Type,   Code,       Icon1,                          Icon2,  DispText,	                IsWithChild,	Controller,         Action) values 
                    (100,	100,    0,			0,		    'Hdr',  'H001',     '',                             '',     'Profiles',                 1,				null,		        null), 
                    (200,	200,    0,			0,		    'Hdr',  'H002',     '',                             '',     'Attendance',               1,				null,		        null),

                    (110,	110,    100,		0,		    'SHdr', 'SH110',    'fa-regular fa-folder-open',    '',     'My 201 Records',           1,				null,		        null),
                    
                    (111,	111,    110,        1,		    'Dtl',  'D111',     'fa-regular fa-file-lines',     '',     'Personal Information',		0,				'Profiles',			'_111PersonnelInformation'), 
                    (112,	112,    110,        1,		    'Dtl',  'D112',     'fa-solid fa-suitcase',         '',     'Employment',		        0,				'Profiles',			'_112Employment'), 
                    (113,	113,    110,        1,		    'Dtl',  'D113',     'fa-solid fa-person-chalkboard','',     'Trainings',		        0,				'Profiles',			'_113Trainings'), 
                    (114,	114,    110,        1,		    'Dtl',  'D114',     'fa-solid fa-cloud-arrow-up',   '',     'Uploadables',		        0,				'Profiles',			'_114Uploadables'), 
                    (115,	115,    110,        1,		    'Dtl',  'D115',     'fa-solid fa-list-check',       '',     'Correction Request',		0,				'Profiles',			'_115CorrectionRequest') 
                    "; 
            await _sql.ExecuteCmd(sql, new { });
        }
    }

    public void _1004_InsertDefaultValuesPis(string schema = "Main")
    {

    }
    public void _1004_InsertDefaultValuesPay(string schema = "Main")
    {

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
