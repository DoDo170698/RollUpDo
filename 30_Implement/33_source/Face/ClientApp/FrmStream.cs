using ClientApp.Models;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class FrmStream : Form
    {
        VideoCapture cam1;
        VideoCapture cam2;
        VideoCapture cam3;
        VideoCapture cam4;
        public FrmStream()
        {
            InitializeComponent();

        }

        private void FrmStream_Load(object sender, EventArgs e)
        {
              Init();
            InitTT();
        }
        void Init()
        {
            var con = new Connect("ChamCong", "dung1996", "192.168.1.7");
            cam1 = new VideoCapture(con.ConnectRTSP(1, 0));
            cam2 = new VideoCapture(con.ConnectRTSP(2, 0));
            cam3 = new VideoCapture(con.ConnectRTSP(3, 0));
            cam4 = new VideoCapture(con.ConnectRTSP(4, 0));

            Application.Idle += ProcessFrame;
        }


       
        private void ProcessFrame(object sender, EventArgs arg)
        {
            pic1.Image = new Bitmap(cam1.QueryFrame().Bitmap, new Size(pic1.Width, pic1.Height));
            pic2.Image = new Bitmap(cam2.QueryFrame().Bitmap, new Size(pic1.Width, pic1.Height));
            pic3.Image = new Bitmap(cam3.QueryFrame().Bitmap, new Size(pic1.Width, pic1.Height));
            pic4.Image = new Bitmap(cam4.QueryFrame().Bitmap, new Size(pic1.Width, pic1.Height));
        }
        void InitTT()
        {
            List<TrangThai> list = new List<TrangThai>();
            list.Add(new TrangThai() {Home="Xưởng Mộc", Status = "Cam 1,3-Waring" ,Time = "Ngoài giờ làm việc" });
            list.Add(new TrangThai() {Home="Kho hàng",Status = "OK" ,Time = "Ngoài giờ làm việc" });
            list.Add(new TrangThai() {Home="Xưởng May",Status = "OK" ,Time = "Ngoài giờ làm việc" });
            list.Add(new TrangThai() {Home="Nhà", Status = "OK" ,Time = "Ngoài giờ làm việc",CountPeople=3 });
            list.Add(new TrangThai() {Home="QN Olimpic Vip", Status = "OK" ,Time = "Đang giao ca",CountPeople=20 });
            list.Add(new TrangThai() {Home="QN Olimpic", Status = "OK" ,Time = "Đang giao ca",CountPeople=38 });
            list.Add(new TrangThai() {Home="QN Quê", Status = "OK" ,Time = "User-Minh",CountPeople=26 });
            gridDacnhSach.DataSource = list;
           
            
        }

    }
}
