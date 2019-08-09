using Entities.Models;
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
    public class UnitHandler : BaseHandler, IUnit
    {
        public UnitHandler(IRepository repository) : base(repository) { }
        public async Task<int> CreateAsync(UnitViewModels model)
        {
            bool any = await _repository.GetRepository<DonVi>().AnyAsync(p => p.Code == model.Code);
            if (!any)
            {
                var record = mapper.Map<UnitViewModels, DonVi>(model);
                var result = await _repository.GetRepository<DonVi>().CreateAsync(record, AccountId);
                return result;
            }
            return 0;
        }

        public async Task<int> UpdateAsync(UnitViewModels model)
        {
            var updating = await _repository.GetRepository<DonVi>().ReadAsync(model.Id);

            var result = await _repository.GetRepository<DonVi>().UpdateAsync(updating, AccountId);
            return result;
        }

        public async Task<int> DeleteAsync(long id)
        {
            var record = await _repository.GetRepository<DonVi>().ReadAsync(id);
            if (record != null)
            {
                int result = await _repository.GetRepository<DonVi>().DeleteAsync(id, AccountId);
                return result;
            }
            return 0;
        }

        public async Task<UnitViewModels> ReadAsync(long id)
        {
            var record = await _repository.GetRepository<DonVi>().ReadAsync(id);
            if (record != null)
            {
                return mapper.Map<DonVi, UnitViewModels>(record);
            }
            return null;
        }

        public async Task<IEnumerable<UnitViewModels>> GetAllAsync()
        {
            var record = await _repository.GetRepository<DonVi>().GetAllAsync();
            if (record.Count() > 0)
            {
                return mapper.Map<IEnumerable<DonVi>, IEnumerable<UnitViewModels>>(record);
            }
            return null;
        }
        public async Task<IEnumerable<UnitViewModels>> GetAllAsync(PageHelper page)
        {
            var record = await _repository.GetRepository<DonVi>().GetAllAsync();
            if (record.Count() > 0)
            {
                return mapper.Map<IEnumerable<DonVi>, IEnumerable<UnitViewModels>>(record);
            }
            return null;
        }
    }
}