using GsiaLibrary.DataAccess._100Main;
using GsiaLibrary.Models.UI._001Main;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GSIA.Components
{
    public class LeftMenuViewComponent : ViewComponent
    {

        private readonly IMenuData _menu;
        public LeftMenuViewComponent(IMenuData menu)
        {
            _menu = menu;
        }

        public IViewComponentResult Invoke()
        {
            var getMenu = _menu._10000_GetMenu();
            var menu = JsonConvert.DeserializeObject<List<MenuOutputModel>>(getMenu.QueryResult!);
            return View("~/Views/Shared/Pis/Components/LeftMenu/Default.cshtml",menu);
        }
    }
}
