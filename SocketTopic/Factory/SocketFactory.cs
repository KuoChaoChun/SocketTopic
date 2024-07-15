using SocketTopic.Interface;
using SocketTopic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTopic.Factory
{
    public class SocketFactory
    {
        public static IServer CreateServer()
        {
            return new Server();
        }

        public static IClient CreateClient()
        {
            return new Client();
        }
    }
}
