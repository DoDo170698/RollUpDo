using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web_Public.Handler;

namespace Web_Public.Controllers
{
    public class UnitController : BaseController
    {
        public string CName = "Unit";
        public string CText = "đơn vị";
        UnitHandler _unitHandler = new UnitHandler(_repository);

        public async Task<ActionResult> Index(PageHelper helper)
        {
            ViewBag.CName = CName;
            ViewBag.CText = CText;
            ViewBag.Title = "Danh sách " + CText;

            var result = await _unitHandler.GetAllAsync();
            if (result.Count() < 1)
            {
                return View(new List<UnitViewModels>());
            }
            return View(helper);
        }
        public async Task<ActionResult> Table()
        {
            var result = await _unitHandler.GetAllAsync();
            if (result.Count() < 1)
            {
                return View(new List<UnitViewModels>());
            }
            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            ViewBag.CName = CName;
            ViewBag.CText = CText;
            ViewBag.Title = "Tạo mới " + CText;

            //var unit = await _unitHandler.GetAllAsync();
            //ViewBag.ParentId = new SelectList(unit, "Name", "Code");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(UnitViewModels model)
        {
            if (ModelState.IsValid)
            {
                var record = await _unitHandler.CreateAsync(model);
                if(record <= 0)
                {
                    ViewBag.Error = "Không thể tạo được bản mới";
                    return View(model);
                }

                //var unit = await _unitHandler.GetAllAsync();
                //ViewBag.ParentId = new SelectList(unit, "Name", "Code", model.ParentId);

                return RedirectToAction("Index","Unit");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Update(long id)
        {
            ViewBag.CName = CName;
            ViewBag.CText = CText;
            ViewBag.Title = "Cập nhật " + CText;

            var record = await _unitHandler.GetAllAsync();
            if(record != null)
            {
                return View(record);
            }
            ViewBag.Error = "Không tìm thấy thành viên này trong hệ thống";
            return RedirectToAction("Index", "Unit");
        }
        [HttpGet]
        public async Task<ActionResult> Update(UnitViewModels model)
        {
            if (ModelState.IsValid)
            {
                var updating = await _unitHandler.ReadAsync(model.Id);
                if (updating == null) 
                {
                    ViewBag.Error = "Không tìm thấy bản ghi tương ứng";
                    return RedirectToAction("Index", "Unit");
                }
                var record = await _unitHandler.UpdateAsync(model);
                if (record <= 0)
                {
                    ViewBag.Error = "Không sửa được" + CText;
                    return View(model);
                }
                ViewBag.Noti = "Cập nhật thành công";
                return RedirectToAction("Index", "Unit");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Detail(long id)
        {
            var record = await _unitHandler.ReadAsync(id);
            if (record == null)
            {
                return Json(new { Message = "Không thế xem được!", IsSuccess = false });
            }
            return View(record);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(long id)
        {
            var record = await _unitHandler.DeleteAsync(id);
            if (record <= 0)
            {
                return Json(new { Message = "Thao tác không thành công", IsSuccess = false });
            }
            return Json(new { Message = string.Format("Đã xóa thành công"), IsSuccess = true });
        }
    }
}