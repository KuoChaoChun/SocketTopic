using SocketTopic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SocketTopic.Services
{
    public class Client : IClient
    {
        private Socket clientSocket;
        public Client()
        {

        }

        public void Connect(string serverIP, int serverPort)
        {
            try
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverIP, serverPort);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}" + ex.Message);
            }
        }

        public void Disconnect()
        {
            clientSocket.Close();
            clientSocket = null;
        }

        public void Send(string message)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                clientSocket.Send(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}" + ex.Message);
            }
        }

        public void ReceiveFile(byte[] buffer, string fileName, string savePath)
        {
            // 將指定文件存到指定路徑
            string filePath = Path.Combine(savePath, fileName);
            File.WriteAllBytes(filePath, buffer.ToArray());
        }

        public IAsyncResult BeginReceive(byte[] buffer, int offset, int size, SocketFlags socketFlags, AsyncCallback callback, object state)
        {
            return clientSocket.BeginReceive(buffer, offset, size, socketFlags, callback, state);
        }

        public int EndReceive(IAsyncResult asyncResult)
        {
            return clientSocket.EndReceive(asyncResult);
        }
    }
}
