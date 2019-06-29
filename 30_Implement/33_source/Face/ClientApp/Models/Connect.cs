using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Models
{
   public class Connect
    {
        public string ConnectString { get; set; }
        public int Channel { get; set; }
        public string Ip { get; set; }
        public int Subtype { get; set; }
        public string UserName { get; set; }
        public string Pass { get; set; }
        public Connect(string userName, string pass, string iP)
        {
            this.UserName = userName;
            this.Pass = pass;
            this.Ip = iP;
           
        }
        public Connect(string userName, string pass, string iP,int channel,int subtype)
        {
            this.UserName = userName;
            this.Pass = pass;
            this.Channel = channel;
            this.Ip = iP;
            this.Subtype = subtype;
           this.ConnectString= "rtsp://"+UserName+":"+Pass+"@"+Ip+":37777/cam/realmonitor?channel="+ Channel + "&subtype="+ Subtype;
        }
        public string ConnectTCP(int channel, int subtype )
        {
            return "rtsp://" + UserName + ":" + Pass + "@" + Ip + ":37777/cam/realmonitor?channel=" + channel + "&subtype=" + subtype;
        }
        public string ConnectRTSP(int channel, int subtype)
        {
            return "rtsp://" + UserName + ":" + Pass + "@" + Ip + ":554/cam/realmonitor?channel=" + channel + "&subtype=" + subtype;
        }
        public string ConnectUDP { get { return ConnectString.Replace("37777", "37778"); } }
    }
}
