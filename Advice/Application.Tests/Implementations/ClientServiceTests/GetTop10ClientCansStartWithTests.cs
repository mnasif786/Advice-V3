using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Tests.Common;
using NUnit.Framework;
using Peninsula.Domain.Entities;

namespace Application.Tests.Implementations.ClientServiceTests
{
    [TestFixture]
    public class GetTop10ClientCansStartWithTests : BaseClientServiceTests
    {
        [Test]
        public void Given_Clients_Requested_With_Can_Of_LowerCase_Initial_Characters_Then_Returns_Correct_Can()
        {
            var clients = GetClients();
            const string canInitials = "zpen";
            TblCustomerRepositoryMock.Setup(x => x.GetTop10CustomersStartWithCustomerKey(canInitials)).Returns(clients);
            var filteredCans = ClientService.GetTop10CansStartWith(canInitials);
            foreach (var filteredCan in filteredCans)
            {
                Assert.IsTrue(filteredCan.ToLowerInvariant().StartsWith(canInitials));
            }
        }

        [Test]
        public void Given_Clients_Requested_With_Can_Of_UpperCase_Initial_Characters_Then_Returns_Correct_Can()
        {
            var clients = GetClients();
            const string canInitials = "ZPEN";
            TblCustomerRepositoryMock.Setup(x => x.GetTop10CustomersStartWithCustomerKey(canInitials)).Returns(clients);
            var filteredCans = ClientService.GetTop10CansStartWith(canInitials);
            foreach (var can in filteredCans)
            {
                Assert.IsTrue(can.ToUpperInvariant().StartsWith(canInitials));
            }
        }

        [Test]
        public void Given_Clients_Requested_With_Can_Of_MixedCase_Initial_Characters_Then_Returns_Correct_Can()
        {
            var clients = GetClients();
            const string canInitials = "zPEn";
            TblCustomerRepositoryMock.Setup(x => x.GetTop10CustomersStartWithCustomerKey(canInitials)).Returns(clients);
            var filteredCans = ClientService.GetTop10CansStartWith(canInitials);
            foreach (var can in filteredCans)
            {
                Assert.IsTrue(can.ToLowerInvariant().StartsWith(canInitials.ToLowerInvariant()));
            }
        }

        private IEnumerable<TBLCustomer> GetClients()
        {
            var clientsList = new List<TBLCustomer>()
            {
                new TBLCustomer()
                {
                    CustomerID = 29252,
                    CustomerKey = "ZPEN021",
                    CompanyName = "Client Data Solutions Ltd"
                },

                new TBLCustomer()
                {
                    CustomerID = 62692,
                    CustomerKey = "ZPEN022",
                    CompanyName = "Client Payments Caledonia"
                },

                new TBLCustomer()
                {
                    CustomerID = 50756,
                    CustomerKey = "ZPEN023",
                    CompanyName = "Client Services"
                },

                new TBLCustomer()
                {
                    CustomerID = 91186,
                    CustomerKey = "ZPEN024",
                    CompanyName = "Clientlogic"
                }

            };

            return clientsList;
        }
    }
    
}
