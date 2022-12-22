using Microsoft.AspNetCore.Mvc;
using MysqlApiLibrary.DataAccess;
using MysqlApiLibrary.DataAccess.Login;
using MysqlApiLibrary.Models;
using MysqlApiLibrary.Models.Login;
using MySqlX.XDevAPI;
using System.Data;

namespace MysqlApi.Controllers.Login; 

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly ILoginAccess _login;
    private readonly ISchemaAccess _schema;
    private readonly IConfiguration _config; 


    public LoginController(ILoginAccess login, ISchemaAccess schema, IConfiguration config)
    {
        _login = login;
        _schema = schema;
        _config = config;
    }



    [HttpGet]
    public async Task<LoginOutputModel?> Login(string username, string password)
    {
        return await _login.LoginEmployee(_schema._0000getDefaultPisSchema(), username, password);
    }

    
    [HttpGet("UserList")] //For testing only
    public async Task<List<LoginOutputModel?>> Login()
    {
        return await _login.LoginEmployee(_schema._0000getDefaultPisSchema());
    }

    [HttpGet("GetConString")] 
    public string GetConnString(string connName = "MySqlConn")
    {
        return _config.GetConnectionString(connName);
    }

    [HttpGet("0000/GetMainScheme")]
    public string MainScheme()
    {
        return _schema._0000getDefaultSchema();
    }
    [HttpGet("0000/GetPisScheme")]
    public string PisScheme()
    {
        return _schema._0000getDefaultPisSchema();
    }
    [HttpGet("0000/GetPayScheme")]
    public string PayScheme()
    {
        return _schema._0000getDefaultPaySchema();
    }


    [HttpGet("0001/EmpmasByEmpnumber/{empNumber}/{schema}/{connName}")]
    public async Task<ActionResult<LoginOutputModel?>> EmpmasByEmpnumber(string empNumber, string schema="SecPis", string connName = "MySqlConn")
    {
        var res = await _login._00001_EmpmasByEmpNumber(empNumber, schema, connName);
        return Ok(res);
        //try
        //{

        //} catch (Exception ex)
        //{
        //    return BadRequest(ex.Message);
        //}
    }


    [HttpHead("1000/CreateMainSchema")]
    public void CreateMainSchema(string schemaName="Main")
    {
        _schema._1001_CreateDefaultSchema(schemaName);
        _schema._1002_CreateLoginTbl(schemaName);
    }

    [HttpGet("1000/userlogin/{loginname}/{password}")]
    public async Task<ActionResult<UserMainModel>> Get(string loginname, string password)
    {
        try
        {
            var output = await _login._1000_Login(loginname, password);
            return Ok(output);
        } catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("1001/{id}")]
    public async Task<ActionResult<UserMainModel>> Get(int id)
    {
        var output = await _login._1001_GetUserMainById(id);
        return Ok(output);
    }

    [HttpGet("1002/GetUser/{loginname}")]
    public async Task<ActionResult<UserMainModel>> Get(string loginname)
    {
        var output = await _login._1002_GetUserMainByLoginName(loginname);
        return Ok(output);
    }


    [HttpGet("1003/GetUser/{email}")]
    public async Task<ActionResult<UserMainModel>> GetByEmail(string email)
    {
        var output = await _login._1003_GetUserMainByEmail(email);
        return Ok(output);
    }


    //[HttpPut("1004/insertUser")]
    //public async Task<ActionResult<UserMainModel?>> Insert(UserMainModel user)
    //{
    //    var usr = await _login._1002_GetUserMainByLoginName(user.LoginName!);
    //    if (usr == null)
    //    {
    //        user.Domain = _config.GetSection("Schema:domain").Value;
    //        await _login._1004_InsertUserMain(user);
    //        usr = await _login._1002_GetUserMainByLoginName(user.LoginName!);
    //    } else
    //    {
    //        usr.Id = -1; 
    //    }
    //    return Ok(usr);
    //}

    [HttpGet("1004/validateUserFromEmpmas/{empnumber}/{dateHired}/{secLicense}/{movNumber}/{schema}")]
    public async Task<ActionResult<UserMainModel?>> 
        validateUserFromEmpmas(string empnumber, string dateHired, string secLicense, string movNumber,
        string schema = "Main", string connName = "MySqlConn")
    {
        // Check kung available sya sa secpis users  ----------------------------
        var userFromEmpmas = await _login._1004_ValidateUserFromEmpmas(empnumber, dateHired, secLicense, movNumber, schema);
        return Ok(userFromEmpmas);
         
    }


    //[HttpPost("1004/InsertUserFromEmpmas/")]
    //[HttpPost("1004/InsertUserFromEmpmas")]
    [HttpPost("1004/InsertUserFromEmpmas/{empnumber}/{password}/{email}/{domain}/{schema}/{connName}")]
    public async Task<ActionResult<UserMainModel?>> InsertUserFromEmpmas(
        string empnumber, string password, string email, string domain, 
        string schema = "Main", string connName = "MySqlConn")
    {
        // Check kung available sya sa main users  ----------------------------
        // await _login._1004_InsertUserMain(empnumber, password,email, domain, schema );
        await _login._1004_InsertUserMain(empnumber, password, email, domain, schema);
        //var user = await _login._00001_EmpmasByEmpNumber(empnumber);
        var user = await _login._00001_EmpmasByEmpNumber(empnumber, "secpis", connName);




        // var userId = user(user => user.Id == empnumber).Value;

        //string domain = Convert.ToString(user.Domain);
        return Ok(user);

    }

    [HttpPut("1005/updateUser/{id}")]
    public async Task<ActionResult<UserMainModel?>> Update(int id, UserMainModel user)
    {
        await _login._1005_Update(id, user);
        var usr = _login._1001_GetUserMainById(id); 
        return Ok(usr);
    }

    [HttpPost("1007/changeUserStatus/{id}/{status}")]
    public async Task<ActionResult<UserMainModel?>> UpdateStatus(int id, string status)
    {
        var usr = await _login._1007_ChangeUserStatus(id, status); 
        return Ok(usr);
    }

    [HttpPost("1008/createUserSchema/{id}/{connName}")]
    public async Task<ActionResult<UserMainModel?>> CreateUserSchema(int id, string connName = "MySqlConn")
    {
        var usr = await _login._1008_CreateUserSchema(id);
        return Ok(usr);
    }










    //// GET: api/<LoginController>
    //[HttpGet]
    //public IEnumerable<string> Get()
    //{
    //    return new string[] { "value1", "value2" };
    //}



    //// GET api/<LoginController>/5
    //[HttpGet("{empnumber}")]
    //public string Get(int id)
    //{
    //    return "value";
    //}





    //// POST api/<LoginController>
    //[HttpPost]
    //public void Post([FromBody] string value)
    //{
    //}

    //// PUT api/<LoginController>/5
    //[HttpPut("{id}")]
    //public void Put(int id, [FromBody] string value)
    //{
    //}

    //// DELETE api/<LoginController>/5
    //[HttpDelete("{id}")]
    //public void Delete(int id)
    //{
    //}
}
