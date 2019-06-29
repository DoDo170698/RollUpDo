using Common.Helpers;
using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Web.Areas.FrontEnd.Interfaces;

namespace Web.Areas.FrontEnd.Controllers
{
    public class BaseAPIController : Controller
    {
        protected  Web.Areas.FrontEnd.Handlers.DiemDanh _handler;
        protected readonly IRepository _repository = DependencyResolver.Current.GetService<IRepository>();

        protected readonly CacheFactory _cacheFactory = new CacheFactory();

        protected readonly long AccountId = 0;
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
        
    }
}
