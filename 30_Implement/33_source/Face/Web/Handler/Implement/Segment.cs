using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Web.Handler.Ingetface;
using Web.Helpers;

namespace Web.Handler.Implement
{
    public class Segment : ISegment
    {
        public int LineSegment(string outFolder, string[] images = null)
        {
            // chưa chuẩn kiểm tra nhỡ trong for nó lỗi thì chưa biết
            // chưa cấu hình file name cho file đầu ra
            List<bool> result = new List<bool>();
            try
            {


                string debugDir = AppDomain.CurrentDomain.BaseDirectory;


                Stopwatch sw = Stopwatch.StartNew();
                string pbFile = @"G:\Project\Itgroup\Itgroup2018\ComputerVision\DetectFace\C\Test\model\frozen_east_text_detection.pb";
                result.Add(EastHelper.Segment(pbFile, images, outFolder));

                sw.Stop();

                Debug.WriteLine("+ DONE in {0} ms: ", sw.ElapsedMilliseconds);

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