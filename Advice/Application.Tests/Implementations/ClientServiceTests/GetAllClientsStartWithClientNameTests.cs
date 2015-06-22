using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Common.Models;
using Application.Tests.Common;
using NUnit.Framework;

namespace Application.Tests.Implementations.ClientServiceTests
{
    [TestFixture]
    public class GetAllClientsStartWithClientNameTests : BaseClientServiceTests
    {
        [Test]
        public void Given_Clients_Requested_By_GetAllClientsStartWithClientName_With_LowerCase_Initial_Characters_Then_Returns_All_Clients_Starting_With_Initial_Characters()
        {
            var customerIds = new List<int>() { 29252, 62692, 50756, 91186 };
            const string clientNameInitials = "clie";
            TblCustomerRepositoryMock.Setup(x => x.GetAllCustomersStartWithCompanyName(clientNameInitials)).Returns(Customers);
            TblSiteAddressMock.Setup(x => x.GetSiteAddressByCustomerIds(customerIds)).Returns(Addresses);
            var filteredClients = ClientService.GetAllClientsStartWithClientName(clientNameInitials);

            Assert.That(filteredClients.Count(), Is.EqualTo(4));
            foreach (var filteredClient in filteredClients)
            {
                Assert.IsTrue(filteredClient.ClientName.ToLowerInvariant().StartsWith(clientNameInitials));
            }
        }

        [Test]
        public void Given_Clients_Requested_By_GetAllClientsStartWithClientName_With_UpperCase_Initial_Characters_Then_Returns_All_Clients_Starting_With_Initial_Characters()
        {
            var customerIds = new List<int>() { 29252, 62692, 50756, 91186 };
            const string clientNameInitials = "CLIE";
            TblCustomerRepositoryMock.Setup(x => x.GetAllCustomersStartWithCompanyName(clientNameInitials)).Returns(Customers);
            TblSiteAddressMock.Setup(x => x.GetSiteAddressByCustomerIds(customerIds)).Returns(Addresses);
            var filteredClients = ClientService.GetAllClientsStartWithClientName(clientNameInitials);

            Assert.That(filteredClients.Count(), Is.EqualTo(4));
            foreach (var filteredClient in filteredClients)
            {
                Assert.IsTrue(filteredClient.ClientName.ToUpperInvariant().StartsWith(clientNameInitials));
            }
        }

        [Test]
        public void Given_Clients_Requested_By_GetAllClientsStartWithClientName_With_CamelCase_Initial_Characters_Then_Returns_All_Clients_Starting_With_Initial_Characters()
        {
            const string clientNameInitials = "Client Da";
            const int customerId = 29252;
            var customerIds = new List<int>() { customerId };
            TblCustomerRepositoryMock.Setup(x => x.GetAllCustomersStartWithCompanyName(clientNameInitials)).Returns(Customers.Where(x => x.CustomerID == customerId));
            TblSiteAddressMock.Setup(x => x.GetSiteAddressByCustomerIds(customerIds)).Returns(Addresses.Where(x => x.CustomerID == customerId));
            var filteredClients = ClientService.GetAllClientsStartWithClientName(clientNameInitials);

            Assert.That(filteredClients.Count(), Is.EqualTo(1));
            foreach (var filteredClient in filteredClients)
            {
                Assert.IsTrue(filteredClient.ClientName.StartsWith(clientNameInitials));
            }
        }

        [Test]
        public void Given_Clients_Requested_By_GetAllClientsStartWithClientName_With_MixedCase_Initial_Characters_Then_Returns_All_Clients_Starting_With_Initial_Characters()
        {
            const string clientNameInitials = "cLieNt Da";
            const int customerId = 29252;
            var customerIds = new List<int>() { customerId };
            TblCustomerRepositoryMock.Setup(x => x.GetAllCustomersStartWithCompanyName(clientNameInitials)).Returns(Customers.Where(x => x.CustomerID == 29252));
            TblSiteAddressMock.Setup(x => x.GetSiteAddressByCustomerIds(customerIds)).Returns(Addresses.Where(x => x.CustomerID == customerId));
            var filteredClients = ClientService.GetAllClientsStartWithClientName(clientNameInitials);

            Assert.That(filteredClients.Count(), Is.EqualTo(1));
            foreach (var filteredClient in filteredClients)
            {
                Assert.IsTrue(filteredClient.ClientName.ToLowerInvariant().StartsWith(clientNameInitials.ToLowerInvariant()));
            }
        }

        [Test]
        public void Given_Clients_Requested_By_GetAllClientsStartWithClientName_With_Initial_Characters_Then_Returns_All_Clients_Starting_With_Initial_Characters_With_Correct_Details()
        {
            const string clientNameInitials = "Client Data Solutions Ltd";
            const int customerId = 29252;
            var customerIds = new List<int>() { customerId };
            TblCustomerRepositoryMock.Setup(x => x.GetAllCustomersStartWithCompanyName(clientNameInitials)).Returns(Customers.Where(x => x.CustomerID == 29252));
            TblSiteAddressMock.Setup(x => x.GetSiteAddressByCustomerIds(customerIds)).Returns(Addresses.Where(x => x.CustomerID == customerId));
            var filteredClients = ClientService.GetAllClientsStartWithClientName(clientNameInitials);

            var clientModels = filteredClients as IList<ClientModel> ?? filteredClients.ToList();
            Assert.That(clientModels.Count(), Is.EqualTo(1));


            Assert.That(clientModels[0].ClientId, Is.EqualTo(29252));
            Assert.That(clientModels[0].ClientName, Is.EqualTo("Client Data Solutions Ltd"));
            Assert.That(clientModels[0].Can, Is.EqualTo("ZPEN001"));
            Assert.That(clientModels[0].PostCode, Is.EqualTo("M4 4FB"));
        }
    }
}
