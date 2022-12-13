
using Microsoft.VisualBasic;
using MysqlApiLibrary.Models;
using MysqlApiLibrary.Models.Login;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System.IO.Pipes;

namespace MysqlApiLibrary.DataAccess.Login;

public class LoginAccess : ILoginAccess
{
    private readonly IMysqlDataAccess _sql;

    public LoginAccess(IMysqlDataAccess sql)
    {
        _sql = sql;
    }

    public async Task<LoginOutputModel?> LoginEmployee(string schema, string empNumber, string password)
    {
        string sql = @" select  e.Empnumber, e.EmpLastNm, e.EmpFirstNm, e.EmpMidNm,
                                c.clNumber, c.ClName,
                                s.code EmpStatCd, s.Name EmpStatus, s.IsResigned,  
                                Position_ PositionCd, p.Name as Position,
                                e.DateHired,
                                e.Sss,
                                e.Tin,
                                e.SecLicense License,
                                e.MovNumber,
                                e.Email,
                                e.passwd
                            from " + schema + ".empmas e " +
                        " left join " + schema + ".client c on c.ClNumber = e.Client_ " +
                        " left join " + schema + ".empstat s on s.code = e.empstat_ " +
                        " left join " + schema + ".Position p on p.Code = e.Position_" +
                        " where e.Empnumber = @Empnumber and passwd = sha1(@Password) ";

        var data = await _sql.FetchData<LoginOutputModel?, dynamic>(sql, new { Empnumber = empNumber, Password = password });

        return data.FirstOrDefault();

    }

    public Task<List<LoginOutputModel?>> LoginEmployee(string schema)
    {
        string sql = @" select  e.Empnumber, e.EmpLastNm, e.EmpFirstNm, e.EmpMidNm,
                                c.clNumber, c.ClName,
                                s.code EmpStatCd, s.Name EmpStatus, s.IsResigned,  
                                Position_ PositionCd, p.Name as Position,
                                e.DateHired,
                                e.Sss,
                                e.Tin,
                                e.SecLicense License,
                                e.MovNumber,
                                e.Email,
                                e.passwd
                            from " + schema + ".empmas e " +
                        " left join " + schema + ".client c on c.ClNumber = e.Client_ " +
                        " left join " + schema + ".empstat s on s.code = e.empstat_ " +
                        " left join " + schema + ".Position p on p.Code = e.Position_" +
                        " limit 50 ";

        return _sql.FetchData<LoginOutputModel?, dynamic>(sql, new { });



    }

    public async Task<LoginOutputModel?> _00001_EmpmasByEmpNumber(string empNumber, string schema = "SecPis", string connName = "MySqlConn")
    {
        string sql = @" select  e.Empnumber, e.EmpLastNm, e.EmpFirstNm, e.EmpMidNm,
                                c.clNumber, c.ClName,
                                s.code EmpStatCd, s.Name EmpStatus, s.IsResigned,  
                                Position_ PositionCd, p.Name as Position,
                                e.DateHired,
                                e.Sss,
                                e.Tin,
                                e.SecLicense License,
                                e.MovNumber,
                                e.Email,
                                e.passwd
                            from " + schema + ".empmas e " +
                        " left join " + schema + ".client c on c.ClNumber = e.Client_ " +
                        " left join " + schema + ".empstat s on s.code = e.empstat_ " +
                        " left join " + schema + ".Position p on p.Code = e.Position_" +
                        " where e.Empnumber = @Empnumber ";

        var data = await _sql.FetchData<LoginOutputModel?, dynamic>(sql, new { Empnumber = empNumber }, connName);

        return data.FirstOrDefault();

    }

    // ---- User ------------------------------------------------------------------------- 
    public async Task<UserMainModel?> _1000_Login(string loginname, string password, string schema = "Main")
    {
        string sql = @" select  LoginName, Email, Domain 
                        from " + schema + @".Users e where e.LoginName = @LoginName and Password = sha1(@Password)";
        var data = await _sql.FetchData<UserMainModel?, dynamic>(sql, new { LoginName = loginname, Password = password });
        return data.FirstOrDefault();
    }

