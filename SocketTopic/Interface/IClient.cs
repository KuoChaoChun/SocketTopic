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
        bool IsConnected { get; }

        void Connect(string serverIP, int serverPort);
        void Disconnect();
        void Send(string message);
        IAsyncResult BeginReceive(byte[] buffer, int offset, int size, SocketFlags socketFlags, AsyncCallback callback, object state);
        int EndReceive(IAsyncResult asyncResult);
    }
}
