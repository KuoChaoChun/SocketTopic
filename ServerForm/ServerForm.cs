using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SocketTopic.Factory;
using SocketTopic.Interface;

namespace ServerForm
{
    public partial class ServerForm : Form
    {
        public ServerForm()
        {
            InitializeComponent();

            IServer server = SocketFactory.CreateServer();
            server.AfterConnect = SetClientInfo;    //註冊
            server.Start();
        }

        private void SetClientInfo(string ip, int port, string fileName)
        {
            IpTxt.Invoke(new Action(() => IpTxt.Text = ip));
            PortTxt.Invoke(new Action(() => PortTxt.Text = port.ToString()));
            FileNameTxt.Invoke(new Action(() => FileNameTxt.Text = fileName.ToString()));
        }
    }
}
