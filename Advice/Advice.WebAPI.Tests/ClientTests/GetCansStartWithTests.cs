using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Advice.WebAPI.Tests.Common;
using Moq;
using NUnit.Framework;

namespace Advice.WebAPI.Tests.ClientTests
{
    [TestFixture]
    public class GetCansStartWithTests : BaseClientControllerTests
    {
        private List<string> _cans;

        [SetUp]
        public void SetUp()
        {
            _cans = new List<string>()
            {
                "ZPEN001",
                "ZPEN002",
                "ZPEN003",
                "ZPEN004",
                "ZPEN005",
                "ZPEN006",
                "ZPEN007",
                "ZPEN008",
                "ZPEN009",
                "ZPEN0010",
            };
        }

        [Test]
        public void Given_Clients_Get_Cans_StartWith_Initial_Characters_Returns_Cans_Starting_With_Initials()
        {
            ClientServiceMock.Setup(x => x.GetTop10CansStartWith(It.IsAny<string>())).Returns(_cans.Take(5));
            var actionResult = ClientController.GetCansStartWith("ZPEN");
            var contentResult = actionResult as OkNegotiatedContentResult<IList<string>>;

            foreach (var can in contentResult.Content)
            {
                if (can == "Show more..")
                {
                    continue;
                }

                Assert.IsTrue(can.StartsWith("ZPEN"));
            }

            Assert.That(contentResult.Content.ToList().Count, Is.EqualTo(6));
        }

        [Test]
        public void Given_10_Clients_Get_Cans_StartWith_Initial_Characters_Returns_Extra_Can_As_Show_More_Results()
        {
            ClientServiceMock.Setup(x => x.GetTop10CansStartWith(It.IsAny<string>())).Returns(_cans);
            var actionResult = ClientController.GetCansStartWith("Clie");
            var contentResult = actionResult as OkNegotiatedContentResult<IList<string>>;

            Assert.IsTrue(contentResult.Content.Contains("Show more.."));

        }

    }
}
