using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SocketTopic.Interface;
using SocketTopic.Utility;

namespace SocketTopic.Services
{
    public class ClientFileService
    {
        private IClient _client;
        private string _fileName;
        private string _savePath;

        public ClientFileService(IClient client, string fileName, string savePath)
        {
            _client = client;
            _fileName = fileName;
            _savePath = savePath;
        }

        public async Task<string> SendRequest(string serverIP, int serverPort, string fileName)
        {
            try
            {
                _client.Connect(serverIP, serverPort);
                _client.Send(fileName);
                return await ReceiveResponse(_client);
            }
            catch (Exception ex)
            {
                return $"SendRequest()_Exception: {ex.Message}";
            }
        }

        public async Task<string> ReceiveResponse(IClient client)
        {
            var buffer = new byte[1024];
            int receivedBytes = client.Receive(buffer);
            string result = Encoding.UTF8.GetString(buffer, 0, receivedBytes);

            if (result == MsgResultState.Success)
            {
                await DownloadFile(client);
            }

            return result;
        }

        public async Task DownloadFile(IClient client)
        {
            var buffer = new byte[1024];
            int receivedBytes = client.Receive(buffer);

            _client.ReceiveFile(buffer, _fileName, _savePath);
        }
    }
}
