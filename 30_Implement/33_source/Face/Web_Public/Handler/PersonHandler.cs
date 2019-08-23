using DataAccess.Repositories;
using Entities.Models.SystemManage;
using Entities.ViewModels;
using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using Web_Public.Interface;

namespace Web_Public.Handler
{
    public class PersonHandler : BaseHandler, IBase<PersonViewModels>
    {
        public PersonHandler(IRepository repository) : base(repository) { }

        public async Task<int> CreateAsync(PersonViewModels model)
        {
            bool any = await _repository.GetRepository<Person>().AnyAsync(
                                                                    k => k.Email == model.Email
                                                                    || k.PhoneNumber == model.PhoneNumber
                                                                    || k.PasspostId == model.PasspostId
                                                                    || k.CardId == model.CardId);
            if (!any)
            {
                Person record = mapper.Map<PersonViewModels, Person>(model);
                int result = await _repository.GetRepository<Person>().CreateAsync(record, AccountId);
                return result;
            }
            return 0;
        }
        public async Task<int> UpdateAsync(PersonViewModels model)
        {
            Person updating = await _repository.GetRepository<Person>().ReadAsync(k => k.Id == model.Id);
            if (updating == null)
            {
                return 0;
            }
            updating.IsDeleted = model.IsDeleted;
            updating.Orther = model.Orther;
            updating.PasspostId = model.PasspostId;
            updating.PhoneNumber = model.PhoneNumber;
            updating.ProfilePicture = model.ProfilePicture;
            updating.Sex = model.Sex;
            updating.UnitCode = model.UnitCode;
            updating.Address = model.Address;
            updating.CardId = model.CardId;
            updating.Code = model.Code;
            updating.DateOfBirth = model.DateOfBirth;
            updating.Email = model.Email;
            updating.FullName = model.FullName;
            int result = await _repository.GetRepository<Person>().UpdateAsync(updating, AccountId);
            return result;
        }
        public async Task<int> DeleteAsync(string key)
        {
            Person record = await _repository.GetRepository<Person>().ReadAsync(k => k.Id == long.Parse(key));
            if (record != null)
            {
                return await _repository.GetRepository<Person>().DeleteAsync(record, AccountId);
            }
            return 0;
        }
        public async Task<PersonViewModels> ReadAsync(string key)
        {
            long id = long.Parse(key);
            Person record = await _repository.GetRepository<Person>().ReadAsync(k => k.Id == id);
            if (record != null)
            {
                return mapper.Map<Person, PersonViewModels>(record);
            }
            return null;
        }
        public async Task<IEnumerable<PersonViewModels>> GetAllAsync()
        {
            var record = await _repository.GetRepository<Person>().GetAllAsync();
            if (record.Count() > 0)
            {
                return mapper.Map<IEnumerable<Person>, IEnumerable<PersonViewModels>>(record);
            }
            return null;
        }
        public async Task<IEnumerable<Person>> GetAllPersonAsync()
        {
            return await _repository.GetRepository<Person>().GetAllAsync();
        }


        public async Task<IEnumerable<PersonViewModels>> GetAllAsync(PageHelper page)
        {
            var record = await _repository.GetRepository<Person>().GetAllAsync();
            if (record.Count() > 0)
            {
                return mapper.Map<IEnumerable<Person>, IEnumerable<PersonViewModels>>(record);
            }
            return null;
        }

        public async Task<IEnumerable<PersonViewModels>> GetAllAsync(Expression<Func<PersonViewModels, bool>> predicate)
        {
            //TODO
            var lamDa = mapper.Map<Expression<Func<PersonViewModels, bool>>, Expression<Func<Person, bool>>>(predicate);
            //var record = await _repository.GetRepository<Person>().GetAllAsync(lamDa);
            var record = await _repository.GetRepository<Person>().GetAllAsync();
           
            if (record.Count() > 0)
            {
                return mapper.Map<IEnumerable<Person>, IEnumerable<PersonViewModels> >(record);
            }
            return null;
        }

        public Task<IEnumerable<PersonViewModels>> GetAllAsync(Func<PersonViewModels, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}