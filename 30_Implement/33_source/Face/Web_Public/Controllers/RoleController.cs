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
using Web_Public.Interface;

namespace Web_Public.Controllers
{
    public class RoleController : BaseController
    {
        IRole _handler;
        // GET: Role
        public async Task<ActionResult> Index(PageHelper model)
        {
            IEnumerable<Role> roles = await _repository.GetRepository<Role>().GetAllAsync();
            //var roles = await _handler.GetAllAsync(model);
            return View(roles);
        }

        // GET: Role/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RoleViewModels model)
        {

            if (ModelState.IsValid)
            {
                var result = await _handler.CreateAsync(model);
                if (result == -1)
                {
                    Log.Error("Tạo mới không thành công");
                    return View(model);
                }
                Log.Debug("Tạo mới thành công");
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Role/Edit/5
        public async Task<ActionResult> Edit(long? id, PageHelper page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = await _handler.Find(id.Value);
            if (result == null)
            {
                return HttpNotFound();
            }
            var roleViewModel = await _handler.GetAllAsync(page);

            return View(roleViewModel);
        }

        // POST: Role/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(RoleViewModels model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var roleEdit = await _handler.EditAsync()
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Role/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Role/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

