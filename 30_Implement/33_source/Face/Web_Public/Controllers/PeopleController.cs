using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Entities.ViewModels;
using Interface;
using Web_Public.Handler;

namespace Web_Public.Controllers
{
    public class PeopleController : BaseController
    {
        public string CName = "People";
        public string CText = "thành viên";
        PersonHandler _peopleHandler = new PersonHandler(_repository);
        UnitHandler _uniteHandler = new UnitHandler(_repository);
        // GET: People
        public async Task<ActionResult> Index(PageHelper model)
        {
            ViewBag.CName = CName;
            ViewBag.CText = CText;
            ViewBag.Title = "Danh sách " + CText;

            var result = await _peopleHandler.GetAllAsync(model);
            if(result.Count() < 1)
            {
                return View(new List<PersonViewModels>());
            }
            return View(result);
        }

        public async Task<ActionResult> Table()
        {
            var result = await _peopleHandler.GetAllAsync();
            if (result.Count() < 1)
            {
                return View(new List<PersonViewModels>());
            }
            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            ViewBag.CName = CName;
            ViewBag.CText = CText;
            ViewBag.Title = "Tạo mới " + CText;

            var unit = await _uniteHandler.GetAllAsync();
            ViewBag.UnitId = new SelectList(unit, "Id", "Name", "Code");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(PersonViewModels model)
        {
            if (ModelState.IsValid)
            {
                var record = await _peopleHandler.CreateAsync(model);
                if(record <= 0)
                {
                    ViewBag.Error = "Không tạo mới được" + CText;
                    return View(model);
                }
                return RedirectToAction("Index", "People");
            }
            var unit = await _uniteHandler.GetAllAsync();
           // ViewBag.UnitId = new SelectList(unit, "Id", "Name", "Code", model.UnitCode);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Update(long id)
        {
            ViewBag.CName = CName;
            ViewBag.CText = CText;
            ViewBag.Title = "Cập nhật " + CText;

            var record = await _peopleHandler.ReadAsync(id.ToString());
            if (record != null)
            {
                return View(record);
            }
            ViewBag.Error = "Không tìm thấy thành viên này trong hệ thống";
            return RedirectToAction("Index", "People");
        }
        [HttpPost]
        public async Task<ActionResult> Update(PersonViewModels model)
        {
            if (ModelState.IsValid)
            {
                
                var updateing = await _peopleHandler.ReadAsync(model.Id.ToString());
                if (updateing == null)
                {
                    ViewBag.Error = "Không tìm thấy bản ghi tương ứng";
                    return RedirectToAction("Index", "People");
                }
                var result = await _peopleHandler.UpdateAsync(model);
                if (result <= 0)
                {
                    ViewBag.Error = "Không sửa được" + CText;
                    return View(model);
                }

                var unit = await _uniteHandler.GetAllAsync();
               // ViewBag.UnitId = new SelectList(unit, "Id", "Name", "Code", model.UnitCode);

                ViewBag.Noti = "Cập nhật thành công";
                return RedirectToAction("Index", "People");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Detail(long id)
        {
            var record = await _peopleHandler.ReadAsync(id.ToString());
            if(record == null)
            {
                return Json(new { Message = "Không thế xem được!", IsSuccess = false });
            }
            return View(record);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(long id)
        {
            var record = await _peopleHandler.DeleteAsync(id.ToString());
            if (record <= 0)
            {
                return Json(new { Message = "Thao tác không thành công", IsSuccess = false });
            }
            return Json(new { Message = string.Format("Đã xóa thành công"), IsSuccess = true });
        }

    }
}