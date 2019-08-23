using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Web_Public.Interface
{
    public interface IUnit
    {
        Task<int> CreateAsync(UnitViewModels model);
        Task<int> UpdateAsync(UnitViewModels model);
        Task<IEnumerable<UnitViewModels>> GetAllAsync();
        Task<IEnumerable<UnitViewModels>> GetAllAsync(PageHelper page);
        Task<int> DeleteAsync(string code);
    }
}