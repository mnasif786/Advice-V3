using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Peninsula.Data.Tests.Common;

namespace Peninsula.Data.Tests.Repositories.TblCustomerRepositoryTests
{
    [TestFixture]
    public class GetCustomersByCustomerIdsTests : BaseTblCustomerRepositoryTests
    {
        [Test]
        public void Given_Customers_When_Requested_By_CustomerIds_Then_All_Customers_With_Ids_Are_Retruned()
        {
            var customerIds = new List<int>() {1, 2, 3, 4};
            var customers = TblCustomerRepository.GetCustomersByCustomerIds(customerIds);
            Assert.That(customers.Count(), Is.EqualTo(4));
        }
    }
}
