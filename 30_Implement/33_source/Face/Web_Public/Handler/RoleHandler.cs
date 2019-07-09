﻿using Entities.Models.SystemManage;
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

        public async Task<IEnumerable<RoleViewModels>> GetAllAsync(PageHelper mpdel)
        {
            var records = await _repository.GetRepository<Role>().GetAllAsync();
            if (records.Count() > 0)
            {
                return mapper.Map<IEnumerable<Role>, IEnumerable<RoleViewModels>>(records);
            }
            return null;

        }

        public Task<int> UpdateAsync(RoleViewModels model)
        {
            throw new NotImplementedException();
        }

        Task<int> IRole.CreateAsync(RoleViewModels model)
        {
            throw new NotImplementedException();
        }

        Task<RoleViewModels> IRole.GetAllAsync(PageHelper mpdel)
        {
            throw new NotImplementedException();
        }

        Task<int> IRole.UpdateAsync(RoleViewModels model)
        {
            throw new NotImplementedException();
        }
    }
}