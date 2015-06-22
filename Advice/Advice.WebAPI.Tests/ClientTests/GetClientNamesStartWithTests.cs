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
    public class GetClientNamesStartWithTests: BaseClientControllerTests
    {
        private List<string> _clientNames;
        
        [SetUp]
        public void SetUp()
        {
            _clientNames = new List<string>()
            {
                "Client Payments Caledonia",
                "Client Data Solutions Ltd",
                "Client Services",
                "Clientlogic",
                "Client Blind Services",
                "Client Curtain Services",
                "Client Eye Services",
                "Client Advice Services",
                "Client Kitchen Services",
                "Client Robot Services",
            };
        }

        [Test]
        public void Given_Clients_Get_ClientNames_StartWith_Initial_Characters_Returns_ClientNames_Starting_With_Initials()
        {
            ClientServiceMock.Setup(x => x.GetTop10ClientNamesStartWith(It.IsAny<string>())).Returns(_clientNames.Take(5));
            var actionResult = ClientController.GetClientNamesStartWith("Clie");
            var contentResult = actionResult as OkNegotiatedContentResult<IList<string>>;

            foreach (var companyName in contentResult.Content)
            {
                if (companyName == "Show more..")
                {
                    continue;
                }

                Assert.IsTrue(companyName.StartsWith("Clie"));
            }

            Assert.That(contentResult.Content.ToList().Count, Is.EqualTo(6));
        }

        [Test]
        public void Given_10_Clients_Get_ClientNames_StartWith_Initial_Characters_Returns_Extra_Name_As_Show_More_Results()
        {
            ClientServiceMock.Setup(x => x.GetTop10ClientNamesStartWith(It.IsAny<string>())).Returns(_clientNames);
            var actionResult = ClientController.GetClientNamesStartWith("Clie");
            var contentResult = actionResult as OkNegotiatedContentResult<IList<string>>;

            Assert.IsTrue(contentResult.Content.Contains("Show more.."));
            
        }
    }
}
