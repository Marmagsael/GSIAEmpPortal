using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MysqlApiLibrary.DataAccess;
using MysqlApiLibrary.Models.Main;
using System.Runtime.CompilerServices;

namespace MysqlApi.Controllers.Login; 

[Route("api/[controller]")]
[ApiController]
public class SchemaController : ControllerBase
{
    private readonly ISchemaAccess _scheme;

    public SchemaController(ISchemaAccess scheme)
    {
        _scheme = scheme;
    }

    [HttpGet("0001/CheckIfTableExists/{schema}/{tableName}")]
    public async Task<ActionResult<SchemaStructureModel>> CheckIfTableExists(string schema, string tableName)
    {
        var res =  await _scheme._0001_CheckIfTableExists(schema, tableName);
        return Ok(res); 
    }

}
