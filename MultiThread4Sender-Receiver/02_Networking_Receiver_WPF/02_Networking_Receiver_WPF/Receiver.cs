using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace _02_Networking_Receiver_WPF
{
    class Receiver
    {
        private static TcpListener listener = null;

        private string downloadsFolder;
        public static string Message = "Stopped";
        List<FileInfo> files = new List<FileInfo>();
        int filecount = 0;

        public Receiver(string downloadsFolder)
        {
            this.downloadsFolder = downloadsFolder;
            if (listener == null)
                listener = new TcpListener(IPAddress.Any, 2021);
        }

        public void Start()
        {
            TesseractImpl tesseractImpl = new TesseractImpl();
            try
            {
                listener.Start();
                Message = "Started";
                while (true)
                {
                    using (var client = listener.AcceptTcpClient())
                    using (var stream = client.GetStream())
                    {
                        byte[] fileNameLengthBytes = new byte[4];
                        stream.Read(fileNameLengthBytes, 0, 4);
                        int fileNameLength = BitConverter.ToInt32(fileNameLengthBytes, 0);
                        byte[] fileNameBytes = new byte[fileNameLength];
                        stream.Read(fileNameBytes, 0, fileNameLength);
                        string fileName = Encoding.ASCII.GetString(fileNameBytes, 0, fileNameLength);

                        string file = $"{downloadsFolder}{fileName}";
                        

                        using (var output = File.Create(file))
                        {
                            Message = "Saving file...";
                            var buffer = new byte[1024];
                            int bytesRead;
                            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                // progresui ideali vieta
                                output.Write(buffer, 0, bytesRead);
                            }
                        }
                        files.Add(new FileInfo(file));
                        filecount++;
                        if(filecount== 5)
                        {
                            //Tesseract launch.
                            List<FileInfo> fileInfos = files.ToList();
                             ThreadPool.QueueUserWorkItem(s =>  tesseractImpl.TesseractExtraction(fileInfos));                           
                            filecount = 0;
                            files.Clear();  
                            
                        }
                        Message = "Saving file complete";
                    }
                }
            }
            catch (Exception exc)
            {
                Message = exc.Message;
            }
            finally
            {
                if (listener != null)
                    listener.Stop();
                Message = "Stopped";
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
