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
        private Socket serverSocket;

        public ServerForm()
        {
            InitializeComponent();

            IServer server = SocketFactory.CreateServer();
            server.AfterConnect = SetClientInfo;    //註冊
            server.Start();
        }

        private void SetClientInfo(string ip, int port, string msg)
        {
            IpTxt.Invoke(new Action(() => IpTxt.Text = ip));
            PortTxt.Invoke(new Action(() => PortTxt.Text = port.ToString()));
        }

        private void AppendLog(string message)
        {
            ResultTxt.Invoke(new Action(() => ResultTxt.AppendText(message + Environment.NewLine)));

            //if (InvokeRequired)
            //{
            //    Invoke(new Action(() => ResultTxt.AppendText(message + Environment.NewLine)));
            //}
            //else
            //{
            //    ResultTxt.AppendText(message + Environment.NewLine);
            //}
        }
    }
}
