using AutoMapper;
using Entities.Models.SystemManage;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web_Public.Handler;
using Web_Public.Interface;

namespace Web_Public.Controllers
{
    public class RoleController : BaseController
    {
        public string CName = "Role";
        public string CText = "nhóm quyền";
        RoleHandler _roleHandler = new RoleHandler(_repository);
        // GET: Role
        public async Task<ActionResult> Index(PageHelper model)
        {
            ViewBag.CName = CName;
            ViewBag.CText = CText;
            ViewBag.Title = "Danh sách " + CText;
            var result = await _roleHandler.GetAllAsync(model);
            if (result.Count() < 1)
            {
                return View(new List<RoleViewModels>());
            }
            return View(result);
        }
        [HttpGet]
        public async Task<ActionResult> Table()
        {
            var result = await _roleHandler.GetAllAsync();
            if (result.Count() < 1)
            {
                return View(new List<RoleViewModels>());
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
        public async Task<ActionResult> Create(RoleViewModels model)
        {
            if (ModelState.IsValid)
            {
                var record = await _roleHandler.CreateAsync(model);
                if (record <= 0)
                {
                    ViewBag.Error = "Không tạo được" + CText;
                    return View(model);
                }
                return RedirectToAction("Index", "Role");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            ViewBag.CName = CName;
            ViewBag.CText = CText;
            ViewBag.Title = "Cập nhật"+CText;
            var updateItem = await _roleHandler.ReadAsync(id);
            if (updateItem != null)
            {
                return View(updateItem);
            }
            ViewBag.Error = "Không tìm thấy bản ghi tương ứng";
            return RedirectToAction("Index", "Role");
        }
        [HttpPost]
        public async Task<ActionResult> Update(RoleViewModels model)
        {
            if (ModelState.IsValid)
            {
                var updateing = await _roleHandler.ReadAsync(model.Id);
                if (updateing == null)
                {
                    ViewBag.Error = "Không tìm thấy bản ghi tương ứng";
                    return RedirectToAction("Index", "Role");
                }
                var result = await _roleHandler.UpdateAsync(model);
                if (result <= 0)
                {
                    ViewBag.Error = "Không sửa được" + CText;
                    return View(model);
                }
                ViewBag.Noti = "Cập nhật thành công";// show 1 cái noti ra để thông báo 
                return RedirectToAction("Index", "Role");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {

            var result = await _roleHandler.DeleteAsync(id);
            if (result <= 0)
            {
                return Json(new { Message = "Thao tác không thành công", IsSuccess = false});
            }
            return Json(new { Message = string.Format("Đã xóa thành công "), IsSuccess = true });

        }
        /// <summary>
        /// Bên dưới này không quan tâm sau phải xóa đi 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public async Task<ActionResult> Init(int? x)
        {
            x = x ?? 10;
            for (int i = 0; i < x; i++)
            {
                var item = new RoleViewModels() { Code = "Role" + i, Name = "Role " + i, Description = "Role " + i };
                await _roleHandler.CreateAsync(item);
            }
            return RedirectToAction("Index", "Role");
        }
       
    }
}

