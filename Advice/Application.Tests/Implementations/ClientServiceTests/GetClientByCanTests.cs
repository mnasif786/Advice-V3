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
    public class GetClientByCanTests : BaseClientServiceTests
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
        public void Given_Clients_Requested_By_Can_Then_Returns_Correct_Client()
        {
            const string can = "CLI28";
            TblCustomerRepositoryMock.Setup(x => x.GetCustomerByCustomerKey(can)).Returns(_customer);
            var client = ClientService.GetClientByCan(can);

            Assert.That(client.Can, Is.EqualTo(can));
        }
    }
}
