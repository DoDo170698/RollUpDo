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

namespace DLLHelper
{
    public partial class Form1 : Form
    {
        Image<Bgr, byte> image1;
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "image file|*.BMP;*.jpg;*.jpge;*.PNG;*.png| all file| *.*";
            if (op.ShowDialog() == DialogResult.OK && string.IsNullOrEmpty(op.FileName) != true)
            {
                image1 = new Image<Bgr, byte>(op.FileName);
                pictureBox1.Image = image1.Resize(pictureBox1.Width, pictureBox1.Height, Emgu.CV.CvEnum.Inter.Cubic).Bitmap;
                //  Debug.WriteLine(string.Format("Có {0} face.", Run(image1).Count()));
                var listNet = FaceNet(image1);
                foreach (var item in listNet)
                {
                    MessageBox.Show(string.Join("\n",item));
                }

            }
        }
        List<Mat> Run(Image<Bgr, byte> img)
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
            var list = api.GetFace(img);
            return list;
        }
        List<float[]> FaceNet(Image<Bgr, byte> imgRoot)
        {
            var result = new List<float[]>();
            string _dir = FileAndFolder.GetRunningPath();
            string modelFace = Path.Combine(_dir, "lib", "FaceModel", "facemodel.pb");
            string modelTensor = Path.Combine(_dir, "lib", "FaceModel", "facetesor.dll");
            var api = new APIRecognizeFace(modelFace, modelTensor);
            var facesInRoot = Run(imgRoot);
            foreach (var item in facesInRoot)
            {
                var ar = api.FaceNet(item);
                result.Add(ar);
            }
            return result;
        }
        static void TestFaceKit()
        {
            string[] cmndFiles = new string[] {
                @"Test\0.jpg",
            };
            //string charFile = @"model\charList.txt";
            string debugDir = AppDomain.CurrentDomain.BaseDirectory;
            foreach (string cmndFile in cmndFiles)
            {

                Stopwatch sw = Stopwatch.StartNew();

              //  API.Recognize_FaceKit(cmndFile);

                sw.Stop();

                Console.WriteLine("+ DONE in {0} ms: ", sw.ElapsedMilliseconds);
            }
        }
    }
}
