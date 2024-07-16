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

            #region 原本OK
            //Task.Run(() => StartServer());
            #endregion

            #region task
            IServer server = SocketFactory.CreateServer();
            server.AfterConnect = SetClientInfo;    //註冊
            server.Start();
            #endregion
        }

        private void SetClientInfo(string ip, int port, string msg)
        {
            IpTxt.Invoke(new Action(() => IpTxt.Text = ip));
            PortTxt.Invoke(new Action(() => PortTxt.Text = port.ToString()));
        }

        #region 原本OK
        private void StartServer()
        {
            try
            {
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 8081);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(10);

                AppendLog("服务器已启动，等待客户端连接...");

                while (true)
                {
                    Socket client = serverSocket.Accept();
                    AppendLog("客户端已连接。");

                    UpdateClientInfo(endPoint.Address.ToString(), endPoint.Port);

                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                AppendLog("服务器错误: " + ex.Message);
            }
        }
        #endregion

        private void HandleClient(Socket client)
        {
            try
            {
                byte[] buffer = new byte[1024];
                int receivedBytes = client.Receive(buffer);
                string receivedData = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
                AppendLog("接收到的数据：" + receivedData);

                string response = "数据已接收";
                byte[] responseData = Encoding.UTF8.GetBytes(response);
                client.Send(responseData);

                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            catch (Exception ex)
            {
                AppendLog("处理客户端错误: " + ex.Message);
            }
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

        private void UpdateClientInfo(string clientIP, int clientPort)
        {
            IpTxt.Invoke(new Action(() => IpTxt.Text = clientIP));
            PortTxt.Invoke(new Action(() => PortTxt.Text = clientPort.ToString()));
        }
    }
}
