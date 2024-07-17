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
using System.IO;
using SocketTopic.Utility;

namespace ClientForm
{
    public partial class ClientForm : Form
    {
        private IClient _client;
        public ClientForm()
        {
            InitializeComponent();

            IpTxt.Text = "127.0.0.1";
            PortTxt.Text = "8081";
            FileNameTxt.Text = "Member.txt";
            SavePathTxt.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            _client = SocketFactory.CreateClient();
        }

        private void RequestBtn_Click(object sender, EventArgs e)
        {
            string serverIP = IpTxt.Text.Trim();
            int serverPort = 8081;
            string fileName = FileNameTxt.Text.Trim();

            if (!int.TryParse(PortTxt.Text.Trim(), out serverPort))
            {
                MessageBox.Show("請輸入有效的Port。");
                return;
            }

            Task.Run(() => SendRequest(serverIP, serverPort, fileName));
        }
         
        private async Task SendRequest(string serverIP, int serverPort, string fileName)
        {
            try
            {
                _client.Connect(serverIP, serverPort);
                _client.Send(fileName);
                await ReceiveResponse(_client);
            }
            catch (Exception ex)
            {
                AppendLog("Exception: {0}" + ex.Message);
            }
        }

        private async Task ReceiveResponse(IClient client)
        {
            var buffer = new byte[1024];
            var receivedBytes = await Task<int>.Factory.FromAsync(
                client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, null, client),
                client.EndReceive);
            string result = Encoding.UTF8.GetString(buffer, 0, receivedBytes);

            AppendLog("檔案讀取：" + result);

            if (result == MsgResultType.Success)
                await DownloadFile(client);
        }

        private async Task DownloadFile(IClient client)
        {
            var buffer = new byte[1024];
            var receivedBytes = await Task<int>.Factory.FromAsync(
                client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, null, client),
                client.EndReceive);

            _client.ReceiveFile(buffer, SavePathTxt.Text, FileNameTxt.Text);
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
    }
}
