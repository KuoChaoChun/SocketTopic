﻿using System;
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
        private ClientFileService _fileService;
        public ClientForm()
        {
            InitializeComponent();

            IpTxt.Text = "127.0.0.1";
            PortTxt.Text = "8081";
            FileNameTxt.Text = "Member.txt";
            SavePathTxt.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            var client = SocketFactory.CreateClient();
            _fileService = new ClientFileService(client, FileNameTxt.Text, SavePathTxt.Text);
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

            Task.Run(async () =>
            {
                string result = await _fileService.SendRequest(serverIP, serverPort, fileName);
                AppendLog(result);
            });
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
