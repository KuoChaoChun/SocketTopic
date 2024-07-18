using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SocketTopic.Interface;

namespace SocketTopic.Apps
{
    public class TcpSocketWrapper : ITcpSocketWrapper
    {
        private Socket _socket;

        public TcpSocketWrapper() 
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect(string ip, int port)
        {            
            _socket.Connect(ip, port);
        }

        public void Close()
        {
            _socket.Close();
            _socket = null;
        }

        public void Send(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            _socket.Send(data);
        }

        public void Listen(IPAddress address, int port)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(address, port);
            _socket.Bind(endPoint);
            _socket.Listen(10);
        }

        public int Receive(byte[] dateBuffer)
        {
            return _socket.Receive(dateBuffer);
        }

        public T Accept<T>() where T : class
        {
            return _socket.Accept() as T;
        }
    }
}
