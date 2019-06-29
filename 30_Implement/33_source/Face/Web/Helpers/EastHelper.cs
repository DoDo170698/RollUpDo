using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Web.Helpers
{
    public class EastHelper
    {

        public static bool Segment(string filePb, string[] fileImages, string outFolder)
        {
            try
            {

                string weights = string.IsNullOrEmpty(filePb) == true ? @"model\frozen_east_text_detection.pb" : filePb;
                //  APISegment.Load_PB_Model_test(weights);
                APISegment.Imporst(@"G:\Project\Itgroup\Itgroup2018\ComputerVision\Face\Web\Dll\East\East.dll", "Load_PB_Model", new object[] { weights });
                if (fileImages == null)
                {
                    fileImages = new string[]
                    {
                     @"Test\Untitled.png",
                    };
                }
                TestLineSec(fileImages, outFolder);
                //   Debug.WriteLine("DONE ALL!!!");
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
        //private static void TestLineSec()
        private static void TestLineSec(string[] arayImage, string outFolder)
        {
            //string[] cmndFiles = new string[] {
            //    @"Test\Untitled.png",
            //};
            string debugDir = AppDomain.CurrentDomain.BaseDirectory;

            foreach (string imgFile in arayImage)
            {

                Stopwatch sw = Stopwatch.StartNew();

                APISegment.LineSegmentationHW_test(imgFile, outFolder);

                sw.Stop();

                Debug.WriteLine("+ DONE in {0} ms: ", sw.ElapsedMilliseconds);
            }
        }
    }
}