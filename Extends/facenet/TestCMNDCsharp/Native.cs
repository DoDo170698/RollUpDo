using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace TestCMNDCsharp
{
    internal class Native
    {
        [DllImport(@"cv3.4.2\crcmnd.dll", EntryPoint = "RGBCmndCrop")]
        internal static extern ushort RGBDocCropDNN(ushort rows, ushort cols, int widthStep, IntPtr rgbData, ushort[] aLeft, ushort[] aTop, ushort[] aRight, ushort[] aBottom);

        [DllImport(@"cv3.4.2\crcmnd.dll", EntryPoint = "LoadCmndCropData")]
        internal static extern ushort LoadCmndCropData(string weights, string prototxt);

        [DllImport(@"cv3.4.2\crcmnd.dll", EntryPoint = "LoadIAMRecognizeHWData")]
        internal static extern ushort LoadIAMRecognizeHWData(string modelFile);

        [DllImport(@"cv3.4.2\crcmnd.dll", EntryPoint = "IAMRecognizeHW")]
        internal static extern ushort IAMRecognizeHW(ushort rows, ushort cols, int widthStep, IntPtr rgbData, ushort segmentationType, ushort[] aLeft, ushort[] aTop, ushort[] aRight, ushort[] aBottom, string[] values);

        [DllImport(@"cv3.4.2\East.dll", EntryPoint = "Load_PB_Model")]
        internal static extern ushort Load_PB_Model(string modelFile);

        [DllImport(@"cv3.4.2\East.dll", EntryPoint = "LineSegmentationHW")]
        internal static extern ushort LineSegmentationHW(ushort rows, ushort cols, int widthStep, IntPtr rgbData, ushort segmentationType, ushort aCountLine, ushort[] aCountSeg, ushort[] aLeft, ushort[] aTop, ushort[] aRight, ushort[] aBottom);

        [DllImport(@"cv3.4.2\East.dll", EntryPoint = "GapSegmentHWT")]
        internal static extern ushort GapSegmentHWT(ushort rows, ushort cols, int widthStep, IntPtr rgbData, ushort segmentationType, ushort[] aLeft, ushort[] aTop, ushort[] aRight, ushort[] aBottom);
        // 14-3-2019
        [DllImport(@"cv3.4.2\East.dll", EntryPoint = "Load_PB_Model_HandWriting")]
        internal static extern ushort Load_PB_Model_HandWriting(string modelFile);

        // 24-04-2019
        [DllImport(@"cv3.4.2\East.dll", EntryPoint = "LoadHTRModel")]
        internal static extern ushort LoadHTRModel(string szModelFileName, byte type); // string thì thì ko rõ có ép về kiểu const char* bên C++?

        [DllImport(@"cv3.4.2\East.dll", EntryPoint = "GrayBlackCharImageToText")]
        internal static extern ushort GrayBlackCharImageToText(ushort rows, ushort cols, int widthStep, IntPtr rgbData, ushort maxCharCount, UInt32[] aUnicodeIndex);

        [DllImport(@"cv3.4.2\East.dll", EntryPoint = "FaceNet")]
        internal static extern Int16 FaceNet(ushort rows, ushort cols, int widthStep, IntPtr RGBData,float[] featureOut);
        //[DllImport(@"cv3.4.2\tf_tutorials_example_trainer.dll", EntryPoint = "Load_Tensor_Model")]
        //internal static extern ushort Load_Tensor_Model(string modelFile, char type );

        //[DllImport(@"cv3.4.2\tf_tutorials_example_trainer.dll", EntryPoint = "PredictImg")]
        //internal static extern ushort PredictImg( short rows, short cols, float[] imgData, short maxTextLen, long[] aUnicodeIndex);
    }
}
