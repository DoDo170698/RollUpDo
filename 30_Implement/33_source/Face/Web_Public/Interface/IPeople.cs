using Entities.Models.SystemManage;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace Web_Public.Interface
{
    public interface IPeople
    {
        Task<int> CreateAsync(PeopleViewModels model);
        Task<int> UpdateAsync(PeopleViewModels model);
        Task<IEnumerable<PeopleViewModels>> GetAllAsync();
        Task<IEnumerable<PeopleViewModels>> GetAllAsync(PageHelper page);
        Task<int> DeleteAsync(long id);
    }
}