using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Peninsula.Data.Tests.Common;

namespace Peninsula.Data.Tests.Repositories.TblCustomerRepositoryTests
{
    [TestFixture]
    public class GetAllCustomersStartWithCustomerKeyTests : BaseTblCustomerRepositoryTests
    {
        [Test]
        public void Given_Customers_Requested_By_Initial_Characters_Then_Returns_All_Customers_Starting_With_Given_CustomerKey()
        {
            const string initials = "ZPEN";
            var customers = TblCustomerRepository.GetAllCustomersStartWithCustomerKey(initials);

            Assert.That(customers.Count(), Is.EqualTo(11));

        }
    }
}
