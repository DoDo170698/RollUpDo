using ClientApp.Handler;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        List<float[]> faceOne = new List<float[]>();
        List<float[]> faceTow = new List<float[]>();
        public string Path1 { get; set; }
        public string Path2 { get; set; }
        
        public frmDetect()
        {
            InitializeComponent();
        }
        bool check21()
        {
          return  (faceTow.Count() - faceOne.Count()) > 0 ? true : false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //  2 > 1
           
            List<string> listDoLech = new List<string>();
            if (check21())
            {
                for (int i = 0; i < faceOne.Count(); i++)
                {
                    Matrix<float> mtLech = new Matrix<float>(faceOne[i]) - new Matrix<float>(faceTow[i]);
                    double do_lech=0;
                    for (int di = 0;di < mtLech.Height; di++)
                    {
                        for (int dj = 0; dj < mtLech.Width; dj++)
                        {
                            do_lech += mtLech.Data[di, dj] * mtLech.Data[di, dj];
                        }
                    }
                    listDoLech.Add(Math.Sqrt(do_lech).ToString());
                }
            }
            toolDebug.Text = "2 hinh: " + String.Join(";", listDoLech);
            MessageBox.Show("2 hinh: "+ String.Join(";", listDoLech));
        }
        List<Mat> deteciton(Image<Bgr, byte> frame, string outFolderFace = null, int maxFace = 30)
        {
            string a = Path.Combine(_dir, "lib", "pcn_model", "PCN-1.prototxt");
            API.ModelDetect(Path.Combine(_dir, "lib", "pcn_model", "PCN-1.prototxt"), Path.Combine(_dir, "lib", "pcn_model", "PCN-2.prototxt"),
               Path.Combine(_dir, "lib", "pcn_model", "PCN-3.prototxt"), Path.Combine(_dir, "lib", "pcn_model", "PCN.caffemodel"));
             Commons.FileAndFolder.CreateFolderIfNotExist(Path.Combine(_dir, "target"));

            // return API.GetFace(frame, outFolderFace, maxFace);
            var x = API.FaceKit(frame, outFolderFace, maxFace);
            return x ;
        }
        List<Mat> deteciton(string frame, string outFolderFace = null, int maxFace = 30)
        {
            string a = Path.Combine(_dir, "lib", "pcn_model", "PCN-1.prototxt");
            API.ModelDetect(Path.Combine(_dir, "lib", "pcn_model", "PCN-1.prototxt"), Path.Combine(_dir, "lib", "pcn_model", "PCN-2.prototxt"),
               Path.Combine(_dir, "lib", "pcn_model", "PCN-3.prototxt"), Path.Combine(_dir, "lib", "pcn_model", "PCN.caffemodel"));
            Commons.FileAndFolder.CreateFolderIfNotExist(Path.Combine(_dir, "target"));

            // return API.GetFace(frame, outFolderFace, maxFace);
            var x = API.FaceKit(frame, outFolderFace, maxFace);
            return x;
        }
        List<float[]> target(List<Mat> mats)
        {
            List<float[]> result = new List<float[]>();
            foreach (var item in mats)
            {
                result.Add(API.FaceNet(item));
            }
            return result;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "image file|*.BMP;*.jog;*.jpge;*.PNG;*.png| all file| *.*";
            if (op.ShowDialog() == DialogResult.OK && string.IsNullOrEmpty(op.FileName)!= true)
            {
               Path1 =  op.FileName;
                pictureBox1.Image = new Image<Bgr,byte>(Path1).Resize(pictureBox1.Width, pictureBox1.Height, Emgu.CV.CvEnum.Inter.Cubic).Bitmap;
               // var list = deteciton(new Image<Bgr, byte>(Path1));
                var list = deteciton( (Path1));
               faceOne= target(list);
                toolDebug.Text = faceOne.Count().ToString();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "image file|*.BMP;*.jog;*.jpge;*.PNG;*.png| all file| *.*";
            if (op.ShowDialog() == DialogResult.OK && string.IsNullOrEmpty(op.FileName) != true)
            {
                Path2 = op.FileName;
                pictureBox2.Image = new Image<Bgr, byte>(Path2).Resize(pictureBox2.Width, pictureBox2.Height, Emgu.CV.CvEnum.Inter.Cubic).Bitmap;
               // var list = deteciton(new Image<Bgr, byte>(Path2));
                var list = deteciton((Path2));
                faceTow = target(list);
                toolDebug.Text = faceTow.Count().ToString();
            }

        }

    }
}
