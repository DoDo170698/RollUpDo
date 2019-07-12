using Common;
using Entities.Models;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web_Public.Handler;
using Web_Public.Interface;
using Web_Public.Mapping;

namespace Web_Public.Controllers
{
    public class UserController : BaseController
    {
        UserHandler _handler = new UserHandler(_repository);
        public UserController()
        {
            Log = log4net.LogManager.GetLogger(typeof(UserController));
        }
        public async Task<ActionResult> Index(PageHelper model)
        {
            var result = await _handler.GetAllAsync(model);
            return View(result);
        }
        public async Task<ActionResult> Table(PageHelper model)
        {
            // TODO Not Yet

            // tạm thời load tất ra dùng data table phân trang ở view nha.
            // view cái này chỉ cõ lỗi cái bảng dữ liệu thôi k có layout để 
            // cps thể trả về view hoặc Json 
            var record = await _handler.GetAllAsync(model, false);
            //return View(record);
            return Json(record, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(UserModels model)
        {
            Log.Debug("------Processing User createing------");
            if (ModelState.IsValid)
            {
                var result = await _handler.Create(model);

                if (result == 0)
                {
                    Log.Debug("------ User đang được tạo mới từ một nơi khác ------");
                    // tempdata ra view nữa nha
                    return View(model);
                }
                else if (result == -1)
                {
                    Log.Error("------ User này đã tồn tại ------");
                    // tempdata ra view nữa nha
                    return View(model);
                }
                Log.Debug("------ Tạp mới thành công user... ------");
                // tempdata ra view nữa nha
                return RedirectToAction("Index");
            }
            Log.Debug("------ Login is thất bại ------");
            // Temp data ...
            return RedirectToAction("Index", "DichVu", new { xxx = "Đăng nhập lỗi" });

            //return View(model);
        }
    }
}