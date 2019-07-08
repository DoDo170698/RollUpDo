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
        public RoleHandler(IRepository repository): base(repository){}

        public async Task<int> Create(RoleViewModels model)
        {
            bool any = await _repository.GetRepository<Role>().AnyAsync(r => r.Name == model.RoleName);
            if (!any)
            {
                var resord = mapper.Map<RoleViewModels, Role>(model);
                var result = await _repository.GetRepository<Role>().CreateAsync(resord, );
            }
        }
    }
}