using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MulthiThreadMasterProgram;

namespace MultiThreadMasterProgram.Backend.Receiver
{
    class Receiver
    {
        private static TcpListener listener = null;
        private static TcpListener listenerUpdates = null;

        private string downloadsFolder;
        public static string Message = "Stopped";
        int port;
        List<string> answerList = new List<string>();

        /* public Receiver(string downloadsFolder, int port)
         {
             this.downloadsFolder = downloadsFolder;
             if (listener == null)
                 listener = new TcpListener(IPAddress.Any, port);
         }*/

        public void StarSlaveProgressUpdateReceiver()
        {
            listenerUpdates = new TcpListener(IPAddress.Any, 2123);
            listenerUpdates.Start();
            bool run = true;
            while (run)
            {
                try
                {
                    using (var client = listenerUpdates.AcceptTcpClient())
                    using (var stream = client.GetStream())
                    {

                        byte[] AnswerLengthBytes = new byte[1024];                        
                        stream.Read(AnswerLengthBytes, 0, 1024);

                        string Answer = Encoding.ASCII.GetString(AnswerLengthBytes);
                        Message = Answer;
                        Form1.processedFiles.Add(Message);
                        Form1.updateProcessed = true;

                    }
                    
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }


        }



        public void StartSlaveOutputReceiver()
        {
            listener = new TcpListener(IPAddress.Any, 2022);
            listener.Start();
            bool run = true;
            while (run) {
                try
                {
                    /// 
                    /// Receiveris kuris priema failo pavadinima kuri apdirbo.
                    /// 
                    /// Receivers kuris priema slave atlikta darbo rezultata.
                    /// 
                    /// Receiveeris kuris gauna signala- turiu 5 failus, gali siust kitus 5. (uzteks bool reiksmes);
                    /// 





                    using (var client = listener.AcceptTcpClient())
                    using (var stream = client.GetStream())
                    {

                        byte[] AnswerLengthBytes = new byte[4096];
                        stream.Read(AnswerLengthBytes, 0, 4096);
                        string Answer = Encoding.ASCII.GetString(AnswerLengthBytes);
                        Message = Answer;
                        answerList.Add(Answer);



                    }


                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }


            }

        }

        public void Stop()
        {
            listener.Stop();
            listener = null;
            Message = "Stopped";
        }

    } 
}
