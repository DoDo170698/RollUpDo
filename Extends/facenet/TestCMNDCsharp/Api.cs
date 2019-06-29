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
using TestCMNDCsharp.Utility;
using System.IO;

namespace TestCMNDCsharp
{
    public class Api
    {
        private static object lockObj = new object();

        private static bool isCropCmndDNNDataLoaded = false;
        private static bool isLoad_PB_Model = false;
        private static bool isLoad_PB_Model_HTR = false;
        private static bool isLoad_Model_HTR = false;
        public static bool LoadHTR(string modelFile, byte type)
        {
            lock (lockObj)
            {
                if (!isLoad_Model_HTR)
                {
                    int status = Native.LoadHTRModel(modelFile, type);
                    isLoad_Model_HTR = status == 1;
                }
                return isLoad_Model_HTR;
            }
        }

       
        public static int RecognizeHW_IAM(string cmndFile, string charFile)
        {
#if(debug)
            Stopwatch sw = Stopwatch.StartNew();
#endif
            Image<Bgr, byte> imageRGB = new Image<Bgr, byte>(cmndFile);

            int nrow = imageRGB.Height;
            int ncol = imageRGB.Width;
            int widthStep = imageRGB.MIplImage.widthStep;
            IntPtr rgbData = imageRGB.MIplImage.imageData;
            
            //string text="";
            ushort maxCharCount = 32;
            //ulong[] aUnicodeIndex = new ulong[maxCharCount];
            UInt32[] aUnicodeIndex = new UInt32[maxCharCount];


            int countChar = Native.GrayBlackCharImageToText((ushort)nrow, (ushort)ncol, widthStep, rgbData, maxCharCount, aUnicodeIndex);
            if (countChar >= 1)
            {
                //for (int i = 0; i < countChar; i++)
                //{
                //    //Console.WriteLine("i", aUnicodeIndex[i]);
                //    //Console.WriteLine(");
                //    Console.WriteLine(aUnicodeIndex[i]);
                //}
                string aa = ReadText(countChar, aUnicodeIndex, charFile);
            }//if

#if (debug)
            sw.Stop();
            Console.WriteLine("RGBDocCropDNN in {0} ms", sw.ElapsedMilliseconds);
#endif
            return 1;
        }
        public static string ReadText(int countChar,UInt32[] value,string file)
        {
            string text="";
            Console.OutputEncoding = Encoding.UTF8;
            FileStream fs = new FileStream(file, FileMode.Open);
            StreamReader rd = new StreamReader(fs, Encoding.UTF8);
            String data = rd.ReadToEnd();// ReadLine() chỉ đọc 1 dòng đầu thoy, ReadToEnd là đọc hết
            //Console.WriteLine(data);
            for (int i = 0; i < countChar; i++)
            {
                int ee = (int)value[i];
                text += data[ee];
            }
            rd.Close();
            Console.WriteLine(text);
            return text;
        }
        //public static bool LoadHTR2(string modelFile, char type)
        //{
        //    lock (lockObj)
        //    {
        //        if (!isLoad_Model_HTR)
        //        {
        //            int status = Native.Load_Tensor_Model(modelFile, type);
        //            isLoad_Model_HTR = status == 1;
        //        }
        //        return isLoad_Model_HTR;
        //    }
        //}
        public static int Recognize_Face(string cmndFile)
        {
#if(debug)
            Stopwatch sw = Stopwatch.StartNew();
#endif
            Image<Bgr, byte> imageRGB = new Image<Bgr, byte>(cmndFile);

            int nrow = imageRGB.Height;
            int ncol = imageRGB.Width;
            int widthStep = imageRGB.MIplImage.widthStep;
            IntPtr rgbData = imageRGB.MIplImage.imageData;
            float[] featureOut = new float[128];
            //string text="";
            //ushort maxCharCount = 32;
            //ulong[] aUnicodeIndex = new ulong[maxCharCount];
            //UInt32[] aUnicodeIndex = new UInt32[maxCharCount];

            // sao lai là ushort
            int countChar = Native.FaceNet((ushort)nrow, (ushort)ncol, widthStep, rgbData, featureOut);
            Console.WriteLine(countChar);
            if (countChar >= 1)
            {
                for (int i = 0; i < 128; i++)
                    Console.WriteLine(featureOut[i]);
               
            }//if

#if (debug)
            sw.Stop();
            Console.WriteLine("Face in {0} ms", sw.ElapsedMilliseconds);
#endif
            return 1;
        }
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
            int widthStep = imageRGB.MIplImage.widthStep;
            int nChannels = imageRGB.MIplImage.nChannels;
            IntPtr rgbData = imageRGB.MIplImage.imageData;

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

        

        public static bool Load_PB_Model_test(string modelFile)
        {
            lock (lockObj)
            {
                if (!isLoad_PB_Model)
                {
                    int status = Native.Load_PB_Model(modelFile);
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
            int widthStep = imageRGB.MIplImage.widthStep;
            int nChannels = imageRGB.MIplImage.nChannels;
            IntPtr rgbData = imageRGB.MIplImage.imageData;
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

        //14-3-2019
        public static bool Load_PB_Model_HandWriting_test(string modelFile)
        {
            lock (lockObj)
            {
                if (!isLoad_PB_Model_HTR)
                {
                    int status = Native.Load_PB_Model_HandWriting(modelFile);
                     if (status == -1)
                        return false;
                    isLoad_PB_Model_HTR = status == 1;
                }
                return isLoad_PB_Model_HTR;
            }
        }

        //public static void GrayBlackCharImageToText_test(string cmndFile)
        //{
        //    if (!isLoad_PB_Model_HTR) return;
        //    Image<Gray, byte> imageGray = new Image<Gray, byte>(cmndFile);
        //    int nrow = imageGray.Height;
        //    int ncol = imageGray.Width;
        //    int widthStep = imageGray.MIplImage.widthStep;
        //    int nChannels = imageGray.MIplImage.nChannels;
        //    IntPtr grayData = imageGray.MIplImage.imageData;
        //    //char [] text=new  char[32+1];
        //    string text="";
        //    Native.GrayBlackCharImageToText((ushort)nrow, (ushort)ncol, widthStep, grayData, text,32);
        //    //Console.WriteLine("Text", text);
        //    imageGray.Dispose();
        //}
    }
}