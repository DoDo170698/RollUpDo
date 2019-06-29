using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ClientApp.Handler
{
    internal class Native
    {
        [DllImport(@"lib\facekit.dll", EntryPoint = "Load_Model")]
      //  [DllImport(@"F:\Project Runing\SmartHome\ComputerVision\Face\x64\Release\facekit.dll", EntryPoint = "Load_Model")]
         internal static extern short Load_Model(string szPrototxt1, string szPrototxt2, string szPrototxt3, string szCaffemodel);

        [DllImport(@"lib\facekit.dll", EntryPoint = "FaceKit")]
       unsafe internal static extern ushort FaceKit(ushort rows, ushort cols,  int widthStep, IntPtr rgbData, ushort* countBox,
            ushort[] x0, ushort[] y0, ushort[] x1, ushort[] y1, ushort[] x2, ushort[] y2, ushort[] x3, ushort[] y3);
        //  [DllImport(@"F:\Project Runing\SmartHome\ComputerVision\Face\x64\Release\facekit.dll", EntryPoint = "FaceKit")]
        //internal static extern ushort FaceKit(ushort rows, ushort cols, int widthStep, IntPtr rgbData, 
        //  ushort[] x0, ushort[] y0, ushort[] x1, ushort[] y1, ushort[] x2, ushort[] y2, ushort[] x3, ushort[] y3);

        // new DLL
        [DllImport(@"lib\Face.dll", EntryPoint = "Load_Model")]
        internal static extern short ModelDetect(string szPrototxt1, string szPrototxt2, string szPrototxt3, string szCaffemodel);
        [DllImport(@"lib\Face.dll", EntryPoint = "LoadHTRModel")]
        internal static extern short ModelGe(string fileName, string modelName, byte modelType);
        [DllImport(@"lib\Face.dll", EntryPoint = "FaceKit")]
         internal static extern Int16 GetFace(ushort rows, ushort cols, int widthStep, IntPtr rgbData, 
            ushort[] x0, ushort[] y0, ushort[] x1, ushort[] y1, ushort[] x2, ushort[] y2, ushort[] x3, ushort[] y3);
        [DllImport(@"lib\Face.dll", EntryPoint = "FaceKit")]
         internal static extern short FaceNet(ushort rows, ushort cols, int widthStep, IntPtr RGBData, float[] OutData);
    }
}
