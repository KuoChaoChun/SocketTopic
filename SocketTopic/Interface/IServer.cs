using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTopic.Interface
{
    public interface IServer
    {
        Action<string, int, string> AfterConnect { get; set; }   //創建事件
        void Start();
        void Stop();
    }
}
