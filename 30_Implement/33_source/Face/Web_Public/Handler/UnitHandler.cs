using Entities.Models;
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
    public class UnitHandler : BaseHandler, IBase<UnitViewModels>
    {
        public UnitHandler(IRepository repository) : base(repository) { }
        public async Task<int> CreateAsync(UnitViewModels model)
        {
            bool any = await _repository.GetRepository<DonVi>().AnyAsync(k => k.Code == model.Code);
            if (!any)
            {
                DonVi record = mapper.Map<UnitViewModels, DonVi>(model);
                // childen = parent +1;
                var parent = await _repository.GetRepository<DonVi>().ReadAsync(k => k.Code == model.CodeParent);
                if (parent != null)
                {
                    record.Level = parent.Level + 1;
                }
                else
                {
                    record.Level = 0;
                }
                int result = await _repository.GetRepository<DonVi>().CreateAsync(record, AccountId);
                return result;
            }
            return 0;
        }

        public async Task<int> UpdateAsync(UnitViewModels model)
        {
            DonVi updating = await _repository.GetRepository<DonVi>().ReadAsync(k => k.Code == model.Code);
            if (updating == null)
            {
                return 0;
            }
            updating.Description = model.Description;
            updating.DiaChi = model.DiaChi;
            updating.Email = model.Email;
            updating.Name = model.Name;
            // childen = parent +1;
            var parent = await _repository.GetRepository<DonVi>().ReadAsync(k => k.Code == model.CodeParent);
            if (parent != null)
            {
                updating.Level = parent.Level + 1;
            }
            else
            {
                updating.Level = 0;
            }
            updating.IsDeleted = model.IsDeleted;
            updating.UpdateDate = DateTime.Now;
            int result = await _repository.GetRepository<DonVi>().UpdateAsync(updating, AccountId);
            return result;
        }

        public async Task<int> DeleteAsync(string code)
        {
            DonVi record = await _repository.GetRepository<DonVi>().ReadAsync(k => k.Code == code);
            if (record != null)
            {
                int result = await _repository.GetRepository<DonVi>().DeleteAsync(record, AccountId);
                return result;
            }
            return 0;
        }
        public UnitViewModels Read(string code)
        {
            DonVi record = _repository.GetRepository<DonVi>().Read(k => k.Code == code);
            if (record != null)
            {
                return mapper.Map<DonVi, UnitViewModels>(record);
            }
            return null;
        }

        public async Task<UnitViewModels> ReadAsync(string code)
        {
            DonVi record = await _repository.GetRepository<DonVi>().ReadAsync(k => k.Code == code);
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
        /// <summary>
        /// Hàm này lỗi
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UnitViewModels>> GetAllAsync(Func<UnitViewModels, bool> predicate)
        {
            //TODO chưa được
            var lamDa = mapper.Map<Func<UnitViewModels, bool>, Func<DonVi, bool>>(predicate);
            var record = await _repository.GetRepository<DonVi>().GetAllAsync();
            record = record.Where(lamDa);
            if (record.Count() > 0)
            {
                return mapper.Map<IEnumerable<DonVi>, IEnumerable<UnitViewModels>>(record);
            }
            return null;
        }
        public async Task<IEnumerable<UnitViewModels>> GetBylever(int lever)
        {

            var record = await _repository.GetRepository<DonVi>().GetAllAsync(o => o.Level == lever);
            if (record.Count() > 0)
            {
                return mapper.Map<IEnumerable<DonVi>, IEnumerable<UnitViewModels>>(record);
            }
            return null;
        }

        public Task<IEnumerable<UnitViewModels>> GetAllAsync(Expression<Func<UnitViewModels, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}