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
    public class RoleHandler : BaseHandler, IRole
    {
        private const string ParamNull = "Không được để trống";


        public RoleHandler(IRepository repository) : base(repository) { }

        public async Task<int> CreateAsync(RoleViewModels model)
        {
            bool any = await _repository.GetRepository<Role>().AnyAsync(r => r.Code == model.Code);
            if (!any)
            {
                var resord = mapper.Map<RoleViewModels, Role>(model);
                var result = await _repository.GetRepository<Role>().CreateAsync(resord, AccountId);
                return result;
            }
            return 0;
        }

        public async Task<int> DeleteAsync(int Id)
        {
            var record = await _repository.GetRepository<Role>().ReadAsync(Id);
            if (record != null)
            {
                int result= await _repository.GetRepository<Role>().DeleteAsync(record, AccountId);
                return result;
            }
            return 0;
        }


        public async Task<int> UpdateAsync(RoleViewModels model)
        {
            var updateing = await _repository.GetRepository<Role>().ReadAsync(model.Id);
            updateing.Code = model.Code;
            updateing.Name = model.Name;
            updateing.Description = model.Description;

            var result = await _repository.GetRepository<Role>().UpdateAsync(updateing, AccountId);
            return result;
        }

        public async Task<RoleViewModels> ReadAsync(int id)
        {
            var record = await _repository.GetRepository<Role>().ReadAsync(id);
            if (record != null)
            {
                return mapper.Map<Role, RoleViewModels>(record);
            }
            return null;
        }

        public async Task<IEnumerable<RoleViewModels>> GetAllAsync()
        {
            var records = await _repository.GetRepository<Role>().GetAllAsync();
            if (records.Count() > 0)
            {
                return mapper.Map<IEnumerable<Role>, IEnumerable<RoleViewModels>>(records);
            }
            return null;
        }

        public async Task<IEnumerable<RoleViewModels>> GetAllAsync(PageHelper page)
        {
            var records = await _repository.GetRepository<Role>().GetAllAsync();
            if (records.Count() > 0)
            {
                return mapper.Map<IEnumerable<Role>, IEnumerable<RoleViewModels>>(records);
            }
            return null;
        }
    }
}