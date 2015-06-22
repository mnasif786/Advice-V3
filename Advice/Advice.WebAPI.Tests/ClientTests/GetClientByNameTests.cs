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
    public class GetClientByNameTests : BaseClientControllerTests
    {

        [Test]
        public void Given_Clients_Get_Client_By_Name_Returns_Correct_Client()
        {
            const string clientName = "Client Payments Caledonia";
            var client = new ClientModel(1, "CAN001", "Client Payments Caledonia", "M4 4FB");
            ClientServiceMock.Setup(x => x.GetClientByName(It.IsAny<string>())).Returns(client);
            var actionResult = ClientController.GetClientByName(clientName);
            var contentResult = actionResult as OkNegotiatedContentResult<ClientModel>;

            Assert.That(contentResult.Content.ClientName, Is.EqualTo(clientName));
        }
    }
}
