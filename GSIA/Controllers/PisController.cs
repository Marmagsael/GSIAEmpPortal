using LibraryMySql;
using LibraryMySql.Models.Pis;
using LibraryMySql.Models.Sample;
using Microsoft.AspNetCore.Mvc;
using System.Security.Permissions;

namespace GSIA.Controllers
{
    public class PisController : Controller
    {
        public readonly IMySqlDataAccess _mysql;
        public readonly IConfiguration _configuration;
        

        public PisController(IMySqlDataAccess mysql, 
                             IConfiguration configuration)
        {
            _mysql = mysql;
            _configuration = configuration; 
        }

        public async Task<IActionResult> Index()
        {
            string PisSchema = "secpis";
            string clnumber = "00471"; 

            string sql = @" select  e.EmpNumber, 
                                    concat(Trim(e.emplastnm),', ',Trim(e.empfirstnm), ' ',trim(e.empmidnm))  as EmpName,  
                                    Client_     as DeploymentCode, 
                                    c.clname    as DeploymentName 
                            from "+ PisSchema + @".empmas e 
                            left join secpis.client c on c.clnumber = e.client_  
                            where e.client_ = @ClNumber 
                            order by e.emplastnm, e.empfirstnm ";
            var empmas = await _mysql.FetchData<EmpmasBasicModel, dynamic>(sql, new { ClNumber = clnumber });
            ViewBag.Empmas = empmas; 

            //var conn = _mysql.GetConnString();
            //ViewBag.Conn = conn; 


            return View(empmas);
        }

        public IActionResult Ram()
        {
            return View();
        }


    }
}
