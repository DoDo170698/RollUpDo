using Common.Helpers;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DLLHelper
{
    internal class Native
    {
        private const string dllFile = @"lib\Face.dll";


        // new DLL
        [DllImport(dllFile, EntryPoint = "Load_Model")]
        internal static extern short Load_Model(string szPrototxt1, string szPrototxt2, string szPrototxt3, string szCaffemodel);
        // [DllImport(@"F:\Project Runing\SmartHome\ComputerVision\30_Implement\33_source\extent\CSharp_Test_Face\TestCMNDCsharp\TestCMNDCsharp\bin\Release\cv3.4.2\Face.dll", EntryPoint = "Load_Model")]
        [DllImport(@"lib\Face.dll", EntryPoint = "Load_Model")]
       // [DllImport(@"F:\Project Runing\SmartHome\ComputerVision\Extends\TestCMNDCsharp\TestCMNDCsharp\bin\Release\cv3.4.2\Face.dll", EntryPoint = "Load_Model")]
        internal static extern ushort Load_Model_FaceKit(string szPrototxt1, string szPrototxt2, string szPrototxt3, string szCaffemodel);
        [DllImport(@"lib\Face.dll", EntryPoint = "LoadHTRModel")]
        internal static extern short LoadHTRModel(string fileName, string modelName, byte modelType);
       // [DllImport(@"F:\Project Runing\SmartHome\ComputerVision\30_Implement\33_source\extent\CSharp_Test_Face\TestCMNDCsharp\TestCMNDCsharp\bin\Release\cv3.4.2\Face.dll", EntryPoint = "FaceKit")]
        [DllImport(@"lib\Face.dll", EntryPoint = "FaceKit")]
        //[DllImport(@"F:\Project Runing\SmartHome\ComputerVision\Extends\TestCMNDCsharp\TestCMNDCsharp\bin\Release\cv3.4.2\Face.dll", EntryPoint = "FaceKit")]
        internal static extern Int16 FaceKit(ushort rows, ushort cols, int widthStep, IntPtr rgbData, 
            ushort[] x0, ushort[] y0, ushort[] x1, ushort[] y1, ushort[] x2, ushort[] y2, ushort[] x3, ushort[] y3);

        [DllImport(@"lib\Face.dll", EntryPoint = "FaceNet")]
         internal static extern short FaceNet(ushort rows, ushort cols, int widthStep, IntPtr RGBData, float[] OutData);
    }
}
