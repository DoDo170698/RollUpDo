using DataAccess.Repositories;
using Entities.Models.SystemManage;
using Entities.ViewModels;
using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web_Public.Interface;

namespace Web_Public.Handler
{
    public class PeopleHandler : BaseHandler, IPeople
    {
        public PeopleHandler(IRepository repository) : base(repository) { }

        public async Task<int> CreateAsync(PeopleViewModels model)
        {
            bool any = await _repository.GetRepository<Person>().AnyAsync(p => p.code == model.code);
            if (!any)
            {
                //var record = mapper.Map<PeopleViewModels, Person>(model);
                var record = mapper.Map<Person>(model);
                var result = await _repository.GetRepository<Person>().CreateAsync(record, AccountId);
                return result;
            }
            return 0;
        }

        public async Task<int> UpdateAsync(PeopleViewModels model)
        {
            var updating = await _repository.GetRepository<Person>().ReadAsync(model.Id);
            updating.FullName = model.FullName;
            updating.Email = model.Email;
            updating.PhoneNumber = model.PhoneNumber;
            updating.Address = model.Address;
            updating.DateOfBirth = model.DateOfBirth;

            var result = await _repository.GetRepository<Person>().UpdateAsync(updating, AccountId);
            return result;
        }

        public async Task<int> DeleteAsync(long id)
        {
            var record = await _repository.GetRepository<Person>().ReadAsync(id);
            if (record != null)
            {
                int result = await _repository.GetRepository<Person>().DeleteAsync(id, AccountId);
                return result;
            }
            return 0;
        }

        public async Task<PeopleViewModels> ReadAsync(long id)
        {
            var record = await _repository.GetRepository<Person>().ReadAsync(id);
            if(record != null)
            {
                return mapper.Map<Person, PeopleViewModels>(record);
            }
            return null;
        }

        public async Task<IEnumerable<PeopleViewModels>> GetAllAsync()
        {
            var record = await _repository.GetRepository<Person>().GetAllAsync();
            if (record.Count() > 0)
            {
                return mapper.Map<IEnumerable<Person>, IEnumerable<PeopleViewModels>>(record);
            }
            return null;
        }
        public async Task<IEnumerable<PeopleViewModels>> GetAllAsync(PageHelper page)
        {
            var record = await _repository.GetRepository<Person>().GetAllAsync();
            if (record.Count() > 0)
            {
                return mapper.Map<IEnumerable<Person>, IEnumerable<PeopleViewModels>>(record);
            }
            return null;
        }
    }
}