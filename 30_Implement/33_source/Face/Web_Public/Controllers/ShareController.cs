using AutoMapper;
using Entities.Models;
using Entities.Models.SystemManage;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Web_Public.Handler;

namespace Web_Public.Controllers
{
    public class ShareController : BaseController
    {
        protected IMapper mapper;
        public ShareController()
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(typeof(ShareController).GetTypeInfo().Assembly);
            });
            Log = log4net.LogManager.GetLogger(typeof(ShareController));
            mapper = config.CreateMapper();
        }
        // GET: Share
        public async Task<ActionResult> Index(string code)
        {
            if(code == "nckhsv")
            {
                var x = await InitDataBase(false);
                Debug.WriteLine(string.Format("Khởi tạo {0} bản ghi", x));
                return Json("Khởi tạo database với "+ x +" bản ghi", JsonRequestBehavior.AllowGet);
            }
            if (code == "reInit")
            {
                var x = await InitDataBase(true);
                Debug.WriteLine(string.Format("Khởi tạo {0} bản ghi", x));
                return Json("Khởi tạo lại database với " + x + " bản ghi", JsonRequestBehavior.AllowGet);
            }
            return Json("Khởi tạo data base lỗi rồi ", JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> EmptyLayout()
        {
            var records = await _repository.GetRepository<SystemInformation>().GetAllAsync(o => o.Activate == true);
            if (records.Count() < 1)
            {
                var x = await InitDataBase(false);
                Debug.WriteLine(string.Format("Khởi tạo {0} bản ghi", x));
            }
            var record = records.First();
            var model = mapper.Map<SystemInformation, AppConfig>(record);
            string Company = "EPUIT", Authen = "Nguyễn Anh Dũng", Description = "", Title = "Hệ thống điểm danh bằng khuôn mặt";

            readingXML(ref Company, ref Authen, ref Description, ref Title);
            ViewBag.HeadView = new HeadViewModel() { Company = Company, Authen = Authen, Description = Description, Title = Title };

            return View(model);
        }
        public static async Task<int> InitDataBase(bool delete = false)
        {
            Log.Debug("----------------------------------Init database Begin----------------------------------");
            int result = 0;
            try
            {
                result += await initSyS(delete);
                result += await initUser(delete);
                result += await initPersion(delete);
            }
            catch (Exception ex)
            {
                Log.Debug(string.Format("----------------------------------Cờ ri ết tựt {0} records.----------------------------------", result));
                Log.Error(ex.Message);
            }
            Log.Debug(string.Format("----------------------------------Init database End using {0} records.----------------------------------",result));
            return result;
        }
        public static async Task<int> initSyS(bool delete = false)
        {
            int result = 0;
            if (delete)
            {
                var sys = await _repository.GetRepository<SystemInformation>().GetAllAsync();
                if (sys.Count() > 0)
                {
                    result = await _repository.GetRepository<SystemInformation>().DeleteAsync(sys, 0);
                }
            }
            var item = new SystemInformation()
            {
                Logo = "",
                PhoneNumber = "0978132474",
                SiteName = "cungcapphanmem",
                Address = "",
                Copyright = "Copyright " + DateTime.Now.Year,
                Email = "itfa.ahihi@gmail.com",
                FacebookPage = "",
                Slogan = "Đơn giản la sự tinh tế cuối cùng",
                WebsiteAddress = "cungcapphanmem.com",
                Activate = true,
            };
            result += await _repository.GetRepository<SystemInformation>().CreateAsync(item, 0);
            return result;
        }
        public static async Task<int> initUser(bool delete = false)
        {
            int result = 0;
            if (delete)
            {
                var sys = await _repository.GetRepository<User>().GetAllAsync();
                if (sys.Count() > 0)
                {
                    result = await _repository.GetRepository<User>().DeleteAsync(sys, 0);
                }
            }
            for (int i = 1; i < 10; i++)
            {
                var item = new User() { UserName = "user" + i, Password = Common.Helpers.StringHelper.stringToSHA512("123456"), IsDeleted = false };
                result += await _repository.GetRepository<User>().CreateAsync(item, 0);
            }

            return result;
        }
        public static async Task<int> initDonVi(bool delete = true)
        {
            int result = 0;
            if (delete)
            {
                var sys = await _repository.GetRepository<DonVi>().GetAllAsync();
                if (sys.Count() > 0)
                {
                    result = await _repository.GetRepository<DonVi>().DeleteAsync(sys, 0);
                }
            }
            List<DonVi> list = new List<DonVi>();
            list.Add(new DonVi() { Code = "EPU", Name = "Đại học điện lực", DiaChi = "235 Hoàn gquốc việt", Level = 1 });
            list.Add(new DonVi() { Code = "CNTT", Name = "Khoa công nghệ thông tin", CodeParent = "EPU", Level = 2 });
            list.Add(new DonVi() { Code = "DTVT", Name = "Khoa điện tử viễn thông", CodeParent = "EPU", Level = 2 });
            list.Add(new DonVi() { Code = "D10CNPM", Name = "Lớp D10 công nghệ phần mềm", CodeParent = "CNTT", Level = 3 });
            list.Add(new DonVi() { Code = "D10QTANM", Name = "Lớp D10 quản trị an ninh mạng", CodeParent = "CNTT", Level = 3 });
            list.Add(new DonVi() { Code = "D10TMDT", Name = "Lớp D10 thương mại điện tử", CodeParent = "CNTT", Level = 3 });

            foreach (var item in list)
            {
                result += await _repository.GetRepository<DonVi>().CreateAsync(item, 0);
            }
            return result;
        }
        public static async Task<int> initPersion(bool delete = false, bool deleteDOnVi = true)
        {
            int result = 0;
            if (delete)
            {
                var sys = await _repository.GetRepository<Person>().GetAllAsync();
                if (sys.Count() > 0)
                {
                    result = await _repository.GetRepository<Person>().DeleteAsync(sys, 0);
                }
            }
            for (int i = 1; i < 10; i++)
            {
                var item = new Person()
                {
                    FullName = "Sinh viên " + i,
                    ProfilePicture = "",
                    Sex = new Random().Next(0, 1) == 0 ? false : true,
                    Address = "epuit",
                    code = "15813100" + i.ToString(),
                    CodeDonVi = ""

                };
                result += await _repository.GetRepository<Person>().CreateAsync(item, 0);
            }

            return result;
        }

        private void readingXML(ref string company, ref string authen, ref string description, ref string title)
        {
            company = WebConfigurationManager.AppSettings["Company"] ?? company;
            company = SEO(ref company);
            authen = WebConfigurationManager.AppSettings["Authen"] ?? authen;
            authen = SEO(ref authen);
            description = WebConfigurationManager.AppSettings["Description"] ?? description;
            description = SEO(ref description);
            title = WebConfigurationManager.AppSettings["Title"] ?? title;
            title = SEO(ref title);
        }
        private string SEO(ref string str)
        {
            str += ";" + str.ToUpper();
            str += ";" + str.ToLower();
            return str;
        }
    }
}