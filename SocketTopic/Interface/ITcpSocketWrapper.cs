using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SocketTopic.Interface
{
    public interface ITcpSocketWrapper
    {
        void Connect(string ip, int port);
        void Close();
        void Listen(IPAddress address, int port);
        void Send(string message);
        int Receive(byte[] dateBuffer);
        T Accept<T>() where T : class;
    }
}

