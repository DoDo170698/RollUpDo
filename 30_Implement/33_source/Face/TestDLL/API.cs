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

namespace DLLHelper
{
    public class API
    {
        private static object lockObj = new object();

        private static bool isCropCmndDNNDataLoaded = false;
        private static bool isLoad_PB_Model = false;
        private static bool isLoad_PB_Model_HTR = false;
        private static bool isLoad_Model_HTR = false;
        private static bool isLoad_Model_FaceKit = false;
        public static bool LoadHTR(string modelFile, string TensoModel, byte type)
        {
            lock (lockObj)
            {
                if (!isLoad_Model_HTR)
                {
                    int status = Native.LoadHTRModel(modelFile, TensoModel, type);

                    isLoad_Model_HTR = status == 1;
                }
                return isLoad_Model_HTR;
            }
        }

        public static int Recognize_Face(string cmndFile)
        {
            Stopwatch sw = Stopwatch.StartNew();
            Image<Bgr, byte> imageRGB = new Image<Bgr, byte>(cmndFile);

            int nrow = imageRGB.Height;
            //Console.WriteLine(nrow);
            int ncol = imageRGB.Width;
            //Console.WriteLine(ncol);
            int widthStep = imageRGB.MIplImage.WidthStep;
            //Console.WriteLine(widthStep);
            IntPtr rgbData = imageRGB.MIplImage.ImageData;
            float[] featureOut = new float[128];

            int countChar = Native.FaceNet((ushort)nrow, (ushort)ncol, widthStep, rgbData, featureOut);
            Console.WriteLine(countChar);
            if (countChar >= 1)
            {
                for (int i = 0; i < 128; i++)
                    Console.WriteLine(featureOut[i]);

            }//if
            return 1;
        }
        public static float[] FaceNet(Image<Bgr, byte> imageRGB, int leghtDetec = 128)
        {
            List<float[]> result = new List<float[]>();

            ushort rows = (ushort)imageRGB.Height;
            ushort cols = (ushort)imageRGB.Width;
            int widthStep = imageRGB.MIplImage.WidthStep;
            IntPtr rgbData = imageRGB.MIplImage.ImageData;
            float[] featureOut = new float[leghtDetec];

            int ok = Native.FaceNet(rows, cols, widthStep, rgbData, featureOut);
            Debug.WriteLine(ok);
            if (ok > 0)
            {
                for (int i = 0; i < 128; i++)
                    Debug.WriteLine(featureOut[i]);
            }
            return featureOut;
        }
        public static bool Load_FaceKit(string szPrototxt1, string szPrototxt2, string szPrototxt3, string szCaffemodel)
        {
            lock (lockObj)
            {
                if (!isLoad_Model_FaceKit)
                {
                    int status = Native.Load_Model_FaceKit(szPrototxt1, szPrototxt2, szPrototxt3, szCaffemodel);

                    isLoad_Model_FaceKit = status == 1;
                }
                return isLoad_Model_FaceKit;
            }
        }

        public static List<Mat> Recognize_FaceKit(Image<Bgr, byte> imageRGB, int maxFace = 30, bool warpTarget = true)
        {
            var list = new List<Mat>();

            ushort rows = (ushort)imageRGB.Height;
            ushort cols = (ushort)imageRGB.Width;
            int widthStep = imageRGB.MIplImage.WidthStep;
            IntPtr rgbData = imageRGB.MIplImage.ImageData;
            ushort[] x0 = new ushort[maxFace];
            ushort[] y0 = new ushort[maxFace];
            ushort[] x1 = new ushort[maxFace];
            ushort[] y1 = new ushort[maxFace];
            ushort[] x2 = new ushort[maxFace];
            ushort[] y2 = new ushort[maxFace];
            ushort[] x3 = new ushort[maxFace];
            ushort[] y3 = new ushort[maxFace];

            int countChar = Native.FaceKit(rows, cols, widthStep, rgbData, x0, y0, x1, y1, x2, y2, x3, y3);
            Debug.WriteLine(countChar);
            //if (countChar >= 1)
            //{
            //    for (int i = 0; i < countChar; i++)
            //        Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7}", x0[i], y0[i], x1[i], y1[i], x2[i], y2[i], x3[i], y3[i]);

            //}
            for (int i = 0; i < countChar; i++)
            {
                if (warpTarget == true)
                {
                    var warp = CVUtils.GetRectangle(imageRGB.Mat, x0[i], y0[i], x1[i], y1[i], x2[i], y2[i], x3[i], y3[i]);
                    list.Add(warp);
                }
                else
                {
                    var Khung = CVUtils.GetRectangle(x0[i], y0[i], x1[i], y1[i], x2[i], y2[i], x3[i], y3[i]);
                    var mat = CVUtils.Crop(imageRGB, Khung);
                    list.Add(mat);
                }

            }
            return list;
        }

    }
}
