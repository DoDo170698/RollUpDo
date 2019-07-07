using Common;
using Common.Helpers;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web_Public.Handler;
using Web_Public.Mapping;

namespace Web_Public.Controllers
{
    public class HomeController : BaseController
    {
        UserHandler _handler = new UserHandler(_repository);
        public HomeController()
        {
            Log = log4net.LogManager.GetLogger(typeof(HomeController));
        }
        public ActionResult Index()
        {
            return RedirectToAction("Login");
            //return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            Log.Debug("------Processing Login------");
            if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
            {
                return View(model);
            }
            UserModels UserFromDb = await _handler.GetByUserName(model.UserName);
            string pass = StringHelper.stringToSHA512(UserFromDb.Password);
            //TODO: xu ly tam thoi
            if (UserFromDb != null && model.Password.Equals(pass))
            {
                Session[SessionEnum.UserID] = UserFromDb.Id;
                Session[SessionEnum.UserName] = UserFromDb.UserName;
                _cacheFactory.RemoveCache("ModuleRoles");
                _cacheFactory.SaveCache("ModuleRoles","get db ra cái role của nó");
                Log.Debug(string.Format("------ Login is Ok by {0} ------", UserFromDb.UserName));
                Log.Debug("------END Login------");
                return RedirectToAction("Index", "DichVu",new { xxx= "Đăng nhập thành công" });
            }

            // hiển thị lỗi sau
            Log.Debug("------ Login is thất bại ------");
            return RedirectToAction("Index", "DichVu", new { xxx= "Đăng nhập lỗi" });

            //return View(model);
        }

    }
}