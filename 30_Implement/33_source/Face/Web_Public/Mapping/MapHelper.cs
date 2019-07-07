using AutoMapper;
using Entities.Models.SystemManage;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Web_Public.Mapping
{
    public class MapHelper : Profile
    {
        public MapHelper()
        {
            // 2 way mapping resource <==> ViewModel
            CreateMap<User, UserModels>();
            CreateMap<UserModels, User>();
        }
    }
}