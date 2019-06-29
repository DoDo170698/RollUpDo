using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace TestCMNDCsharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //const String weights = @"D:\DOAN\WORK\RnD Team\TrungTQ\TestCMNDCsharp\trunk\model\frozen_inference_graph.pb";
            //const String prototxt = @"D:\DOAN\WORK\RnD Team\TrungTQ\TestCMNDCsharp\trunk\model\graph.pbtxt";
            //const String weights = @"model\frozen_inference_graph.pb";
            //const String prototxt = @"model\graph.pbtxt";
            //Api.LoadCropCmndDNNData(weights, prototxt);

            //const string weights = @"model\frozen_east_text_detection.pb";
            //Api.Load_PB_Model_test(weights);
            //const string model = @"model\snapshot-12.meta,model\snapshot-12";// mấy acsi tên file này đáng ngờ đấyem quay lại C++ để in file 
            const string model = @"model\facemodel.pb";
            //TestLineSec();
            //14-3-2019
            //const string weights = @"model\output_graph.pb";
            //if (Api.Load_PB_Model_HandWriting_test(weights)==false)
            //    Console.WriteLine("can not load the pb file");

            //    TestHTR//
            if (Api.LoadHTR(model, 1) == false)// stop, sai mấy chỗ đấy
                Console.WriteLine("can not load thefile");
            else
            {
                Console.WriteLine("ok load thefile");
            }

            //TestReg();
            //TestTheard();
            TestFace();
            Console.WriteLine("DONE ALL!!!");
            //Console.ReadLine();
            Console.ReadKey();
        }

        //private static void TestFace()
        //{
        //    throw new NotImplementedException();
        //}

        static void TestCropCMND()
        {
            string[] cmndFiles = new string[] {
                @"Test\Removed.jpg",
            };
            string debugDir = AppDomain.CurrentDomain.BaseDirectory;

            //string cmndDir = @"D:\DOAN\WORK\OCR\Development64\DEV_DATA\OCR\CMND\11-12-2018\CMND-xoay\CMND-xoay-90\CMND-mattruoc";

            //string debugDir = Path.GetFileNameWithoutExtension(cmndDir);
            //if (!Directory.Exists(debugDir)) Directory.CreateDirectory(debugDir);

            //Console.WriteLine("Processing cmnd: {0}", cmndDir);

            //string[] cmndFiles = Directory.GetFiles(cmndDir, "*.jp*g");

            foreach (string cmndFile in cmndFiles)
            {

                Stopwatch sw = Stopwatch.StartNew();

                Bitmap croppedBitmap = Api.CropCmndDNN(cmndFile);

                sw.Stop();

                string fileNamePrefix = Path.GetFileNameWithoutExtension(cmndFile);
                croppedBitmap.Save(Path.Combine(debugDir, string.Format("{0}_Crop.jpg", fileNamePrefix)));

                Console.WriteLine("+ {0} in {1} ms: ", fileNamePrefix, sw.ElapsedMilliseconds);
            }
        }

        static void TestLineSec()
        {
            string[] cmndFiles = new string[] {
                @"Test\para2.jpg",
            };
            string debugDir = AppDomain.CurrentDomain.BaseDirectory;

            foreach (string cmndFile in cmndFiles)
            {

                Stopwatch sw = Stopwatch.StartNew();

                Api.LineSegmentationHW_test(cmndFile);

                sw.Stop();

                Console.WriteLine("+ DONE in {0} ms: ", sw.ElapsedMilliseconds);
            }
        }

        //14-3-2019
        static void TestHTR()
        {
            string[] cmndFiles = new string[] {
                @"Test\Removed.jpg",
            };
            string debugDir = AppDomain.CurrentDomain.BaseDirectory;

            foreach (string cmndFile in cmndFiles)
            {

                //Stopwatch sw = Stopwatch.StartNew();

                Api.LineSegmentationHW_test(cmndFile);

                //sw.Stop();

                //Console.WriteLine("+ DONE in {0} ms: ", sw.ElapsedMilliseconds);
            }

        }

        static void TestReg()
        {
            string[] cmndFiles = new string[] {
                @"Test\1.png",
            };
            string charFile = @"model\charList.txt";
            string debugDir = AppDomain.CurrentDomain.BaseDirectory;
            foreach (string cmndFile in cmndFiles)
            {

                Stopwatch sw = Stopwatch.StartNew();

                Api.RecognizeHW_IAM(cmndFile, charFile);

                sw.Stop();

                Console.WriteLine("+ DONE in {0} ms: ", sw.ElapsedMilliseconds);
            }
        }

        
        static void TestTheard()
        {
            string charFile = @"model\charList.txt";
            var t1 = Task.Factory.StartNew(() =>
            {
                string[] cmndFiles = new string[] {
                @"Test\1.png",
            };
                
                string debugDir = AppDomain.CurrentDomain.BaseDirectory;
                foreach (string cmndFile in cmndFiles)
                {

                    Stopwatch sw = Stopwatch.StartNew();
                    for (int loop = 0; loop < 3; loop++)
                    {
                        Api.RecognizeHW_IAM(cmndFile, charFile);

                    sw.Stop();

                    Console.WriteLine("+ DONE in {0} ms: ", sw.ElapsedMilliseconds);
                    }//for
                }
            });
            var t2 = Task.Factory.StartNew(() =>
            {
                string[] cmndFiles = new string[] {
                @"Test\2.png",
            };
               
                string debugDir = AppDomain.CurrentDomain.BaseDirectory;
                foreach (string cmndFile in cmndFiles)
                {
                    for (int loop = 0; loop < 3; loop++)
                    { 
                        Stopwatch sw = Stopwatch.StartNew();

                    Api.RecognizeHW_IAM(cmndFile, charFile);

                    sw.Stop();

                    Console.WriteLine("+ DONE in {0} ms: ", sw.ElapsedMilliseconds);
                    }//for
                }
            });
            var t3 = Task.Factory.StartNew(() =>
            {
                string[] cmndFiles = new string[] {
                @"Test\3.png",
            };
                
                string debugDir = AppDomain.CurrentDomain.BaseDirectory;
                foreach (string cmndFile in cmndFiles)
                {

                    Stopwatch sw = Stopwatch.StartNew();

                    Api.RecognizeHW_IAM(cmndFile, charFile);

                    sw.Stop();

                    Console.WriteLine("+ DONE in {0} ms: ", sw.ElapsedMilliseconds);
                }
            });
            var t4 = Task.Factory.StartNew(() =>
            {
                string[] cmndFiles = new string[] {
                @"Test\4.png",
            };
                
                string debugDir = AppDomain.CurrentDomain.BaseDirectory;
                foreach (string cmndFile in cmndFiles)
                {

                    Stopwatch sw = Stopwatch.StartNew();

                    Api.RecognizeHW_IAM(cmndFile, charFile);

                    sw.Stop();

                    Console.WriteLine("+ DONE in {0} ms: ", sw.ElapsedMilliseconds);
                }
            });
            var allTasks = new Task[] { t1, t2,t3,t4 };
            Task.WaitAll(allTasks);
        }
        static void TestFace()
        {
            string[] cmndFiles = new string[] {
                @"Test\a.jpg",
            };
            //string charFile = @"model\charList.txt";
            string debugDir = AppDomain.CurrentDomain.BaseDirectory;
            foreach (string cmndFile in cmndFiles)
            {

                Stopwatch sw = Stopwatch.StartNew();

                Api.Recognize_Face(cmndFile);

                sw.Stop();

                Console.WriteLine("+ DONE in {0} ms: ", sw.ElapsedMilliseconds);
            }
        }
    }
    

}
