using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_Public.Controllers
{
    public class DichVuController : Controller
    {
        // GET: DichVu
        public ActionResult Index(string xxx)
        {
            // return View();
            return Json(xxx, JsonRequestBehavior.AllowGet);
        }
    }
}