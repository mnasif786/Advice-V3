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
    public class GetAllClientsStartWithCanTests : BaseClientControllerTests
    {
        [SetUp]
        public void Setup()
        {
            var clientsList = new List<ClientModel>()
            {
                new ClientModel(62692, "ZPEN021", "Client Payments Caledonia","M4 4FB"),
                new ClientModel(50756, "ZPEN022", "Client Services", "M4 4FB")
            };

            ClientServiceMock.Setup(x => x.GetAllClientsStartWithCan(It.IsAny<string>())).Returns(clientsList);
        }

        [Test]
        public void Given_Clients_Requested_By_GetClientsStartWithCan_With_Initial_Characters_Then_Returns_All_Clients_Starting_With_Initial_Characters()
        {
            var actionResult = ClientController.GetAllClientsStartWithCan("ZPEN");
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<ClientModel>>;

            foreach (var clientModel in contentResult.Content)
            {
                Assert.IsTrue(clientModel.Can.StartsWith("ZPEN"));
            }

            Assert.That(contentResult.Content.ToList().Count, Is.EqualTo(2));
        }
    }
}
