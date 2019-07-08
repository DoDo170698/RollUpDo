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

        public RoleHandler(IRepository repository): base(repository){}

        public async Task<int> CreateAsync(RoleViewModels model)
        {
            bool any = await _repository.GetRepository<Role>().AnyAsync(r => r.Name == model.RoleName);
            if (!any)
            {
                var resord = mapper.Map<RoleViewModels, Role>(model);
                var result = await _repository.GetRepository<Role>().CreateAsync(resord, AccountId);
                return result;
            }
            return 1;
        }

        public async Task<RoleViewModels> Find(long Id)
        {
            throw new NotImplementedException();
        }

        public async Task<RoleViewModels> GetAllAsync(PageHelper mpdel)
        {
            RoleViewModels model = null;
            var records = await _repository.GetRepository<Role>().GetAllAsync();
            if (records.Count() > 0)
            {
                Role record = records.First();
                model = mapper.Map<Role, RoleViewModels>(record);
            }
            return model;

        }

        public async Task<int> EditAsync(RoleViewModels model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(ParamNull);
            }
            var record = await _repository.GetRepository<Role>().ReadAsync(model.Id);
            if(record == null)
            {
                return 1;
            }
            return 0;
        } 
    }
}