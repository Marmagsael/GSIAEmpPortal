using LibraryMySql.Models.Pis;

namespace LibraryMySql.DataAccess.Pis; 

public class Empmas
{
    public readonly IMySqlDataAccess _mysql;

    public Empmas(IMySqlDataAccess mysql)
    {
        _mysql = mysql;
    }

    //public List<EmpmasBasicModel>  Get()
    //{
    //    string sql = "select empnumber, concat(Trim(emplastnm),', ',Trim(empfirstnm) )";
    //    var empmas = _mysql.FetchData<EmpmasBasicModel, dynamic>(sql, new { }); 
    //}
}
