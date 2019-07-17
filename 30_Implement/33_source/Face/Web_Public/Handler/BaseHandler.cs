using AutoMapper;
using Interface;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Web_Public.Mapping;
using Web_Public;

namespace Web_Public.Handler
{
    public class BaseHandler
    {
        protected IRepository _repository;
        protected IMapper mapper;
        public long AccountId
        {
            get
            {
                object objAccountId = System.Web.HttpContext.Current.Session[SessionEnum.UserID];
                long _AccountId = -1;
                if (objAccountId == null)
                    _AccountId = -1;
                else
                    _AccountId = Convert.ToInt64(objAccountId.ToString());
                return _AccountId;
            }
            set { System.Web.HttpContext.Current.Session[SessionEnum.UserID] = value; }
        }

        protected static log4net.ILog Log { get; set; }
        public BaseHandler(IRepository repository)
        {
            _repository = repository;
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(typeof(MapHelper).GetTypeInfo().Assembly);
            });
          //  Log = LogManager.GetLogger("DebugLogger");
            mapper = config.CreateMapper();
           
        }
    }
}