using SocketTopic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.InteropServices;
using SocketTopic.Utility;

namespace SocketTopic.Services
{
    public class Server : IServer
    {
        private Socket serverSocket;
        private bool isRunning;
        string baseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Files");

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
                    Console.WriteLine("port已被占用，請選擇其他port。");
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
                string fileName = string.Empty;
                Socket client = serverSocket.Accept();
                IPEndPoint clientEndPoint = (IPEndPoint)client.RemoteEndPoint;
                Console.WriteLine($"Client端已連接，IP: {clientEndPoint.Address}, Port: {clientEndPoint.Port}");

                fileName = GetClientFile(client);
                AfterConnect?.Invoke(clientEndPoint.Address.ToString(), clientEndPoint.Port, fileName); //觸發事件

                Task.Run(() => SendFileExist(client, fileName));
            }
        }

        private string GetClientFile(Socket client)
        {
            byte[] buffer = new byte[1024];
            int receivedBytes = client.Receive(buffer);
            string fileName = Encoding.UTF8.GetString(buffer, 0, receivedBytes).Trim();

            return fileName;
        }

        private void SendFileExist(Socket client, string fileName)
        {
            try
            {
                string filePath = Path.Combine(baseDirectory, fileName);

                if (File.Exists(filePath))
                {
                    byte[] msgResult = Encoding.UTF8.GetBytes(MsgResultType.Success);
                    client.Send(msgResult);

                    byte[] fileBytes = File.ReadAllBytes(filePath);
                    client.Send(fileBytes);
                }
                else
                {
                    byte[] msgResult = Encoding.UTF8.GetBytes(MsgResultType.Error);
                    client.Send(msgResult);
                }

                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}" + ex.Message);
            }
        }
    }
}
