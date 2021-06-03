using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreadMasterProgram.Backend
{
    class ReceiverSlave
    {
        private static   TcpListener listener = null;

        private string downloadsFolder;
        public static string Message = "Stopped";
        int port;

        public ReceiverSlave(string downloadsFolder, int port)
        {
            this.downloadsFolder = downloadsFolder;
            if (listener == null)
                listener = new TcpListener(IPAddress.Any, port);
        }

        public void Start()
        {
            /// Receiveris kuris priema failo pavadinima kuri apdirbo.
            /// 
            /// Receivers kuris priema slave atlikta darbo rezultata.
            /// 
            /// Receiveeris kuris gauna signala- turiu 5 failus, gali siust kitus 5. (uzteks bool reiksmes);
            /// 
            
            // Priima failo pavadinima
           // byte[] fileNameLengthBytes = new byte[4];
           // stream.Read(fileNameLengthBytes, 0, 4);
                       

        }

        public void Send(string sendString = null)
        {
            if (port == 3000)
            {
                //Jeigu nenusiunčia stringo tiesiog nevykdom nieko
                if (sendString == null)
                {

                }
                else
                {
                    // Nusiunčia stringus su baitais
                    IPAddress receiverIP = IPAddress.Parse("127.0.0.1"); // receiver adresas
                    IPEndPoint endPoint = new IPEndPoint(receiverIP, port);
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    byte[] answerBytes = Encoding.ASCII.GetBytes(sendString);
                    byte[] answerLength = BitConverter.GetBytes(answerBytes.Length);
                    byte[] fullAnswerWithLength = new byte[4 + answerBytes.Length];

                    answerBytes.CopyTo(fullAnswerWithLength, 0);
                    answerLength.CopyTo(fullAnswerWithLength, 4);
                    socket.Connect(endPoint);
                    socket.Send(fullAnswerWithLength);
                    socket.Close();
                }

            }
            else if (port == 4000)
            {

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
