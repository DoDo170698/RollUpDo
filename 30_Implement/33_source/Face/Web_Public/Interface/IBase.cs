using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace Web_Public.Interface
{
    interface IBase<T> where T : class, new()
    {
        Task<int> CreateAsync(T model);
        Task<int> UpdateAsync(T model);
        Task<int> DeleteAsync(string key);
        Task<T> ReadAsync(string key);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync(Func<T, bool> predicate);
        Task<IEnumerable<T>> GetAllAsync(PageHelper page);

    }
}