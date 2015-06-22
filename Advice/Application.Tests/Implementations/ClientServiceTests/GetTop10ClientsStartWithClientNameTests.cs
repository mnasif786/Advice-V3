using System.Collections.Generic;
using System.Linq;
using Advice.Common.Models;
using Application.Tests.Common;
using Moq;
using NUnit.Framework;
using Peninsula.Domain.Entities;

namespace Application.Tests.Implementations.ClientServiceTests
{
    [TestFixture]
    public class GetTop10ClientsStartWithClientNameTests : BaseClientServiceTests
    {
        [Test]
        public void Given_Clients_Requested_By_LowerCase_Initial_Characters_Then_Returns_Correct_Clients()
        {
            var customerIds = new List<int>() { 29252, 62692, 50756, 91186 };
            const string clientNameInitials = "clie";
            TblCustomerRepositoryMock.Setup(x => x.GetTop10CustomersStartWithCompanyName(clientNameInitials)).Returns(Customers);
            TblSiteAddressMock.Setup(x => x.GetSiteAddressByCustomerIds(customerIds)).Returns(Addresses);
            var filteredClients = ClientService.GetTop10ClientsStartWithClientName(clientNameInitials);

            Assert.That(filteredClients.Count(), Is.EqualTo(4));
            foreach (var filteredClient in filteredClients)
            {
                Assert.IsTrue(filteredClient.ClientName.ToLowerInvariant().StartsWith(clientNameInitials));
            }
        }

        [Test]
        public void Given_Clients_Requested_By_UpperCase_Initial_Characters_Then_Returns_Correct_Clients()
        {
            var customerIds = new List<int>() { 29252, 62692, 50756, 91186 };
            const string clientNameInitials = "CLIE";
            TblCustomerRepositoryMock.Setup(x => x.GetTop10CustomersStartWithCompanyName(clientNameInitials)).Returns(Customers);
            TblSiteAddressMock.Setup(x => x.GetSiteAddressByCustomerIds(customerIds)).Returns(Addresses);
            var filteredClients = ClientService.GetTop10ClientsStartWithClientName(clientNameInitials);

            Assert.That(filteredClients.Count(), Is.EqualTo(4));
            foreach (var filteredClient in filteredClients)
            {
                Assert.IsTrue(filteredClient.ClientName.ToUpperInvariant().StartsWith(clientNameInitials));
            }
        }

        [Test]
        public void Given_Clients_Requested_By_CamelCase_Initial_Characters_Then_Returns_Correct_Clients()
        {
            const string clientNameInitials = "Client Da";
            const int customerId = 29252; 
            var customerIds = new List<int>() { customerId };
            TblCustomerRepositoryMock.Setup(x => x.GetTop10CustomersStartWithCompanyName(clientNameInitials)).Returns(Customers.Where(x => x.CustomerID == customerId));
            TblSiteAddressMock.Setup(x => x.GetSiteAddressByCustomerIds(customerIds)).Returns(Addresses.Where(x => x.CustomerID == customerId));
            var filteredClients = ClientService.GetTop10ClientsStartWithClientName(clientNameInitials);

            Assert.That(filteredClients.Count(), Is.EqualTo(1));
            foreach (var filteredClient in filteredClients)
            {
                Assert.IsTrue(filteredClient.ClientName.StartsWith(clientNameInitials));
            }
        }

        [Test]
        public void Given_Clients_Requested_By_MixedCase_Initial_Characters_Then_Returns_Correct_Clients()
        {
            const string clientNameInitials = "cLieNt Da";
            const int customerId = 29252;
            var customerIds = new List<int>() { customerId };
            TblCustomerRepositoryMock.Setup(x => x.GetTop10CustomersStartWithCompanyName(clientNameInitials)).Returns(Customers.Where(x => x.CustomerID == 29252));
            TblSiteAddressMock.Setup(x => x.GetSiteAddressByCustomerIds(customerIds)).Returns(Addresses.Where(x => x.CustomerID == customerId));
            var filteredClients = ClientService.GetTop10ClientsStartWithClientName(clientNameInitials);

            Assert.That(filteredClients.Count(), Is.EqualTo(1));
            foreach (var filteredClient in filteredClients)
            {
                Assert.IsTrue(filteredClient.ClientName.ToLowerInvariant().StartsWith(clientNameInitials.ToLowerInvariant()));
            }
        }

        [Test]
        public void Given_Clients_Requested_By_Initial_Characters_Then_Returns_Correct_Clients_Details()
        {
            const string clientNameInitials = "Client Data Solutions Ltd";
            const int customerId = 29252;
            var customerIds = new List<int>() { customerId };
            TblCustomerRepositoryMock.Setup(x => x.GetTop10CustomersStartWithCompanyName(clientNameInitials)).Returns(Customers.Where(x => x.CustomerID == 29252));
            TblSiteAddressMock.Setup(x => x.GetSiteAddressByCustomerIds(customerIds)).Returns(Addresses.Where(x => x.CustomerID == customerId));
            var filteredClients = ClientService.GetTop10ClientsStartWithClientName(clientNameInitials);

            var clientModels = filteredClients as IList<ClientModel> ?? filteredClients.ToList();
            Assert.That(clientModels.Count(), Is.EqualTo(1));


            Assert.That(clientModels[0].ClientId, Is.EqualTo(29252));
            Assert.That(clientModels[0].ClientName, Is.EqualTo("Client Data Solutions Ltd"));
            Assert.That(clientModels[0].Can, Is.EqualTo("ZPEN001"));
            Assert.That(clientModels[0].PostCode, Is.EqualTo("M4 4FB"));
        }
    }
}
