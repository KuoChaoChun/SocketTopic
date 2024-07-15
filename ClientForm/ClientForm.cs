using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SocketTopic.Interface;
using SocketTopic.Factory;
using SocketTopic.Services;

namespace ClientForm
{
    public partial class ClientForm : Form
    {
        private IClient _client;
        private Socket clientSocket;

        public ClientForm()
        {
            InitializeComponent();

            _client = SocketFactory.CreateClient();
        }

        private void RequestBtn_Click(object sender, EventArgs e)
        {
            string serverIP = IpTxt.Text.Trim();
            int serverPort = 0;

            if (!int.TryParse(PortTxt.Text.Trim(), out serverPort))
            {
                MessageBox.Show("請輸入有效的Port。");
                return;
            }

            Task.Run(() => SendRequest(serverIP, serverPort, FileNameTxt.Text.Trim()));

            #region 原始OK            
            //await Task.Run(() => StartClient(serverIP, serverPort));
            #endregion
        }

        private async Task SendRequest(string serverIP, int serverPort, string fileName)
        {
            try
            {
                _client.Connect(serverIP, serverPort);
                AppendLog($"已连接到服务器 {serverIP}:{serverPort}");

                _client.Send(fileName);

                await ReceiveResponse(_client);

                _client.Disconnect();
                AppendLog("已断开连接");
            }
            catch (Exception ex)
            {
                AppendLog("错误: " + ex.Message);
            }
        }

        private async Task ReceiveResponse(IClient client)
        {
            var buffer = new byte[1024];
            var receivedBytes = await Task<int>.Factory.FromAsync(
                client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, null, client),
                client.EndReceive);
            string receivedData = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
            AppendLog("从服务器接收到的数据：" + receivedData);
        }

        private void AppendLog(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ResultTxt.AppendText(message + Environment.NewLine)));
            }
            else
            {
                ResultTxt.AppendText(message + Environment.NewLine);
            }
        }

        #region 原始測試OK
        //private async Task StartClient(string serverIP, int serverPort)
        //{
        //    try
        //    {
        //        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 8081);
        //        clientSocket.Connect(serverEndPoint);

        //        AppendLog("已连接到服务器。");
                                
        //        string dataToSend = "Hello, Server!";
        //        byte[] data = Encoding.UTF8.GetBytes(dataToSend);
        //        clientSocket.Send(data);

        //        await ReceiveDataAsync(clientSocket);
        //    }
        //    catch (Exception ex)
        //    {
        //        AppendLog("客户端错误: " + ex.Message);
        //    }
        //}

        //private async Task ReceiveDataAsync(Socket client)
        //{
        //    byte[] buffer = new byte[1024];

        //    while (true)
        //    {
        //        int receivedBytes = await Task<int>.Factory.FromAsync(
        //            client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, null, client),
        //            client.EndReceive);

        //        if (receivedBytes == 0) break; // 连接已关闭

        //        string receivedData = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
        //        AppendLog("从服务器接收到的数据：" + receivedData);
        //    }
        //}

        //private void AppendLog(string message)
        //{
        //    ResultTxt.Invoke(new Action(() => ResultTxt.AppendText(message + Environment.NewLine)));
        //}
        #endregion
    }
}
