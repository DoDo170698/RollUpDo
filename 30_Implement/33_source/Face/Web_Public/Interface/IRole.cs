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
        Task<int> UpdateAsync(RoleViewModels model);
        Task<IEnumerable<RoleViewModels>> GetAllAsync();
        Task<IEnumerable<RoleViewModels>> GetAllAsync(PageHelper page);
        //#region CRUD
        Task<int> DeleteAsync(int id);
        //#endregion

    }
}