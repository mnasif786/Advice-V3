using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Advice.Common.Models;
using Advice.WebAPI.Tests.Common;
using Moq;
using NUnit.Framework;

namespace Advice.WebAPI.Tests.ClientTests
{
    [TestFixture]
    public class GetAllClientsStartWithClientNameTests : BaseClientControllerTests
    {
        [SetUp]
        public void Setup()
        {
            var clientsList = new List<ClientModel>()
            {
                new ClientModel(62692, "ABEDP078", "Client Payments Caledonia","M4 4FB"),
                new ClientModel(50756, "ZPEN022", "Client Services", "M4 4FB")
            };

            ClientServiceMock.Setup(x => x.GetAllClientsStartWithClientName(It.IsAny<string>())).Returns(clientsList);
        }

        [Test]
        public void Given_Clients_Requested_By_GetClientsStartWithClientName_With_Initial_Characters_Then_Returns_All_Clients_Starting_With_Initial_Characters()
        {
            var actionResult = ClientController.GetAllClientsStartWithClientName("Clie");
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<ClientModel>>;

            foreach (var clientModel in contentResult.Content)
            {
                Assert.IsTrue(clientModel.ClientName.StartsWith("Clie"));
            }

            Assert.That(contentResult.Content.ToList().Count, Is.EqualTo(2));
        }
    }
}
