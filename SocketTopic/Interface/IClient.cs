using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketTopic.Interface
{
    public interface IClient
    {
        void Connect(string serverIP, int serverPort);
        void Disconnect();
        void Send(string message);
        void ReceiveFile(byte[] fileContent, string fileName, string savePath);
        IAsyncResult BeginReceive(byte[] buffer, int offset, int size, SocketFlags socketFlags, AsyncCallback callback, object state);
        int EndReceive(IAsyncResult asyncResult);
    }
}
