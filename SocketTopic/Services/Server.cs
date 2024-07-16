using SocketTopic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace SocketTopic.Services
{
    public class Server : IServer
    {
        private Socket serverSocket;
        private bool isRunning;

        public Server()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            isRunning = false;
        }

        public void Start()
        {
            Task.Run(() =>
            {
                try
                {
                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 8081);
                    serverSocket.Bind(endPoint);
                    serverSocket.Listen(10);

                    isRunning = true;
                    ListenForClients();

                }
                catch (SocketException ex) when (ex.SocketErrorCode == SocketError.AddressAlreadyInUse)
                {
                    Console.WriteLine("端口已被占用，请选择其他端口。");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("服务器启动错误: " + ex.Message);
                }
            });
        }

        public void Stop()
        {
            isRunning = false;
            serverSocket.Close();
        }

        public Action<string, int, string> AfterConnect { get; set; }

        private void ListenForClients()
        {
            while (isRunning)
            {
                Socket client = serverSocket.Accept();
                IPEndPoint clientEndPoint = (IPEndPoint)client.RemoteEndPoint;
                Console.WriteLine($"客户端已连接，IP: {clientEndPoint.Address}, 端口: {clientEndPoint.Port}");

                AfterConnect?.Invoke(clientEndPoint.Address.ToString(), clientEndPoint.Port, string.Empty); //觸發事件

                Task.Run(() => HandleClient(client));
            }
        }

        private void HandleClient(Socket client)
        {
            try
            {
                byte[] buffer = new byte[1024];
                int receivedBytes = client.Receive(buffer);
                string receivedData = Encoding.UTF8.GetString(buffer, 0, receivedBytes).Trim();

                bool fileExists = CheckFileExists(receivedData);

                string response = fileExists ? $"文件 '{receivedData}' 存在。" : $"文件 '{receivedData}' 不存在。";
                byte[] responseData = Encoding.UTF8.GetBytes(response);
                client.Send(responseData);

                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("处理客户端错误: " + ex.Message);
            }
        }

        private bool CheckFileExists(string fileName)
        {
            switch (fileName)
            {
                case "file1.txt":
                    return true;
                case "file2.txt":
                    return false;
                default:
                    return false;
            }
        }
    }
}
