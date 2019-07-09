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

        #region CRUD
        Task<int> CreateAsync(RoleViewModels model);
        Task<int> UpdateAsync(RoleViewModels model);
        #endregion

    }
}