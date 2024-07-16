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
        public bool IsConnected => clientSocket == null ? false : clientSocket.Connected;

        public Client()
        {

        }

        public void Connect(string serverIP, int serverPort)
        {
            try
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                clientSocket.Connect(serverIP, serverPort);
                Console.WriteLine($"已连接到服务器 {serverIP}:{serverPort}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("连接服务器错误: " + ex.Message);
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
                Console.WriteLine("发送数据错误: " + ex.Message);
            }
        }

        public string ReceiveFile(byte[] buffer, string savePath, string fileName)
        {
            string result = string.Empty;
            int receivedBytes = clientSocket.Receive(buffer);

            string dataReceived = Encoding.UTF8.GetString(buffer, 0, receivedBytes);

            if(!string.IsNullOrEmpty(dataReceived))
            {
                // 將指定文件存到指定路徑
                string filePath = Path.Combine(savePath, fileName);
                File.WriteAllBytes(filePath, buffer.ToArray());

                result = "Successful";
            }
            else
            {
                result = "Error";
            }
            
            return result;
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
