using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MysqlApiLibrary.DataAccess;
using MysqlApiLibrary.DataAccess._100Main;
using MysqlApiLibrary.Models.Main;
using MySqlX.XDevAPI;

namespace MysqlApi.Controllers._100Main
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuUsersAccess _menu;



        public MenuController(IMenuUsersAccess menu)
        {
            _menu = menu;
        }


        [HttpGet("GetMenu")]
        public async Task<List<MenuUsersModel?>> GetMenu(string schema = "Main")
        {
            return await _menu.GetMenuUsers(schema);
        }

    }
}
