using SocketTopic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SocketTopic.Utility;

namespace SocketTopic.Services
{
    public class Client : IClient
    {
        private ITcpSocketWrapper _socketWrapper;
        private string _fileName;
        private string _savePath;

        public Client(ITcpSocketWrapper socketWrapper, string fileName, string savePath)
        {
            _socketWrapper = socketWrapper;
            _fileName = fileName;
            _savePath = savePath;
        }

        public void Connect(string serverIP, int serverPort)
        {
            try
            {
                _socketWrapper.Connect(serverIP, serverPort);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Client_Connect()_Exception: {0}" + ex.Message);
            }
        }

        public void Disconnect()
        {
            _socketWrapper.Close();
        }

        public void Send(string message)
        {
            try
            {
                _socketWrapper.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Client_Send()_Exception: {0}" + ex.Message);
            }
        }

        public int Receive(byte[] dateBuffer)
        {
            return _socketWrapper.Receive(dateBuffer);
        }

        public void ReceiveFile(byte[] buffer, string fileName, string savePath)
        {
            // 將指定文件存到指定路徑
            string filePath = Path.Combine(savePath, fileName);
            File.WriteAllBytes(filePath, buffer.ToArray());
        }

        public async Task<string> SendRequest(string serverIP, int serverPort, string fileName)
        {
            try
            {
                _socketWrapper.Connect(serverIP, serverPort);
                _socketWrapper.Send(fileName);
                return await ReceiveResponse(_socketWrapper);
            }
            catch (Exception ex)
            {
                return $"SendRequest()_Exception: {ex.Message}";
            }
        }

        public async Task<string> ReceiveResponse(ITcpSocketWrapper socketWrapper)
        {
            var buffer = new byte[1024];
            int receivedBytes = socketWrapper.Receive(buffer);
            string result = Encoding.UTF8.GetString(buffer, 0, receivedBytes);

            if (result == MsgResultState.Success)
            {
                await DownloadFile(socketWrapper);
            }

            return result;
        }

        public async Task DownloadFile(ITcpSocketWrapper socketWrapper)
        {
            var buffer = new byte[1024];
            int receivedBytes = socketWrapper.Receive(buffer);

            ReceiveFile(buffer, _fileName, _savePath);
        }
    }
}
