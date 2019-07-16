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
        Task<RoleViewModels> GetAllAsync(PageHelper mpdel);

        Task<int> CreateAsync(RoleViewModels model);
        Task<int> UpdateAsync(RoleViewModels model);
        Task GetAllAsync();
        //#region CRUD
        Task<int> DeleteAsync(RoleViewModels model);
        //#endregion

    }
}