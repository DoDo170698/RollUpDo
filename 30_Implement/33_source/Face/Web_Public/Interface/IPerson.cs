using Entities.Models.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace Web_Public.Interface
{
    public interface IPerson
    {
        //Task<IEnumerable<Person>> GetAll();
        //Task<Person> Find(long id);
        //IQueryable<Person> FindBy(Exception<>)
        //Task<IEnumerable<Person>> GetAllAsync();
        //IQueryable<Person> GetAllIncluding(params Exception<Func<Person, object>>[] includingProperties);

        Task<IEnumerable<Person>> GetAll();
        Task<Person> Find(Guid id);
        IQueryable<Person> FindBy(Expression<Func<Person, bool>> predicate);
        Task<ICollection<Person>> FindByAsyn(Expression<Func<Person, bool>> predicate);
        Task<ICollection<Person>> GetAllAsyn();
        IQueryable<Person> GetAllIncluding(params Expression<Func<Person, object>>[] includeProperties);
        Task<bool> Exist(Expression<Func<Person, bool>> spec = null);
        Task<int> Count(Expression<Func<Person, bool>> spec = null);
        Task CreateAsync(Person entity, bool isSaved = false);
        Task DeleteAsync(Person entity, bool isSaved = false);
        Task UpdateAsync(Person entity, object key, bool isSaved = false);
    }