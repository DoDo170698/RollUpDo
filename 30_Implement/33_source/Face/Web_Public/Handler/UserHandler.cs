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
    }
}