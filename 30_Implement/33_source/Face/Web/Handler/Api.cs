#define debug

using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;

namespace SegmentText
{
    public class Api
    {
        private static object lockObj = new object();

        private static bool isCropCmndDNNDataLoaded = false;
        private static bool isLoad_PB_Model = false;

        public static bool LoadCropCmndDNNData(string weights, string prototxt)
        {
            lock (lockObj)
            {
                if (!isCropCmndDNNDataLoaded)
                {
                    int status = Native.LoadCmndCropData(weights, prototxt);
                    isCropCmndDNNDataLoaded = status == 1;
                }
                return isCropCmndDNNDataLoaded;
            }
        }

        public static Bitmap CropCmndDNN(string cmndFile)
        {
#if(debug)
            Stopwatch sw = Stopwatch.StartNew();
#endif
            Image<Bgr, byte> imageRGB = new Image<Bgr, byte>(cmndFile);

            int nrow = imageRGB.Height;
            int ncol = imageRGB.Width;
            int widthStep = imageRGB.MIplImage.WidthStep;
            int nChannels = imageRGB.MIplImage.NChannels;
            IntPtr rgbData = imageRGB.MIplImage.ImageData;

            ushort[] aLeft = new ushort[4];
            ushort[] aRight = new ushort[4];
            ushort[] aTop = new ushort[4];
            ushort[] aBottom = new ushort[4];

            int countCrop = Native.RGBDocCropDNN((ushort)nrow, (ushort)ncol, widthStep, rgbData, aLeft, aTop, aRight, aBottom);
            Bitmap cmndBitmap;
#if (debug)
            List<Rectangle> areas = new List<Rectangle>();
#endif

            Rectangle area = new Rectangle(aLeft[0], aTop[0], aRight[0] - aLeft[0], aBottom[0] - aTop[0]);
            Image<Bgr, byte> cmndImage = CVUtils.CropBgr(imageRGB, area);
#if (debug)
            cmndImage.Save(string.Format("Cmnd_{0}.jpg", 0));
            areas.Add(area);
#endif
            cmndBitmap = cmndImage.ToBitmap();
            cmndImage.Dispose();
#if (debug)
            CVUtils.DrawRect(imageRGB.ToBitmap(), "CMND_Areas.jpg", areas.ToArray());
#endif
#if (debug)
            sw.Stop();
            Console.WriteLine("RGBDocCropDNN in {0} ms", sw.ElapsedMilliseconds);
#endif
            imageRGB.Dispose();

            return cmndBitmap;
        }

        public static string RecognizeHW_IAM(string cmndFile, ushort segmentationType)
        {
#if(debug)
            Stopwatch sw = Stopwatch.StartNew();
#endif
            Image<Bgr, byte> imageRGB = new Image<Bgr, byte>(cmndFile);

            int nrow = imageRGB.Height;
            int ncol = imageRGB.Width;
            int widthStep = imageRGB.MIplImage.WidthStep;
            int nChannels = imageRGB.MIplImage.NChannels;
            IntPtr rgbData = imageRGB.MIplImage.ImageData;

            ushort[] aLeft = new ushort[4000];
            ushort[] aRight = new ushort[4000];
            ushort[] aTop = new ushort[4000];
            ushort[] aBottom = new ushort[4000];
            string[] values = new string[4000];

            int countCrop = Native.IAMRecognizeHW((ushort)nrow, (ushort)ncol, widthStep, rgbData, segmentationType, aLeft, aTop, aRight, aBottom, values);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < countCrop; i++)
            {
                sb.Append(values[i]).Append(" ");
            }

#if (debug)
            sw.Stop();
            Console.WriteLine("RGBDocCropDNN in {0} ms", sw.ElapsedMilliseconds);
#endif
            imageRGB.Dispose();

            return sb.ToString();
        }

        public static bool Load_PB_Model_test(string modelFile)
        {
            lock (lockObj)
            {
                if (!isLoad_PB_Model)
                {
                    var status = Native.Load_PB_Model(modelFile);
                    isLoad_PB_Model = status == 1;
                }
                return isLoad_PB_Model;
            }
        }

