using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web_Public.Handler;

namespace Web_Public.Controllers
{
    public class PersonController : BaseController
    {
        public string CName = "Person";
        public string CText = "Nhân viên";

        UnitHandler _unitHandler = new UnitHandler(_repository);
        PersonHandler _Handler = new PersonHandler(_repository);
        async Task<bool> baseView(Func<UnitViewModels, bool> predicate = null, object selectedValue = null, string title = null)
        {
            ViewBag.CName = CName;
            ViewBag.CText = CText;
            ViewBag.Title = title == null ? "Danh sách " + CText : title;
            // load SelectList
            IEnumerable<UnitViewModels> unitParent;
            if (predicate == null)
            {
                unitParent = await _unitHandler.GetAllAsync();
            }
            else
            {
                unitParent = await _unitHandler.GetAllAsync(predicate);
            }

            if (unitParent != null && unitParent.Count() > 0)
                ViewBag.UnitCode = selectedValue == null ? new SelectList(unitParent, "Code", "Name") : new SelectList(unitParent, "Code", "Name", selectedValue);
            return true;

        }
        public async Task<ActionResult> Index(PageHelper helper)
        {
            await baseView();

            var result = await _Handler.GetAllAsync();
            if (result == null || result.Count() < 1)
            {
                return View(new List<PersonViewModels>());
            }
            return View(helper);
        }
        public async Task<ActionResult> Table()
        {
            await baseView();
            var result = await _Handler.GetAllPersonAsync();
           // if (result == null || result.Count() < 1)
           // {
           //     return View(new List<PersonViewModels>());
           // }
           //// result = result.OrderByDescending(o => o.CreateDate);
            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult> Create(string UnitCode)
        {
           
            await baseView(null,null, "Tạo mới " + CText);

            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> Create(PersonViewModels model)
        {
           
            await baseView(null,model.UnitCode, "Tạo mới " + CText);
            if (ModelState.IsValid)
            {
                var record = await _Handler.CreateAsync(model);
                if (record <= 0)
                {
                    ViewBag.Error = "Không thể tạo được bản mới";
                    return View(model);
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> Update(string code)
        {
            var record = await _Handler.ReadAsync(code);
            if (record != null)
            {
                await baseView(null,record.UnitCode, "Cập nhật " + CText);
                return View(record);
            } 
            await baseView(null,null, "Cập nhật " + CText);
            ViewBag.Error = "Không tìm thấy thành viên này trong hệ thống";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<ActionResult> Update(PersonViewModels model)
        {
            if (ModelState.IsValid)
            {
                var record = await _Handler.UpdateAsync(model);
                if (record <= 0)
                {
                    await baseView(null,model.UnitCode, "Cập nhật " + CText);
                    ViewBag.Error = "Không sửa được" + CText;
                    return View(model);
                }
                ViewBag.Noti = "Cập nhật thành công";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            var record = await _Handler.DeleteAsync(id);
            if (record <= 0)
            {
                return Json(new { Message = "Thao tác không thành công", IsSuccess = false });
            }
            return Json(new { Message = string.Format("Đã xóa thành công"), IsSuccess = true });
        }

        //TODO dưới này chưa làm 

        [HttpGet]
        public async Task<ActionResult> Detail(string code)
        {
            var record = await _unitHandler.ReadAsync(code);
            if (record == null)
            {
                return Json(new { Message = "Không thế xem được!", IsSuccess = false });
            }
            return View(record);
        }


    }
}