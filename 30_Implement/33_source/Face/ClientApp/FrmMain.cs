using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Globalization;
using System.Speech.AudioFormat;

namespace ClientApp
{
    public partial class FrmMain : Form
    {
        protected readonly HttpClient _client;
        public FrmMain()
        {
            InitializeComponent();
            _client = new HttpClient();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
         //   // string url = "http://localhost:1996/dich-vu/api-diem-danh";
         //   string url = "http://iam.com:500/dich-vu/api-diem-danh";
         //   var result = await PostImage(url, new PostResult()
         //   {
         //       IdLop = 1,
         //       IdTruong = 1,
         //       FileName = @"G:\512.jpg"
         //   });
         //   SpeechSynthesizer synthesizer = new SpeechSynthesizer();
         //   //using (var speaker = new SpeechSynthesizer())
         //   //{
         //   //    var lang = new System.Globalization.CultureInfo("vi-VN");
         //   //    speaker.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Child, 10, lang);
         //   //}


         //   //var lang = new System.Globalization.CultureInfo("vi-VN");
         ////   CultureInfo lang = CultureInfo.GetCultureInfo("vi-VN");
         ////   synthesizer.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Child, 100, lang);
         //   synthesizer.Volume = 100;  // 0...100
         //   synthesizer.Rate = -2;     // -10...10
         //   synthesizer.SpeakAsync(result.Message);


            //using (SpeechSynthesizer synth = new SpeechSynthesizer())
            //{

            //    // Output information about all of the installed voices.   
            //    Console.WriteLine("Installed voices -");
            //    Console.WriteLine("số giọng được cài đặt"+synth.GetInstalledVoices().Count());

            //    foreach (InstalledVoice voice in synth.GetInstalledVoices())
            //    {
            //        VoiceInfo info = voice.VoiceInfo;
            //        string AudioFormats = "";
            //        foreach (SpeechAudioFormatInfo fmt in info.SupportedAudioFormats)
            //        {
            //            AudioFormats += String.Format("{0}\n",
            //            fmt.EncodingFormat.ToString());
            //        }

            //        Console.WriteLine(" Name:          " + info.Name);
            //        Console.WriteLine(" Culture:       " + info.Culture);
            //        Console.WriteLine(" Age:           " + info.Age);
            //        Console.WriteLine(" Gender:        " + info.Gender);
            //        Console.WriteLine(" Description:   " + info.Description);
            //        Console.WriteLine(" ID:            " + info.Id);
            //        Console.WriteLine(" Enabled:       " + voice.Enabled);
            //        if (info.SupportedAudioFormats.Count != 0)
            //        {
            //            Console.WriteLine(" Audio formats: " + AudioFormats);
            //        }
            //        else
            //        {
            //            Console.WriteLine(" No supported audio formats found");
            //        }

            //        string AdditionalInfo = "";
            //        foreach (string key in info.AdditionalInfo.Keys)
            //        {
            //            AdditionalInfo += String.Format("  {0}: {1}\n", key, info.AdditionalInfo[key]);
            //        }

            //        Console.WriteLine(" Additional Info - " + AdditionalInfo);
            //        Console.WriteLine();
            //    }
            //}
        }
       async Task<ExecuteResult> PostImage(string url, PostResult data)
        {
            ExecuteResult executeResult;
            try
            {
                var resourceDocument = JsonConvert.SerializeObject(data);
                using(var form = new MultipartFormDataContent("Upload"))
                {
                    // data
                     var content = new StringContent(resourceDocument, Encoding.UTF8, "application/json");
                    // var content = new StringContent(new JavaScriptSerializer().Serialize(data), Encoding.UTF8, "application/json");
                   form.Add(content, "PostResult");
                    // image
                   
                    form.Add(new StreamContent(new FileStream(data.FileName, FileMode.Open)), "Image", Path.GetFileName(data.FileName));
                    using (var response = await _client.PostAsync(url, form))
                        {
                            var responseDocument = await response.Content.ReadAsStringAsync();
                            executeResult = JsonConvert.DeserializeObject<ExecuteResult>(responseDocument);
                        }
                  
                }
               
                ///
               
            }
            catch (Exception ex)
            {
                executeResult = new ExecuteResult()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }

            return executeResult;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            var lang = new System.Globalization.CultureInfo("vi-VN");
          //  CultureInfo lang = CultureInfo.GetCultureInfo("vi-VN");
            synthesizer.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Child, 100, lang);
            synthesizer.Volume = 100;  // 0...100
            synthesizer.Rate = -2;     // -10...10
            synthesizer.SpeakAsync("Xin chào dũng ");
        }
    }
}
