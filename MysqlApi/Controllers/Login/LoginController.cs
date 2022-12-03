using Microsoft.AspNetCore.Mvc;
using MysqlApiLibrary.DataAccess;
using MysqlApiLibrary.DataAccess.Login;
using MysqlApiLibrary.Models;
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
        return await _login.LoginEmployee(_schema.getDefaultPisSchema(), username, password);
    }

    
    [HttpGet("UserList")] //For testing only
    public async Task<List<LoginOutputModel?>> Login()
    {
        return await _login.LoginEmployee(_schema.getDefaultPisSchema());
    }

    [HttpGet("ConString")] 
    public string GetConnString(string connName = "MySqlConn")
    {
        return _config.GetConnectionString(connName);
    }



    [HttpGet("PisScheme")]
    public string PisScheme()
    {
        return _schema.getDefaultPisSchema(); 
    }

    [HttpGet("CreateMainSchema")]
    public void CreateMainSchema(string schemaName="Main")
    {
        _schema._1000_CreateDefaultSchema(schemaName);
        _schema._2001_CreateLoginTbl(schemaName);
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
