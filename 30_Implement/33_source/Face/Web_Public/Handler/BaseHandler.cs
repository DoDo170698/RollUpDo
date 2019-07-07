using AutoMapper;
using Interface;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Web_Public.Mapping;

namespace Web_Public.Handler
{
    public class BaseHandler
    {
        protected IRepository _repository;
        protected IMapper mapper;
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