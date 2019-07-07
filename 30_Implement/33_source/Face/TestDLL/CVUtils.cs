using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;

namespace ClientApp.Helper
{
    public class CVUtils
    {
        public static Image<Bgr, byte> CropBgr(Image<Bgr, byte> image, System.Drawing.Rectangle area, string description = null)
        {
            try
            {
                Image<Bgr, byte> croped_image = new Image<Bgr, byte>(area.Width, area.Height);
                image.ROI = area;
                CvInvoke.cvCopy(image, croped_image, IntPtr.Zero);
                CvInvoke.cvResetImageROI(image);

                return croped_image;
            }
            catch (Exception e)
            {
                if (description == null)
                    throw new InvalidOperationException(string.Format("Unable crop image:\n{0}", e.ToString()));
                else throw new InvalidOperationException(string.Format("Unable crop image:{0}\n{1}", description, e.ToString()));
            }
        }
        public static Mat Crop(Image<Bgr, byte> image, System.Drawing.Rectangle area)
        {
            return new Mat(image.Mat, area);
        }
        public static Mat Crop(Image<Bgr, byte> image, Mat area)
        {
            Mat result = new Mat();
            image.Mat.CopyTo(result, area);
            return result;
        }
        public static Mat Crop(Mat image, Mat area)
        {
            Mat result = new Mat();
            image.CopyTo(result, area);
            return result;
        }

        public static void DrawRect(System.Drawing.Bitmap sourceImage, string save_image, params Rectangle[] areas)
        {
            Image<Bgr, byte> cvimage = new Image<Bgr, byte>(sourceImage);

            foreach (Rectangle area in areas)
            {
                cvimage.Draw(area, new Bgr(0, 0, 255), 1);
            }

            cvimage.ToBitmap().Save(save_image, ImageFormat.Jpeg);
        }
        public static Rectangle GetRectangle(int x0, int y0,
            int x1, int y1, int x2, int y2, int x3, int y3)
        {
            int[] Xs = new int[4];Xs[0] = x0; Xs[1] = x1; Xs[2] = x2; Xs[3] = x3;
            int[] Ys = new int[4];Ys[0] = y0; Ys[1] = y1; Ys[2] = y2; Ys[3] = y3;
            int widh = Math.Abs(Xs.Max() - Xs.Min());
            int height = Math.Abs(Ys.Max() - Ys.Min());

            return new Rectangle(Xs.Min(), Ys.Min(), widh, height);
        }
        public static Mat GetRectangle(Point[] point)
        {
            VectorOfPoint vt = new VectorOfPoint(point);
            Mat face = vt.GetInputArray().GetMat();
            return face;
        }
        //public static Rectangle GetRectangle(Mat imageRoot, int x0, int y0,
        //    int x1, int y1, int x2, int y2, int x3, int y3)
        //{

        //    // taọ ra Rectangle là hình chữ nhật bao quanh hình chữ nhật chéo : có thể hình này trùng vs hình nếu hình là hình thẳng
        //    // check xem 2 hình có trùng nhau không
        //    // nếu trùng thì lấy luôn ==>-----------
        //    // nếu k trùng:
        //    // lập pt đường thẳng đi qua 4 điểm _> 4 đường == 4 cạnh 
        //    // lâp pt 2 đường chéo -> giao điẻm == tâm 
        //    // for tất cả các điểm tròng hình bao  so sánh với tâm trên cả 4 đường thẳng
        //    // nếu cùng dấu vs tâm => cùng phía -> lấy còn lại bỏ 
        //    // các điểm lấy tương tự for tiếp với các đường khác
        //    // kết quả đc tập hợp điểm thuộc hình
        //    //dùng phương thức ày lấy ra Mat

        //    //VectorOfPoint vt = new VectorOfPoint(point);
        //    //Mat face = vt.GetInputArray().GetMat();
        //    //return face;


        //    Rectangle r = new Rectangle();
        //    int widh = x1 - x0;
        //    int height = y3 - y0;
        //    return r;

        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageRoot">hình gốc</param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="saveToFile">đường dẫn đến file  được lưu
        /// Nếu bỏ trông bằng k lưu hình
        /// </param>
        /// <returns>Mat target warp</returns>
        public static Mat GetRectangle(Mat imageRoot, int x0, int y0,
            int x1, int y1, int x2, int y2, int x3, int y3, string saveToFile= null)
        {
            PointF[] srcCorners = new PointF[] {
                new PointF(x0,  y0),
                new PointF(x1,  y1),
                new PointF(x2,  y2),
                new PointF(x3,  y3)
                };
            float width = float.Parse(Math.Sqrt((x1 - x0)* (x1 - x0) + (y1 - y0) * (y1 - y0)).ToString());
            float height = float.Parse(Math.Sqrt((x3 - x0)* (x3 - x0) + (y3 - y0) * (y3 - y0)).ToString());
            PointF[] destCorners = new PointF[] {
                //new PointF(x0,  y0),
                //new PointF(width+x0,  y0),
                // new PointF(width+x0,height+y0),
                // new PointF(x0,height+y0),
                new PointF(width,  0),
                new PointF(width,  height),
                new PointF(0,  height),
                new PointF(0,  0)
                };
            Mat srcImg = imageRoot;
            Mat destImg = new Mat();

            Mat warpMatrix = CvInvoke.GetAffineTransform(srcCorners, destCorners);

            CvInvoke.WarpAffine(srcImg, destImg, warpMatrix, new System.Drawing.Size((int)width, (int)height),
                Inter.Linear, Warp.Default, BorderType.Transparent);
            if(saveToFile != null)
            destImg.Save(saveToFile);
            return destImg;


        }
    }
   
}
