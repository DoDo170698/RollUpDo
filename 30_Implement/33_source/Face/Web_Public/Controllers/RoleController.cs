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
        RoleHandler _roleHandler = new RoleHandler(_repository);
        // GET: Role
        public async Task<ActionResult> Index(PageHelper model)
        {
            var result = await _roleHandler.GetAllAsync(model);
            return View(result);
        }

        public async Task<ActionResult> Create(RoleViewModels model)
        {
            if (ModelState.IsValid)
            {
                var record = await _roleHandler.CreateAsync(model);
                if (record == 0)
                {
                    return View(model);
                }
                else if (record == -1)
                {
                    return View(model);
                }
                return RedirectToAction("Index", "Role");
            }
            return View(model);
        }

        //public async Task<ActionResult> Update()
        //{
        //    return View();
        //}
    }
}

