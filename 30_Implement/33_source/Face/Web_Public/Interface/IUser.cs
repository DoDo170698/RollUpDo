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

        #region CRUD
        Task<int> Create(UserModels model);
        Task<int> Update(UserModels model);
        #endregion
    }

}
