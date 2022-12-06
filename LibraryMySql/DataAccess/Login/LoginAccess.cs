using Dapper;
using LibraryMySql.Models;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public async Task<LoginOutputModel?> LoginEmployee(string Schema, string EmpNumber)
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
                            " where e.Empnumber = @Empnumber";

            var data = await _mysql.FetchData<LoginOutputModel?, dynamic>(sql, new { Empnumber = EmpNumber });

            return data.FirstOrDefault();

        }

        public async Task<LoginOutputModel?> LoginEmployeeByEmpnoMain(LoginInputModel input)
        {
            string schema = input.Schema;
            var parameter = new DynamicParameters();
            parameter.Add("@EmpNumber", input.EmpNumber, System.Data.DbType.String);
            var cmd = "select * from " + schema + ".users  where Empnumber = @EmpNumber";

            var data = await _mysql.FetchData<LoginOutputModel?, dynamic>(cmd, parameter);
            return data.FirstOrDefault();
        }


        public async Task<LoginOutputModel?> FetchEmployeeByEmailMain(string email)
        {
            string schema = "main";
            var parameter = new DynamicParameters();
            parameter.Add("@Email", email, System.Data.DbType.String);
            var cmd = "select * from " + schema + ".users  where Email = @email";

            var data = await _mysql.FetchData<LoginOutputModel?, dynamic>(cmd, parameter);
            return data.FirstOrDefault();
        }



    }
}
