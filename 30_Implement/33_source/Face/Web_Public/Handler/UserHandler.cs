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
    public class UserHandler : BaseHandler, IUser
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
            UserModels model = null;
            var records = await _repository.GetRepository<User>().GetAllAsync(x => x.UserName == userName);

            if (records.Count() > 0)
            {
                User record = records.First();
                model = mapper.Map<User, UserModels>(record);
            }
            return model;
        }
        public async Task<int> Create(UserModels model)
        {
            bool any = await _repository.GetRepository<User>().AnyAsync(o => o.UserName == model.UserName);

            if (!any)
            {
                var record = mapper.Map<UserModels, User>(model);
                var result = await _repository.GetRepository<User>().CreateAsync(record, AccountId);
                return result;
            }
            return -1;
        }

        //Hàm này em dùng cho quản lý user riêng. Sau này tất cả sẽ dùng hàm tạo mới của CreateAsync.
        public async Task<int> CreateAsync(UserModels model)
        {
            bool any = await _repository.GetRepository<User>().AnyAsync(u => u.UserName == model.UserName);
            if (!any)
            {
                var record = mapper.Map<UserModels, User>(model);
                var result = await _repository.GetRepository<User>().CreateAsync(record, AccountId);
                return result;
            }
            return 0;
        }

        public async Task<int> DeleteAsync(long id)
        {
            var record = await _repository.GetRepository<User>().ReadAsync(id);

            if(record != null)
            {
                int result = await _repository.GetRepository<User>().DeleteAsync(id, AccountId);
                return result;
            }
            return 0;
        }

        public async Task<int> UpdateAsync(UserModels model)
        {
            var updateing = await _repository.GetRepository<User>().ReadAsync(model.Id);
            updateing.UserName = model.UserName;
            updateing.Password = model.Password;

            var result = await _repository.GetRepository<User>().UpdateAsync(updateing, AccountId);
            return result;
        }

        public async Task<IEnumerable<UserModels>> GetAllAsync()
        {
            var records = await _repository.GetRepository<User>().GetAllAsync();
            if (records.Count() > 0)
            {
                return mapper.Map<IEnumerable<User>, IEnumerable<UserModels>>(records);
            }
            return null;
        }

        public async Task<UserModels> ReadAsync(long id)
        {
            var record = await _repository.GetRepository<User>().ReadAsync(id);
            if (record != null)
            {
                return mapper.Map<User, UserModels>(record);
            }
            return null;
        }

        public async Task<IEnumerable<UserModels>> GetAllAsync(PageHelper mpdel, bool showDelete = false)
        {
            var records = await _repository.GetRepository<User>().GetAllAsync(o => o.IsDeleted == showDelete);

            if (records.Count() > 0)
            {

                return mapper.Map<IEnumerable<User>, IEnumerable<UserModels>>(records);
            }
            return null;
        }

        Task<UserModels> IUser.GetAllAsync(PageHelper mpdel, bool showDelete)
        {
            throw new NotImplementedException();
        }
    }
}