    public async Task<UserMainModel?> _1001_GetUserMainById(int id, string schema = "Main")
    {
        string sql = @" select  LoginName, Password, Email, Domain, Status 
                        from " + schema + @".Users e where e.Id = @Id ";
        var data = await _sql.FetchData<UserMainModel?, dynamic>(sql, new { Id = id });
        return data.FirstOrDefault();
    }
    public async Task<UserMainModel?> _1002_GetUserMainByLoginName(string loginName, string schema = "Main")
    {
        string sql = @" select  LoginName, Password, Email, Domain, Status 
                        from " + schema + @".Users e where e.LoginName = @LoginName ";
        var data = await _sql.FetchData<UserMainModel?, dynamic>(sql, new { LoginName = loginName });
        return data.FirstOrDefault();
    }
    public async Task<UserMainModel?> _1003_GetUserMainByEmail(string email, string schema = "Main")
    {
        string sql = @" select  LoginName, Password, Email, Domain, Status 
                        from " + schema + @".Users e where e.Email = @Email ";
        var data = await _sql.FetchData<UserMainModel?, dynamic>(sql, new { Email = email });
        return data.FirstOrDefault();
    }

    public async Task<LoginOutputModel?> _1004_ValidateUserFromEmpmas(
        string empnumber, string dateHired, string secLicense, string movNumber,
        string schema = "SecPis", string connName = "MySqlConn")
    {
        string sql = @" select  e.Empnumber, e.EmpLastNm, e.EmpFirstNm, e.EmpMidNm,
                                c.clNumber, c.ClName,
                                s.code EmpStatCd, s.Name EmpStatus, s.IsResigned,  
                                Position_ PositionCd, p.Name as Position,
                                e.DateHired,
                                e.Sss,
                                e.Tin,
                                e.SecLicense License,
                                e.MovNumber,
                                e.Email,
                                e.passwd
                            from " + schema + ".empmas e " +
                        " left join " + schema + ".client c on c.ClNumber = e.Client_ " +
                        " left join " + schema + ".empstat s on s.code = e.empstat_ " +
                        " left join " + schema + @".Position p on p.Code = e.Position_ 
                          where e.Empnumber = @Empnumber and DateHired = @DateHired and 
                                SecLicense = @SecLicense and MovNumber = @MovNumber ";

        var data = await _sql.FetchData<LoginOutputModel?, dynamic>(sql,
                                                        new
                                                        {
                                                            Empnumber = empnumber,
                                                            DateHired = dateHired,
                                                            SecLicense = secLicense,
                                                            MovNumber = movNumber
                                                        }, connName);

        return data.FirstOrDefault();

    }

    public async Task _1004_InsertUserMain(string loginName, string password, string email, string domain, string schema = "Main")
    {
        string msql = @" Insert into " + schema + @".Users (LoginName, Password, Email, Domain )  values 
                                            (@LoginName, sha1(@Password), @Email, @Domain)";

        await _sql.ExecuteCmd<dynamic>(
            msql,
            new { LoginName = loginName, Password = password, Email = email, Domain = domain });
    }
    public async Task _1005_Update(int id, UserMainModel user, string schema = "Main")
    {
        string msql = @" Update " + schema + @".Users set 
                LoginName   = @LoginName, 
                Password    = sha1(@Password), 
                Email       = @Email, 
                Domain      = @Domain where Id = @Id; ";

        await _sql.ExecuteCmd<dynamic>(
            msql,
            new { Id = id, LoginName = user.LoginName, Password = user.Password, Email = user.Email, Domain = user.Domain });
    }

    public async Task _1006_Delete(int id, string schema = "Main")
    {
        string msql = @" Delete from " + schema + @".Users where Id = @Id; ";

        await _sql.ExecuteCmd<dynamic>(
            msql,
            new { Id = id });
    }

    public async Task<UserMainModel?> _1007_ChangeUserStatus(int id, string status, string schema = "Main")
    {
        string msql = @" Update " + schema + @".Users set Status = @Status where Id = @Id; ";

        await _sql.ExecuteCmd<dynamic>(
            msql,
            new { Status = status, Id = id });

        var data = await this._1001_GetUserMainById(id);
        return data;
    }

    public async Task<string[]> _1008_CreateUserSchema(int id, string connName = "MySqlConn")
    {
        var usr = await _1001_GetUserMainById(id);

        if (usr == null)
        {
            return new string[] { "failed", "user not exists" };
        }

        // Sample Schema ---------------------------------------------
        // db1Pay
        // db1Pis 
        // db1Main 
        // ************************************************************

        string schemapay = "db" + id.ToString().Trim() + "Pay";
        string schemapis = "db" + id.ToString().Trim() + "Pis";
        string schemamain = "db" + id.ToString().Trim() + "Main";
        string sql = "create database if not exists ";

        await _sql.ExecuteCmd<dynamic>(sql + schemapay, new { }, connName);
        await _sql.ExecuteCmd<dynamic>(sql + schemapis, new { }, connName);
        await _sql.ExecuteCmd<dynamic>(sql + schemamain, new { }, connName);

        return new string[] { "suceeded", "schema created" };


    }

    // **** User *****************************************************************


    // **** User Tables **********************************************************



}
