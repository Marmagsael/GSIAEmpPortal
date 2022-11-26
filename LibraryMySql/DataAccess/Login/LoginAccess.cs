using LibraryMySql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMySql.DataAccess.Login
{
    public class LoginAccess : ILoginAccess
    {
        private readonly IMySqlDataAccess _mysql;

        public LoginAccess(IMySqlDataAccess mysql)
        {
            _mysql = mysql;
        }

        public async Task<LoginOutputModel?> LoginEmployee(LoginInputModel input)
        {
            string schema = input.Schema;
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

            var data = await _mysql.FetchData<LoginOutputModel?, dynamic>(sql, new { Empnumber = input.EmpNumber });

            return data.FirstOrDefault();

        }

        public async Task<LoginOutputModel?> LoginEmployee(string Schema, string EmpNumber, string Password)
        {
            string schema = Schema;
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

            var data = await _mysql.FetchData<LoginOutputModel?, dynamic>(sql, new { Empnumber = EmpNumber });

            return data.FirstOrDefault();

        }


    }
}
