using SocketTopic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketTopic.Services
{
    public class Client : IClient
    {
        private Socket clientSocket;

        public Client()
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect(string serverIP, int serverPort)
        {
            try
            {
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
