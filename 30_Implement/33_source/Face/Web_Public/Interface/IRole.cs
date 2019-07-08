using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Web_Public.Interface
{
    interface IRole
    {
        Task<int> Create(RoleViewModels model);
        //Task<UserModels> GetUserModels(string Name);
        Task<RoleViewModels> GetAllAsync(PageHelper mpdel, bool showDelete = false);
    }
}