        public static void LineSegmentationHW_test(string cmndFile)
        {
#if(debug)
            Stopwatch sw = Stopwatch.StartNew();
#endif
            Image<Bgr, byte> imageRGB = new Image<Bgr, byte>(cmndFile);
            int nrow = imageRGB.Height;
            int ncol = imageRGB.Width;
            //int widthStep = imageRGB.MIplImage.widthStep;
            //int nChannels = imageRGB.MIplImage.nChannels;
            //IntPtr rgbData = imageRGB.MIplImage.imageData;
            int widthStep = imageRGB.MIplImage.WidthStep;
            int nChannels = imageRGB.MIplImage.NChannels;
            IntPtr rgbData = imageRGB.MIplImage.ImageData;
            ushort sectype = 2;
            int[] idl = new int[400];
            ushort[] aLeft = new ushort[400];
            ushort[] aRight = new ushort[400];
            ushort[] aTop = new ushort[400];
            ushort[] aBottom = new ushort[400];
            //ushort aCountLine = 0;
            ushort[] aCountSeg = new ushort[400];

            //int countCrop = Native.LineSegmentationHW((ushort)nrow, (ushort)ncol, widthStep, rgbData, sectype, aCountLine, aCountSeg, aLeft, aTop, aRight, aBottom);
            int countCrop = Native.GapSegmentHWT((ushort)nrow, (ushort)ncol, widthStep, rgbData, sectype, aLeft, aTop, aRight, aBottom);
            //if (countCrop == 0)
            //{
            //    Console.WriteLine("countCrop =0");
            //}
            Bitmap ochuBitmap;
#if (debug)
            List<Rectangle> areas = new List<Rectangle>();
#endif
            for (int i = 0; i < countCrop; i++)
            {
                Rectangle area = new Rectangle(aLeft[i], aTop[i], aRight[i] - aLeft[i], aBottom[i] - aTop[i]);
                Image<Bgr, byte> ochuImage = CVUtils.CropBgr(imageRGB, area);
#if (debug)
                ochuImage.Save(string.Format("Ochu_{0}.jpg", i));
                areas.Add(area);
#endif
                ochuBitmap = ochuImage.ToBitmap();
                ochuImage.Dispose();
            }
#if (debug)
            CVUtils.DrawRect(imageRGB.ToBitmap(), "Test.jpg", areas.ToArray());
#endif
#if (debug)
            sw.Stop();
            Console.WriteLine("Test in {0} ms", sw.ElapsedMilliseconds);
#endif
            imageRGB.Dispose();
        }
        public static bool LineSegmentationHW_test(string ImageRoot, string outFolder)
        {
            try
            {
                Stopwatch sw = Stopwatch.StartNew();
                Image<Bgr, byte> imageRGB = new Image<Bgr, byte>(ImageRoot);
                int nrow = imageRGB.Height;
                int ncol = imageRGB.Width;
                int widthStep = imageRGB.MIplImage.WidthStep;
                int nChannels = imageRGB.MIplImage.NChannels;
                IntPtr rgbData = imageRGB.MIplImage.ImageData;
                ushort sectype = 1;
                int[] idl = new int[400];
                ushort[] aLeft = new ushort[400];
                ushort[] aRight = new ushort[400];
                ushort[] aTop = new ushort[400];
                ushort[] aBottom = new ushort[400];
                //ushort aCountLine = 0;
                ushort[] aCountSeg = new ushort[400];

                //int countCrop = Native.LineSegmentationHW((ushort)nrow, (ushort)ncol, widthStep, rgbData, sectype, aCountLine, aCountSeg, aLeft, aTop, aRight, aBottom);
                int countCrop = Native.GapSegmentHWT((ushort)nrow, (ushort)ncol, widthStep, rgbData, sectype, aLeft, aTop, aRight, aBottom);
                //if (countCrop == 0)
                //{
                //    Console.WriteLine("countCrop =0");
                //}
                Bitmap ochuBitmap;
                List<Rectangle> areas = new List<Rectangle>();
                for (int i = 0; i < countCrop; i++)
                {
                    Rectangle area = new Rectangle(aLeft[i], aTop[i], aRight[i] - aLeft[i], aBottom[i] - aTop[i]);
                    Image<Bgr, byte> ochuImage = CVUtils.CropBgr(imageRGB, area);
                    ochuImage.Save(string.Format(outFolder + "\\Ochu_{0}.jpg", i));
                    areas.Add(area);
                    ochuBitmap = ochuImage.ToBitmap();
                    ochuImage.Dispose();
                }
                CVUtils.DrawRect(imageRGB.ToBitmap(), outFolder + "\\Test.jpg", areas.ToArray());
                sw.Stop();
                Console.WriteLine("Test in {0} ms", sw.ElapsedMilliseconds);
                imageRGB.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
