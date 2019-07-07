using ClientApp.Commons;
using Emgu.CV;
using Emgu.CV.Structure;
using log4net;
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

namespace ClientApp.FormTests
{
    public partial class Crop_Test : Form
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(Crop_Test).Name);
        string _dir= Commons.FileAndFolder.GetRunningPath();
        VideoCapture cam;
        private bool captureInProgress;
        public Crop_Test()
        {
            InitializeComponent();
            
        }

        private void Crop_Test_Load(object sender, EventArgs e)
        {
          
        }
        //void Test()
        //{
        //    _logger.Debug("-----load start----------");

           
        //    string modelFiles = _dir;
            
        //    API.Load_Model(Path.Combine(_dir, "lib", "pcn_model", "PCN-1.prototxt"), Path.Combine(_dir, "lib", "pcn_model", "PCN-2.prototxt"),
        //        Path.Combine(_dir, "lib", "pcn_model", "PCN-3.prototxt"), Path.Combine(_dir, "lib", "pcn_model", "PCN.caffemodel"));

        //    //List<string> models = Commons.FileAndFolder.GetFiles(@"F:\lib\pcn_model", "prototxt");
        //    //foreach (var item in models)
        //    //{
        //    //    Emgu.CV.Dnn.DnnInvoke.ShrinkCaffeModel("PCN-1.prototxt", "PCN.caffemodel");
        //    //}
            
        //    //Handler.Native.FaceKit();
        //    Commons.FileAndFolder.CreateFolderIfNotExist(Path.Combine(_dir, "OUT"));
        //    var listFace = API.FaceKit(Path.Combine(_dir, "lib", "image", "OLD.jpg"), Path.Combine(_dir, "OUT"));


        //    //   API.FaceKit(Path.Combine(_dir, "lib", "image", "OLD.jpg"), Path.Combine(_dir, "OUT"));
        //    //Image<Bgr, byte> image = new Image<Bgr, byte>(Path.Combine(_dir, "lib", "image", "OLD.jpg"));
        //    //var listFace = API.GetFace(image, 30);
        //    //Debug.WriteLine("Có {0} hình trong bức ảnh", listFace.Count());

           
        //}
        //List<Mat> deteciton(Image<Bgr, byte> frame, string outFolderFace = null, int maxFace = 30)
        //{
        //    API.Load_Model(Path.Combine(_dir, "lib", "pcn_model", "PCN-1.prototxt"), Path.Combine(_dir, "lib", "pcn_model", "PCN-2.prototxt"),
        //       Path.Combine(_dir, "lib", "pcn_model", "PCN-3.prototxt"), Path.Combine(_dir, "lib", "pcn_model", "PCN.caffemodel"));
        //    Commons.FileAndFolder.CreateFolderIfNotExist(Path.Combine(_dir, "target"));
        //    var listFace = API.GetFace(frame,outFolderFace, maxFace);
        //    return listFace;
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            //var list = deteciton(new Image<Bgr, byte>(Path.Combine(_dir, "lib", "image", "OLD.jpg")), Path.Combine(_dir, "OUT"));
            //Debug.WriteLine(list.Where(o => o.IsEmpty != true).Count());
            cam = new VideoCapture();
            Mat m = new Mat();
            cam.ImageGrabbed += ProcessFrame;
            if (cam != null)
            {
                try
                {
                    cam.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            if (cam != null && cam.Ptr != IntPtr.Zero)
            {
                Mat _frame = new Mat();
                cam.Retrieve(_frame, 0);
                pcCam.Image = _frame.ToImage<Bgr, byte>().Resize(pcCam.Width, pcCam.Height,Emgu.CV.CvEnum.Inter.Area).Bitmap;

                //var list = deteciton(_frame.ToImage<Bgr, byte>(), Path.Combine(_dir, "OUT"));
                //Debug.WriteLine(list.Where(o=>o.IsEmpty!= true).Count());
            }
        }
    }
}
