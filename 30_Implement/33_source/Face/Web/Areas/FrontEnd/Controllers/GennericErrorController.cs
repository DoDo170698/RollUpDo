using Entities.Models.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.FrontEnd.Controllers
{
    [RouteArea("FrontEnd", AreaPrefix = "dich-vu")]
    public class GennericErrorController : PublicController
    {
        public const string CRoute = "loi";
        public const string CName = "GennericError";
        public const string CText = "";
        // GET: FrontEnd/GennericError
        [Route("danh-muc" + CRoute, Name = CName + "_Index")]
        public async Task<ActionResult> Index()
        {
            await _repository.GetRepository<Account>().ReadAsync(1);
            return View();
        }
        public ActionResult Error404()
        {
            return View();
        }
        public ActionResult Error403()
        {
            return View();
        }
        public ActionResult Error500()
        {
            return View();
        }
        public ActionResult Error400()
        {
            return View();
        }
        public ActionResult Error505()
        {
            return View();
        }
    }
}