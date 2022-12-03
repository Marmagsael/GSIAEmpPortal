
using MysqlApiLibrary.Models;
using MySqlX.XDevAPI;

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

        return _sql.FetchData<LoginOutputModel?, dynamic>(sql, new {  });

        

    }

    


}
