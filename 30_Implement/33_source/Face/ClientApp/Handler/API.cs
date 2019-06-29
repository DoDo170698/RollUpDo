using ClientApp.Helper;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientApp.Handler
{
    public class API
    {
        private static object lockObj = new object();

        private static bool isLoad_PB_Model = false;
        public static ushort[] ALeft { get; set; }
        public static ushort[] ARight { get; set; }
        public static ushort[] ATop { get; set; }
        public static ushort[] ABottom { get; set; }
        public static bool Load_Model(string one, string tow, string three, string four)
        {
            lock (lockObj)
            {
                if (!isLoad_PB_Model)
                {
                    try
                    {
                        var status = Native.Load_Model(one, tow, three, four);
                        isLoad_PB_Model = status == 1;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }

                }
                return isLoad_PB_Model;
            }
        }
        public static bool ModelDetect(string one, string tow, string three, string four)
        {
            lock (lockObj)
            {
                if (!isLoad_PB_Model)
                {
                    try
                    {
                        var status = Native.ModelDetect(one, tow, three, four);
                        isLoad_PB_Model = status == 1;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }

                }
                return isLoad_PB_Model;
            }
        }

        unsafe public static List<Mat> FaceKit(string ImageRoot, string outFolder, int x = 30)
        {
            List<Mat> list = new List<Mat>();
            try
            {
                Image<Bgr, byte> image = new Image<Bgr, byte>(ImageRoot);
                ushort rows = (ushort)image.Height;
                ushort cols = (ushort)image.Width;
                int widthStep = image.MIplImage.WidthStep;
                IntPtr rgbData = image.MIplImage.ImageData;
               
                ushort countObject = 1;
                ushort[] x0 = new ushort[x], y0 = new ushort[x],
                      x1 = new ushort[x], y1 = new ushort[x],
                         x2 = new ushort[x], y2 = new ushort[x],
                            x3 = new ushort[x], y3 = new ushort[x];
                // call từ dll 
               

                int countCrop = Native.FaceKit(rows, cols, widthStep, rgbData, &countObject, x0, y0, x1, y1, x2, y2, x3, y3);


                Debug.WriteLine(countCrop);


                Bitmap ochuBitmap;
                List<Rectangle> areas = new List<Rectangle>();
                PointF[] points = new PointF[4];
                // bên dưới lỗi 
                //for (int i = 0; i < countObject; i++)
                //{
                //    //Rectangle area = CVUtils.GetRectangle(x0[i], y0[i], x1[i], y1[i], x2[i], y2[i], x3[i], y3[i]);
                //    //var xxx = image.Mat;
                //    //var face = CVUtils.Crop(image, area);

                //    //string name = string.Format(outFolder + "\\face_{0}.jpg", i);
                //    //face.Bitmap.Save(name);
                //    var warp = CVUtils.GetRectangle(image.Mat, x0[i], y0[i], x1[i], y1[i], x2[i], y2[i], x3[i], y3[i]);
                //    warp.Bitmap.Save(string.Format(outFolder + "\\warp_{0}.PNG", i));
                //    //  areas.Add(area);

                //}
                for (int i = 0; i < countObject; i++)
                {
                    var warp = CVUtils.GetRectangle(image.Mat, x0[i], y0[i], x1[i], y1[i], x2[i], y2[i], x3[i], y3[i]);
                    list.Add(warp);
                    warp.Bitmap.Save(string.Format(outFolder + "\\warp_{0}.PNG", i));
                }
                CVUtils.DrawRect(image.ToBitmap(), outFolder + "\\Test.jpg", areas.ToArray());
                image.Dispose();
              //  return list;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
             //   return false;
            }
            return list;
        }
        unsafe public static List<Mat> FaceKit(Image<Bgr, byte> image , string outFolder, int x = 30)
        {
            List<Mat> list = new List<Mat>();
            try
            {
               
                ushort rows = (ushort)image.Height;
                ushort cols = (ushort)image.Width;
                int widthStep = image.MIplImage.WidthStep;
                IntPtr rgbData = image.MIplImage.ImageData;

                ushort countObject = 1;
                ushort[] x0 = new ushort[x], y0 = new ushort[x],
                      x1 = new ushort[x], y1 = new ushort[x],
                         x2 = new ushort[x], y2 = new ushort[x],
                            x3 = new ushort[x], y3 = new ushort[x];
                // call từ dll 


                int countCrop = Native.FaceKit(rows, cols, widthStep, rgbData, &countObject, x0, y0, x1, y1, x2, y2, x3, y3);


                Debug.WriteLine(countCrop);


                Bitmap ochuBitmap;
                List<Rectangle> areas = new List<Rectangle>();
                PointF[] points = new PointF[4];
                // bên dưới lỗi 
                //for (int i = 0; i < countObject; i++)
                //{
                //    //Rectangle area = CVUtils.GetRectangle(x0[i], y0[i], x1[i], y1[i], x2[i], y2[i], x3[i], y3[i]);
                //    //var xxx = image.Mat;
                //    //var face = CVUtils.Crop(image, area);

                //    //string name = string.Format(outFolder + "\\face_{0}.jpg", i);
                //    //face.Bitmap.Save(name);
                //    var warp = CVUtils.GetRectangle(image.Mat, x0[i], y0[i], x1[i], y1[i], x2[i], y2[i], x3[i], y3[i]);
                //    warp.Bitmap.Save(string.Format(outFolder + "\\warp_{0}.PNG", i));
                //    //  areas.Add(area);

                //}
                for (int i = 0; i < countObject; i++)
                {
                    var warp = CVUtils.GetRectangle(image.Mat, x0[i], y0[i], x1[i], y1[i], x2[i], y2[i], x3[i], y3[i]);
                    list.Add(warp);
                    warp.Bitmap.Save(string.Format(outFolder + "\\warp_{0}.PNG", i));
                }
                CVUtils.DrawRect(image.ToBitmap(), outFolder + "\\Test.jpg", areas.ToArray());
                image.Dispose();
                //  return list;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                //   return false;
            }
            return list;
        }
        ///// <summary>
        ///// Lấy ra list các khuôn mặt trong frame 
        ///// </summary>
        ///// <param name="ImageRoot">hình ảnh đưa vào</param>
        ///// <param name="maxFace">số lượng mặt tối đa có thể nhận được.giá trị mặc định là 30 nếu không truyền vào</param>
        ///// <returns></returns>
        //unsafe public static List<Mat> GetFace(Image<Bgr, byte> ImageRoot, string outFace, int maxFace = 30)
        //{
        //    var list = new List<Mat>();
        //    ushort rows = (ushort)ImageRoot.Height;
        //    ushort cols = (ushort)ImageRoot.Width;
        //    int widthStep = ImageRoot.MIplImage.WidthStep;
        //    IntPtr rgbData =ImageRoot.MIplImage.ImageData;
        //    ushort countObject = 1;
        //    ushort[] x0 = new ushort[maxFace], y0 = new ushort[maxFace],
        //          x1 = new ushort[maxFace], y1 = new ushort[maxFace],
        //             x2 = new ushort[maxFace], y2 = new ushort[maxFace],
        //                x3 = new ushort[maxFace], y3 = new ushort[maxFace];

        //    int countCrop = 30;
        //    int x = Native.FaceKit(rows, cols, widthStep, rgbData, &countObject, x0, y0, x1, y1, x2, y2, x3, y3);
        //    Debug.WriteLine(string.Format("frame có {0} khuôn mặt", +countCrop));

        //    List<Rectangle> areas = new List<Rectangle>();
        //    for (int i = 0; i < countObject; i++)
        //    {
        //        var warp = CVUtils.GetRectangle(ImageRoot.Mat, x0[i], y0[i], x1[i], y1[i], x2[i], y2[i], x3[i], y3[i]);
        //        list.Add(warp);
        //        warp.Bitmap.Save(string.Format(outFace + "\\warp_{0}.PNG", DateTime.Now.ToString("yyyy_MM_dd HH_mm_ss_fff")));
        //        Thread.Sleep(300);
        //    }

        //    //CVUtils.DrawRect(ImageRoot.ToBitmap(), outFace + "\\Test.jpg", areas.ToArray());
        //    ImageRoot.Dispose();
        //    return list;
        //}
        /// <summary>
        /// Lấy ra list các khuôn mặt trong frame 
        /// </summary>
        /// <param name="ImageRoot">hình ảnh đưa vào</param>
        /// <param name="maxFace">số lượng mặt tối đa có thể nhận được.giá trị mặc định là 30 nếu không truyền vào</param>
        /// <returns></returns>
        public unsafe static List<Mat> GetFace(Image<Bgr, byte> ImageRoot, string outFace, int maxFace = 30)
        {
            var list = new List<Mat>();
            ushort rows = (ushort)ImageRoot.Height;
            ushort cols = (ushort)ImageRoot.Width;
            int widthStep = ImageRoot.MIplImage.WidthStep;
            IntPtr rgbData = ImageRoot.MIplImage.ImageData;
           
            ushort[] x0 = new ushort[maxFace], y0 = new ushort[maxFace],
                  x1 = new ushort[maxFace], y1 = new ushort[maxFace],
                     x2 = new ushort[maxFace], y2 = new ushort[maxFace],
                        x3 = new ushort[maxFace], y3 = new ushort[maxFace];
            ushort countObjectOLD = 1;
            int countObject =0;

             countObject = Native.GetFace(rows, cols, widthStep, rgbData, x0, y0, x1, y1, x2, y2, x3, y3);
         //  Native.FaceKit(rows, cols, widthStep, rgbData, &countObjectOLD, x0, y0, x1, y1, x2, y2, x3, y3);
            Debug.WriteLine(string.Format("frame có {0} khuôn mặt", +countObject));

            List<Rectangle> areas = new List<Rectangle>();
            for (int i = 0; i < countObject; i++)
            {
                var warp = CVUtils.GetRectangle(ImageRoot.Mat, x0[i], y0[i], x1[i], y1[i], x2[i], y2[i], x3[i], y3[i]);
                list.Add(warp);
            }
            //for (int i = 0; i < countObjectOLD; i++)
            //{
            //    var warp = CVUtils.GetRectangle(ImageRoot.Mat, x0[i], y0[i], x1[i], y1[i], x2[i], y2[i], x3[i], y3[i]);
            //    list.Add(warp);
            //      warp.Bitmap.Save(string.Format(outFace + "\\warp_{0}.PNG", DateTime.Now.ToString("yyyy_MM_dd HH_mm_ss_fff")));
            //      Thread.Sleep(300);
            //}

            //CVUtils.DrawRect(ImageRoot.ToBitmap(), outFace + "\\Test.jpg", areas.ToArray());
            ImageRoot.Dispose();
            return list;
        }
        public static float[] FaceNet(Mat mat)
        {
            Image<Bgr, byte> ImageRoot = mat.ToImage<Bgr, byte>();
            ushort rows = (ushort)ImageRoot.Height;
            ushort cols = (ushort)ImageRoot.Width;
            int widthStep = ImageRoot.MIplImage.WidthStep;
            IntPtr rgbData = ImageRoot.MIplImage.ImageData;
            float[] DacTrung = new float[128];

            int ok = Native.FaceNet(rows, cols, widthStep, rgbData,DacTrung);
            return DacTrung;

        }
    }
}
