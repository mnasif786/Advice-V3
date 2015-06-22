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
    public class GetClientByNameTests : BaseClientServiceTests
    {
        private TBLCustomer _customer;
        [SetUp]
        public void SetUp()
        {
            _customer = new TBLCustomer()
            {
                CustomerID = 29252,
                CustomerKey = "CLI28",
                CompanyName = "Client Data Solutions Ltd"
            };
        }
        [Test]
        public void Given_Clients_Requested_By_Name_Then_Returns_Correct_Client()
        {
            const string companyNameInitials = "Client Data Solutions Ltd";
            TblCustomerRepositoryMock.Setup(x => x.GetCustomerByCompanyName(companyNameInitials)).Returns(_customer);
            var client = ClientService.GetClientByName(companyNameInitials);

            Assert.That(client.ClientName, Is.EqualTo(companyNameInitials));
        }
    }
}
