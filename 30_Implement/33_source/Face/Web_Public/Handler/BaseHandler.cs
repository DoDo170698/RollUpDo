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
                // get trong Session ra nha ở đây a ms đê rlà 0
                return 0;
            }
            //set { Session[SessionEnum.UserId] = value; }
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