using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Moq;
using SocketTopic.Interface;
using SocketTopic.Services;
using SocketTopic.Utility;


namespace SocketTopicTests.Services
{
    [TestClass]
    public class ClientFileServiceTests
    {
        private Mock<IClient> _mockClient;
        private ClientFileService _fileService;
        private const string TestFileName = "test.txt";
        private const string TestSavePath = "C:\\TestPath";

        [TestInitialize]
        public void Setup()
        {
            _mockClient = new Mock<IClient>();
            _fileService = new ClientFileService(_mockClient.Object, TestFileName, TestSavePath);
        }

        [TestMethod]
        public async Task SendRequest_ReturnsSuccess()
        {
            // Arrange
            _mockClient.Setup(client => client.Connect(It.IsAny<string>(), It.IsAny<int>()));
            _mockClient.Setup(client => client.Send(It.IsAny<string>()));
            _mockClient.Setup(client => client.BeginReceive(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>(), SocketFlags.None, null, null))
                      .Returns((byte[] buffer, int offset, int size, SocketFlags socketFlags, AsyncCallback callback, object state) =>
                      {
                          callback(new Mock<IAsyncResult>().Object);
                          return new Mock<IAsyncResult>().Object;
                      });
            _mockClient.Setup(client => client.EndReceive(It.IsAny<IAsyncResult>()))
                      .Returns(Encoding.UTF8.GetBytes(MsgResultType.Success).Length);
                       
            // Act
            var result = await _fileService.SendRequest("127.0.0.1", 8081, "test.txt");

            // Assert
            Assert.AreEqual(MsgResultType.Success, result);
        }
    }
}
