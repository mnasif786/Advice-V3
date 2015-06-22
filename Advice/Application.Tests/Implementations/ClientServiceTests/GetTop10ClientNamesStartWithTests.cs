using System.Collections.Generic;
using System.Linq;
using Application.Tests.Common;
using NUnit.Framework;
using Peninsula.Domain.Entities;

namespace Application.Tests.Implementations.ClientServiceTests
{
    [TestFixture]
    public class GetTop10ClientNamesStartWithTests : BaseClientServiceTests
    {
        [Test]
        public void Given_Clients_Requested_By_LowerCase_Initial_Characters_Then_Returns_Correct_CompanyName()
        {
            var clients = GetClients();
            const string companyNameInitials = "clie";
            TblCustomerRepositoryMock.Setup(x => x.GetTop10CustomersStartWithCompanyName(companyNameInitials)).Returns(clients);
            var filteredCompanyNames = ClientService.GetTop10ClientNamesStartWith(companyNameInitials);
            foreach (var filteredCompanyName in filteredCompanyNames)
            {
                Assert.IsTrue(filteredCompanyName.ToLowerInvariant().StartsWith(companyNameInitials));
            }
        }

        [Test]
        public void Given_Clients_Requested_By_UpperCase_Initial_Characters_Then_Returns_Correct_CompanyName()
        {
            var clients = GetClients();
            const string companyNameInitials = "CLIE";
            TblCustomerRepositoryMock.Setup(x => x.GetTop10CustomersStartWithCompanyName(companyNameInitials)).Returns(clients);
            var filteredCompanyNames = ClientService.GetTop10ClientNamesStartWith(companyNameInitials);
            foreach (var filteredCompanyName in filteredCompanyNames)
            {
                Assert.IsTrue(filteredCompanyName.ToUpperInvariant().StartsWith(companyNameInitials));
            }
        }

        [Test]
        public void Given_Clients_Requested_By_CamelCase_Initial_Characters_Then_Returns_Correct_CompanyName()
        {
            var clients = GetClients();
            const string companyNameInitials = "Client Da";
            TblCustomerRepositoryMock.Setup(x => x.GetTop10CustomersStartWithCompanyName(companyNameInitials)).Returns(clients.Where(x => x.CustomerID == 29252));
            var filteredCompanyNames = ClientService.GetTop10ClientNamesStartWith(companyNameInitials);
            foreach (var filteredCompanyName in filteredCompanyNames)
            {
                Assert.IsTrue(filteredCompanyName.StartsWith(companyNameInitials));
            }
        }

        [Test]
        public void Given_Clients_Requested_By_MixedCase_Initial_Characters_Then_Returns_Correct_CompanyName()
        {
            var clients = GetClients();
            const string companyNameInitials = "cLieNt Da";
            TblCustomerRepositoryMock.Setup(x => x.GetTop10CustomersStartWithCompanyName(companyNameInitials)).Returns(clients.Where(x => x.CustomerID == 29252));
            var filteredCompanyNames = ClientService.GetTop10ClientNamesStartWith(companyNameInitials);
            foreach (var filteredCompanyName in filteredCompanyNames)
            {
                Assert.IsTrue(filteredCompanyName.ToLowerInvariant().StartsWith(companyNameInitials.ToLowerInvariant()));
            }
        }

        private IEnumerable<TBLCustomer> GetClients()
        {
            var clientsList = new List<TBLCustomer>()
            {
                new TBLCustomer()
                {
                    CustomerID = 29252,
                    CustomerKey = "CLI28",
                    CompanyName = "Client Data Solutions Ltd"
                },

                new TBLCustomer()
                {
                    CustomerID = 62692,
                    CustomerKey = "ABEDP078",
                    CompanyName = "Client Payments Caledonia"
                },

                new TBLCustomer()
                {
                    CustomerID = 50756,
                    CustomerKey = "ZPEN022",
                    CompanyName = "Client Services"
                },

                new TBLCustomer()
                {
                    CustomerID = 91186,
                    CustomerKey = "C101",
                    CompanyName = "Clientlogic"
                }

            };

            return clientsList;
        }
    }
}
