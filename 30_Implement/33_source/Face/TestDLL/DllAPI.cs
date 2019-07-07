using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLLHelper
{
    public class APIGetFace
    {
        public string PCNModel1 { get; set; }
        public string PCNModel2 { get; set; }
        public string PCNModel3 { get; set; }
        public string CafeModel { get; set; }
        public APIGetFace(string one, string tow, string thr, string fo) {
            PCNModel1 = one;
            PCNModel2 = tow;
            PCNModel3 = thr;
            CafeModel = fo;
        }
        public List<Mat> GetFace(Image<Bgr, byte> ImageRoot, int maxFace = 30, bool warpTarget= true)
        {
            Debug.WriteLine(API.Load_FaceKit(PCNModel1, PCNModel2, PCNModel3, CafeModel) == true?"Đã load model get face" : "Không load dc model");
            var x = API.Recognize_FaceKit(ImageRoot, maxFace, warpTarget);
            Debug.WriteLine(string.Format("Có {0} khuôn mặt trong hình", x));
            return x;
        }
    }
    public class APIRecognizeFace
    {
        public string Model { get; set; }
        public string ModelTesor { get; set; }
        public APIRecognizeFace(string one, string tow) {
            Model = one;
            ModelTesor = tow;
        }
       
        public float[] FaceNet(Image<Bgr, byte> mat)
        {
            Debug.WriteLine(API.LoadHTR(Model, ModelTesor, 1) == true ? "Đã load model face net" : "Không load dc model");
            return API.FaceNet(mat);
        }
        public float[] FaceNet(Mat mat)
        {
            Debug.WriteLine(API.LoadHTR(Model, ModelTesor, 1) == true ? "Đã load model face net" : "Không load dc model");
            return API.FaceNet(mat.ToImage<Bgr, byte>());
        }
    }
}
