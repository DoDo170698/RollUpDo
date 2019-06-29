using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Common.Helpers;
using Entities.Models;
using Web.Areas.Management.Filters;
using Web.Helpers;
namespace Web.Areas.Management.Controllers
{
    [RouteArea("Management", AreaPrefix = "quan-ly")]
   // [RoutePrefix("nhom-quyen")]
    public class MenuController : BaseController
    {
        [Route("quan-ly-menu", Name = "MenuIndex")]
        [ValidationPermission(Action = ActionEnum.Read, Module = ModuleEnum.Role)]
        public async Task<ActionResult> Index()
        {
            IEnumerable<Entities.Models.Menu> roles = await _repository.GetRepository<Menu>().GetAllAsync();
            return View(roles);
        }
    }
}