using Common;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Public.Interface
{
    interface IUser 
    {
        Task<ExecuteResult> CheckLogIn(string UserName, string Password);

        Task<UserModels> GetByUserName(string userName);
        Task<UserModels> GetAllAsync(PageHelper mpdel, bool showDelete = false);
        Task<IEnumerable<UserModels>> GetAllAsync();

        #region CRUD
        Task<int> Create(UserModels model);
        Task<int> UpdateAsync(UserModels model);
        Task<int> DeleteAsync(long id);
        Task<int> CreateAsync(UserModels model);
        #endregion
    }

}
