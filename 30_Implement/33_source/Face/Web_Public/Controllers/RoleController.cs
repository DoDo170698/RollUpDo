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
        public ActionResult Index(PageHelper model)
        {
            var record = _handler.GetAllAsync(model);
            return View(record);
        }
    }
}

