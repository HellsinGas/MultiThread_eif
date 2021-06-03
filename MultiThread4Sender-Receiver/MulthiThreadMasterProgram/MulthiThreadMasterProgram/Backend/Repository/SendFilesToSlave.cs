using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MulthiThreadMasterProgram.Backend.Models;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace MulthiThreadMasterProgram.Backend.Repository
{
    class SendFilesToSlave
    {

               
        
            public void SendFiles(SlaveItems slaveItems)
            {
                foreach (FileInfo fileInfo in slaveItems.filesForSlave)
                {
                    IPAddress receiverIP = IPAddress.Parse("127.0.0.1"); // receiver adresas
                    IPEndPoint endPoint = new IPEndPoint(receiverIP, 2021);
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    byte[] fileNameBytes = Encoding.ASCII.GetBytes(Path.GetFileName(fileInfo.FullName));
                    byte[] fileNameLength = BitConverter.GetBytes(Path.GetFileName(fileInfo.FullName).Length);
                    byte[] fullBuffer = new byte[4 + fileNameBytes.Length];

                    fileNameLength.CopyTo(fullBuffer, 0);
                    fileNameBytes.CopyTo(fullBuffer, 4);

                    socket.Connect(endPoint);
                    socket.SendFile(fileInfo.FullName, fullBuffer, null, TransmitFileOptions.UseDefaultWorkerThread);
                    socket.Close();
                }

            }

        
    }


}
