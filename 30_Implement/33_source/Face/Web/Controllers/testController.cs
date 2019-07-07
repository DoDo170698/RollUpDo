using CoreEntities.ViewModels;
using SegmentText;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Helpers;

namespace Web.Controllers
{
    public class testController : Controller
    {
        // GET: test
        public ActionResult Index()
        {
            if (Request.HttpMethod == "POST")
            {
                #region thủ tục
                //string weights = @"G:\Project\Itgroup\Itgroup2018\ComputerVision\DetectFace\Net_Main\Models\east_text_detection.pb";// Server.MapPath("~/Models/frozen_east_text_detection.pb");
                //API.Load_PB_Model_test(weights);

                //const string weights = @"G:\Project\Itgroup\Itgroup2018\ComputerVision\DetectFace\Net_Main\Models\frozen_east_text_detection.pb";
                //var a = System.IO.File.Exists(weights);
                //API.Load_PB_Model_test(weights);
                #endregion

                ViewBag.isSegment = false;
                // Try to process the image.
                if (Request.Files.Count > 0)
                {
                    // There will be just one file.
                    var file = Request.Files[0];

                    var fileName = Guid.NewGuid().ToString() + ".jpg";
                    file.SaveAs(Server.MapPath("~/Images/Upload/" + fileName));

                    string[] imageFiles = new string[] { Server.MapPath("~/Images/Upload/" + fileName), };
                    // hình ánh au khi sessmen ra sẽ ra thư mục này
                    string outFolder = Server.MapPath("~/Out");

                    int result = LineSec2(@"G:\Project\Itgroup\Itgroup2018\ComputerVision\DetectFace\Net_Main\Models\frozen_east_text_detection.pb", outFolder, imageFiles);

                    // ViewBag.Result = " this is ok";
                    List<ImageInfo> imgs = new List<ImageInfo>();
                    foreach (var files in Directory.GetFiles(outFolder))
                    {
                        FileInfo info = new FileInfo(files);
                        //  var fileName = Path.GetFileName(info.FullName);
                        var temp = new Bitmap(info.FullName);
                        imgs.Add(new ImageInfo() { CreateTime = info.CreationTime, Uri = "/Out/" + info.Name, Name = info.Name, Width = temp.Width });
                    }
                    ViewBag.Images = imgs;
                    ViewBag.isSegment = true;

                    return View();


                }
            }
            return View();
        }
        int LineSec2(string filePb, string outFolder, string[] imageFiles = null)
        {
            // chưa chuẩn kiểm tra nhỡ trong for nó lỗi thì chưa biết
            // chưa cấu hình file name cho file đầu ra
            List<bool> result = new List<bool>();
            try
            {
                string[] cmndFiles = imageFiles;

                string debugDir = AppDomain.CurrentDomain.BaseDirectory;

                foreach (string cmndFile in cmndFiles)
                {
                    Stopwatch sw = Stopwatch.StartNew();

                    result.Add(EastHelper.Segment(filePb, imageFiles, outFolder));

                    sw.Stop();

                    Debug.WriteLine("+ DONE in {0} ms: ", sw.ElapsedMilliseconds);
                }
                if (result.Any(o => o.Equals(false)))
                    return 2;
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}