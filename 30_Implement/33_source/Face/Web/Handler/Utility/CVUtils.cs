using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace SegmentText
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

        public static void DrawRect(System.Drawing.Bitmap sourceImage, string save_image, params Rectangle[] areas)
        {
            Image<Bgr, byte> cvimage = new Image<Bgr, byte>(sourceImage);

            foreach (Rectangle area in areas)
            {
                cvimage.Draw(area, new Bgr(0, 0, 255), 1);
            }

            cvimage.ToBitmap().Save(save_image, ImageFormat.Jpeg);
        }
    }
}
