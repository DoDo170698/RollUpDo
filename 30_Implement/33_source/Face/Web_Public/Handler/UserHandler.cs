using Common;
using Entities.Models.SystemManage;
using Entities.ViewModels;
using Interface;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web_Public.Interface;

namespace Web_Public.Handler
{
    public class UserHandler : BaseHandler,IUser
    {
      
        public UserHandler(IRepository repository) : base(repository)
        {
            Log = log4net.LogManager.GetLogger(typeof(UserHandler));
        }

        public Task<ExecuteResult> CheckLogIn(string UserName, string Password)
        {
            throw new NotImplementedException();
        }

        public async Task<UserModels> GetByUserName(string userName)
        {
            Log.Debug("-------------------------Begin GetUserByUsername --------------------------------");
            UserModels model = null;
            var records = await _repository.GetRepository<User>().GetAllAsync(x => x.UserName == userName);

            if (records.Count() >0 )
            {
                User record = records.First();
                model = mapper.Map<User, UserModels>(record);
            }
            Log.Debug(string.Format("----------------End Count records= {0} ------------------", records.Count()));
            return model;
        }
        public async Task<int> Create(UserModels model)
        {
            Log.Debug("-------------------------Begin Handl Createing User --------------------------------");
            
            bool any = await _repository.GetRepository<User>().AnyAsync(o=>o.UserName == model.UserName);

            if (!any)
            {
                var record = mapper.Map<UserModels, User>(model);
                var result = await _repository.GetRepository<User>().CreateAsync(record, AccountId);
                Log.Debug(string.Format("----------------End Handl create records= {0} ------------------", result));
                return result;
            }
            Log.Error(string.Format("----------------End Handl create error : Đã tồn tại UserName này trong hệ thống ------------------"));
            return -1;
        }
        public async Task<UserModels> GetAllAsync(PageHelper mpdel, bool showDelete= false)
        {
            Log.Debug("-------------------------Begin GetUserByUsername --------------------------------");
            UserModels model = null;
            var records = await _repository.GetRepository<User>().GetAllAsync(o=>o.IsDeleted == showDelete);

            if (records.Count() > 0)
            {
                User record = records.First();
                model = mapper.Map<User, UserModels>(record);
            }
            Log.Debug(string.Format("----------------End Count records= {0} ------------------", records.Count()));
            return model;
        }
    }
}