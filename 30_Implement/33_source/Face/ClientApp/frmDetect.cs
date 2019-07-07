using DLLHelper;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class frmDetect : Form
    {
        string _dir = Commons.FileAndFolder.GetRunningPath();
        float[] face1 = new float[128];
        float[] face2 = new float[128];
        public string Path1 { get; set; }
        public string Path2 { get; set; }

        public frmDetect()
        {
            InitializeComponent();
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
           var a = DoLech(face1, face2);
            
            MessageBox.Show(a>=0.7?"khác nhau": "Giống nhau");
        }
        double DoLech(float[] _faceOne, float[] _faceTow)
        {
            Matrix<float> mtLech = new Matrix<float>(_faceOne) - new Matrix<float>(_faceTow);
            Debug.WriteLine(string.Format("ma trận chiều cao là : {0}", mtLech.Height));
            double do_lech = 0;
            for (int di = 0; di < mtLech.Height; di++)
            {
                for (int dj = 0; dj < mtLech.Width; dj++)
                {
                    do_lech += mtLech.Data[di, dj] * mtLech.Data[di, dj];
                }
            }
            return Math.Sqrt(do_lech);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "image file|*.BMP;*.jpg;*.jpge;*.PNG;*.png;*.JPEG| all file| *.*";
            if (op.ShowDialog() == DialogResult.OK && string.IsNullOrEmpty(op.FileName) != true)
            {
                var img = new Image<Bgr, byte>(op.FileName);
                // pictureBox1.Image = img.Resize(pictureBox1.Width, pictureBox1.Height, Emgu.CV.CvEnum.Inter.Cubic).Bitmap;
                var f = Detect(img, false);
                pictureBox1.Image = f.FirstOrDefault().ToImage<Bgr, byte>().Resize(pictureBox1.Width, pictureBox1.Height, Emgu.CV.CvEnum.Inter.Cubic).Bitmap;
                var DacTrung = FaceNet(img);
                face1 = DacTrung.FirstOrDefault();

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "image file|*.BMP;*.jpg;*.jpge;*.PNG;*.png;*.JPEG| all file| *.*";
            if (op.ShowDialog() == DialogResult.OK && string.IsNullOrEmpty(op.FileName) != true)
            {
                Stopwatch st = new Stopwatch();
                st.Start();
                var img = new Image<Bgr, byte>(op.FileName);
                // pictureBox2.Image = img.Resize(pictureBox2.Width, pictureBox2.Height, Emgu.CV.CvEnum.Inter.Cubic).Bitmap;
                var f = Detect(img, false);
                pictureBox2.Image = f.FirstOrDefault().ToImage<Bgr, byte>().Resize(pictureBox2.Width, pictureBox2.Height, Emgu.CV.CvEnum.Inter.Cubic).Bitmap;
                var DacTrung = FaceNet(img);
                face2 = DacTrung.FirstOrDefault();
                st.Stop();
                MessageBox.Show(string.Format("Tốn {0} để lấy ra đặc trưng của một hình", st.ElapsedMilliseconds));
            }

        }
        List<Mat> Detect(Image<Bgr, byte> img, bool xoay = true)
        {
            string _dir = FileAndFolder.GetRunningPath();
            string modelDetect = Path.Combine(_dir, "lib", "pcn_model");
            var api = new APIGetFace(
                Path.Combine(modelDetect, "PCN-1.prototxt"),
                Path.Combine(modelDetect, "PCN-2.prototxt"),
                Path.Combine(modelDetect, "PCN-3.prototxt"),
                Path.Combine(modelDetect, "PCN.caffemodel")
                );
            //  var list = API.Recognize_FaceKit(img);
            var list = api.GetFace(img, 30, xoay);
            return list;
        }
        List<float[]> FaceNet(Image<Bgr, byte> imgRoot)
        {
            var result = new List<float[]>();
            string _dir = FileAndFolder.GetRunningPath();
            string modelFace = Path.Combine(_dir, "lib", "FaceModel", "facemodel.pb");
            string modelTensor = Path.Combine(_dir, "lib", "FaceModel", "facetesor.dll");
            var api = new APIRecognizeFace(modelFace, modelTensor);
            var facesInRoot = Detect(imgRoot);
            foreach (var item in facesInRoot)
            {
                var ar = api.FaceNet(item);
                result.Add(ar);
            }
            return result;
        }
    }
}
