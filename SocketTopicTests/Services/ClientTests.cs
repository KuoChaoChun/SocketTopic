using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SocketTopic.Interface;
using SocketTopic.Services;
using SocketTopic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTopic.Services.Tests
{
    [TestClass()]
    public class ClientTests
    {
        private Mock<ITcpSocketWrapper> _mockSocketWrapper;
        private Client _clientService;
        private const string TestFileName = "test.txt";
        private const string TestSavePath = "C:\\";

        [TestInitialize]
        public void Setup()
        {
            _mockSocketWrapper = new Mock<ITcpSocketWrapper>();
            _clientService = new Client(_mockSocketWrapper.Object, TestFileName, TestSavePath);
        }

        [TestMethod()]
        public async Task SendRequest_ReturnsSuccess()
        {
            // Arrange
            byte[] bytes = Encoding.ASCII.GetBytes("hello");
            _mockSocketWrapper.Setup(s => s.Receive(bytes));

            _mockSocketWrapper.Setup(client => client.Connect(It.IsAny<string>(), It.IsAny<int>()));
            _mockSocketWrapper.Setup(client => client.Send(It.IsAny<string>()));
            _mockSocketWrapper.Setup(client => client.Receive(It.IsAny<byte[]>()))
                      .Returns(Encoding.UTF8.GetBytes(MsgResultState.Success).Length);

            // Act
            var result = await _clientService.SendRequest("127.0.0.1", 8081, "test.txt");

            // Assert
            Assert.AreEqual(MsgResultState.Success, result);
        }
    }
}