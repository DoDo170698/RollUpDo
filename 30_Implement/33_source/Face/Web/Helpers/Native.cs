using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace Web.Helpers
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

        //[DllImport(@"cv3.4.2\East.dll", EntryPoint = "Load_PB_Model")]
        [DllImport(@"G:\Project\Itgroup\Itgroup2018\ComputerVision\DetectFace\SegmentText\dll\East.dll", EntryPoint = "Load_PB_Model")]
        internal static extern ushort Load_PB_Model(string modelFile);

        [DllImport(@"G:\Project\Itgroup\Itgroup2018\ComputerVision\DetectFace\SegmentText\dll\East.dll", EntryPoint = "LineSegmentationHW")]
        internal static extern ushort LineSegmentationHW(ushort rows, ushort cols, int widthStep, IntPtr rgbData, ushort segmentationType, ushort aCountLine, ushort[] aCountSeg, ushort[] aLeft, ushort[] aTop, ushort[] aRight, ushort[] aBottom);

        [DllImport(@"G:\Project\Itgroup\Itgroup2018\ComputerVision\DetectFace\SegmentText\dll\East.dll", EntryPoint = "GapSegmentHWT")]
        internal static extern ushort GapSegmentHWT(ushort rows, ushort cols, int widthStep, IntPtr rgbData, ushort segmentationType, ushort[] aLeft, ushort[] aTop, ushort[] aRight, ushort[] aBottom);


    }
}