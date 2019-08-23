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
        public int _level
        {
            get
            {
                if (!string.IsNullOrEmpty(codeParent))
                {
                    var unit = _unitHandler.Read(codeParent);

                    return unit == null ? 0 : unit.Level;
                }
                return 0;
            }
            set
            {
                this._level = value;
            }
        }
        string codeParent = null;
        UnitHandler _unitHandler = new UnitHandler(_repository);
        async Task<bool> baseView(object selectedValue = null, string title = null)
        {
            ViewBag.CName = CName;
            ViewBag.CText = CText;
            ViewBag.Title = title == null ? "Danh sách " + CText : title;
            // load SelectList
            IEnumerable<UnitViewModels> unitParent;
            if (!string.IsNullOrEmpty(codeParent))
            {
                unitParent = await _unitHandler.GetBylever(_level);
            }
            else
            {
                unitParent = await _unitHandler.GetAllAsync();
            }
            if (unitParent != null && unitParent.Count() > 0)
                ViewBag.CodeParent = selectedValue == null ? new SelectList(unitParent, "Code", "Name") : new SelectList(unitParent, "Code", "Name", selectedValue);
            return true;

        }
        public async Task<ActionResult> Index(PageHelper helper)
        {
            await baseView();

            var result = await _unitHandler.GetAllAsync();
            if (result == null || result.Count() < 1)
            {
                return View(new List<UnitViewModels>());
            }
            return View(helper);
        }
        public async Task<ActionResult> Table()
        {
            var result = await _unitHandler.GetAllAsync();
            if (result == null || result.Count() < 1)
            {
                return View(new List<UnitViewModels>());
            }
            result = result.OrderBy(o => o.Level);
            result = result.OrderByDescending(o => o.CreateDate);
            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult> Create(string parentCode)
        {
            codeParent = parentCode;
            await baseView(null, "Tạo mới " + CText);

            return View();
        }
        [HttpGet]
        public async Task<ActionResult> SelectDonVi(string parentCode, string curentCode)
        {
            codeParent = parentCode;
            IEnumerable<UnitViewModels> unitParent;
            if (_level != 0)
            {
                unitParent = await _unitHandler.GetBylever(_level);
            }
            else
            {
                unitParent = await _unitHandler.GetAllAsync();
            }
            if (string.IsNullOrEmpty(curentCode))
                unitParent = unitParent.Where(o => o.Code != curentCode);
            ViewBag.CodeParent = new SelectList(unitParent, "Code", "Name");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(UnitViewModels model)
        {
            codeParent = model.CodeParent;
            await baseView(model.CodeParent, "Tạo mới " + CText);

            if (ModelState.IsValid)
            {
                var record = await _unitHandler.CreateAsync(model);
                if (record <= 0)
                {
                    ViewBag.Error = "Không thể tạo được bản mới";
                    return View(model);
                }

                var unit = await _unitHandler.GetAllAsync();

                return RedirectToAction("Index");
            }
            return View(model);
        }


        [HttpGet]
        public async Task<ActionResult> Update(string code)
        {
            var record = await _unitHandler.ReadAsync(code);
            if (record != null)
            {
                codeParent = record.CodeParent;
                await baseView(record.CodeParent, "Cập nhật " + CText);
                return View(record);
            }
            codeParent = null;
            await baseView(null, "Cập nhật " + CText);
            ViewBag.Error = "Không tìm thấy thành viên này trong hệ thống";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<ActionResult> Update(UnitViewModels model)
        {
            if (ModelState.IsValid)
            {

                var record = await _unitHandler.UpdateAsync(model);
                if (record <= 0)
                {
                    await baseView(model.CodeParent, "Cập nhật " + CText);
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
            var record = await _unitHandler.DeleteAsync(id);
            if (record <= 0)
            {
                return Json(new { Message = "Thao tác không thành công", IsSuccess = false });
            }
            return Json(new { Message = string.Format("Đã xóa thành công"), IsSuccess = true });
        }

        //TODO dưới này a chưa làm 

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