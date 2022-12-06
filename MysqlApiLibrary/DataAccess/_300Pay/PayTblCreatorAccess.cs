namespace MysqlApiLibrary.DataAccess._300Pay;

public class PayTblCreatorAccess : IPayTblCreatorAccess
{
    private readonly IMysqlDataAccess _sql;

    public PayTblCreatorAccess(IMysqlDataAccess sql)
    {
        _sql = sql;
    }

    public async Task<string> _1101PayrollGrp(string schema, string connName = "MySqlConn")
    {
        /* 
            IdRateType : 1) per hout 2) per day 3) per week 4) semi-monthly 5 ) monthly  
            HrsInDay    : Standard duty per day to consider as Regular rate, in PH it is 8 hrs 
            LastPayPrd : Last pay period posted 
        */

        string sql = "CREATE TABLE  " + schema + @".PGrp (
                          Id            INTEGER       UNSIGNED NOT NULL AUTO_INCREMENT,
                          GrpName       varchar(60)   DEFAULT NULL,
                          Status        Char(1)       DEFAULT 'A',
                          RateHr        double(10,4)  NOT NULL            DEFAULT '0.00',
                          MinRateHr     double(10,4)  NOT NULL            DEFAULT '0.00',
                          IdRateType    INTEGER       UNSIGNED            Default 0,     
                          HrsInDay      double(10,2)  NOT NULL            DEFAULT '0.00',
                          LastPayPrd    char(15)      NOT NULL            DEFAULT '',
                          IdSGrade      INTEGER       UNSIGNED            Default 0,
                          PRIMARY       KEY(`Id`)) ENGINE = InnoDB";
        await _sql.ExecuteCmd<dynamic>(sql, new { }, connName);

        return "succeeded";
    }

    public async Task<string> _1102PayrollGrpSub(string schema, string connName = "MySqlConn")
    {
        string sql = "CREATE TABLE  " + schema + @".PGrpInfo (
                          Id          INTEGER       UNSIGNED NOT NULL ,
                          Addr1       varchar(150)  DEFAULT NULL,
                          Addr2       varchar(150)  DEFAULT NULL,
                          IdArea      INTEGER               UNSIGNED    Default 0,
                          IdRegion    INTEGER       UNSIGNED            Default 0,
                          IdParent    INTEGER               UNSIGNED    Default 0,
                          RateMo      double(10,2)  NOT NULL            DEFAULT '0.00',
                          MinRateMo   double(10,2)  NOT NULL            DEFAULT '0.00',
                          BillRateHr  double(9,2)                       DEFAULT NULL,
                          MealAllow   double(6,2)   NOT NULL            DEFAULT '0.00',
                          PRIMARY     KEY(`Id`)) ENGINE = InnoDB";
        await _sql.ExecuteCmd<dynamic>(sql, new { }, connName);

        return "succeeded";
    }

    //public async Task<string> _1101PayrollGrp()
    //{
    //    return "succeeded";
    //}
}
