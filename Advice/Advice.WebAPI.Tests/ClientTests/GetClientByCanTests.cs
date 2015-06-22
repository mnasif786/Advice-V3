using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Advice.Common.Models;
using Advice.WebAPI.Tests.Common;
using Moq;
using NUnit.Framework;

namespace Advice.WebAPI.Tests.ClientTests
{
    [TestFixture]
    public class GetClientByCanTests : BaseClientControllerTests
    {
        [Test]
        public void Given_Clients_Get_Clients_By_Can_Returns_Correct_Client()
        {
            const string can = "CAN001";
            var client = new ClientModel(1, "CAN001", "Client Payments Caledonia", "M4 4FB");
            ClientServiceMock.Setup(x => x.GetClientByCan(It.IsAny<string>())).Returns(client);
            var actionResult = ClientController.GetClientByCan(can);
            var contentResult = actionResult as OkNegotiatedContentResult<ClientModel>;

            Assert.That(contentResult.Content.Can, Is.EqualTo(can));
        }
    }
}
