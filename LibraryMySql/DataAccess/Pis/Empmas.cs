using LibraryMySql.Models.Pis;

namespace LibraryMySql.DataAccess.Pis; 

public class Empmas
{
    public readonly IMySqlDataAccess _mysql;

    public Empmas(IMySqlDataAccess mysql)
    {
        _mysql = mysql;
    }

    public async Task<List<EmpmasBasicModel>> Get()
    {
        string sql = @"select e.Empnumber, 
                              concat(Trim(e.emplastnm),', ',Trim(empfirstnm), ' ', trim(e.EmpMidNm) )  as EmpName, 
                              e.Client_ as DeploymentCode, 
                              c.ClName as DeploymentName  
                        From empmas e 
                        left join Client c on c.ClNumber = e.Client_ 
                        order by c.ClName, e.emplastnm, e.empfirstnm " ;

        var empmas = await _mysql.FetchData<EmpmasBasicModel, dynamic>(sql, new { });
        return empmas; 

    }
}
