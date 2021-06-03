using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Net;
using System.Net.Sockets;

namespace _02_Networking_Receiver_WPF
{
    class TesseractImpl
    {




        public void TesseractExtraction(List<FileInfo> slaveObjects)
        {

            List<string> pulledFileText = new List<string>();

            foreach (FileInfo file in slaveObjects)
            {

                
                
                var testImagePath = file.FullName;
                /*  if (args.Length > 0)
                  {
                      testImagePath = args[0];
                }*/

                string text = null;

                try
                {
                    var engine = new TesseractEngine(@"D:\Download\ReceiverApp\ReceiverApp\ReceiverApp\TesseractFiles", "eng", EngineMode.Default);

                    var img = Pix.LoadFromFile(testImagePath);

                    var page = engine.Process(img);

                    text = page.GetText();



                }
                catch (Exception e)
                {
                    Trace.TraceError(e.ToString());
                    MessageBox.Show("Unexpected Error: " + e.Message);
                    MessageBox.Show("Details: ");
                    MessageBox.Show(e.ToString());
                }
                // MessageBox.Show(text);
                pulledFileText.Add(text);
                string filename = file.Name;
                UpdateMaster(filename);
                File.Delete(testImagePath);                
               //Senderis siuncia kad extraktino texta is failo {file}



            }




            OutputText(pulledFileText);

            // MessageBox.Show($"how many times run :{Form1.totalProgress}");
            // senderis kuris siuncia visa musu stringa;
            // pulledFileText;
        }

        private void UpdateMaster(string name)
        {
           
                IPAddress receiverIP = IPAddress.Parse("127.0.0.1");            
                IPEndPoint endPoint = new IPEndPoint(receiverIP, 2123);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
               // byte[] fileNameLength = BitConverter.GetBytes(name.Length);
              //  byte fileNameLenghtBytes 
                socket.Connect(endPoint);
                socket.Send(Encoding.ASCII.GetBytes(name));
               /* byte[] bbz = Encoding.ASCII.GetBytes(singleFileText);
                string bbz2 = Encoding.ASCII.GetString(bbz);*/
                socket.Close();            

        }

        public void OutputText(List<string> text)
        {



            IPAddress receiverIP = IPAddress.Parse("127.0.0.1");
            /*IPEndPoint endPoint = new IPEndPoint(receiverIP, 2022);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);*/

          //  string fulltext = " ";

            foreach (string singleFileText in text)
            {
                IPEndPoint endPoint = new IPEndPoint(receiverIP, 2022);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(endPoint);
                socket.Send(Encoding.ASCII.GetBytes(singleFileText));
                /*byte[] bbz = Encoding.ASCII.GetBytes(singleFileText);
                string bbz2 = Encoding.ASCII.GetString(bbz);*/
                socket.Close();
            }
            

            /*  var path = @"F:\MULTITHREAD4TESTSPACE\OutputDirectory\outputFile.txt";
              File.Create(path).Dispose();

              foreach (string singleFileText in text)
              {
                  File.AppendAllText(path, singleFileText);
                  File.AppendAllText(path, Environment.NewLine);

              }*/

        }




    }
}
