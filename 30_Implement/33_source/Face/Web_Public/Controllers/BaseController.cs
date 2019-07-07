using Common.Helpers;
using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_Public.Controllers
{
    public class BaseController : Controller
    {
        protected static IRepository _repository = DependencyResolver.Current.GetService<IRepository>();
        protected readonly CacheFactory _cacheFactory = new CacheFactory();
        protected static log4net.ILog Log { get; set; }
    }
}