using CoreEntities.Models;
using System.Web.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using CoreEntities.Enums;
using Web.Areas.Management.Filters;
using Common.Helpers;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Web;
using System.Runtime.InteropServices;
using CoreEntities.Models.SystemManage;
using Common;
using log4net;
using Web.Areas.FrontEnd.Interfaces;
using System.Web.Http;
using Newtonsoft.Json;

namespace Web.Areas.FrontEnd.Controllers
{

    /// <summary>
    /// điểm danh
    /// </summary>
	[RouteArea("FrontEnd", AreaPrefix = "dich-vu")]
	public class DiemDanhController : BaseAPIController
    {
       
        #region base
        ILog log = log4net.LogManager.GetLogger(typeof(DiemDanhController));
        #endregion
        
        #region index
        [System.Web.Mvc.Route("danh-sach-hoc-sinh", Name = "DiemDanh_Index")]
		public async Task<ActionResult> Index()
		{
			try
			{
			
                var list = await _repository.GetRepository<DiemDanh>().GetAllAsync();
				return Json(new ExecuteResult() { IsSuccess = true, Message="",Result = list}, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				throw;
			}
		}
        #endregion
        #region API
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("api-diem-danh")]
       // public async Task<ActionResult> DiemDanh(PostResult model, HttpPostedFileBase file)
        public async Task<ActionResult> DiemDanh([FromBody]PostFileResult model)
        {
            try
            {
                var httpRequest = HttpContext.Request;
                var postResult =  JsonConvert.DeserializeObject<PostResult>(httpRequest.Form.GetValues("PostResult").First());

                _handler = new Handlers.DiemDanh(_repository);
                var IHandler = (IDiemDanh)_handler;
                log.Debug("Begin điển danh");
                // save file
                //...
                // giả sử deeplearning đã tìm ra học sinh là học sinh có Id=1
                var result = await _handler.DiemDanhAsync(1);
                log.Debug("End điển danh");
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Error("Error ", ex);
                return Json(new ExecuteResult(), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}