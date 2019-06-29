using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Common;
using Entities.Models.SystemManage;
using Interface;
using Web.Areas.FrontEnd.Interfaces;

namespace Web.Areas.FrontEnd.Handlers
{
    public class DiemDanh : IDiemDanh
    {
        protected IRepository _repository;
        // accountid = 0 mặc định là điểm danh tự động
        private long _accounId = 0;

        public DiemDanh(IRepository repository)
        {
            _repository = repository;
        }
        IGenericRepository<Entities.Models.SystemManage.DiemDanh> getRepon()
        {
            return _repository.GetRepository<Entities.Models.SystemManage.DiemDanh>();
        }
        public async Task<ExecuteResult> DiemDanhAsync(long id)
        {
            //// search HocSinh
            //var obj = await _repository.GetRepository<HocSinh>().ReadAsync(id);
            //// add new row to table DiemDanh
            //var result = await getRepon().CreateAsync(new Entities.Models.SystemManage.DiemDanh()
            //{
            //    HocSinhId = id,
            //    TinhTrang = 1
            //}, _accounId);
            //return new ExecuteResult() { IsSuccess = result > 1, Result = obj.FullName };
            ExecuteResult result = new ExecuteResult();
            await Task.Run(() =>
            {
                result.IsSuccess = true;
                result.Message = "Hello Tom" ;
            });
            return result;
        }

     
    }
}