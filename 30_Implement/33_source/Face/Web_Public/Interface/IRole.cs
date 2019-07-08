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
        Task<int> CreateAsync(RoleViewModels model);
        Task<RoleViewModels> Find(long Id);
        Task<RoleViewModels> GetAllAsync(PageHelper mpdel);

        Task<int> Delete(RoleViewModels model);
        Task<int> Edit(RoleViewModels model, bool IsSave = false);
    }
}