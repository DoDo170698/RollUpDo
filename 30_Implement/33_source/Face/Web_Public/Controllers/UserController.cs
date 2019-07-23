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

        public string CName = "User";
        public string CText = "nhóm tài khoản";
        UserHandler _userHandler = new UserHandler(_repository);

        public async Task<ActionResult> Index(PageHelper model)
        {
            ViewBag.CName = CName;
            ViewBag.CText = CText;
            ViewBag.Title = "Danh sách  " + CText;

            var result = await _userHandler.GetAllAsync(model);
            if (result.Count() < 1)
            {
                return View(new List<UserModels>());
            }
            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult> Table()
        {
            var result = await _userHandler.GetAllAsync();
            if (result.Count() < 1)
            {
                return View(new List<UserModels>());
            }
            return View(result);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CName = CName;
            ViewBag.CText = CText;
            ViewBag.Title = "Tạo mới";
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserModels model)
        {
            if (ModelState.IsValid)
            {
                var record = await _userHandler.CreateAsync(model);
                if(record <= 0)
                {
                    ViewBag.Error = "Không tạo được" + CText;
                    return View(model);
                }
                return RedirectToAction("Index", "User");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Update(long id)
        {
            ViewBag.CName = CName;
            ViewBag.CText = CText;
            ViewBag.Title = "Cập nhật " + CText;
            var record = await _userHandler.ReadAsync(id);
            if(record != null)
            {
                return View(record);
            }
            ViewBag.Error = "Không tìm thấy bản ghi tương ứng";
            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public async Task<ActionResult> Update(UserModels model)
        {
            var record = await _userHandler.ReadAsync(model.Id);
            if(record == null)
            {
                ViewBag.Error = "Không tìm thấy bản ghi tương ứng";
                return RedirectToAction("Index", "User");
            }

            var result = await _userHandler.UpdateAsync(model);
            if (result <= 0) 
            {
                ViewBag.Error = "Không thế sửa" + CText;
                return RedirectToAction("Index", "User");
            }
            return View(result);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(long id)
        {
            var result = await _userHandler.DeleteAsync(id);
            if(result <= 0)
            {
                return Json(new { Message = "Thao tác không thành công", IsSuccess = false });
            }
            return Json(new { Message = "Thao tác thành công", IsSuccess = true });
        }
    }
}